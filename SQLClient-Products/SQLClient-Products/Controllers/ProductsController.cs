using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SQLClient_Products.Models;

namespace SQLClient_Products.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            //return a list of products to the view.  The view will display a table of all products, with links to edit or delete the product.
            return View(); 
        }

       


/// <summary>
/// Returns the list of the items in the product database
/// </summary>
/// <returns></returns>
        public ActionResult List()
        {
            //show all the contacts
            return View(ProductRepository.GetAllItems());
        }




        //The GET action will take no arguments and pass an empty Product object to the View and display the Create form to the user.  
        
        /// <summary>
        /// shows the page to create a new item/product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            //return a blank create form
            return View(new Product());
        }




        //The POST action will accept a Product object as an argument, handle the uploading of an image file, then add it to the database.
        
        /// <summary>
        /// creates a new item and uploads a file to the application and saves the path to the table
        /// </summary>
        /// <param name="item"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// 

        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            string url;
            //if theres a file
            if (Request.Files.Count > 1)
            {
                try
                {
                    //get file name
                    var filename = Path.GetFileName(file.FileName);
                    var fileNamePath = Path.Combine(Server.MapPath("~/Content/Uploads"), filename);
                    file.SaveAs(fileNamePath);

                    ViewBag.Message = "File uploaded correctly";
                    //assigs the relative path to the item url propierty
                    url = "~/Content/Uploads/" + filename;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.ToString();
                }



            }
            else
            {
                //<td><img src="@Url.Content(product.ItemUrl)" alt="Image" width="100" height="100" /></td>
                //if not file was uploaded it adds a default image
                url = "~/Content/Uploads/DefaultImage.jpg";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product item)
        {



            //add the new item to the database
            if (  ProductRepository.InsertItem(item.ItemName, item.ItemDescription, item.ItemPrice))
            {
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Error = "Failed to create new product.";
                return View(item);
            }
        }


        //The GET action will accept an integer Id as an arguement.  The action will retrieve the product from the database, and pass it to the view to display the Edit form to the user, with the field values populated from the database.
        
        
        /// <summary>
        /// gets an item from the product table based on the id pessed on
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get the contact from the database and pass it to the view to populate the form.
            Product item = ProductRepository.GetItemById(id);
            return View(item);
        }



        //The POST action will accept an integer Id and a product object as arguements.  The action will then upload a new file if one was selected, then update the record in the database.
        
        /// <summary>
        /// edits an item from the products table
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, Product item)
        {
            //update the contacti in the database
            if (ProductRepository.UpdateItem(id, item.ItemName, item.ItemDescription, item.ItemPrice, item.ItemUrl))
            {
                return RedirectToAction("List");
            }
            else
            {
                //failed
                ViewBag.Error = "Failed to update product.  Set a breakpoint and figure out why!";
                return View(item);
            }
        }

        //The GET action will accept an integer Id as an arguement and delete the product from the database.  After the deletion is complete, redirect the user to the Index (listing) action.

       /// <summary>
       /// deletes the item by id from products
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            //delete thecontact from the database.
            if (ProductRepository.DeleteItem(id))
            {
                return RedirectToAction("List"); //go back to the list
            }
            else
            {
                //failed
                ViewBag.Error = "Failed to delete .  Set a breakpoint and figure out why!";
                return RedirectToAction("List");
            }
        }


        //The GET action will accept an integer Id as an arguement and retrieve the product from the database.  The product object will be passed to the view to display to the user a confirmation screen with a button to confirm that links to the DeleteConfirmation action.

        /// <summary>
        /// shows the item before deleting it based on the id of the item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //get the contact from the database and pass it to the view to populate the form.
            Product item = ProductRepository.GetItemById(id);
            return View(item);
        }


       

       


    }
}