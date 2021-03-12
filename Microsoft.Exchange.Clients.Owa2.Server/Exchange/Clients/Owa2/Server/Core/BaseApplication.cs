using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000062 RID: 98
	internal abstract class BaseApplication
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000CFCE File Offset: 0x0000B1CE
		protected BaseApplication()
		{
			this.applicationStopwatch.Start();
			OwaRegistryKeys.Initialize();
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000D006 File Offset: 0x0000B206
		public string ApplicationVersion
		{
			get
			{
				return this.applicationVersion;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000D00E File Offset: 0x0000B20E
		public long ApplicationTime
		{
			get
			{
				return this.applicationStopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D01C File Offset: 0x0000B21C
		public bool ArePerfCountersEnabled
		{
			get
			{
				bool result = true;
				try
				{
					result = BaseApplication.GetAppSetting<bool>("ArePerfCountersEnabled", true);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.CoreTracer.TraceError<string, string>(0L, "Failed to get value for ArePerfCountersEnabled from AppSetting. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
				}
				return result;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000D06C File Offset: 0x0000B26C
		public bool SendWatsonReports
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("SendWatsonReports", true);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000D079 File Offset: 0x0000B279
		public bool DisableBreadcrumbs
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("DisableBreadcrumbs", false);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000D086 File Offset: 0x0000B286
		public bool CheckForForgottenAttachmentsEnabled
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("CheckForForgottenAttachmentsEnabled", true);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000D093 File Offset: 0x0000B293
		public string BlockedQueryStringValues
		{
			get
			{
				return BaseApplication.GetAppSetting<string>("BlockedQueryStringValues", null);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public string ContentDeliveryNetworkEndpoint
		{
			get
			{
				return BaseApplication.GetAppSetting<string>("ContentDeliveryNetworkEndpoint", null);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000D0AD File Offset: 0x0000B2AD
		public bool ControlTasksQueueDisabled
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("ControlTasksQueueDisabled", false);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000D0BA File Offset: 0x0000B2BA
		public bool IsPreCheckinApp
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("IsPreCheckinApp", false);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000D0C7 File Offset: 0x0000B2C7
		public bool IsFirstReleaseFlightingEnabled
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("FirstReleaseFlightingEnabled", false);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public bool OwaIsNoRecycleEnabled
		{
			get
			{
				return BaseApplication.GetAppSetting<bool>("OwaIsNoRecycleEnabled", false);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000D0E1 File Offset: 0x0000B2E1
		public double OwaVersionReadingInterval
		{
			get
			{
				return BaseApplication.GetAppSetting<double>("OwaVersionReadingInterval", OwaVersionId.OwaVersionReadingInterval);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600034B RID: 843
		public abstract int ActivityBasedPresenceDuration { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600034C RID: 844
		public abstract int MaxBreadcrumbs { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600034D RID: 845
		public abstract bool LogVerboseNotifications { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600034E RID: 846
		public abstract TroubleshootingContext TroubleshootingContext { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600034F RID: 847
		public abstract bool LogErrorDetails { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000350 RID: 848
		public abstract bool LogErrorTraces { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000351 RID: 849
		public abstract HttpClientCredentialType ServiceAuthenticationType { get; }

		// Token: 0x06000352 RID: 850 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		internal static BaseApplication CreateInstance()
		{
			BaseApplication result;
			if (!string.IsNullOrEmpty(HttpRuntime.AppDomainAppId) && HttpRuntime.AppDomainAppId.EndsWith("calendar", StringComparison.CurrentCultureIgnoreCase))
			{
				result = new OwaAnonymousApplication();
			}
			else
			{
				result = new OwaApplication();
			}
			return result;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D130 File Offset: 0x0000B330
		internal static TimeSpan GetTimeSpanAppSetting(string key, TimeSpan defaultValue)
		{
			string text = ConfigurationManager.AppSettings.Get(key);
			TimeSpan result = defaultValue;
			if (!string.IsNullOrWhiteSpace(text) && !TimeSpan.TryParse(text, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D160 File Offset: 0x0000B360
		internal static T GetAppSetting<T>(string key, T defaultValue)
		{
			try
			{
				string value = ConfigurationManager.AppSettings.Get(key);
				if (!string.IsNullOrWhiteSpace(value))
				{
					return (T)((object)Convert.ChangeType(value, typeof(T)));
				}
			}
			catch (ConfigurationErrorsException ex)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>(0L, "Failed to load setting {0}. Error: {1}", key, ex.Message);
			}
			catch (FormatException ex2)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>(0L, "Failed to load setting {0}. Error: {1}", key, ex2.Message);
			}
			catch (InvalidOperationException ex3)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>(0L, "Failed to load setting {0}. Error: {1}", key, ex3.Message);
			}
			return defaultValue;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000D21C File Offset: 0x0000B41C
		internal void Dispose()
		{
			this.InternalDispose();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000D224 File Offset: 0x0000B424
		internal void ExecuteApplicationStart(object sender, EventArgs e)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "Global.Application_Start");
			try
			{
				int num = Privileges.RemoveAllExcept(new string[]
				{
					"SeAuditPrivilege",
					"SeChangeNotifyPrivilege",
					"SeCreateGlobalPrivilege",
					"SeImpersonatePrivilege",
					"SeIncreaseQuotaPrivilege",
					"SeAssignPrimaryTokenPrivilege"
				}, "MSExchangeOWAAppPool");
				if (num != 0)
				{
					throw new OwaWin32Exception(num, "Failed to remove unnecessary privileges");
				}
				if (BaseApplication.IsRunningDfpowa)
				{
					string localPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
					DirectoryInfo[] directories = Directory.GetParent(Directory.GetParent(localPath).FullName).GetDirectories("Config");
					if (directories.Length > 0)
					{
						VariantConfiguration.Initialize(directories[0].FullName);
					}
				}
				string applicationName = this.IsPreCheckinApp ? "PCDFOWA" : "OWA";
				Globals.InitializeMultiPerfCounterInstance(applicationName);
				Globals.Initialize();
				Kerberos.FlushTicketCache();
				ExRpcModule.Bind();
				this.ExecuteApplicationSpecificStart();
			}
			catch (OwaWin32Exception ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<int>(0L, "Application initialization failed with a win32 error: {0}", ex.LastError);
				Globals.InitializationError = ex;
				return;
			}
			catch (Exception initializationError)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Application initialization failed");
				Globals.InitializationError = initializationError;
				throw;
			}
			Globals.IsInitialized = true;
			ExTraceGlobals.CoreTracer.TraceDebug(0L, "Application initialization succeeded");
			int id = Process.GetCurrentProcess().Id;
			if (Globals.IsPreCheckinApp)
			{
				OwaDiagnostics.Logger.LogEvent(ClientsEventLogConstants.Tuple_DfpOwaStartedSuccessfully, string.Empty, new object[]
				{
					id
				});
				return;
			}
			OwaDiagnostics.Logger.LogEvent(ClientsEventLogConstants.Tuple_OwaStartedSuccessfully, string.Empty, new object[]
			{
				id
			});
			OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.Owa.Name, "OwaWebAppStarted", "Outlook Web App started successfully", ResultSeverityLevel.Error);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D414 File Offset: 0x0000B614
		internal void ExecuteApplicationEnd(object sender, EventArgs e)
		{
			Tokenizer.ReleaseWordBreakers();
			InstantMessageOCSProvider.DisposeEndpointManager();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D420 File Offset: 0x0000B620
		internal virtual void ExecuteApplicationBeginRequest(object sender, EventArgs e)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D422 File Offset: 0x0000B622
		internal virtual void ExecuteApplicationEndRequest(object sender, EventArgs e)
		{
		}

		// Token: 0x0600035A RID: 858
		internal abstract void UpdateErrorTracingConfiguration();

		// Token: 0x0600035B RID: 859
		internal abstract void Initialize();

		// Token: 0x0600035C RID: 860
		protected abstract void InternalDispose();

		// Token: 0x0600035D RID: 861
		protected abstract void ExecuteApplicationSpecificStart();

		// Token: 0x04000192 RID: 402
		protected const int FailoverServerLcid = 1033;

		// Token: 0x04000193 RID: 403
		private static readonly bool IsRunningDfpowa = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["IsPreCheckinApp"]) && StringComparer.OrdinalIgnoreCase.Equals("true", ConfigurationManager.AppSettings["IsPreCheckinApp"]);

		// Token: 0x04000194 RID: 404
		private readonly string applicationVersion = typeof(Globals).GetApplicationVersion();

		// Token: 0x04000195 RID: 405
		private Stopwatch applicationStopwatch = new Stopwatch();
	}
}
