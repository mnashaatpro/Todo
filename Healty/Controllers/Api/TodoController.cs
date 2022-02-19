using Healty.Core;
using Healty.Core.Models;
using Healty.Core.ViewModels;

using Microsoft.AspNet.Identity;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Healty.Controllers.Api
{
    [Authorize]
    public class TodoController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpSert(TodoVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Enter required fields");

            if (vm.Id == 0)
            {
                var todo = new Todo
                {
                    ApplicationUserId = User.Identity.GetUserId(),
                    DueDate = vm.DueDate,
                    Title = vm.Title,
                    IsDone = vm.IsDone
                };

                _unitOfWork.Todos.Add(todo);
            }
            else
            {
                var todo = _unitOfWork.Todos.GetById(vm.Id, false);
                if (todo == null) return NotFound();

                if (todo.ApplicationUserId != User.Identity.GetUserId())
                    return Unauthorized();

                //todo = _mapper.Map<TodoVM>(vm);
                todo.ApplicationUserId = User.Identity.GetUserId();
                todo.DueDate = vm.DueDate;
                todo.Title = vm.Title;
                todo.IsDone = vm.IsDone;
            }
            
            await _unitOfWork.CompleteAsync();

            //_toastNotification.AddSuccessToastMessage("Task Added successfully");
            return Ok("Success");
        }

        [HttpPost]
        public async Task<IHttpActionResult> MarkDone(int id)
        {
            var todo = _unitOfWork.Todos.GetById(id , false);
            if (todo == null) return NotFound();
            var userId = User.Identity.GetUserId();

            if (todo.ApplicationUserId != userId)
                return Unauthorized();

            todo.IsDone=true;
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var todo = _unitOfWork.Todos.GetById(id);
            if (todo == null) return NotFound();
            var userId = User.Identity.GetUserId();

            if (todo.ApplicationUserId != userId)
                return Unauthorized();

            _unitOfWork.Todos.Delete(todo);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

    }
}
