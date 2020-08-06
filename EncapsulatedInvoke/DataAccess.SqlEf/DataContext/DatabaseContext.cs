using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.SqlEf.DataContext
{
  public class DatabaseContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    //entities
    public DbSet<CategoryTable> CategoryTables { get; set; }
    public DbSet<OrderData> Orders { get; set; }
    public DbSet<OrderLineItemData> OrderLineItems { get; set; }
    public DbSet<OrderLineItemPersonData> OrderLineItemPersons { get; set; }
    public DbSet<PersonData> Persons { get; set; }
    public DbSet<SkillData> Skills { get; set; }
  }
}
