using System;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B9 RID: 185
	public class PendingGetRequestHandler : IHttpAsyncHandler, IHttpHandler
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00013C77 File Offset: 0x00011E77
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00013C7C File Offset: 0x00011E7C
		public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			AsyncResult asyncResult = new AsyncResult(cb, extraData);
			string subscriptionId = PendingGetRequestHandler.GetSubscriptionId(context);
			IPendingGetConnection pendingGetConnection = PendingGetConnectionCache.Instance.AddOrGetConnection(subscriptionId);
			ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<string>((long)this.GetHashCode(), "[BeginProcessRequest] PendingGetRequestHandler is requested from the client (subscription id = '{0}')", subscriptionId);
			long timeoutInMilliseconds = PendingGetRequestHandler.GetTimeoutInMilliseconds(context);
			int unseenEmailNotificationId = PendingGetRequestHandler.GetUnseenEmailNotificationId(context);
			PendingGetContext pendingGetContext = new PendingGetContext
			{
				AsyncResult = asyncResult,
				Response = new PendingGetResponse(context.Response)
			};
			pendingGetConnection.SubscribeToUnseenEmailNotification(pendingGetContext, timeoutInMilliseconds, unseenEmailNotificationId);
			return asyncResult;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00013CFB File Offset: 0x00011EFB
		public void EndProcessRequest(IAsyncResult result)
		{
			ExTraceGlobals.PendingGetPublisherTracer.TraceDebug((long)this.GetHashCode(), "[EndProcessRequest] PendingGetRequestHandler finishes request");
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00013D13 File Offset: 0x00011F13
		public void ProcessRequest(HttpContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00013D1C File Offset: 0x00011F1C
		private static int GetUnseenEmailNotificationId(HttpContext context)
		{
			string parameter = PendingGetRequestHandler.GetParameter(context, "US");
			int result;
			if (int.TryParse(parameter, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00013D44 File Offset: 0x00011F44
		private static long GetTimeoutInMilliseconds(HttpContext context)
		{
			string parameter = PendingGetRequestHandler.GetParameter(context, "T");
			long result;
			if (long.TryParse(parameter, out result))
			{
				return result;
			}
			return 0L;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00013D6B File Offset: 0x00011F6B
		private static string GetSubscriptionId(HttpContext context)
		{
			return PendingGetRequestHandler.GetParameter(context, "S");
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00013D78 File Offset: 0x00011F78
		private static string GetParameter(HttpContext context, string parameterName)
		{
			return context.Request.QueryString[parameterName];
		}

		// Token: 0x04000312 RID: 786
		private const string TimeoutParameter = "T";

		// Token: 0x04000313 RID: 787
		private const string UnseenEmailNotificationIdParameter = "US";

		// Token: 0x04000314 RID: 788
		private const string SubscriptionIdParameter = "S";
	}
}
