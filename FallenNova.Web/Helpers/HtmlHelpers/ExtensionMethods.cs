using FallenNova.Shared.ExtensionMethods;
using FallenNova.Web.Areas.Shared.Models;
using FallenNova.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FallenNova.Web.HtmlHelpers
{
    public static class ExtensionMethods
    {
        public static IHtmlString BasicCheckBoxFor<T>(
            this HtmlHelper<T> htmlHelper,
            Expression<Func<T, bool>> expression,
            object htmlAttributes = null)
        {
            var mvcCheckBoxHtml = htmlHelper.CheckBoxFor(expression).ToString();

            // Hidden field to remove.
            const string hiddenFieldHtml = @"<input name=""[^""]+"" type=""hidden"" value=""false"" />";

            var checkBoxHtml = Regex.Replace(mvcCheckBoxHtml, hiddenFieldHtml, "");

            return MvcHtmlString.Create(checkBoxHtml);
        }
 
        public static IHtmlString Copyright(
            this HtmlHelper htmlHelper,
            string name)
        {
            return MvcHtmlString.Create(string.Format("&copy; {0} {1}", name, DateTime.Now.ToGmtDateTime().Year));
        }

        public static IHtmlString DropDownList(
            this HtmlHelper htmlHelper,
            string id,
            PagingInfo pagingInfo)
        {
            IList<SelectListItem> selectListItems = 
                new[] {"10", "20", "50", "100"}.Select(value => new SelectListItem
                {
                    Text = value,
                    Value = value,
                    Selected = (value.Equals(pagingInfo.PageSize.ToString(CultureInfo.CurrentCulture)))
                }).ToList();

            var onChangeUrl = new StringBuilder();
            onChangeUrl.Append("/");
            onChangeUrl.Append(pagingInfo.RouteUrl);

            object controller;
            htmlHelper.ViewContext.RequestContext.RouteData.Values.TryGetValue("controller", out controller);

            onChangeUrl.Replace("{controller}", controller.ToString());
            onChangeUrl.Replace("{pageIndex}", "1");
            onChangeUrl.Replace("{pageSize}", "' + this.value + '");
            onChangeUrl.Replace("{sortBy}", pagingInfo.SortBy.ToLower());
            onChangeUrl.Replace("{sortAscending}", pagingInfo.SortAscending.ToString().ToLower());
            
            if (htmlHelper.ViewContext.HttpContext.Request.QueryString.Count > 0)
            {
                onChangeUrl.Append("?");
                onChangeUrl.Append(htmlHelper.ViewContext.HttpContext.Request.QueryString);
            }

            // TODO: Store this in a data-dash attribute data-url="<<onchangeurl>>" which the jQuery uses - separation of concerns.
            var htmlAttributes = new
            {
                onchange = string.Format("location.href='{0}';", onChangeUrl),
                @class = "span1"
            };

            var mvcDropDownList = htmlHelper.DropDownList(id, selectListItems, htmlAttributes).ToHtmlString();
            
            // TODO: For adaptive web design adherence put the drop-down list in a GET form with a button that appears when JavaScript is disabled.
            return MvcHtmlString.Create(mvcDropDownList.ToString(CultureInfo.CurrentCulture));
        }

        public static IHtmlString RadioButtonWithLabelFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            TProperty value,
            string labelText,
            string helpText = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var currentValue = metadata.Model;
            var property = metadata.PropertyName;

            var radioButton = new FluentTagBuilder("input")
                .GenerateId(property + value)
                .MergeAttribute("type", "radio")
                .MergeAttribute("name", property)
                .MergeAttribute("value", Convert.ToString(value));

            if (currentValue.Equals(value))
            {
                radioButton.MergeAttribute("checked", "checked");
            }

            var label = new FluentTagBuilder("label")
                .MergeAttribute("for", radioButton.Attributes["id"])
                .SetInnerText(labelText);

            var radioButtonHelpText = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(helpText))
            {
                radioButtonHelpText.Append(new TagBuilder("br").ToString(TagRenderMode.SelfClosing));
                radioButtonHelpText.Append(new FluentTagBuilder("span")
                    .MergeAttribute("class", "help")
                    .SetInnerText(helpText));
            }

            return new MvcHtmlString(
                radioButton.ToString(TagRenderMode.SelfClosing) + 
                label +
                radioButtonHelpText);
        }

        public static IHtmlString SearchRouteLink(
            this HtmlHelper htmlHelper, 
            string linkText, 
            string routeName, 
            object routeValues,
            bool wrapInListItem = false)
        {     
            // Extends the base functionality of Html.RouteLink() by including the querystring parameters in the constructed anchor.
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var request = htmlHelper.ViewContext.HttpContext.Request;
            var uriBuilder = new UriBuilder(urlHelper.RouteUrl(routeName, routeValues, request.Url.Scheme));
            string mvcHtmlString;

            var query = HttpUtility.ParseQueryString(string.Empty);
            foreach (string key in request.QueryString.Keys)
            {
                query[key] = request[key];
            }

            var routeQuery = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (string key in routeQuery)
            {
                query[key] = routeQuery[key];
            }

            uriBuilder.Query = query.ToString();

            // TODO: Test the linkText, it might need to be Url.Encoded to avoid users typing something malicious into the search field which then sits in a number of links.

            var anchor = new FluentTagBuilder("a")
                .MergeAttribute("href", uriBuilder.ToString())
                .SetInnerText(linkText);

            if (wrapInListItem)
            {
                var li = new FluentTagBuilder("li")
                    .SetInnerHtml(anchor.ToString());

                mvcHtmlString = li.ToString();
            }
            else
            {
                mvcHtmlString = anchor.ToString();
            }

            return MvcHtmlString.Create(mvcHtmlString);
        }
    }
}