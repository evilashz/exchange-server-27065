using System;
using System.Web;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001B RID: 27
	public abstract class MapiHttpModule : IHttpModule
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000075AF File Offset: 0x000057AF
		internal HttpApplicationBase Application
		{
			get
			{
				return this.httpApplication;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000075B7 File Offset: 0x000057B7
		public virtual void Init(HttpApplication application)
		{
			this.httpApplication = new HttpApplicationWrapper(application);
			this.InitializeModule(application);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000075CC File Offset: 0x000057CC
		public virtual void Dispose()
		{
		}

		// Token: 0x06000135 RID: 309
		internal abstract void InitializeModule(HttpApplication application);

		// Token: 0x06000136 RID: 310 RVA: 0x000075CE File Offset: 0x000057CE
		internal virtual void OnBeginRequest(HttpContextBase context)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000075D0 File Offset: 0x000057D0
		internal virtual void OnAuthorizeRequest(HttpContextBase context)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000075D2 File Offset: 0x000057D2
		internal virtual void OnPostAuthorizeRequest(HttpContextBase context)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000075D4 File Offset: 0x000057D4
		internal virtual void OnPreRequestHandlerExecute(HttpContextBase context)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000075D6 File Offset: 0x000057D6
		internal virtual void OnPostRequestHandlerExecute(HttpContextBase context)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000075D8 File Offset: 0x000057D8
		internal virtual void OnEndRequest(HttpContextBase context)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000075DA File Offset: 0x000057DA
		internal void SetMockApplicationWrapper(HttpApplicationBase application)
		{
			this.httpApplication = application;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000075E3 File Offset: 0x000057E3
		internal void SendErrorResponse(HttpContextBase context, int httpStatusCode, string httpStatusText)
		{
			this.SendErrorResponse(context, httpStatusCode, 0, httpStatusText);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000075EF File Offset: 0x000057EF
		internal void SendErrorResponse(HttpContextBase context, int httpStatusCode, int httpSubStatusCode, string httpStatusText)
		{
			this.SendErrorResponse(context, httpStatusCode, httpSubStatusCode, httpStatusText, null);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007600 File Offset: 0x00005800
		internal void SendErrorResponse(HttpContextBase context, int httpStatusCode, int httpSubStatusCode, string httpStatusText, Action<HttpResponseBase> customResponseAction)
		{
			HttpResponseBase response = context.Response;
			response.Clear();
			response.StatusCode = httpStatusCode;
			response.SubStatusCode = httpSubStatusCode;
			response.StatusDescription = httpStatusText;
			if (customResponseAction != null)
			{
				customResponseAction(response);
			}
			this.Application.CompleteRequest();
		}

		// Token: 0x0400009E RID: 158
		private HttpApplicationBase httpApplication;
	}
}
