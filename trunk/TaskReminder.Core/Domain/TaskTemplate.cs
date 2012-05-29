using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public class TaskTemplate : Task
    {
        [Display(Name = "Automaticky opakovat")]
        public bool AutoRepeat { get; set; }

        [Display(Name = "Opakování")]
        public int Period { get; set; }
    }

    public static class TemplatePeriods
    {
        /// <summary>
        /// Měsíční opakování.
        /// </summary>
        public const int Monthly = 1;

        /// <summary>
        /// Čtvrtletní opakování.
        /// </summary>
        public const int Quarterly = 3;

        /// <summary>
        /// Pololetní opakování.
        /// </summary>
        public const int Biannually = 6;

        /// <summary>
        /// Roční opakování.
        /// </summary>
        public const int PerAnnum = 12;

        public static IEnumerable<SelectListItem> AsSelectList(int? value = null)
        {
            yield return new SelectListItem
            {
                Text = "Měsíčně",
                Value = Monthly.ToString(),
                Selected = value != null && value.Value == Monthly
            };
            yield return new SelectListItem
            {
                Text = "Čtvrtletně",
                Value = Quarterly.ToString(),
                Selected = value != null && value.Value == Quarterly
            };
            yield return new SelectListItem
            {
                Text = "Polopetně",
                Value = Biannually.ToString(),
                Selected = value != null && value.Value == Biannually
            };
            yield return new SelectListItem
            {
                Text = "Ročně",
                Value = PerAnnum.ToString(),
                Selected = value != null && value.Value == PerAnnum
            };
        }
    }
}
