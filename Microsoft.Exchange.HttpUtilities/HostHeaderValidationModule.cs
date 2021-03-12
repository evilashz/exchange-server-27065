using System;
using System.Web;

namespace Microsoft.Exchange.HttpUtilities
{
	// Token: 0x02000004 RID: 4
	public class HostHeaderValidationModule : IHttpModule
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000023B0 File Offset: 0x000005B0
		public void Init(HttpApplication application)
		{
			application.BeginRequest += this.OnBeginRequest;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023C4 File Offset: 0x000005C4
		public void Dispose()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023C8 File Offset: 0x000005C8
		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			try
			{
				Uri url = context.Request.Url;
			}
			catch (UriFormatException)
			{
				HttpResponse response = context.Response;
				response.Clear();
				response.StatusCode = 400;
				response.StatusDescription = "Bad Request - Invalid Host Header.";
				response.AppendToLog("InvalidHostHeader");
				httpApplication.CompleteRequest();
			}
		}

		// Token: 0x0400000C RID: 12
		private const string HttpStatusDescription = "Bad Request - Invalid Host Header.";

		// Token: 0x0400000D RID: 13
		private const string LogDescription = "InvalidHostHeader";
	}
}
