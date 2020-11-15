﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ch24ShoppingCartMVC.Models;
using Ch24ShoppingCartMVC.Models.DataAccess;

namespace Ch24ShoppingCartMVC.Models {
    public class OrderModel
    {
        private List<ProductViewModel> products;
        //Implement GetAllProductsFromDataStore
        public List<Product> GetAllProductsFromDataStore()
        {    
            using (HalloweenEntities1 data = new HalloweenEntities1())
            {  //get all the products from the Collection Products order by name using HalloweenEntities
                return data.Products.OrderBy(s=>s.Name).ToList() ;
            }
        }
        //Implement the method ConvertToViewModel
        private ProductViewModel ConvertToViewModel(Product product)
        {
            ProductViewModel model = new ProductViewModel();
            model.ProductID = product.ProductID;
            model.Name = product.Name;
            model.ShortDescription = product.ShortDescription;
            model.LongDescription = product.LongDescription;
            model.UnitPrice = product.UnitPrice;
            model.ImageFile = product.ImageFile;
            model.Quantity = product.OnHand;
            //implement other required properties
            
            return model;
        }
        //Implement the method GetProductList
        public List<ProductViewModel> GetProductsList() {
            if (this.products == null)
                //Call the method GetAllProducts
                this.products = this.GetAllProducts();
            //Return the products
            return this.products;
        }
        public List<ProductViewModel> GetAllProducts()
        {
            List<ProductViewModel> productmodels = new List<ProductViewModel>();
            // Call the GetAllProductsFromDataStore()
            List<Product> products = this.GetAllProductsFromDataStore();
            foreach (Product p in products)
            {  //Call the method ConvertToViewModel to convert p and pass the method ConvertToViewModel to the method add of the productmodels
                productmodels.Add(this.ConvertToViewModel(p));
            }
            return productmodels;
        }
        
        public Product GetProductByIdFromDataStore(string id)
        {
            using (HalloweenEntities1 data = new HalloweenEntities1())
            {  //Get a product from Products of data where ProductID is matched with id parameter
                return data.Products.Where(s=> s.ProductID==id).FirstOrDefault();
                
            }
          
        }
        public OrderViewModel GetOrderInfo(string id)
        {
            OrderViewModel order = new OrderViewModel();
            //Call the method GetSelectedProduct and assign the return value to SelectedProduct property
            order.SelectedProduct = this.GetSelectedProduct(id);
            return order;
        }  
        public ProductViewModel GetSelectedProduct(string id)
        {
            if (this.products == null)
                //call the method ConvertToViewModel and pass the method GetProductByIdFromDataStore(id)

                return this.ConvertToViewModel(this.GetProductByIdFromDataStore(id));
            else
                //Get the product from the products where ProductID is matched with id (Using Lambda expression)
                return this.products.Where(s => s.ProductID == id).FirstOrDefault();
        }
              
        
        
        
        
        
    }
}