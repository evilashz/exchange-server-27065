using System;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000060 RID: 96
	public class AutodiscoverThrottlingModule : IHttpModule
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00012484 File Offset: 0x00010684
		public void Dispose()
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00012486 File Offset: 0x00010686
		public void Init(HttpApplication context)
		{
			context.PostAuthenticateRequest += this.Application_PostAuthenticate;
			AutodiscoverThrottlingModule.InitializeCPUSlowdown();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000124A0 File Offset: 0x000106A0
		private static void InitializeCPUSlowdown()
		{
			int appSettingAsInt = AutodiscoverThrottlingModule.GetAppSettingAsInt("WSSecuritySlowdownCpuThreshold", 25);
			AutodiscoverThrottlingModule.anonymousSlowdownCpuThreshold = (uint)(((long)appSettingAsInt <= 10L) ? 25 : appSettingAsInt);
			if (AutodiscoverThrottlingModule.anonymousSlowdownCpuThreshold > CPUBasedSleeper.ProcessCpuSlowDownThreshold)
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug(0L, "[AutodiscoverThrottlingModule::InitializeCPUSlowdown] The Sharing CPU threshold is higher than the default threshold");
				AutodiscoverThrottlingModule.anonymousSlowdownCpuThreshold = CPUBasedSleeper.ProcessCpuSlowDownThreshold;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000124F4 File Offset: 0x000106F4
		private void Application_PostAuthenticate(object source, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			Uri url = context.Request.Url;
			if (!context.Request.IsAuthenticated && !ExternalAuthentication.GetCurrent().Enabled && (Common.IsWsSecurityAddress(url) || Common.IsWsSecuritySymmetricKeyAddress(url) || Common.IsWsSecurityX509CertAddress(url)))
			{
				context.Response.Close();
				httpApplication.CompleteRequest();
				return;
			}
			uint processCpuSlowDownThreshold = CPUBasedSleeper.ProcessCpuSlowDownThreshold;
			if (!context.Request.IsAuthenticated)
			{
				processCpuSlowDownThreshold = AutodiscoverThrottlingModule.anonymousSlowdownCpuThreshold;
			}
			int arg;
			float arg2;
			if (CPUBasedSleeper.SleepIfNecessary(processCpuSlowDownThreshold, out arg, out arg2))
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug<int, float>((long)this.GetHashCode(), "[AutodiscoverThrottlingModule::Application_PostAuthenticate] Slept request for {0} msec due to current process CPU percent of {1}%", arg, arg2);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000125A0 File Offset: 0x000107A0
		internal static int GetAppSettingAsInt(string key, int defaultValue)
		{
			string s = ConfigurationManager.AppSettings[key];
			int result;
			if (int.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x040002B1 RID: 689
		private const string AppSettingsAnonymousSlowdownCpuThreshold = "WSSecuritySlowdownCpuThreshold";

		// Token: 0x040002B2 RID: 690
		private const uint MinimumAnonymousSlowdownCpuThreshold = 10U;

		// Token: 0x040002B3 RID: 691
		private const uint DefaultAnonymousSlowdownCpuThreshold = 25U;

		// Token: 0x040002B4 RID: 692
		private static uint anonymousSlowdownCpuThreshold;
	}
}
