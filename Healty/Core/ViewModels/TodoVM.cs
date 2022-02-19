using Healty.Core.Models;

using System;
using System.ComponentModel.DataAnnotations;

namespace Healty.Core.ViewModels
{
    public class TodoVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        //[FutureDate]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        //public string ApplicationUserId { get; set; }
        public bool IsDone { get; set; }


        //public string Action
        //{
        //    get
        //    {
        //        Expression<Func<TodosController, Task<ActionResult>>> update =
        //            (c => c.Update(this));

        //        Expression<Func<TodosController, Task<ActionResult>>> create =
        //            (c => c.Create(this));

        //        var action = (Id != 0) ? update : create;
        //        return (action.Body as MethodCallExpression).Method.Name;
        //    }
        //}


    }

}