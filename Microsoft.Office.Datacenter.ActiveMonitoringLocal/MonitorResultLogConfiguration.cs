using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000087 RID: 135
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MonitorResultLogConfiguration : ILogConfiguration
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		public MonitorResultLogConfiguration(string logType, string logComponentValue)
		{
			this.logPrefix = logType;
			this.logType = logType;
			this.logComponentValue = logType;
			this.IsLoggingEnabled = (Settings.IsResultsLoggingEnabled && (Datacenter.IsMicrosoftHostedOnly(false) || Datacenter.IsDatacenterDedicated(false)));
			if (this.IsLoggingEnabled)
			{
				string fullName = Assembly.GetEntryAssembly().FullName;
				if (string.IsNullOrEmpty(fullName) || !fullName.StartsWith("MSExchangeHMWorker", StringComparison.OrdinalIgnoreCase))
				{
					this.IsLoggingEnabled = false;
				}
			}
			if (this.IsLoggingEnabled)
			{
				this.LogPath = Settings.DefaultResultsLogPath;
				if (string.IsNullOrEmpty(this.LogPath))
				{
					try
					{
						this.LogPath = ExchangeSetupContext.InstallPath;
						if (string.IsNullOrEmpty(this.LogPath))
						{
							this.LogPath = "C:\\Program Files\\Microsoft\\Exchange Server\\V15";
						}
					}
					catch (Exception)
					{
						this.LogPath = "C:\\Program Files\\Microsoft\\Exchange Server\\V15";
					}
				}
				this.LogPath = Path.Combine(Path.Combine(this.LogPath, "Logging\\Monitoring\\Monitoring"), logType);
				this.MaxLogAge = TimeSpan.FromDays((double)Settings.MaxLogAge);
				this.MaxLogDirectorySizeInBytes = (long)Settings.MaxResultsLogDirectorySizeInBytes;
				this.MaxLogFileSizeInBytes = (long)Settings.MaxResultsLogFileSizeInBytes;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001D320 File Offset: 0x0001B520
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001D328 File Offset: 0x0001B528
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001D331 File Offset: 0x0001B531
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001D334 File Offset: 0x0001B534
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001D33C File Offset: 0x0001B53C
		public string LogPath { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001D345 File Offset: 0x0001B545
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001D34D File Offset: 0x0001B54D
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001D356 File Offset: 0x0001B556
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001D35E File Offset: 0x0001B55E
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001D367 File Offset: 0x0001B567
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001D36F File Offset: 0x0001B56F
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001D378 File Offset: 0x0001B578
		public string LogComponent
		{
			get
			{
				return this.logComponentValue;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001D380 File Offset: 0x0001B580
		public string LogPrefix
		{
			get
			{
				return this.logPrefix;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001D388 File Offset: 0x0001B588
		public string LogType
		{
			get
			{
				return this.logType;
			}
		}

		// Token: 0x04000463 RID: 1123
		private const string DefaultLogDirectoryPath = "C:\\Program Files\\Microsoft\\Exchange Server\\V15";

		// Token: 0x04000464 RID: 1124
		private readonly string logPrefix;

		// Token: 0x04000465 RID: 1125
		private readonly string logType;

		// Token: 0x04000466 RID: 1126
		private readonly string logComponentValue;
	}
}
