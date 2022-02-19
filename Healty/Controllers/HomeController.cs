using Healty.Core;
using Healty.Core.Models;
using Healty.Core.ViewModels;
using Healty.Persistence;

using Microsoft.AspNet.Identity;
using System.Linq.Dynamic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Healty.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private ApplicationDbContext _appDbContext = new ApplicationDbContext();
        
        // GET: Todos
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetDataTableData()
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDirection = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            //try
            //{
                var todoData = await _unitOfWork.Todos.GetDataTable(
                       criteria:
                       m => string.IsNullOrEmpty(searchValue) ? true :
                       (m.Title.Contains(searchValue) || m.DueDate.ToString() == searchValue),
                        pageSize, skip,
                        SortColumnAndDirection: (string.Concat(sortColumn, " ", sortColumnDirection))
                        );

                int recordsTotal = todoData.Count;

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = todoData.DataList }, JsonRequestBehavior.AllowGet);
            //}
            //catch 
            //{
            //    return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = null }, JsonRequestBehavior.AllowGet);

            //}


        }


        public ActionResult Create()
        {
            return PartialView("_CreateOrEditModal", new TodoVM());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return HttpNotFound();

            var todo = _unitOfWork.Todos.GetById(id.Value);
            if (todo == null) return View("NotFound");
            if (todo.IsDone) return View(nameof(Index));

            if (todo.ApplicationUserId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var vm = new TodoVM()
            {
                DueDate = todo.DueDate,
                Title = todo.Title,
                Id = todo.Id,
                IsDone = todo.IsDone
            };
            //var vm = _mapper.Map<ActorFormVM>(actor);
            return PartialView("_CreateOrEditModal", vm);
        }

    }

}