using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x0200000E RID: 14
	public abstract class RpcHttpModule : IHttpModule
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000260E File Offset: 0x0000080E
		internal HttpApplicationBase Application
		{
			get
			{
				return this.httpApplication;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002616 File Offset: 0x00000816
		public virtual void Init(HttpApplication application)
		{
			this.httpApplication = new HttpApplicationWrapper(application);
			this.InitializeModule(application);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000262B File Offset: 0x0000082B
		public virtual void Dispose()
		{
		}

		// Token: 0x0600002C RID: 44
		internal abstract void InitializeModule(HttpApplication application);

		// Token: 0x0600002D RID: 45 RVA: 0x0000262D File Offset: 0x0000082D
		internal virtual void OnBeginRequest(HttpContextBase context)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000262F File Offset: 0x0000082F
		internal virtual void OnAuthorizeRequest(HttpContextBase context)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002631 File Offset: 0x00000831
		internal virtual void OnPostAuthorizeRequest(HttpContextBase context)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002633 File Offset: 0x00000833
		internal virtual void OnEndRequest(HttpContextBase context)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002635 File Offset: 0x00000835
		internal void SetMockApplicationWrapper(HttpApplicationBase application)
		{
			this.httpApplication = application;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000263E File Offset: 0x0000083E
		internal void SendErrorResponse(HttpContextBase context, int httpStatusCode, string httpStatusText)
		{
			this.SendErrorResponse(context, httpStatusCode, 0, httpStatusText);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000264A File Offset: 0x0000084A
		internal void SendErrorResponse(HttpContextBase context, int httpStatusCode, int httpSubStatusCode, string httpStatusText)
		{
			this.SendErrorResponse(context, httpStatusCode, httpSubStatusCode, httpStatusText, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002658 File Offset: 0x00000858
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

		// Token: 0x06000035 RID: 53 RVA: 0x000026A0 File Offset: 0x000008A0
		protected string GetOutlookSessionId(HttpContextBase context)
		{
			string text = context.Items["OutlookSession"] as string;
			if (string.IsNullOrEmpty(text))
			{
				HttpCookie httpCookie = context.Request.Cookies["OutlookSession"];
				if (httpCookie != null)
				{
					text = httpCookie.Value.Trim();
					context.Items["OutlookSession"] = text;
				}
			}
			return text;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002704 File Offset: 0x00000904
		protected Guid GetRequestId(HttpContextBase context)
		{
			if (context.Items["LogRequestId"] != null)
			{
				return (Guid)context.Items["LogRequestId"];
			}
			ActivityScope activityScope = null;
			Guid activityId;
			try
			{
				IActivityScope activityScope2 = ActivityContext.GetCurrentActivityScope();
				if (activityScope2 == null)
				{
					activityScope = ActivityContext.Start(null);
					activityScope2 = activityScope;
				}
				activityScope2.UpdateFromMessage(context.Request);
				context.Items["LogRequestId"] = activityScope2.ActivityId;
				activityId = activityScope2.ActivityId;
			}
			finally
			{
				if (activityScope != null)
				{
					activityScope.Dispose();
				}
			}
			return activityId;
		}

		// Token: 0x0400000E RID: 14
		public const int HttpStatusCodeRoutingError = 555;

		// Token: 0x0400000F RID: 15
		public const string SessionIdPrefix = "SessionId=";

		// Token: 0x04000010 RID: 16
		internal const string OutlookSessionCookieName = "OutlookSession";

		// Token: 0x04000011 RID: 17
		internal const string RequestIdHttpContextKeyName = "LogRequestId";

		// Token: 0x04000012 RID: 18
		private HttpApplicationBase httpApplication;
	}
}
