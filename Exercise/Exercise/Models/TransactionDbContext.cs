using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercise.Models
{
    public class TransactionDbContext:DbContext
    {


        //public TransactionDbContext(DbContextOptions<TransactionDbContext> options):
        //    base(options)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=LAPTOP-9R7341J9;initial catalog=Comm;persist security info=True;Trusted_Connection=True;multipleactiveresultsets=True");
        }

        public virtual DbSet<Transaction> transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.accountNumber)
                    .HasColumnType("accountNumber");

                entity.Property(e => e.tranactionType)
                    .HasColumnType("tranactionType");

                entity.Property(e => e.amount).HasColumnType("amount");

                entity.Property(e => e.balance).HasColumnType("balance");

                entity.Property(e => e.description).HasColumnType("description");

                entity.Property(e => e.transaction_date)
                    .HasColumnName("transaction_date");

                entity.Property(e => e.modified_date)
                    .HasColumnType("modified_date");
            });


        }
    }
}
