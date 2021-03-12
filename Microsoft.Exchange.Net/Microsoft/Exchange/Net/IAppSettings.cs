using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BC3 RID: 3011
	internal interface IAppSettings
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x060040C1 RID: 16577
		string PodRedirectTemplate { get; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x060040C2 RID: 16578
		string SiteRedirectTemplate { get; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x060040C3 RID: 16579
		bool TenantRedirectionEnabled { get; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x060040C4 RID: 16580
		bool RedirectionEnabled { get; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x060040C5 RID: 16581
		int MaxPowershellAppPoolConnections { get; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x060040C6 RID: 16582
		string ProvisioningCacheIdentification { get; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x060040C7 RID: 16583
		string DedicatedMailboxPlansCustomAttributeName { get; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x060040C8 RID: 16584
		bool DedicatedMailboxPlansEnabled { get; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060040C9 RID: 16585
		bool ShouldShowFismaBanner { get; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060040CA RID: 16586
		int ThreadPoolMaxThreads { get; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060040CB RID: 16587
		int ThreadPoolMaxCompletionPorts { get; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060040CC RID: 16588
		PSLanguageMode PSLanguageMode { get; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060040CD RID: 16589
		string SupportedEMCVersions { get; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060040CE RID: 16590
		bool FailFastEnabled { get; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060040CF RID: 16591
		int PSMaximumReceivedObjectSizeMB { get; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060040D0 RID: 16592
		int PSMaximumReceivedDataSizePerCommandMB { get; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060040D1 RID: 16593
		string LogSubFolderName { get; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060040D2 RID: 16594
		bool LogEnabled { get; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060040D3 RID: 16595
		string LogDirectoryPath { get; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060040D4 RID: 16596
		TimeSpan LogFileAgeInDays { get; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060040D5 RID: 16597
		int MaxLogDirectorySizeInGB { get; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060040D6 RID: 16598
		int MaxLogFileSizeInMB { get; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060040D7 RID: 16599
		int ThresholdToLogActivityLatency { get; }

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060040D8 RID: 16600
		int MaxCmdletRetryCnt { get; }

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x060040D9 RID: 16601
		string WebSiteName { get; }

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x060040DA RID: 16602
		string VDirName { get; }

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x060040DB RID: 16603
		string ConfigurationFilePath { get; }

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x060040DC RID: 16604
		int LogCPUMemoryIntervalInMinutes { get; }

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x060040DD RID: 16605
		TimeSpan SidsCacheTimeoutInHours { get; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x060040DE RID: 16606
		int ClientAccessRulesLimit { get; }
	}
}
