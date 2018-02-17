using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rtsp_client
{
    public partial class Default : System.Web.UI.Page
    {
        public string Width { get; set; }

        public string Height { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Width = GetUnit(Request.QueryString["width"]);
            Height = GetUnit(Request.QueryString["height"]);
        }

        public string GetUnit(string val)
        {
            if (val == null)
                return val;

            int num;
            if (int.TryParse(val, out num))
                return num + "px";

            return val;
        }
    }
}