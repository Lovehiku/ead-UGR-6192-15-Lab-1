using System.Collections.Generic;

public class TodoRepository
{
    private readonly List<TodoItem> items = new();
    private int nextId = 1;

    public IEnumerable<TodoItem> GetAll() => items;

    public TodoItem? Get(int id) =>
        items.Find(t => t.Id == id);

    public TodoItem Add(TodoItem item)
    {
        item.Id = nextId++;
        items.Add(item);
        return item;
    }

    public bool Update(int id, TodoItem updated)
    {
        var existing = items.Find(t => t.Id == id);
        if (existing == null) return false;

        existing.Name = updated.Name;
        existing.IsComplete = updated.IsComplete;
        return true;
    }

    public bool Delete(int id)
    {
        var existing = items.Find(t => t.Id == id);
        if (existing == null) return false;
        items.Remove(existing);
        return true;
    }
}
