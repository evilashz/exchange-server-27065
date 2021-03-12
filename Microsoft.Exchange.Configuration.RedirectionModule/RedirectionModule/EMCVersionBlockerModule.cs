using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.RedirectionModule.LocStrings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x02000006 RID: 6
	public class EMCVersionBlockerModule : IHttpModule
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000030F0 File Offset: 0x000012F0
		static EMCVersionBlockerModule()
		{
			EMCVersionBlockerModule.perfCounter.PID.RawValue = (long)Process.GetCurrentProcess().Id;
			Globals.InitializeMultiPerfCounterInstance("RemotePS");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003150 File Offset: 0x00001350
		void IHttpModule.Init(HttpApplication application)
		{
			application.PostAuthenticateRequest += EMCVersionBlockerModule.OnPostAuthenticateRequestHandler;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000315D File Offset: 0x0000135D
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003160 File Offset: 0x00001360
		private static void OnPostAuthenticateRequest(object source, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpRequest request = httpApplication.Context.Request;
			if (!request.IsAuthenticated)
			{
				Logger.LogWarning(EMCVersionBlockerModule.traceSrc, "[EMCVersionBlockerModule] OnPostAuthenticateRequest was called on a not Authenticated Request!");
				return;
			}
			if (!EMCVersionBlockerModule.ShouldBlockConnectionRequest(request))
			{
				Logger.LogVerbose(EMCVersionBlockerModule.traceSrc, "[EMCVersionBlockerModule] ShouldProcessClientRedirection == false, for user '{0}', Uri '{1}'.", new object[]
				{
					httpApplication.Context.User,
					request.Url
				});
				return;
			}
			EMCVersionBlockerModule.ReportError(httpApplication.Context.Response, HttpStatusCode.BadRequest, 100, Strings.ExchangeClientVersionBlocked(ExchangeSetupContext.InstalledVersion.ToString()));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000031FC File Offset: 0x000013FC
		private static bool ShouldBlockConnectionRequest(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			NameValueCollection urlProperties = RedirectionHelper.GetUrlProperties(request.Url);
			return "EMC".Equals(urlProperties["clientApplication"], StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(urlProperties["ExchClientVer"]);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003250 File Offset: 0x00001450
		private static void ReportError(HttpResponse response, HttpStatusCode status, int subStatus, string errorMessage)
		{
			Logger.LogVerbose(EMCVersionBlockerModule.traceSrc, "Reporting HTTP error: Status - {0}, SubStatus - {1}, Message - {2}", new object[]
			{
				status,
				subStatus,
				errorMessage
			});
			response.Clear();
			response.StatusCode = (int)status;
			response.SubStatusCode = subStatus;
			if (EMCVersionBlockerModule.IsExchangeCustomError(response.StatusCode, response.SubStatusCode))
			{
				response.TrySkipIisCustomErrors = true;
			}
			if (!string.IsNullOrEmpty(errorMessage))
			{
				Logger.LogVerbose(EMCVersionBlockerModule.traceSrc, "Set the content type to {0} so as to trigger the WSMan plug-in for further handling", new object[]
				{
					"application/soap+xml;charset=UTF-8"
				});
				response.ContentType = "application/soap+xml;charset=UTF-8";
				response.Write(errorMessage);
			}
			response.End();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000032F7 File Offset: 0x000014F7
		private static bool IsExchangeCustomError(int status, int subStatus)
		{
			return (status / 100 == 4 && subStatus / 100 == 1) || (status / 100 == 5 && subStatus / 100 == 2);
		}

		// Token: 0x04000015 RID: 21
		internal const string ExchangeClientVersionUriProperty = "ExchClientVer";

		// Token: 0x04000016 RID: 22
		private const string WSManContentType = "application/soap+xml;charset=UTF-8";

		// Token: 0x04000017 RID: 23
		private const int ExchangeVersionBlockedSubStatusCode = 100;

		// Token: 0x04000018 RID: 24
		private const string W3wpPerfCounterInstanceName = "RemotePS";

		// Token: 0x04000019 RID: 25
		private const string ClientVersionItemKeyName = "ExchangeVersionRedirection.ClientVersionItemKeyName";

		// Token: 0x0400001A RID: 26
		private static readonly EventHandler OnPostAuthenticateRequestHandler = new EventHandler(EMCVersionBlockerModule.OnPostAuthenticateRequest);

		// Token: 0x0400001B RID: 27
		private static readonly TraceSource traceSrc = new TraceSource("EMCVersionBlockerModule");

		// Token: 0x0400001C RID: 28
		private static RemotePowershellPerformanceCountersInstance perfCounter = RemotePowershellPerformanceCounters.GetInstance("RemotePS");
	}
}
