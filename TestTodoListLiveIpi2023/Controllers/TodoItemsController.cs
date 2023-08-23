using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTodoListLiveIpi2023.Models;
using TestTodoListLiveIpi2023.Models.Context;

namespace TestTodoListLiveIpi2023.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _contextTodo;

        public TodoItemsController(TodoContext context)
        {
            _contextTodo = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
          if (_contextTodo.TodoItems == null)
          {
              return NotFound();
          }
            return await _contextTodo.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            //Vérfifier si la table existe
          if (_contextTodo.TodoItems == null)
          {
              return NotFound();
          }
          //Cherche l'objet
            var todoItem = await _contextTodo.TodoItems.FindAsync(id);

            //On vérifie si l'objet existe
            if (todoItem == null)
            {
                return NotFound();
            }

            //On retourne l'objet
            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _contextTodo.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _contextTodo.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
          if (_contextTodo.TodoItems == null)
          {
              return Problem("Entity set 'TodoContext.TodoItems'  is null.");
          }
            _contextTodo.TodoItems.Add(todoItem);
            await _contextTodo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (_contextTodo.TodoItems == null)
            {
                return NotFound();
            }
            var todoItem = await _contextTodo.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _contextTodo.TodoItems.Remove(todoItem);
            await _contextTodo.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return (_contextTodo.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
