using System;
using System.Configuration;
using System.DirectoryServices;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Configuration;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C50 RID: 3152
	internal class ManualLoadAppSettings : IAppSettings
	{
		// Token: 0x06004565 RID: 17765 RVA: 0x000B80FE File Offset: 0x000B62FE
		internal ManualLoadAppSettings(string connectionUri)
		{
			if (string.IsNullOrWhiteSpace(connectionUri))
			{
				throw new ArgumentException("connectionUri is null/Whitespace");
			}
			this.LoadWebConfig(connectionUri);
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x000B8120 File Offset: 0x000B6320
		// (set) Token: 0x06004567 RID: 17767 RVA: 0x000B8128 File Offset: 0x000B6328
		public string PodRedirectTemplate { get; private set; }

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x000B8131 File Offset: 0x000B6331
		// (set) Token: 0x06004569 RID: 17769 RVA: 0x000B8139 File Offset: 0x000B6339
		public string SiteRedirectTemplate { get; private set; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x000B8142 File Offset: 0x000B6342
		// (set) Token: 0x0600456B RID: 17771 RVA: 0x000B814A File Offset: 0x000B634A
		public bool TenantRedirectionEnabled { get; private set; }

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x0600456C RID: 17772 RVA: 0x000B8153 File Offset: 0x000B6353
		// (set) Token: 0x0600456D RID: 17773 RVA: 0x000B815B File Offset: 0x000B635B
		public bool RedirectionEnabled { get; private set; }

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x0600456E RID: 17774 RVA: 0x000B8164 File Offset: 0x000B6364
		// (set) Token: 0x0600456F RID: 17775 RVA: 0x000B816C File Offset: 0x000B636C
		public int MaxPowershellAppPoolConnections { get; private set; }

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06004570 RID: 17776 RVA: 0x000B8175 File Offset: 0x000B6375
		// (set) Token: 0x06004571 RID: 17777 RVA: 0x000B817D File Offset: 0x000B637D
		public string ProvisioningCacheIdentification { get; private set; }

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06004572 RID: 17778 RVA: 0x000B8186 File Offset: 0x000B6386
		// (set) Token: 0x06004573 RID: 17779 RVA: 0x000B818E File Offset: 0x000B638E
		public string DedicatedMailboxPlansCustomAttributeName { get; private set; }

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x000B8197 File Offset: 0x000B6397
		// (set) Token: 0x06004575 RID: 17781 RVA: 0x000B819F File Offset: 0x000B639F
		public bool DedicatedMailboxPlansEnabled { get; private set; }

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x000B81A8 File Offset: 0x000B63A8
		// (set) Token: 0x06004577 RID: 17783 RVA: 0x000B81B0 File Offset: 0x000B63B0
		public bool ShouldShowFismaBanner { get; private set; }

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x000B81B9 File Offset: 0x000B63B9
		// (set) Token: 0x06004579 RID: 17785 RVA: 0x000B81C1 File Offset: 0x000B63C1
		public int ThreadPoolMaxThreads { get; private set; }

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x000B81CA File Offset: 0x000B63CA
		// (set) Token: 0x0600457B RID: 17787 RVA: 0x000B81D2 File Offset: 0x000B63D2
		public int ThreadPoolMaxCompletionPorts { get; private set; }

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x000B81DB File Offset: 0x000B63DB
		// (set) Token: 0x0600457D RID: 17789 RVA: 0x000B81E3 File Offset: 0x000B63E3
		public PSLanguageMode PSLanguageMode { get; private set; }

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x000B81EC File Offset: 0x000B63EC
		// (set) Token: 0x0600457F RID: 17791 RVA: 0x000B81F4 File Offset: 0x000B63F4
		public string SupportedEMCVersions { get; set; }

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x000B81FD File Offset: 0x000B63FD
		// (set) Token: 0x06004581 RID: 17793 RVA: 0x000B8205 File Offset: 0x000B6405
		public bool FailFastEnabled { get; private set; }

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x000B820E File Offset: 0x000B640E
		// (set) Token: 0x06004583 RID: 17795 RVA: 0x000B8216 File Offset: 0x000B6416
		public int PSMaximumReceivedObjectSizeMB { get; private set; }

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x000B821F File Offset: 0x000B641F
		// (set) Token: 0x06004585 RID: 17797 RVA: 0x000B8227 File Offset: 0x000B6427
		public int PSMaximumReceivedDataSizePerCommandMB { get; private set; }

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x000B8230 File Offset: 0x000B6430
		// (set) Token: 0x06004587 RID: 17799 RVA: 0x000B8238 File Offset: 0x000B6438
		public string VDirName { get; private set; }

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x000B8241 File Offset: 0x000B6441
		// (set) Token: 0x06004589 RID: 17801 RVA: 0x000B8249 File Offset: 0x000B6449
		public string WebSiteName { get; private set; }

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x000B8252 File Offset: 0x000B6452
		// (set) Token: 0x0600458B RID: 17803 RVA: 0x000B825A File Offset: 0x000B645A
		public string ConfigurationFilePath { get; private set; }

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x000B8263 File Offset: 0x000B6463
		// (set) Token: 0x0600458D RID: 17805 RVA: 0x000B826B File Offset: 0x000B646B
		public string LogSubFolderName { get; private set; }

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x000B8274 File Offset: 0x000B6474
		// (set) Token: 0x0600458F RID: 17807 RVA: 0x000B827C File Offset: 0x000B647C
		public bool LogEnabled { get; private set; }

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x000B8285 File Offset: 0x000B6485
		// (set) Token: 0x06004591 RID: 17809 RVA: 0x000B828D File Offset: 0x000B648D
		public string LogDirectoryPath { get; private set; }

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x000B8296 File Offset: 0x000B6496
		// (set) Token: 0x06004593 RID: 17811 RVA: 0x000B829E File Offset: 0x000B649E
		public TimeSpan LogFileAgeInDays { get; private set; }

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x000B82A7 File Offset: 0x000B64A7
		// (set) Token: 0x06004595 RID: 17813 RVA: 0x000B82AF File Offset: 0x000B64AF
		public int MaxLogDirectorySizeInGB { get; private set; }

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x000B82B8 File Offset: 0x000B64B8
		// (set) Token: 0x06004597 RID: 17815 RVA: 0x000B82C0 File Offset: 0x000B64C0
		public int MaxLogFileSizeInMB { get; private set; }

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x000B82C9 File Offset: 0x000B64C9
		// (set) Token: 0x06004599 RID: 17817 RVA: 0x000B82D1 File Offset: 0x000B64D1
		public int ThresholdToLogActivityLatency { get; private set; }

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x000B82DA File Offset: 0x000B64DA
		// (set) Token: 0x0600459B RID: 17819 RVA: 0x000B82E2 File Offset: 0x000B64E2
		public int LogCPUMemoryIntervalInMinutes { get; private set; }

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x000B82EB File Offset: 0x000B64EB
		// (set) Token: 0x0600459D RID: 17821 RVA: 0x000B82F3 File Offset: 0x000B64F3
		public TimeSpan SidsCacheTimeoutInHours { get; private set; }

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x000B82FC File Offset: 0x000B64FC
		// (set) Token: 0x0600459F RID: 17823 RVA: 0x000B8304 File Offset: 0x000B6504
		public int ClientAccessRulesLimit { get; private set; }

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x000B830D File Offset: 0x000B650D
		// (set) Token: 0x060045A1 RID: 17825 RVA: 0x000B8315 File Offset: 0x000B6515
		public int MaxCmdletRetryCnt { get; private set; }

		// Token: 0x060045A2 RID: 17826 RVA: 0x000B831E File Offset: 0x000B651E
		private static string GetConfigurationElementStringValue(KeyValueConfigurationElement valueElement, string defaultValue)
		{
			if (valueElement == null)
			{
				return defaultValue;
			}
			return valueElement.Value;
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x000B832C File Offset: 0x000B652C
		private static TimeSpan GetConfigurationElementTimeSpanValue(KeyValueConfigurationElement valueElement, TimeSpan defaultValue, TimeSpanUnit unit)
		{
			if (valueElement == null)
			{
				return defaultValue;
			}
			TimeSpan result = defaultValue;
			string value = valueElement.Value;
			int num;
			if (int.TryParse(value, out num))
			{
				switch (unit)
				{
				case TimeSpanUnit.Seconds:
					result = TimeSpan.FromSeconds((double)num);
					break;
				case TimeSpanUnit.Minutes:
					result = TimeSpan.FromMinutes((double)num);
					break;
				case TimeSpanUnit.Hours:
					result = TimeSpan.FromHours((double)num);
					break;
				case TimeSpanUnit.Days:
					result = TimeSpan.FromDays((double)num);
					break;
				}
			}
			return result;
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000B8394 File Offset: 0x000B6594
		private static T GetConfigurationElementValue<T>(KeyValueConfigurationElement valueElement, T defaultValue, ManualLoadAppSettings.TryParseDelegate<T> tryParseDelegate)
		{
			if (tryParseDelegate == null)
			{
				throw new ArgumentNullException("tryParseDelegate can't be null.");
			}
			if (valueElement == null)
			{
				return defaultValue;
			}
			T result;
			if (tryParseDelegate(valueElement.Value, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x000B8AC8 File Offset: 0x000B6CC8
		private void LoadWebConfig(string connectionUrl)
		{
			Diagnostics.ExecuteAndLog("ManualLoadAppSettings.LoadWebConfig", true, null, EventLogConstants.NetEventLogger, CommonEventLogConstants.Tuple_UnhandledException, ExTraceGlobals.AppSettingsTracer, null, delegate(Exception ex)
			{
				AuthZLogger.SafeAppendGenericError("LoadWebConfig", ex.ToString(), true);
			}, delegate()
			{
				ExTraceGlobals.AppSettingsTracer.TraceDebug<string>((long)this.GetHashCode(), "Load web.config for Url {0}.", connectionUrl);
				Uri uri = new Uri(connectionUrl, UriKind.Absolute);
				string vdirPathFromUriLocalPath = ManualLoadAppSettings.GetVDirPathFromUriLocalPath(uri);
				string host = uri.Host;
				int port = uri.Port;
				string text;
				Configuration configuration = this.OpenWebConfig(host, port, vdirPathFromUriLocalPath, out text);
				ExTraceGlobals.AppSettingsTracer.TraceDebug<string, string>((long)this.GetHashCode(), "webSiteName = {0}, vdirPath = {1}.", text, vdirPathFromUriLocalPath);
				this.VDirName = vdirPathFromUriLocalPath;
				this.ConfigurationFilePath = configuration.FilePath;
				this.WebSiteName = text;
				this.ReadRemotePSMaxLimitParameters(configuration, vdirPathFromUriLocalPath);
				KeyValueConfigurationCollection keyValueConfigurationCollection = (configuration == null) ? new KeyValueConfigurationCollection() : configuration.AppSettings.Settings;
				this.PodRedirectTemplate = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["PodRedirectTemplate"], null);
				this.SiteRedirectTemplate = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["SiteRedirectTemplate"], null);
				this.TenantRedirectionEnabled = ManualLoadAppSettings.GetConfigurationElementValue<bool>(keyValueConfigurationCollection["TenantRedirectionEnabled"], false, new ManualLoadAppSettings.TryParseDelegate<bool>(bool.TryParse));
				this.RedirectionEnabled = ManualLoadAppSettings.GetConfigurationElementValue<bool>(keyValueConfigurationCollection["RedirectionEnabled"], true, new ManualLoadAppSettings.TryParseDelegate<bool>(bool.TryParse));
				this.MaxPowershellAppPoolConnections = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["MaxPowershellAppPoolConnections"], 0, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.ProvisioningCacheIdentification = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["ProvisioningCacheIdentification"], null);
				this.DedicatedMailboxPlansCustomAttributeName = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["DedicatedMailboxPlansCustomAttributeName"], null);
				this.DedicatedMailboxPlansEnabled = ManualLoadAppSettings.GetConfigurationElementValue<bool>(keyValueConfigurationCollection["DedicatedMailboxPlansEnabled"], false, new ManualLoadAppSettings.TryParseDelegate<bool>(bool.TryParse));
				this.ShouldShowFismaBanner = ManualLoadAppSettings.GetConfigurationElementValue<bool>(keyValueConfigurationCollection["ShouldShowFismaBanner"], false, new ManualLoadAppSettings.TryParseDelegate<bool>(bool.TryParse));
				this.ThreadPoolMaxThreads = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["ThreadPool.MaxWorkerThreads"], AppSettings.DefaultThreadPoolMaxThreads, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.ThreadPoolMaxCompletionPorts = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["ThreadPool.MaxCompletionPortThreads"], AppSettings.DefaultThreadPoolMaxCompletionPorts, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.PSLanguageMode = ManualLoadAppSettings.GetConfigurationElementValue<PSLanguageMode>(keyValueConfigurationCollection["PSLanguageMode"], PSLanguageMode.NoLanguage, new ManualLoadAppSettings.TryParseDelegate<PSLanguageMode>(Enum.TryParse<PSLanguageMode>));
				this.SupportedEMCVersions = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["SupportedEMCVersions"], null);
				this.FailFastEnabled = ManualLoadAppSettings.GetConfigurationElementValue<bool>(keyValueConfigurationCollection["FailFastEnabled"], false, new ManualLoadAppSettings.TryParseDelegate<bool>(bool.TryParse));
				this.LogSubFolderName = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["LogSubFolderName"], "Others");
				this.LogEnabled = ManualLoadAppSettings.GetConfigurationElementValue<bool>(keyValueConfigurationCollection["LogEnabled"], true, new ManualLoadAppSettings.TryParseDelegate<bool>(bool.TryParse));
				this.LogDirectoryPath = ManualLoadAppSettings.GetConfigurationElementStringValue(keyValueConfigurationCollection["LogDirectoryPath"], null);
				this.LogFileAgeInDays = ManualLoadAppSettings.GetConfigurationElementTimeSpanValue(keyValueConfigurationCollection["LogFileAgeInDays"], AppSettings.DefaultLogFileAgeInDays, TimeSpanUnit.Days);
				this.MaxLogDirectorySizeInGB = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["MaxLogDirectorySizeInGB"], 1, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.MaxLogFileSizeInMB = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["MaxLogFileSizeInMB"], 10, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.ThresholdToLogActivityLatency = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["ThresholdToLogActivityLatency"], 1000, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.LogCPUMemoryIntervalInMinutes = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["LogCPUMemoryIntervalInMinutes"], AppSettings.DefaultLogCPUMemoryIntervalInMinutes, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.SidsCacheTimeoutInHours = ManualLoadAppSettings.GetConfigurationElementTimeSpanValue(keyValueConfigurationCollection["SidsCacheTimeoutInHours"], AppSettings.DefaultSidsCacheTimeoutInHours, TimeSpanUnit.Hours);
				this.ClientAccessRulesLimit = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["ClientAccessRulesLimit"], AppSettings.DefaultClientAccessRulesLimit, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				this.MaxCmdletRetryCnt = ManualLoadAppSettings.GetConfigurationElementValue<int>(keyValueConfigurationCollection["MaxCmdletRetryCnt"], AppSettings.DefaultMaxCmdletRetryCnt, new ManualLoadAppSettings.TryParseDelegate<int>(int.TryParse));
				if (this.MaxCmdletRetryCnt < 0)
				{
					this.MaxCmdletRetryCnt = 0;
				}
				ExTraceGlobals.AppSettingsTracer.TraceDebug((long)this.GetHashCode(), "this.PodRedirectTemplate = {0}, this.SiteRedirectTemplate = {1}, this.TenantRedirectionEnabled = {2}, this.RedirectionEnabled = {3}, this.MaxPowershellAppPoolConnections = {4}, this.ProvisioningCacheIdentification = {5}, this.ShouldShowFismaBanner = {6}, this.ThreadPoolMaxThreads = {7}, this.ThreadPoolMaxCompletionPorts  = {8}, this.PSLanguageMode = {9}, this.SupportedEMCVersions = {10}, this.FailFastEnabled = {11}, this.LogSubFolderName = {12}, this.LogEnabled = {13}, this.LogDirectoryPath = {14}, this.LogFileAgeInDays = {15}, this.MaxLogDirectorySizeInGB = {16}, this.MaxLogFileSizeInMB = {17}, this.ThresholdToLogActivityLatency = {18}, this.DedicatedMailboxPlansCustomAttributeName = {19}, this.DedicatedMailboxPlansEnabled = {20}, this.LogCPUMemoryIntervalInMinutes = {21}, this.SidsCacheTimeoutInHours={22} this.ClientAccessRulesLimit={23}, this.MaxCmdletRetryCnt={24}", new object[]
				{
					this.PodRedirectTemplate,
					this.SiteRedirectTemplate,
					this.TenantRedirectionEnabled,
					this.RedirectionEnabled,
					this.MaxPowershellAppPoolConnections,
					this.ProvisioningCacheIdentification,
					this.ShouldShowFismaBanner,
					this.ThreadPoolMaxThreads,
					this.ThreadPoolMaxCompletionPorts,
					this.PSLanguageMode,
					this.SupportedEMCVersions,
					this.FailFastEnabled,
					this.LogSubFolderName,
					this.SupportedEMCVersions,
					this.FailFastEnabled,
					this.LogSubFolderName,
					this.LogEnabled,
					this.LogDirectoryPath,
					this.LogFileAgeInDays,
					this.MaxLogDirectorySizeInGB,
					this.MaxLogFileSizeInMB,
					this.ThresholdToLogActivityLatency,
					this.DedicatedMailboxPlansCustomAttributeName,
					this.DedicatedMailboxPlansEnabled,
					this.LogCPUMemoryIntervalInMinutes,
					this.SidsCacheTimeoutInHours,
					this.ClientAccessRulesLimit,
					this.MaxCmdletRetryCnt
				});
			});
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000B8B30 File Offset: 0x000B6D30
		private Configuration OpenWebConfig(string host, int port, string vdirPath, out string webSiteName)
		{
			ExTraceGlobals.AppSettingsTracer.TraceDebug<string, int, string>((long)this.GetHashCode(), "Host = {0}, Port = {1}, vdirPath = {2}.", host, port, vdirPath);
			Configuration result = null;
			webSiteName = this.FindWebSite(host, port, vdirPath);
			try
			{
				if (string.IsNullOrEmpty(webSiteName))
				{
					result = WebConfigurationManager.OpenWebConfiguration(vdirPath);
				}
				else
				{
					result = WebConfigurationManager.OpenWebConfiguration(vdirPath, webSiteName);
				}
			}
			catch (InvalidOperationException ex)
			{
				this.LogIisHierarchy(host, port, vdirPath, webSiteName, ex.ToString());
			}
			return result;
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x000B8BDC File Offset: 0x000B6DDC
		private void ReadRemotePSMaxLimitParameters(Configuration configuration, string vdirPath)
		{
			this.PSMaximumReceivedObjectSizeMB = AppSettings.DefaultPSMaximumReceivedObjectSizeByte;
			this.PSMaximumReceivedDataSizePerCommandMB = AppSettings.DefaultPSMaximumReceivedDataSizePerCommandByte;
			string configSection = configuration.GetSection("system.webServer").SectionInformation.GetRawXml();
			if (string.IsNullOrEmpty(configSection))
			{
				return;
			}
			SafeXmlDocument xmlDoc = new SafeXmlDocument();
			Diagnostics.ExecuteAndLog("ManualLoadAppSettings.ReadRemotePSMaxLimitParameters.xmldoc.LoadXml", false, null, EventLogConstants.NetEventLogger, CommonEventLogConstants.Tuple_NonCrashingException, ExTraceGlobals.AppSettingsTracer, null, delegate(Exception ex)
			{
				AuthZLogger.SafeAppendGenericError("ReadRemotePSMaxLimitParameters.xmldoc.LoadXml", ex.ToString(), true);
			}, delegate()
			{
				xmlDoc.LoadXml(configSection);
			});
			using (XmlNodeList xmlNodeList = xmlDoc.SelectNodes("system.webServer/system.management.wsmanagement.config/PluginModules/OperationsPlugins/Plugin/InitializationParameters/Param"))
			{
				if (xmlNodeList != null)
				{
					foreach (object obj in xmlNodeList)
					{
						XmlElement xmlElement = (XmlElement)obj;
						double num2;
						if (xmlElement.GetAttribute("Name").Equals("PSMaximumReceivedObjectSizeMB"))
						{
							double num;
							if (double.TryParse(xmlElement.GetAttribute("Value"), out num) || double.TryParse(xmlElement.GetAttribute("value"), out num))
							{
								this.PSMaximumReceivedObjectSizeMB = (int)num * 1048576;
							}
						}
						else if (xmlElement.GetAttribute("Name").Equals("PSMaximumReceivedDataSizePerCommandMB") && (double.TryParse(xmlElement.GetAttribute("Value"), out num2) || double.TryParse(xmlElement.GetAttribute("value"), out num2)))
						{
							this.PSMaximumReceivedDataSizePerCommandMB = (int)num2 * 1048576;
						}
					}
					ExTraceGlobals.AppSettingsTracer.TraceDebug<int, int>((long)this.GetHashCode(), "PSMaximumReceivedObjectSizeMB = {0}, PSMaximumReceivedDataSizePerCommandMB = {1}.", this.PSMaximumReceivedObjectSizeMB, this.PSMaximumReceivedDataSizePerCommandMB);
				}
			}
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x000B8F48 File Offset: 0x000B7148
		private string FindWebSite(string hostName, int port, string vdirPath)
		{
			string text = (port == 444) ? "Exchange Back End" : string.Empty;
			text = ((port == 446) ? "Ucc Web Site" : text);
			return Diagnostics.ExecuteAndLog<string>("ManualLoadAppSettings.FindWebSite", false, null, EventLogConstants.NetEventLogger, CommonEventLogConstants.Tuple_NonCrashingException, ExTraceGlobals.AppSettingsTracer, (object ex) => !(ex is COMException), delegate(Exception ex)
			{
				AuthZLogger.SafeAppendGenericError("FindWebSite", ex.ToString(), true);
			}, text, delegate()
			{
				string iisServiceRoot = this.GetIisServiceRoot(hostName);
				using (DirectoryEntry directoryEntry = new DirectoryEntry(iisServiceRoot))
				{
					foreach (object obj in directoryEntry.Children)
					{
						using (DirectoryEntry directoryEntry2 = (DirectoryEntry)obj)
						{
							if (string.CompareOrdinal(directoryEntry2.SchemaClassName, "IIsWebServer") == 0)
							{
								string text2 = (string)directoryEntry2.Properties["ServerComment"].Value;
								string path = string.Format("{0}/ROOT{1}", directoryEntry2.Path, vdirPath);
								if (DirectoryEntry.Exists(path))
								{
									bool flag = text2.Equals("Exchange Back End", StringComparison.OrdinalIgnoreCase);
									bool flag2 = text2.Equals("Ucc Web Site", StringComparison.OrdinalIgnoreCase);
									if ((port == 444 && flag) || (port == 446 && flag2) || (port != 444 && !flag && port != 446 && !flag2))
									{
										return text2;
									}
								}
							}
						}
					}
				}
				return string.Empty;
			});
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x000B900C File Offset: 0x000B720C
		private string GetIisServiceRoot(string hostName)
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
			string text = string.Empty;
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					text = ipaddress.ToString();
					break;
				}
			}
			string result;
			if (string.IsNullOrEmpty(text))
			{
				result = string.Format("IIS://{0}/W3SVC", hostName);
			}
			else
			{
				result = string.Format("IIS://{0}/W3SVC", text);
			}
			return result;
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x000B907C File Offset: 0x000B727C
		private void LogIisHierarchy(string hostName, int port, string vdirPath, string websiteName, string errorMsg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("LoadWebConfigFailed {0}.", errorMsg);
			stringBuilder.AppendFormat("HostName:{0} Port:{1} VirtualDirectoryPath:{2} WebSiteName:{3}.", new object[]
			{
				hostName,
				port,
				vdirPath,
				websiteName
			});
			string iisServiceRoot = this.GetIisServiceRoot(hostName);
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			if (!string.IsNullOrEmpty(iisServiceRoot))
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(iisServiceRoot))
				{
					foreach (object obj in directoryEntry.Children)
					{
						DirectoryEntry directoryEntry2 = (DirectoryEntry)obj;
						if (directoryEntry2.SchemaClassName.Equals("IIsWebServer", StringComparison.OrdinalIgnoreCase))
						{
							string text = (string)directoryEntry2.Properties["ServerComment"].Value;
							using (DirectoryEntry directoryEntry3 = new DirectoryEntry(directoryEntry2.Path + "/ROOT"))
							{
								foreach (object obj2 in directoryEntry3.Children)
								{
									DirectoryEntry directoryEntry4 = (DirectoryEntry)obj2;
									if (text.Equals("Default Web Site", StringComparison.OrdinalIgnoreCase))
									{
										stringBuilder3.AppendFormat("{0}  ", directoryEntry4.Name);
									}
									else
									{
										stringBuilder2.AppendFormat("{0}  ", directoryEntry4.Name);
									}
								}
							}
						}
					}
					goto IL_198;
				}
			}
			stringBuilder.Append("HostNameNotFound");
			IL_198:
			stringBuilder.AppendFormat("DefaultWebSite:'{0}' ExchangeBackEnd:'{1}'", stringBuilder3.ToString(), stringBuilder2.ToString());
			EventLogConstants.NetEventLogger.LogEvent(CommonEventLogConstants.Tuple_AppSettingLoadException, null, new string[]
			{
				stringBuilder.ToString()
			});
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x000B92C4 File Offset: 0x000B74C4
		private static string GetVDirPathFromUriLocalPath(Uri uri)
		{
			string localPath = uri.LocalPath;
			if (string.IsNullOrEmpty(localPath) || localPath[0] != '/')
			{
				return localPath;
			}
			int num = localPath.IndexOf('/', 1);
			if (num == -1)
			{
				return localPath;
			}
			return localPath.Substring(0, num);
		}

		// Token: 0x04003A38 RID: 14904
		private const string ConfigInitializationParameterPath = "system.webServer/system.management.wsmanagement.config/PluginModules/OperationsPlugins/Plugin/InitializationParameters/Param";

		// Token: 0x02000C51 RID: 3153
		// (Invoke) Token: 0x060045B1 RID: 17841
		private delegate bool TryParseDelegate<T>(string value, out T parsedValue);
	}
}
