using System.ComponentModel.DataAnnotations.Schema;

namespace ShoesProject.Models
{
    [Table("roles", Schema = "public")]
    public class Role
    {
        [Column("id")] public int Id { get; set; }
        [Column("role_name")] public string RoleName { get; set; } = null!;
    }

    [Table("users", Schema = "public")]
    public class User
    {
        [Column("id")] public int Id { get; set; }
        [Column("id_role")] public int IdRole { get; set; }
        [Column("last_name")] public string LastName { get; set; } = null!;
        [Column("first_name")] public string FirstName { get; set; } = null!;
        [Column("middle_name")] public string? MiddleName { get; set; }
        [Column("login")] public string Login { get; set; } = null!;
        [Column("pass")] public string? Pass { get; set; }

        [ForeignKey("IdRole")] public Role Role { get; set; } = null!;

        [NotMapped]
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    }

    [Table("categories", Schema = "public")]
    public class Category
    {
        [Column("id")] public int Id { get; set; }
        [Column("category_name")] public string CategoryName { get; set; } = null!;
    }

    [Table("manufacturers", Schema = "public")]
    public class Manufacturer
    {
        [Column("id")] public int Id { get; set; }
        [Column("manufacturer_name")] public string ManufacturerName { get; set; } = null!;
    }

    [Table("measures", Schema = "public")]
    public class Measure
    {
        [Column("id")] public int Id { get; set; }
        [Column("measure_name")] public string MeasureName { get; set; } = null!;
    }

    [Table("suppliers", Schema = "public")]
    public class Supplier
    {
        [Column("id")] public int Id { get; set; }
        [Column("supplier_name")] public string SupplierName { get; set; } = null!;
    }

    [Table("product_types", Schema = "public")]
    public class ProductType
    {
        [Column("id")] public int Id { get; set; }
        [Column("prod_type")] public string ProdType { get; set; } = null!;
    }

    [Table("products", Schema = "public")]
    public class Product
    {
        [Column("id")] public int Id { get; set; }
        [Column("art")] public string Art { get; set; } = null!;
        [Column("id_type")] public int IdType { get; set; }
        [Column("id_measure")] public int IdMeasure { get; set; }
        [Column("price")] public decimal Price { get; set; }
        [Column("id_supplier")] public int IdSupplier { get; set; }
        [Column("id_manufacturer")] public int IdManufacturer { get; set; }
        [Column("id_category")] public int IdCategory { get; set; }
        [Column("discount")] public int Discount { get; set; }
        [Column("count_in_stock")] public int CountInStock { get; set; }
        [Column("description")] public string? Description { get; set; }
        [Column("photo_url")] public string? PhotoUrl { get; set; }

        [ForeignKey("IdType")] public ProductType ProductType { get; set; } = null!;
        [ForeignKey("IdMeasure")] public Measure Measure { get; set; } = null!;
        [ForeignKey("IdSupplier")] public Supplier Supplier { get; set; } = null!;
        [ForeignKey("IdManufacturer")] public Manufacturer Manufacturer { get; set; } = null!;
        [ForeignKey("IdCategory")] public Category Category { get; set; } = null!;
    }

    [Table("statuses", Schema = "public")]
    public class Status
    {
        [Column("id")] public int Id { get; set; }
        [Column("status_name")] public string StatusName { get; set; } = null!;
    }

    [Table("delivery_points", Schema = "public")]
    public class DeliveryPoint
    {
        [Column("id")] public int Id { get; set; }
        [Column("delivery_address")] public string DeliveryAddress { get; set; } = null!;
    }

    [Table("orders", Schema = "public")]
    public class Order
    {
        [Column("id")] public int Id { get; set; }
        [Column("order_date")] public DateOnly OrderDate { get; set; }
        [Column("delivery_date")] public DateOnly? DeliveryDate { get; set; }
        [Column("id_delivery_point")] public int IdDeliveryPoint { get; set; }
        [Column("id_user")] public int IdUser { get; set; }
        [Column("code")] public int Code { get; set; }
        [Column("id_statuses")] public int IdStatuses { get; set; }

        [ForeignKey("IdDeliveryPoint")] public DeliveryPoint DeliveryPoint { get; set; } = null!;
        [ForeignKey("IdUser")] public User User { get; set; } = null!;
        [ForeignKey("IdStatuses")] public Status Status { get; set; } = null!;
        public List<ProductOrder> ProductOrders { get; set; } = new();
    }

    [Table("products_orders", Schema = "public")]
    public class ProductOrder
    {
        [Column("id")] public int Id { get; set; }
        [Column("id_order")] public int IdOrder { get; set; }
        [Column("id_product")] public int IdProduct { get; set; }
        [Column("quantity")] public int Quantity { get; set; }
        [Column("price_at_order")] public decimal PriceAtOrder { get; set; }

        [ForeignKey("IdOrder")] public Order Order { get; set; } = null!;
        [ForeignKey("IdProduct")] public Product Product { get; set; } = null!;
    }
}