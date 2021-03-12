using System;
using System.Security;
using System.Web;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000038 RID: 56
	public class ReportingWebServiceHttpHandler : IHttpHandler
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00007262 File Offset: 0x00005462
		public ReportingWebServiceHttpHandler()
		{
			this.actualHandler = (IHttpHandler)Activator.CreateInstance("System.ServiceModel.Activation, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.ServiceModel.Activation.HttpHandler").Unwrap();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007289 File Offset: 0x00005489
		public bool IsReusable
		{
			get
			{
				return this.actualHandler.IsReusable;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000072D4 File Offset: 0x000054D4
		[SecurityCritical]
		public void ProcessRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.HttpHandlerProcessRequestLatency, delegate
			{
				if (request.IsAuthenticated)
				{
					this.actualHandler.ProcessRequest(context);
					return;
				}
				response.StatusCode = 401;
			});
		}

		// Token: 0x040000AD RID: 173
		private IHttpHandler actualHandler;
	}
}
