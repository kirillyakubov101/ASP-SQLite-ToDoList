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

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var elements =  await _dbcontext.ToDoElements.ToListAsync();
            return View(elements);
        }

    }
}
