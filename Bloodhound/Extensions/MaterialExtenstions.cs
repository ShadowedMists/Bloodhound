using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bloodhound.Extensions
{
    public static class MaterialExtenstions
    {
        /// <summary>
        /// Creates a Material Dropdown list for the expression specified.
        /// </summary>
        /// <typeparam name="TModel">The Type for the model to render.</typeparam>
        /// <typeparam name="TResult">The result Type for the expression.</typeparam>
        /// <param name="html">The IHtmlHelper received from the framework.</param>
        /// <param name="expression">The MemberExpression representing the Member for this drop down list.</param>
        /// <param name="selectList">the list of SelectListItem objects that are the selection.</param>
        /// <param name="optionLabel">The optional label to display for the control.</param>
        /// <param name="htmlAttributes">The optional html attributes to add to the control.</param>
        /// <returns>An IHtmlContext object that will render to HTML.</returns>
        /// <remarks>In traditional ASP.NET MVC, optionLabel is added as a first item in the select list for presentation however in Material, the optionLabel is used as a floating label.</remarks>
        public static IHtmlContent MaterialDropdownListFor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            string style = GetStringProperty(htmlAttributes, "style");

            // determine control name
            MemberExpression body = expression.Body as MemberExpression;
            string expressionName = body.Member.Name;
            string labelId = expressionName + "-label";
            string textId = expressionName + "-text";
            string modelValue = html.ValueFor(expression);

            // set the selected item
            SelectListItem selected = selectList.FirstOrDefault(x => x.Value == modelValue);
            if (selected != null)
            {
                foreach (SelectListItem item in selectList)
                    item.Selected = false;
                selected.Selected = true;
            }

            // build the ul list for selection items
            TagBuilder ulSelectMenu = new TagBuilder("ul");
            ulSelectMenu.AddCssClass("mdc-list");

            // create the list items
            if (selectList != null)
            {
                foreach (SelectListItem item in selectList)
                {
                    TagBuilder liListItem = new TagBuilder("li");
                    liListItem.AddCssClass("mdc-list-item");
                    liListItem.Attributes.Add("role", "option");

                    if (item.Selected)
                    {
                        liListItem.Attributes.Add("aria-selected", "true");
                        liListItem.AddCssClass("mdc-list-item--selected");
                    }

                    if (item.Value != null)
                        liListItem.Attributes.Add("data-value", item.Value);

                    if (item.Text != null)
                        liListItem.InnerHtml.Append(item.Text);

                    ulSelectMenu.InnerHtml.AppendHtml(liListItem);
                }
            }

            // main div
            TagBuilder divMdcSelect = new TagBuilder("div");
            divMdcSelect.AddCssClass("mdc-select");
            divMdcSelect.Attributes.Add("data-target", expressionName);
            divMdcSelect.Attributes.Add("data-mdc-auto-init", "MDCSelect");
            if (!string.IsNullOrEmpty(style))
                divMdcSelect.Attributes.Add("style", style);

            // hidden input for forms 
            TagBuilder inputHidden = new TagBuilder("input");
            inputHidden.Attributes.Add("type", "hidden");
            inputHidden.Attributes.Add("id", expressionName);
            inputHidden.Attributes.Add("name", expressionName);
            inputHidden.Attributes.Add("value", html.ValueFor(expression));
            divMdcSelect.InnerHtml.AppendHtml(inputHidden);

            // achor
            TagBuilder divMdcAnchor = new TagBuilder("div");
            divMdcAnchor.AddCssClass("mdc-select__anchor");
            divMdcSelect.InnerHtml.AppendHtml(divMdcAnchor);

            // icon
            TagBuilder iMdcIcon = new TagBuilder("i");
            iMdcIcon.AddCssClass("mdc-select__dropdown-icon");
            divMdcAnchor.InnerHtml.AppendHtml(iMdcIcon);

            // material selected text container
            TagBuilder divMdcSelected = new TagBuilder("div");
            divMdcSelected.AddCssClass("mdc-select__selected-text");
            divMdcSelected.Attributes.Add("id", textId);
            divMdcSelected.Attributes.Add("role", "button");
            divMdcSelected.Attributes.Add("aria-haspopup", "listbox");
            divMdcAnchor.InnerHtml.AppendHtml(divMdcSelected);

            // if optionLabel is specifed, as material floating label
            if (optionLabel != null)
            {
                TagBuilder spanMdcLabel = new TagBuilder("span");
                spanMdcLabel.AddCssClass("mdc-floating-label");
                spanMdcLabel.InnerHtml.Append(optionLabel);
                divMdcAnchor.InnerHtml.AppendHtml(spanMdcLabel);
                spanMdcLabel.Attributes.Add("id", labelId);

                divMdcSelected.Attributes.Add("aria-labelledby", labelId + " " + textId);
            }

            // ripple
            TagBuilder divMdcRipple = new TagBuilder("div");
            divMdcRipple.AddCssClass("mdc-line-ripple");
            divMdcAnchor.InnerHtml.AppendHtml(divMdcRipple);

            // add items to menu
            TagBuilder divMdcMenu = new TagBuilder("div");
            divMdcMenu.AddCssClass("mdc-select__menu");
            divMdcMenu.AddCssClass("mdc-menu");
            divMdcMenu.AddCssClass("mdc-menu-surface");
            divMdcMenu.AddCssClass("dropdown-width");
            divMdcMenu.Attributes.Add("role", "listbox");
            divMdcSelect.InnerHtml.AppendHtml(divMdcMenu);
            divMdcMenu.InnerHtml.AppendHtml(ulSelectMenu);

            // return created tag
            return divMdcSelect;
        }

        internal static string GetStringProperty(object sourceObject, string propertyName)
        {
            if (sourceObject == null)
                return null;
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));


            Type attributesType = sourceObject.GetType();

            PropertyInfo propertyInfo = attributesType.GetProperty(propertyName);
            if (propertyInfo == null)
                return null;

            object typeObject = propertyInfo.GetValue(sourceObject);
            if (typeObject == null)
                return null;

            return typeObject.ToString();
        }

        public static IHtmlContent MaterialTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> html,
             Expression<Func<TModel, TResult>> expression,
             object htmlAttributes = null)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            string type = GetStringProperty(htmlAttributes, "type") ?? "text";
            string style = GetStringProperty(htmlAttributes, "style");
            string modelValue = html.ValueFor(expression);

            // determine control name
            MemberExpression body = expression.Body as MemberExpression;
            string expressionName = body.Member.Name;
            string displayName = html.DisplayNameFor(expression);

            // return input
            return CreateInput(expressionName, displayName, type, modelValue, style);
        }

        public static IHtmlContent MaterialPasswordFor<TModel, TResult>(this IHtmlHelper<TModel> html,
             Expression<Func<TModel, TResult>> expression,
             object htmlAttributes = null)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            string style = GetStringProperty(htmlAttributes, "style");

            // determine control name
            MemberExpression body = expression.Body as MemberExpression;
            string expressionName = body.Member.Name;
            string displayName = html.DisplayNameFor(expression);
            string modelValue = html.ValueFor(expression);

            // return input control
            return CreateInput(expressionName, displayName, "password", modelValue, style);
        }

        internal static TagBuilder CreateInput(string name, string label, string type, string value, string style = null)
        {
            // wrapper div
            TagBuilder divTextField = new TagBuilder("div");
            divTextField.AddCssClass("mdc-text-field");
            divTextField.Attributes.Add("data-mdc-auto-init", "MDCTextField");
            if (!string.IsNullOrEmpty(style))
                divTextField.Attributes.Add("style", style);

            // input
            TagBuilder inputField = new TagBuilder("input");
            inputField.Attributes.Add("id", name);
            inputField.Attributes.Add("name", name);
            inputField.Attributes.Add("type", type);
            inputField.AddCssClass("mdc-text-field__input");
            if (!string.IsNullOrEmpty(value))
                inputField.Attributes.Add("value", value);

            // label
            TagBuilder labelControl = new TagBuilder("label");
            labelControl.AddCssClass("mdc-floating-label");
            labelControl.Attributes.Add("for", name);
            labelControl.InnerHtml.Append(label);

            // ripple
            TagBuilder divRipple = new TagBuilder("div");
            divRipple.AddCssClass("mdc-line-ripple");

            // append controls
            divTextField.InnerHtml.AppendHtml(inputField);
            divTextField.InnerHtml.AppendHtml(labelControl);
            divTextField.InnerHtml.AppendHtml(divRipple);

            // return wrapper content
            return divTextField;
        }

        public static IHtmlContent MaterialCheckBoxFor<TModel, TResult>(this IHtmlHelper<TModel> html,
             Expression<Func<TModel, TResult>> expression)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            //string style = GetStringProperty(htmlAttributes, "style");

            // determine control name
            MemberExpression body = expression.Body as MemberExpression;
            string expressionName = body.Member.Name;
            string displayName = html.DisplayNameFor(expression);
            string modelValue = html.ValueFor(expression);

            TagBuilder divWrapper = new TagBuilder("div");
            divWrapper.AddCssClass("mdc-touch-target-wrapper");

            TagBuilder divFormField = new TagBuilder("div");
            divFormField.AddCssClass("mdc-form-field");

            TagBuilder divCheckbox = new TagBuilder("div");
            divCheckbox.AddCssClass("mdc-checkbox");
            divCheckbox.AddCssClass("mdc-checkbox--touch");

            TagBuilder checkbox = new TagBuilder("input");
            checkbox.Attributes.Add("type", "checkbox");
            checkbox.AddCssClass("mdc-checkbox__native-control");
            checkbox.Attributes.Add("id", expressionName);
            checkbox.Attributes.Add("name", expressionName);
            checkbox.Attributes.Add("data-val", "true");
            checkbox.Attributes.Add("value", "true");
            if (modelValue == "True")
                checkbox.Attributes.Add("checked", "checked");

            TagBuilder divCheckboxBackground = new TagBuilder("div");
            divCheckboxBackground.AddCssClass("mdc-checkbox__background");

            TagBuilder svgCheckmark = new TagBuilder("svg");
            svgCheckmark.AddCssClass("mdc-checkbox__checkmark");
            svgCheckmark.Attributes.Add("viewBox", "0 0 24 24");

            TagBuilder pathCheckmark = new TagBuilder("path");
            pathCheckmark.AddCssClass("mdc-checkbox__checkmark-path");
            pathCheckmark.Attributes.Add("fill", "none");
            pathCheckmark.Attributes.Add("d", "M1.73,12.91 8.1,19.28 22.79,4.59");

            TagBuilder divMixedMark = new TagBuilder("div");
            divMixedMark.AddCssClass("mdc-checkbox__mixedmark");

            TagBuilder divRipple = new TagBuilder("div");
            divRipple.AddCssClass("mdc-checkbox__ripple");

            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("for", expressionName);
            label.InnerHtml.Append(displayName);

            // build structure
            svgCheckmark.InnerHtml.AppendHtml(pathCheckmark);
            divCheckboxBackground.InnerHtml.AppendHtml(svgCheckmark);
            divCheckboxBackground.InnerHtml.AppendHtml(divMixedMark);
            divCheckbox.InnerHtml.AppendHtml(checkbox);
            divCheckbox.InnerHtml.AppendHtml(divCheckboxBackground);
            divCheckbox.InnerHtml.AppendHtml(divRipple);
            divFormField.InnerHtml.AppendHtml(divCheckbox);
            divFormField.InnerHtml.AppendHtml(label);
            divWrapper.InnerHtml.AppendHtml(divFormField);

            // return input
            return divWrapper;
        }
    }
}
