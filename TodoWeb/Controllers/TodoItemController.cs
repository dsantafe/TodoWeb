using System.Threading.Tasks;
using System.Web.Mvc;
using TodoWeb.DTOs;
using TodoWeb.Services.Implements;

namespace TodoWeb.Controllers
{
    public class TodoItemController : Controller
    {
        readonly TodoItemService todoItemService;

        public TodoItemController()
        {
            todoItemService = new TodoItemService();
        }

        public async Task<ActionResult> Index()
        {
            var listTodoItemDTO = await todoItemService.GetAll();

            return View(listTodoItemDTO);
        }

        // GET: TodoItem
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TodoItemDTO todoItemDTO)
        {
            if (ModelState.IsValid)
            {
                todoItemDTO = await todoItemService.Create(todoItemDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(todoItemDTO);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var todoItemDTO = new TodoItemDTO();

            if (id != null)
                todoItemDTO = await todoItemService.GetById(id.Value);

            return View(todoItemDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int? id, TodoItemDTO todoItemDTO)
        {
            if (ModelState.IsValid && id != null)
            {
                await todoItemService.Update(id.Value, todoItemDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(todoItemDTO);
        }

        public async Task<ActionResult> Details(int? id)
        {
            var todoItemDTO = new TodoItemDTO();

            if (id != null)
                todoItemDTO = await todoItemService.GetById(id.Value);

            return View(todoItemDTO);
        }     
        
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
                await todoItemService.Delete(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}