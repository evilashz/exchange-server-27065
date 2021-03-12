using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002A3 RID: 675
	public class HttpHandlerICal : IHttpHandler
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x000966EC File Offset: 0x000948EC
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000966F0 File Offset: 0x000948F0
		public void ProcessRequest(HttpContext context)
		{
			if (Globals.OwaVDirType == OWAVDirType.OWA)
			{
				ExTraceGlobals.CoreCallTracer.TraceError((long)this.GetHashCode(), "HttpHandlerICal.ProcessRequest: Returns 400 since OWA vdir doesn't support.");
				Utilities.EndResponse(context, HttpStatusCode.BadRequest);
				return;
			}
			PublishingUrl publishingUrl = (PublishingUrl)context.Items["AnonymousUserContextPublishedUrl"];
			if (publishingUrl == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceError((long)this.GetHashCode(), "HttpHandlerICal.ProcessRequest: Missing publishing url,");
				Utilities.EndResponse(context, HttpStatusCode.BadRequest);
				return;
			}
			if (publishingUrl.DataType != SharingDataType.Calendar && publishingUrl.DataType != SharingDataType.ReachCalendar)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<SharingDataType>((long)this.GetHashCode(), "HttpHandlerICal.ProcessRequest: Returns 400 due to invalid data type '{0}'.", publishingUrl.DataType);
				Utilities.EndResponse(context, HttpStatusCode.BadRequest);
				return;
			}
			if (!Utilities.IsGetRequest(context.Request))
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>((long)this.GetHashCode(), "HttpHandlerICal.ProcessRequest: Stop process if it is not GET request. HttpMethod = {0}.", context.Request.HttpMethod);
				return;
			}
			try
			{
				using (PublishedCalendar publishedCalendar = (PublishedCalendar)PublishedFolder.Create(publishingUrl))
				{
					publishedCalendar.WriteInternetCalendar(context.Response.OutputStream, "utf-8");
				}
				context.Response.ContentType = "text/calendar; charset=utf-8";
			}
			catch (FolderNotPublishedException arg)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<FolderNotPublishedException>((long)this.GetHashCode(), "HttpHandlerICal.ProcessRequest: Returns 404 due to folder is not published. Exception = {0}.", arg);
				Utilities.EndResponse(context, HttpStatusCode.NotFound);
			}
			catch (PublishedFolderAccessDeniedException arg2)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<PublishedFolderAccessDeniedException>((long)this.GetHashCode(), "HttpHandlerICal.ProcessRequest: Returns 404 due to access is denied. Exception = {0}.", arg2);
				Utilities.EndResponse(context, HttpStatusCode.NotFound);
			}
		}

		// Token: 0x040012D3 RID: 4819
		private const string Charset = "utf-8";
	}
}
