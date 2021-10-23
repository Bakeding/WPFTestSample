﻿using HotelManagementMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelManagementMVVM.Services
{
    class XmlDataService : IDataService
    {
        public List<Models.Dish> GetAllDishes()
        {
            List<Dish> dishList=new List<Dish>();
            string xmlFileName=System.IO.Path.Combine(Environment.CurrentDirectory,@"Data\Data.xml");
            XDocument xDoc=XDocument.Load(xmlFileName);
            var dishes=xDoc.Descendants("Dish");
            foreach (var d in dishes)
	        {
                Dish dish=new Dish();
                dish.Name=d.Element("Name").Value;
                dish.Category=d.Element("Category").Value;
                dish.Comment=d.Element("Comment").Value;
                dish.Scores=double.Parse(d.Element("Scores").Value);
                dishList.Add(dish);
	        }
            return dishList;
        }
    }
}
