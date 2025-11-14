using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Register the repository as a service
builder.Services.AddSingleton<TodoRepository>();

var app = builder.Build();

// ----------- GROUP ROUTES --------------
var todoItems = app.MapGroup("/todoitems");

// GET ALL
todoItems.MapGet("/", GetAllTodos);

// GET BY ID
todoItems.MapGet("/{id}", GetTodoById);

// POST
todoItems.MapPost("/", CreateTodo);

// PUT
todoItems.MapPut("/{id}", UpdateTodo);

// DELETE
todoItems.MapDelete("/{id}", DeleteTodo);

// ---------------------------------------

app.Run();

// ------------ ROUTE HANDLER METHODS ---------------

// GET: /todoitems
static Ok<IEnumerable<TodoItem>> GetAllTodos(TodoRepository repo)
{
    return TypedResults.Ok(repo.GetAll());
}

// GET: /todoitems/{id}
static Results<Ok<TodoItem>, NotFound> GetTodoById(TodoRepository repo, int id)
{
    var item = repo.Get(id);

    return item is not null
        ? TypedResults.Ok(item)
        : TypedResults.NotFound();
}

// POST: /todoitems
static Created<TodoItem> CreateTodo(TodoRepository repo, TodoItem item)
{
    var created = repo.Add(item);

    return TypedResults.Created($"/todoitems/{created.Id}", created);
}

// PUT: /todoitems/{id}
static Results<NoContent, NotFound> UpdateTodo(TodoRepository repo, int id, TodoItem updated)
{
    var success = repo.Update(id, updated);

    return success
        ? TypedResults.NoContent()
        : TypedResults.NotFound();
}

// DELETE: /todoitems/{id}
static Results<NoContent, NotFound> DeleteTodo(TodoRepository repo, int id)
{
    var success = repo.Delete(id);

    return success
        ? TypedResults.NoContent()
        : TypedResults.NotFound();
}
