using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Models
{
    public class DropDownModel<T> : IEnumerable<T> where T : IIdentifier
    {
        public bool EmptyItem { get; set; }

        public IEnumerable<T> Items { get; protected set; }

        public Func<T, string> TextGetter { get; set; }

        public DropDownModel(IEnumerable<T> items, Func<T, string> textGetter, bool emptyItem = false)
        {
            Items = items;
            TextGetter = textGetter;
            EmptyItem = emptyItem;
        }

        public IEnumerable<SelectListItem> AsSelectedList(int? selectedID = null, bool? emptyItem = null)
        {
            if (emptyItem != null)
                EmptyItem = emptyItem.Value;

            if (EmptyItem)
                yield return new SelectListItem
                {
                    Selected = selectedID == null,
                    Value = "",
                    Text = "---"
                };

            foreach (T item in Items)
                yield return new SelectListItem
                {
                    Selected = item.ID == selectedID,
                    Value = item.ID.ToString(),
                    Text = TextGetter(item)
                };
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
