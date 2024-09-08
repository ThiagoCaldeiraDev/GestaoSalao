using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoSalao.UI.ExtensionWeb
{
    public static class EnumerableExtensions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, object> value, Func<T, object> text,
            object selectedValue = null, Func<T, string> group = null, bool toUpper = false)
        {
            return new SelectList(enumerable.Select(x => new SelectListItem
            {
                Value = value(x).ToString(),
                Text = toUpper ? text(x).ToString().ToUpper() : text(x).ToString(),
                Group = group != null ? new SelectListGroup { Name = group(x) } : null
            }), nameof(SelectListItem.Value), nameof(SelectListItem.Text), selectedValue, nameof(SelectListItem.Group));
        }

        public static SelectList ToSelectList<T>(this List<T> enumerable, Func<T, object> value, Func<T, object> text,
            object selectedValue = null, Func<T, string> group = null, bool toUpper = false)
        {
            return new SelectList(enumerable.Select(x => new SelectListItem
            {
                Value = value(x).ToString(),
                Text = toUpper ? text(x).ToString().ToUpper() : text(x).ToString(),
                Group = group != null ? new SelectListGroup { Name = group(x) } : null
            }), nameof(SelectListItem.Value), nameof(SelectListItem.Text), selectedValue, nameof(SelectListItem.Group));
        }
    }
}
