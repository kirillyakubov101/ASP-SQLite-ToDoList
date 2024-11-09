using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Web.Data;
using Todo.Web.Models;
using Todo.Web.Models.Entities;

namespace Todo.Web.Controllers
{
    public class ToDoElementController : Controller
    {
        private readonly ToDoContext _dbcontext;
        public ToDoElementController(ToDoContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddElementViewModel ele)
        {
            ToDoElement newElement = new ToDoElement(ele.Title, ele.IsCompleted);

            await _dbcontext.ToDoElements.AddAsync(newElement);
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction("List", "ToDoElement");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var elements =  await _dbcontext.ToDoElements.ToListAsync();
            return View(elements);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var foundElement = await _dbcontext.ToDoElements.FindAsync(id);

            return View(foundElement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoElement viewModel)
        {
           var element =  await _dbcontext.ToDoElements.FindAsync(viewModel.Id);
            
            //it can be null, if it is not, update the properties
            if(element != null)
            {
                element.Title = viewModel.Title;
                element.IsCompleted = viewModel.IsCompleted;

                await _dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("List", "ToDoElement");
        }

        [HttpPost]
        public async Task<IActionResult> Delete (Guid id)
        {
            var toDoItem = await _dbcontext.ToDoElements.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _dbcontext.ToDoElements.Remove(toDoItem);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("List", "ToDoElement");
        }

    }
}
