using System.Collections.Generic;
using System.Web.Mvc;

namespace FallenNova.Web.Helpers
{
    /// <summary>
    /// Wrapper for <see cref="TagBuilder"/> that makes it a fluent API.
    /// </summary>
    /// <remarks>
    /// Adopted from "Fluent wrapper for ASP.NET MVC TagBuilder" by "http://trycatchfail.com/blog/post/Fluent-wrapper-for-ASPNET-MVC-TagBuilder.aspx"
    /// </remarks>
    public class FluentTagBuilder
    {
        private readonly TagBuilder _tagBuilder;

        public FluentTagBuilder(string tagName)
        {
            _tagBuilder = new TagBuilder(tagName);
        }

        public IDictionary<string, string> Attributes
        {
            get { return _tagBuilder.Attributes; }
        }
          
        public string InnerHtml
        {
            get { return _tagBuilder.InnerHtml; }
            set { _tagBuilder.InnerHtml = value; }
        }

        public FluentTagBuilder AddCssClass(string cssClass)
        {
            _tagBuilder.AddCssClass(cssClass);

            return this;
        }

        public FluentTagBuilder AddInnerText(string innerText)
        {
            _tagBuilder.SetInnerText(innerText);

            return this;
        }

        public FluentTagBuilder GenerateId(string id)
        {
            _tagBuilder.GenerateId(id);

            return this;
        }

        public FluentTagBuilder MergeAttribute(string key, string value)
        {
            _tagBuilder.MergeAttribute(key, value);

            return this;
        }

        public FluentTagBuilder SetInnerHtml(string innerHtml)
        {
            _tagBuilder.InnerHtml = innerHtml;

            return this;
        }

        public FluentTagBuilder SetInnerText(string innerText)
        {
            _tagBuilder.SetInnerText(innerText);

            return this;
        }

        public override string ToString()
        {
            return _tagBuilder.ToString();
        }

        public string ToString(TagRenderMode tagRenderMode)
        {
            return _tagBuilder.ToString(tagRenderMode);
        }
    }
}