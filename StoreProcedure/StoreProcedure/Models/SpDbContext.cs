using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace StoreProcedure.Models;

public partial class SpDbContext : DbContext
{
    public SpDbContext()
    {
    }

    public SpDbContext(DbContextOptions<SpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=HUNTERXBD\\SQLEXPRESS;Database=spDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB7D821651A3");

            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public IQueryable<Customer>SearchCustomer(string name)
    {
        SqlParameter pName = new SqlParameter("@name", name);
        return this.Customers.FromSqlRaw("Exec spSearchCustomer @name", pName);
    }
    public void InsertCustomer(Customer customer)
    {
        SqlParameter pName = new SqlParameter("@name", customer.Name);
        SqlParameter pCountry = new SqlParameter("@country", customer.Country);
        this.Database.ExecuteSqlRaw("EXEC spCustomerInsert @name, @country", pName, pCountry);
    }
    public void UpdateCustomer(Customer customer)
    {
        SqlParameter pId = new SqlParameter("@id", customer.CustomerId);
        SqlParameter pName = new SqlParameter("@name", customer.Name);
        SqlParameter pCountry = new SqlParameter("@country", customer.Country);
        this.Database.ExecuteSqlRaw("EXEC spUpdateCustomer @id, @name, @country", pId, pName, pCountry);
    }
    public void DeleteCustomer(int id)
    {
        SqlParameter pId = new SqlParameter("@id", id);
        this.Database.ExecuteSqlRaw("EXEC spDeleteCustomer @id", pId);
    }
}
