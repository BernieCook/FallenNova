using FallenNova.Web.Helpers;
using NUnit.Framework;

namespace FallenNova.Test.Web.Helpers
{
    [Category("Helpers")]
    [TestFixture]
    public class FluentTagBuilderTests
    {
        private string _attributeName;
        private string _attributeValue;
        private string _cssClass;
        private string _mergeAttributeKey;
        private string _mergeAttributeValue;
        private string _innerText;
        private string _tagName;

        private FluentTagBuilder _fluentTag;

        [SetUp]
        public void SetUp()
        {
            _attributeName = "this";
            _attributeValue = "is";
            _cssClass = "good";
            _mergeAttributeKey = "for";
            _mergeAttributeValue = "testing";
            _innerText = "this extension";
            _tagName = "method";
        }

        [Test]
        public void FluentTagBuilder_Basic_Test()
        {
            _fluentTag = new FluentTagBuilder(_tagName)
                .MergeAttribute(_attributeName, _attributeValue)
                .SetInnerText(_innerText);

            var tag = _fluentTag.ToString();

            StringAssert.IsMatch(tag, string.Format("<{0} {1}=\"{2}\">{3}</{0}>", _tagName, _attributeName, _attributeValue, _innerText));
        }

        [Test]
        public void FluentTagBuilder_CssClass_Test()
        {
            _fluentTag = new FluentTagBuilder(_tagName)
                .MergeAttribute(_attributeName, _attributeValue)
                .AddCssClass(_cssClass)
                .SetInnerText(_innerText);

            var tag = _fluentTag.ToString();

            StringAssert.IsMatch(tag, string.Format("<{0} class=\"{4}\" {1}=\"{2}\">{3}</{0}>", _tagName, _attributeName, _attributeValue, _innerText, _cssClass));
        }

        [Test]
        public void FluentTagBuilder_MergeAttribute_Test()
        {
            _fluentTag = new FluentTagBuilder(_tagName)
                .MergeAttribute(_attributeName, _attributeValue)
                .MergeAttribute(_mergeAttributeKey, _mergeAttributeValue)
                .SetInnerText(_innerText);

            var tag = _fluentTag.ToString();

            StringAssert.IsMatch(tag, string.Format("<{0} {4}=\"{5}\" {1}=\"{2}\">{3}</{0}>", _tagName, _attributeName, _attributeValue, _innerText, _mergeAttributeKey, _mergeAttributeValue));
        }

        [Test]
        public void FluentTagBuilder_Attribute_Test()
        {
            _fluentTag = new FluentTagBuilder(_tagName)
                .MergeAttribute(_attributeName, _attributeValue)
                .MergeAttribute(_mergeAttributeKey, _mergeAttributeValue)
                .SetInnerText(_innerText);

            var value = _fluentTag.Attributes[_mergeAttributeKey];

            StringAssert.IsMatch(value, _mergeAttributeValue);
        }
    }
}
