using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using ProductManagementApp.Models;
using System.Data;

namespace ProductManagementApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _connectionString;

        // Constructor: Injects configuration to access the connection string
        public ProductController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        /// <summary>
        /// Displays the list of all products.
        /// Fetches data from SQL Server using Dapper.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Use Dapper's QueryAsync to retrieve the list
                var sql = "SELECT * FROM Products ORDER BY CreatedDate DESC";
                var products = await connection.QueryAsync<Product>(sql);
                return View(products);
            }
        }

        /// <summary>
        /// Renders the Create Product view.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the POST request to create a new product.
        /// Saves data to the database using Dapper.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                // Dapper: ExecuteAsync is used for Insert operations
                var sql = "INSERT INTO Products (Name, Description, Price, CreatedDate) VALUES (@Name, @Description, @Price, GETDATE())";
                await connection.ExecuteAsync(sql, product);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Renders the Edit view with existing product details.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Products WHERE Id = @Id";
                var product = await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });

                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        /// <summary>
        /// Handles the POST request to update an existing product.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Dapper: ExecuteAsync is used for Update operations
                var sql = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";
                await connection.ExecuteAsync(sql, product);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a product by Id.
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // Dapper: ExecuteAsync is used for Delete operations
                var sql = "DELETE FROM Products WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
            return RedirectToAction("Index");
        }
    }
}