using System;
using System.Web;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.DispatchPipe.Base;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD2 RID: 3538
	internal class EwsServiceHttpAsyncHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x06005A66 RID: 23142 RVA: 0x00119D5E File Offset: 0x00117F5E
		public EwsServiceHttpAsyncHandler(HttpContext httpContext, EWSService service, ServiceMethodInfo methodInfo)
		{
			this.httpContext = httpContext;
			this.ewsMethodDispatcher = new EwsMethodDispatcher(service, methodInfo);
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x00119DAC File Offset: 0x00117FAC
		public IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = null;
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				asyncResult = this.ewsMethodDispatcher.InvokeBeginMethod(httpContext, callback, state);
			});
			return asyncResult;
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x00119E2C File Offset: 0x0011802C
		public void EndProcessRequest(IAsyncResult result)
		{
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				this.ewsMethodDispatcher.InvokeEndMethod(result, this.httpContext.Response);
			});
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x00119E5E File Offset: 0x0011805E
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x00119E61 File Offset: 0x00118061
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040031F1 RID: 12785
		private HttpContext httpContext;

		// Token: 0x040031F2 RID: 12786
		private EwsMethodDispatcher ewsMethodDispatcher;
	}
}
