/*
Copyright Upendo Ventures, LLC 

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models
{
    public class EasyDNNNewsCategoryList
    {
        [Key]
        public int CategoryID { get; set; }
        public int PortalID { get; set; }
        public string CategoryName { get; set; }
        public int Position { get; set; }
        public int ParentCategory { get; set; }
        public int Level { get; set; }
        public string CategoryURL { get; set; }
        public string CategoryImage { get; set; }
        public string CategoryText { get; set; }
        public string Color { get; set; }
        public string SearchableText { get; set; }
        public string QueryLink { get; set; }
        public string TitleTag { get; set; }
        public string MetaDecription { get; set; }
        public string MetaKeywords { get; set; }
        public string AddRobotsFollowTag { get; set; }
    }

}