using System;
using System.Web;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000027 RID: 39
	public class RpsHttpDatabaseValidationModule : IHttpModule
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000671E File Offset: 0x0000491E
		void IHttpModule.Init(HttpApplication context)
		{
			context.PostAuthenticateRequest += this.OnPostAuthenticateRequest;
			context.EndRequest += this.OnEndRequest;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006744 File Offset: 0x00004944
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006748 File Offset: 0x00004948
		internal void SendErrorResponse(HttpContextBase context, int httpStatusCode, int httpSubStatusCode, string httpStatusText, Action<HttpResponseBase> customResponseAction, bool closeConnection)
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
			if (closeConnection)
			{
				response.Close();
			}
			context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000068C0 File Offset: 0x00004AC0
		private void OnPostAuthenticateRequest(object sender, EventArgs e)
		{
			HttpContextBase context = new HttpContextWrapper(((HttpApplication)sender).Context);
			WinRMInfo winRMInfoFromHttpHeaders = WinRMInfo.GetWinRMInfoFromHttpHeaders(context.Request.Headers);
			if (winRMInfoFromHttpHeaders != null)
			{
				string sessionUniqueId = winRMInfoFromHttpHeaders.SessionUniqueId;
				int num = 0;
				if (!string.IsNullOrEmpty(sessionUniqueId) && RpsHttpDatabaseValidationModule.ActiveSessionCache.TryGetValue(sessionUniqueId, out num))
				{
					HttpLogger.SafeAppendGenericInfo("CachedSessionId", sessionUniqueId);
					return;
				}
			}
			using (new MonitoredScope("RpsHttpDatabaseValidationModule", "RpsHttpDatabaseValidationModule", HttpModuleHelper.HttpPerfMonitors))
			{
				HttpDatabaseValidationHelper.ValidateHttpDatabaseHeader(context, delegate
				{
				}, delegate(string routingError)
				{
					if (context.Request.Headers[WellKnownHeader.XCafeLastRetryHeaderKey] != null)
					{
						HttpLogger.SafeAppendGenericInfo("IgnoreRoutingError", "Cafe last retry");
						return;
					}
					HttpLogger.SafeAppendGenericError("ServerRoutingError", routingError, false);
					WinRMInfo.SetFailureCategoryInfo(context.Response.Headers, FailureCategory.DatabaseValidation, "ServerRoutingError");
					this.SendErrorResponse(context, 555, 0, routingError, delegate(HttpResponseBase response)
					{
						response.Headers[WellKnownHeader.BEServerRoutingError] = routingError;
					}, false);
				}, delegate
				{
					HttpLogger.SafeAppendGenericError("InvalidMailboxDatabaseGuid", "Cannot Parse MailboxDatabaseGuid From Header", false);
					WinRMInfo.SetFailureCategoryInfo(context.Response.Headers, FailureCategory.DatabaseValidation, "InvalidDatabaseGuid");
					this.SendErrorResponse(context, 400, 0, "Invalid database guid", null, false);
				});
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000069C4 File Offset: 0x00004BC4
		private void OnEndRequest(object sender, EventArgs e)
		{
			HttpContext context = ((HttpApplication)sender).Context;
			HttpResponse response = context.Response;
			WinRMInfo winRMInfoFromHttpHeaders = WinRMInfo.GetWinRMInfoFromHttpHeaders(context.Request.Headers);
			string text = (winRMInfoFromHttpHeaders != null) ? winRMInfoFromHttpHeaders.SessionUniqueId : null;
			if (response == null || winRMInfoFromHttpHeaders == null || text == null)
			{
				return;
			}
			if ("Remove-PSSession".Equals(winRMInfoFromHttpHeaders.Action, StringComparison.OrdinalIgnoreCase))
			{
				RpsHttpDatabaseValidationModule.ActiveSessionCache.Remove(text);
				return;
			}
			if (response.StatusCode == 200 && !RpsHttpDatabaseValidationModule.ActiveSessionCache.Contains(text))
			{
				RpsHttpDatabaseValidationModule.ActiveSessionCache.TryInsertSliding(text, 0, RpsHttpDatabaseValidationModule.SlidingTimeSpanForActiveSession);
			}
		}

		// Token: 0x0400008D RID: 141
		private const int HttpStatusCodeRoutingError = 555;

		// Token: 0x0400008E RID: 142
		private const string GroupNameForMonitor = "RpsHttpDatabaseValidationModule";

		// Token: 0x0400008F RID: 143
		private static readonly ExactTimeoutCache<string, int> ActiveSessionCache = new ExactTimeoutCache<string, int>(null, null, null, 50000, false);

		// Token: 0x04000090 RID: 144
		private static readonly TimeSpan SlidingTimeSpanForActiveSession = TimeSpan.FromMinutes(5.0);
	}
}
