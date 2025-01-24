


using Microsoft.EntityFrameworkCore;

namespace Prueba.Models;

  public class TodoContext: DbContext
  {
    public TodoContext(DbContextOptions<TodoContext>
      options)
     : base(options)
    { }

  public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get;  set; }
    
}
