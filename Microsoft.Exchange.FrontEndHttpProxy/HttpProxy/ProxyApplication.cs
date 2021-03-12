using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.EventLogs;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C5 RID: 197
	public class ProxyApplication : HttpApplication
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0002B88E File Offset: 0x00029A8E
		public static string ApplicationVersion
		{
			get
			{
				return HttpProxyGlobals.ApplicationVersion;
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0002B895 File Offset: 0x00029A95
		private static void ConfigureServicePointManager()
		{
			ServicePointManager.DefaultConnectionLimit = HttpProxySettings.ServicePointConnectionLimit.Value;
			ServicePointManager.UseNagleAlgorithm = false;
			ProxyApplication.ConfigureSecureProtocols();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0002B8B4 File Offset: 0x00029AB4
		private static void ConfigureSecureProtocols()
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.EnableTls11.Enabled)
			{
				ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11;
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.EnableTls12.Enabled)
			{
				ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0002B98C File Offset: 0x00029B8C
		private void Application_Start(object sender, EventArgs e)
		{
			Diagnostics.InitializeWatsonReporting();
			if (Globals.InstanceType == InstanceType.NotInitialized)
			{
				string text = HttpProxyGlobals.ProtocolType.ToString();
				text = "FE_" + text;
				Globals.InitializeMultiPerfCounterInstance(text);
			}
			Diagnostics.SendWatsonReportOnUnhandledException(delegate()
			{
				Task.Factory.StartNew(delegate()
				{
					SettingOverrideSync.Instance.Start(true);
				});
				CertificateValidationManager.RegisterCallback(Constants.CertificateValidationComponentId, ProxyApplication.RemoteCertificateValidationCallback);
				ProxyApplication.ConfigureServicePointManager();
				if (DownLevelServerManager.IsApplicable)
				{
					DownLevelServerManager.Instance.Initialize();
				}
			});
			PerfCounters.UpdateHttpProxyPerArrayCounters();
			Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_ApplicationStart, null, new object[]
			{
				HttpProxyGlobals.ProtocolType
			});
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0002BA14 File Offset: 0x00029C14
		private void Application_End(object sender, EventArgs e)
		{
			Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_ApplicationShutdown, null, new object[]
			{
				HttpProxyGlobals.ProtocolType
			});
			RequestDetailsLogger.FlushQueuedFileWrites();
			SettingOverrideSync.Instance.Stop();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0002BA58 File Offset: 0x00029C58
		private void Application_Error(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			Exception lastError = httpApplication.Server.GetLastError();
			Diagnostics.ReportException(lastError, FrontEndHttpProxyEventLogConstants.Tuple_InternalServerError, null, "Exception from Application_Error event: {0}");
		}

		// Token: 0x040004A5 RID: 1189
		internal static readonly RemoteCertificateValidationCallback RemoteCertificateValidationCallback = (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => HttpProxyRegistry.OwaAllowInternalUntrustedCerts.Member || errors == SslPolicyErrors.None;
	}
}
