using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;


namespace SQLClient_Products.Models
{
    public class ProductRepository
    {

        public static bool InsertProductImage(int id, string url)
        {
            //TODO: INSERT contact in database
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try //tries to execute the query
                {
                    //sql insert call
                    SqlCommand command = new SqlCommand("insert into ProductImages (itemId, itemUrl) values (@itemId, @itemUrl)", con);
                    command.Parameters.Add(new SqlParameter("itemId", id));
                    command.Parameters.Add(new SqlParameter("itemUrl", url));
                 

                    //executes the query
                    command.ExecuteNonQuery();
                    return true;
                }
                catch //if theres a problem executing query
                {
                    return false;
                }
            }
        }





        //TODO: Fill in product data access methods....
        // InsertProduct - inserts a product into the database
        public static bool InsertItem(string itemName, string itemDescription, double itemPrice)
        {
            //TODO: INSERT contact in database
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try //tries to execute the query
                {
                    //sql insert call
                    SqlCommand command = new SqlCommand("insert into Products (itemName,itemDescription,itemPrice) values (@itemName,@itemDescription,@itemPrice)", con);
                    command.Parameters.Add(new SqlParameter("itemName", itemName));
                    command.Parameters.Add(new SqlParameter("itemDescription", itemDescription));
                    command.Parameters.Add(new SqlParameter("itemPrice", itemPrice));
                   
                    //executes the query
                    command.ExecuteNonQuery();
                    return true;
                }
                catch //if theres a problem executing query
                {
                    return false;
                }
            }

        }



        // DeleteProduct - deletes a product in the database 

        public static bool DeleteItemImage(int id)
        {
            //TODO: DELETE contact in the database by ID


            using (SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con1.Open(); //opens the conection to the database
                try
                {
                    //sql call to delete the row
                    SqlCommand command = new SqlCommand("Delete from ProductImages where imageId=@id", con1);
                    command.Parameters.Add(new SqlParameter("id", id));

                    //executes the query
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }


        }


        // DeleteProduct - deletes a product in the database 

        public static bool DeleteItem(int id)
        {
            //TODO: DELETE contact in the database by ID


            using (SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con1.Open(); //opens the conection to the database
                try
                {
                    //sql call to delete the row
                    SqlCommand command = new SqlCommand("Delete from Products where itemId=@id", con1);
                    command.Parameters.Add(new SqlParameter("id", id));

                    //executes the query
                    command.ExecuteNonQuery();
                    
                }
                catch
                {
                   
                }
            }


            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try
                {
                    //sql call to delete the row
                    SqlCommand command = new SqlCommand("Delete from Products where itemId=@id", con);
                    command.Parameters.Add(new SqlParameter("id", id));

                    //executes the query
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }


        // UpdateProduct - updates a product in the database

        public static bool UpdateItem(int id, string itemName, string itemDescription, double itemPrice, string itemUrl)
        {
            //TODO: UPDATE contact in the database by Id
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try
                {

                    //sql calln to update the table row
                    SqlCommand command = new SqlCommand("UPDATE Products set itemName=@itemName, itemDescription=@itemdescription, itemPrice=@itemPrice, itemImageUrl=@itemImageUrl, dateModified=@dateModified where itemId= @id", con);
                    command.Parameters.Add(new SqlParameter("id", id));
                    command.Parameters.Add(new SqlParameter("itemName", itemName));
                    command.Parameters.Add(new SqlParameter("itemDescription", itemDescription));
                    command.Parameters.Add(new SqlParameter("itemPrice", itemPrice));
                    command.Parameters.Add(new SqlParameter("itemImageUrl", itemUrl));
                    command.Parameters.Add(new SqlParameter("dateModified", DateTime.Now));//adds the current time to refect the last time it was modified
                    //executes the query
                    command.ExecuteNonQuery();
                    return true;
                }
                catch //if it catches an error
                {
                    return false;
                }
            }

        }
        // GetProductById - gets a single product from the database by it's Id

        public static Product GetItemById(int id)
        {
            //TODO: SELECT a contact from the database by Id

            Product item = new Product();

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {


                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try
                {

                    //sql call to make the selection
                    SqlCommand command = new SqlCommand("Select * from Products where ItemId=@id", con);
                    command.Parameters.Add(new SqlParameter("id", id));

                    //executes the query and returns the row to a reader
                    SqlDataReader reader = command.ExecuteReader();
                    //while there are rows in the reader
                    while (reader.Read())
                    {
                        //get the row values
                        int itemId = reader.GetInt32(0);    // product id
                        string name = reader.GetString(1);  // Name string
                        string description = reader.GetString(2); // description
                        double price = (double)reader.GetDecimal(3); // price
                      

                        //assign those values to the card object
                        item.ItemId = itemId;
                        item.ItemName = name;
                        item.ItemDescription = description;
                        item.ItemPrice = price;
                    
                    }



                }
                catch //if it catches an error
                {
                    return item;
                }
            }

            return item; //returns a row of type contact 

        }



        public static ItemImages GetProductImageById(int id)
        {
            //TODO: SELECT a contact from the database by Id

            ItemImages itemImages = new ItemImages();

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {


                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try
                {

                    //sql call to make the selection
                    SqlCommand command = new SqlCommand("Select * from ProductImages where ImageId=@id", con);
                    command.Parameters.Add(new SqlParameter("id", id));

                    //executes the query and returns the row to a reader
                    SqlDataReader reader = command.ExecuteReader();
                    //while there are rows in the reader
                    while (reader.Read())
                    {
                        //get the row values
                        int itemId = reader.GetInt32(0);    // product id
                        int productId = reader.GetInt32(1);  // Name string
                        string Url = reader.GetString(2); // description
             


                        //assign those values to the card object
                        itemImages.ImageId = itemId;
                        itemImages.ItemId = productId;
                        itemImages.ItemUrl = Url;

                    }



                }
                catch //if it catches an error
                {
                    return itemImages;
                }
            }

            return itemImages; //returns a row of type contact 

        }


        // GetAllProducts - returns all products from the database

        public static List<Product> GetAllItems()
        {
            //TODO: SELECT all contacts from the database

            List<Product> listOfItems = new List<Product>();

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try
                {
                    //sql call
                    SqlCommand command = new SqlCommand("Select * from Products", con);

                    //executes the query and saves it to a sql reader
                    SqlDataReader reader = command.ExecuteReader();
                    //while the reader has rows
                    while (reader.Read())
                    {
                        //gets the values from the row
                        int itemId = reader.GetInt32(0);    // product id
                        string name = reader.GetString(1);  // Name string
                        string description = reader.GetString(2); // descriptionC:\Users\Technologist\Documents\GitHub\SQLClient-Products\SQLClient-Products\SQLClient-Products\Views\ProductImages\
                        double price = (double) reader.GetDecimal(3); // price
                       

                        //adds the values into the list as a contact object
                        listOfItems.Add(new Product() { ItemId= itemId, ItemName= name, ItemDescription= description, ItemPrice= price});
                    }



                }
                catch//if it catches an error
                {
                    return listOfItems;
                }
            }
            return listOfItems; //return the contacts
        }



        public static List<ItemImages> GetProductImagesByItemId(int id)
        {
            //TODO: SELECT all contacts from the database

            List<ItemImages> listOfItems = new List<ItemImages>();

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                //declares a variable for the scope of the code block
                con.Open(); //opens the conection to the database
                try
                {
                    //sql call
                    SqlCommand command = new SqlCommand("Select * from ProductImages where itemid=@itemid", con);
                    command.Parameters.Add(new SqlParameter("itemId", id));
                    //executes the query and saves it to a sql reader
                    SqlDataReader reader = command.ExecuteReader();
                    //while the reader has rows
                    while (reader.Read())
                    {
                        //gets the values from the row
                        int imageId = reader.GetInt32(0);    // product id
                        int itemId = reader.GetInt32(1);  // Name string
                        string Url = reader.GetString(2); // descriptionC:\Users\Technologist\Documents\GitHub\SQLClient-Products\SQLClient-Products\SQLClient-Products\Views\ProductImages\
                       


                        //adds the values into the list as a contact object
                        listOfItems.Add(new ItemImages { ImageId= imageId, ItemId= itemId, ItemUrl=Url });
                    }



                }
                catch//if it catches an error
                {
                    return listOfItems;
                }
            }
            return listOfItems; //return the contacts
        }

    }
}