using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WP_search
{
    public partial class _Default : Page
    {
        protected IList<RootObject> postsList = new List<RootObject>();
        protected IList<RootObject> matchedPagesList = new List<RootObject>();
        protected IList<RootObject> pagesList = new List<RootObject>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //Clear repeater controls.
            postrptResults.DataSource = null;
            postrptResults.DataBind();
            PageRepeater.DataSource = null;
            PageRepeater.DataBind();
            string keyword = SearchWordTextBox.Text;

            //search posts
            postsList = searchPosts(keyword);
            //search pages
            pagesList = searchPages(keyword);
            FilterPages(pagesList);
           
            //Bind the search results to repeater controls.
            this.BindRepeater();

            PostCountLabel1.Text = postsList.Count.ToString();
            MatchedPagesLabel1.Text = matchedPagesList.Count.ToString();
            PagesCountLabel1.Text = pagesList.Count.ToString();
        }

        private void BindRepeater()
        {
            BindPostResults();
            //BindProductResults();
            BindPageResults();

            //categoryListRepeater.DataSource = categoryListDic;
            //categoryListRepeater.DataBind();
            //categoryListRepeater.Visible = true;
        }

        private void BindPostResults()
        {
            if (postsList != null && postsList.Count > 0)
            {
                string PostCount = String.Format(("Blogs({0})"), postsList.Count);
                postrptResults.DataSource = postsList;
                postrptResults.DataBind();
                postrptResults.Visible = true;
            }
            else
            {
                postrptResults.Visible = false;
            }
        }

        private void BindPageResults()
        {
            if (matchedPagesList != null && matchedPagesList.Count > 0)
            {
                string pageCount = String.Format(("Blogs({0})"), matchedPagesList.Count);
                PageRepeater.DataSource = matchedPagesList;
                PageRepeater.DataBind();
                PageRepeater.Visible = true;
            }
            else
            {
                PageRepeater.Visible = false;
            }
        }

        private void FilterPages(IList<RootObject> pageList)
        {
            foreach (var item in pageList)
            {
                Match titleMatch = Regex.Match(item.title.rendered, "test", RegexOptions.IgnoreCase);
                Match titleMatch2 = Regex.Match(item.title.rendered, "category", RegexOptions.IgnoreCase);
                if (!titleMatch.Success)
                {
                    if (titleMatch2.Success)
                    {
                        if (Regex.Match(item.title.rendered, "amplifiers", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/amplifiers/amplifiers-products.aspx";                            
                        }
                        else if(Regex.Match(item.title.rendered, "baluns", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/baluns/baluns-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "bias", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/bias-tees/bias-tees-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "couplers", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/couplers/couplers-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "equalizers", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/equalizers/equalizers-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "filters", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/filters-diplexers/filters-diplexers-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "high", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/high-speed-data/high-speed-data-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "hybrids", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/hybrids/hybrids-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "mixers", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/mixers/mixers-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "multipliers", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/multipliers/multipliers-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "dividers", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/power-dividers/power-dividers-products.aspx";
                        }
                        else if (Regex.Match(item.title.rendered, "accessories", RegexOptions.IgnoreCase).Success)
                        {
                            item.link = "http://dev.markimicrowave.com/accessories/accessories-products.aspx";
                        }
                        item.title.rendered = item.title.rendered.Replace("Category", "");
                    }
                    matchedPagesList.Add(item);
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private static IList<RootObject> searchPosts(string keyword)
        {
            IList<RootObject> postsList = new List<RootObject>();
            try
            {
                // string host = HttpContext.Current.Request.Url.Host;
                // string protocol = HttpContext.Current.Request.Url.Scheme + "://";
                //string uri = FormatUrl(string.Format("/blog/?wpapi=search&keyword={0}&content=0&comment=0&type=post", keyword));
                string uri = string.Format("http://dev.markimicrowave.com/blog/wp-json/wp/v2/posts?search={0}&per_page=100", keyword);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "application/json";

                using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
                {
                    //using (var reader = new StreamReader(ExecuteRest(uri)))
                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = reader.ReadToEnd();
                        postsList = JsonConvert.DeserializeObject<List<RootObject>>(objText.ToString());
                    }

                }
                return postsList;
            }
            catch (Exception ex)
            {
                //Logger.Error("Failed to search posts for : " + keyword + "--------" + ex.Message, ex);
                return postsList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private static IList<RootObject> searchPages(string keyword)
        {
            IList<RootObject> pagesList = new List<RootObject>();

            try
            {
                // string host = HttpContext.Current.Request.Url.Host;
                // string protocol = HttpContext.Current.Request.Url.Scheme + "://";
                //string uri = FormatUrl(string.Format("/blog/?wpapi=search&keyword={0}&content=0&comment=0&type=post", keyword));
                string uri = string.Format("http://dev.markimicrowave.com/blog/wp-json/wp/v2/pages?search={0}&per_page=100", keyword);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "application/json";

                using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
                {
                    //using (var reader = new StreamReader(ExecuteRest(uri)))
                    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        var objText = reader.ReadToEnd();
                        pagesList = JsonConvert.DeserializeObject<List<RootObject>>(objText.ToString());
                    }

                }
                return pagesList;
            }
            catch (Exception ex)
            {
                //Logger.Error("Failed to search posts for : " + keyword + "--------" + ex.Message, ex);
                return pagesList;
            }
        }

    }
}