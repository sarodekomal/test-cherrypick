using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoreWebAPIExample.Models;

namespace CoreWebAPIExample.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {

        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _todoRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            _todoRepository.Add(todoItem);
            return CreatedAtRoute("GetTodo", new { id = todoItem.Key }, todoItem);
        }



        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TodoItem todoItem)
        {
            if (todoItem == null || todoItem.Key != id)
            {
                return BadRequest();
            }

            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = todoItem.IsComplete;
            todo.Name = todoItem.Name;

            _todoRepository.Update(todo);
            return new NoContentResult();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoRepository.Remove(id);
            return new NoContentResult();
        }
    }
}