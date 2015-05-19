using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SQLClient_Products.Models;

namespace SQLClient_Products.Controllers
{
    public class ProductImagesController : Controller
    {
        // GET: ProductImages
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// displays the list of images associated with an itemId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int id)
        {
            //get the images from the table and pass it to the view to populate the form.
            List<ItemImages> item = ProductRepository.GetProductImagesByItemId(id);
            //returns the itemId in the ViewBag Message propierty
            ViewBag.Message = id;
            return View(item);
        }


        /// <summary>
        /// View to upload the image
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(int id)
        {
            //returns the itemid for the back button
            ViewBag.Message = id;
            //return a blank create form
            return View(new ItemImages());
        }
        
        /// <summary>
        /// Inserts an image to the image table and uploadsit to the folder 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productimage"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int id, ItemImages productimage,HttpPostedFileBase file)
        {
            productimage.ItemId = id;
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
                    productimage.ItemUrl = "~/Content/Uploads/" + filename;
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
                productimage.ItemUrl = "~/Content/Uploads/DefaultImage.jpg";
            }


            //add the new item to the database
            if (ProductRepository.InsertProductImage(productimage.ItemId, productimage.ItemUrl))
            {
                //returns the itemID
                ViewBag.Message = id;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Failed to create new product.";
                ViewBag.Message = id;
                return View(productimage);
            }

           
        }


        /// <summary>
        /// Pre delete procedure
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //get the image from the list and pass it to the view to populate the delete form.
            ItemImages item = ProductRepository.GetProductImageById(id);

            return View(item);
        }


       /// <summary>
       /// Deletes an image from the image table
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteConfirmation(int id)
        {
            //gets the ItemId number to retun it along with the view 
            int itemid = ProductRepository.GetProductImageById(id).ItemId;
            //gets the file url
            string fileName = ProductRepository.GetProductImageById(id).ItemUrl;
            //delete thecontact from the database.

            //if the image exist in the folder
            if (ProductRepository.DeleteItemImage(id))
            {
                if ((System.IO.File.Exists(fileName)))
                {
                    //delete image
                    System.IO.File.Delete(fileName);
                }

                //retuns to the list along with the itemId 
                ViewBag.Message = id;
                return View("Index", new {id= itemid });
            }
            else
            {
                //failed
                ViewBag.Error = "Failed to delete .  Set a breakpoint and figure out why!";
                return RedirectToAction("Index", new {id =  itemid });
            }
        }

    }
}