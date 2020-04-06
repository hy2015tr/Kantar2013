using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KrmKantar2013.WR
{

    //===================================================================================================================//

    public partial class ZWS_KANTAR_FUNCTIONS_V2 : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            // Get Request
            System.Net.HttpWebRequest p_Request = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            
            // Set Values
            p_Request.KeepAlive = false;
            p_Request.ProtocolVersion = System.Net.HttpVersion.Version10;
            
            // Return
            return p_Request;
        }
    }

    //===================================================================================================================//

}
