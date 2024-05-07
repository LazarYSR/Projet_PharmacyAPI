using Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Prj_APIPharmacy.Data;

public class PharmacyDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Pharmacy> Pharmacies { get; set; } = null!;
    public DbSet<PharmacyProduct> PharmacyProducts { get; set; } = null!;

    public DbSet<Commande> Orders { get; set; } = null!;
    public DbSet<CommandeProduct> OrderProducts { get; set; } = null!;

    public DbSet<Delivery> Deliveries { get; set; } = null!;
    public DbSet<DeliveryOrder> DeliveryOrders { get; set; } = null!;


    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options)
    {
    }
}