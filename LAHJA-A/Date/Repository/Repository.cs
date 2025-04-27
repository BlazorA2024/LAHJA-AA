namespace Date.Repository;
public interface IRepsitory<T>
{
    bool Add(T item);
    List<bool> AddRang(List<T> citems);
    bool Remove(T item);
    T Serach(T item);

    T Update(T item);
    ICollection<T> Items { get; }

    ICollection<T> GetAll() => Items; // Method to get all items in the repository

    T GetById(string id);// Method to get an item by its ID
    IEnumerable<T> Search(Func<T, bool> predicate);

}

public abstract class Repository<T> : IRepsitory<T>
{
    private static readonly List<T> items = new();

    public ICollection<T> Items => items;

    public virtual bool Add(T item)
    {
        if (items.Contains(item)) return false;
        items.Add(item);
        return true;
    }



    //public virtual T GetById(string id)
    //{
    //    return (T)items.Select(i => i.GetType().GetProperty("Id")?.GetValue(i, null))
    //        .FirstOrDefault(i => i?.ToString() == id);
    //}
    public virtual T GetById(string id)
    {
        return items.FirstOrDefault(i =>
            i.GetType().GetProperty("Id")?.GetValue(i)?.ToString() == id);
    }

    public virtual bool Remove(T item)
    {
        items.Remove(item);
        return true;
    }
    public virtual T Serach(T item)
    {
        return items.Find(i => i.Equals(item));
    }
    public virtual T Update(T item)
    {
        var index = items.IndexOf(item);
        if (index != -1)
        {
            items[index] = item;
            return item;
        }
        return default;
    }
    public virtual IEnumerable<T> Search(Func<T, bool> predicate)
    {
        return items.Where(predicate);
    }
    public static bool Validate<T>(List<T> items, T item) where T : class
    {

        foreach (var ob in items)
        {
            if (ob == item)
            {
                Console.WriteLine("Error: Null item found in the list.");
                return false;
            }

        }

        return true;

    }

    public virtual List<bool> AddRang(List<T> citems)
    {
        var list = new List<bool>();
        foreach (var c in citems)
        {
            var b = Add(c);
            list.Add(b);
        }
        return list;
    }
}

