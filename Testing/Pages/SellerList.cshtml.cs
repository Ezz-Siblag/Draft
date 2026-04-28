using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Testing.Pages
{
    public class SellerListModel : PageModel
    {
        public List<Seller> Sellers { get; set; } = new();
        public List<Item> Items { get; set; } = new();

        [BindProperty]
        public int SelectedSellerID { get; set; }

        [BindProperty]
        public int SelectedItemID { get; set; }

        [BindProperty]
        public string reason { get; set; }

        string connString = "server=localhost;port=1108;database=DropZoneDB;user=root;password=;";

        public void OnGet()
        {
            LoadSellers();
        }

        public void OnPostLoadItems()
        {
            LoadSellers();
            LoadItems();
        }

        public void OnPostRemove()
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = "UPDATE Items SET itemStatus = @status WHERE ItemID = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", reason);
                    cmd.Parameters.AddWithValue("@id", SelectedItemID);

                    cmd.ExecuteNonQuery();
                }
            }

            LoadSellers();
            LoadItems();
        }

        public void OnPostUpdate()
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    UPDATE Items 
                    SET BuyerName = 'Updated',
                        Price = Price
                    WHERE ItemID = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", SelectedItemID);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadSellers();
            LoadItems();
        }

        private void LoadSellers()
        {
            Sellers.Clear();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT * FROM Sellers";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sellers.Add(new Seller
                        {
                            SellerID = reader.GetInt32("SellerID"),
                            SellerName = reader.GetString("SellerName")
                        });
                    }
                }
            }
        }

        private void LoadItems()
        {
            Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                string query = @"
                    SELECT * FROM Items 
                    WHERE SellerID = @id 
                    AND itemStatus = 'InDA'";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", SelectedSellerID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Items.Add(new Item
                            {
                                ItemID = reader.GetInt32("ItemID"),
                                BuyerName = reader.GetString("BuyerName"),
                                Price = reader.GetDecimal("price"),
                                ItemStatus = reader.GetString("itemStatus")
                            });
                        }
                    }
                }
            }
        }
    }

    public class Seller
    {
        public int SellerID { get; set; }
        public string SellerName { get; set; }
    }

    public class Item
    {
        public int ItemID { get; set; }
        public string BuyerName { get; set; }
        public decimal Price { get; set; }
        public string ItemStatus { get; set; }
    }
}