using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Logging
{
	// Token: 0x0200076B RID: 1899
	internal sealed class ProtocolLogConfiguration
	{
		// Token: 0x06002579 RID: 9593 RVA: 0x0004ECF0 File Offset: 0x0004CEF0
		public ProtocolLogConfiguration(string logNameInitials)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logNameInitials", logNameInitials);
			this.softwareName = "Microsoft Exchange Server";
			this.softwareVersion = Assembly.GetExecutingAssembly().GetName().Version;
			this.logTypeName = logNameInitials + " Protocol Logs";
			this.logFilePrefix = logNameInitials;
			this.logComponent = logNameInitials + "ProtocolLogs";
			string path = string.Format(CultureInfo.InvariantCulture, "TransportRoles\\Logs\\ProtocolLog\\{0}Client", new object[]
			{
				logNameInitials
			});
			this.logFilePath = Path.Combine(ProtocolLogConfiguration.exchangeInstallDirPath, path);
			this.ageQuota = 168L;
			this.directorySizeQuota = 256000L;
			this.perFileSizeQuota = 10240L;
			this.protocolLoggingLevel = ProtocolLoggingLevel.None;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x0004EDAF File Offset: 0x0004CFAF
		// (set) Token: 0x0600257B RID: 9595 RVA: 0x0004EDB7 File Offset: 0x0004CFB7
		public string SoftwareName
		{
			get
			{
				return this.softwareName;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("SoftwareName", value);
				this.softwareName = value;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x0004EDCB File Offset: 0x0004CFCB
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x0004EDD3 File Offset: 0x0004CFD3
		public Version SoftwareVersion
		{
			get
			{
				return this.softwareVersion;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("SoftwareVersion", value);
				this.softwareVersion = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0004EDE7 File Offset: 0x0004CFE7
		// (set) Token: 0x0600257F RID: 9599 RVA: 0x0004EDEF File Offset: 0x0004CFEF
		public string LogTypeName
		{
			get
			{
				return this.logTypeName;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("LogTypeName", value);
				this.logTypeName = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x0004EE03 File Offset: 0x0004D003
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x0004EE0B File Offset: 0x0004D00B
		public string LogFilePrefix
		{
			get
			{
				return this.logFilePrefix;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("LogFilePrefix", value);
				this.logFilePrefix = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x0004EE1F File Offset: 0x0004D01F
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x0004EE27 File Offset: 0x0004D027
		public string LogComponent
		{
			get
			{
				return this.logComponent;
			}
			set
			{
				ArgumentValidator.ThrowIfNull("LogComponent", value);
				this.logComponent = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0004EE3B File Offset: 0x0004D03B
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x0004EE43 File Offset: 0x0004D043
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x0004EE4C File Offset: 0x0004D04C
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x0004EE54 File Offset: 0x0004D054
		public string LogFilePath
		{
			get
			{
				return this.logFilePath;
			}
			set
			{
				ArgumentValidator.ThrowIfNullOrEmpty("LogFilePath", value);
				this.logFilePath = Path.Combine(ProtocolLogConfiguration.exchangeInstallDirPath, value);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x0004EE72 File Offset: 0x0004D072
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x0004EE7A File Offset: 0x0004D07A
		public long AgeQuota
		{
			get
			{
				return this.ageQuota;
			}
			set
			{
				ProtocolLogConfiguration.ThrowIfArgumentLessThanZero("AgeQuota", value);
				this.ageQuota = value;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x0004EE8E File Offset: 0x0004D08E
		// (set) Token: 0x0600258B RID: 9611 RVA: 0x0004EE96 File Offset: 0x0004D096
		public long DirectorySizeQuota
		{
			get
			{
				return this.directorySizeQuota;
			}
			set
			{
				ProtocolLogConfiguration.ThrowIfArgumentLessThanZero("DirectorySizeQuota", value);
				this.directorySizeQuota = value;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x0004EEAA File Offset: 0x0004D0AA
		// (set) Token: 0x0600258D RID: 9613 RVA: 0x0004EEB2 File Offset: 0x0004D0B2
		public long PerFileSizeQuota
		{
			get
			{
				return this.perFileSizeQuota;
			}
			set
			{
				ProtocolLogConfiguration.ThrowIfArgumentLessThanZero("PerFileSizeQuota", value);
				this.perFileSizeQuota = value;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0004EEC6 File Offset: 0x0004D0C6
		// (set) Token: 0x0600258F RID: 9615 RVA: 0x0004EECE File Offset: 0x0004D0CE
		public ProtocolLoggingLevel ProtocolLoggingLevel
		{
			get
			{
				return this.protocolLoggingLevel;
			}
			set
			{
				this.protocolLoggingLevel = value;
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0004EED7 File Offset: 0x0004D0D7
		private static void ThrowIfArgumentLessThanZero(string name, long arg)
		{
			if (arg < 0L)
			{
				throw new ArgumentOutOfRangeException(name, arg, "The value is set to less than 0.");
			}
		}

		// Token: 0x040022D6 RID: 8918
		private static string exchangeInstallDirPath = Assembly.GetExecutingAssembly().Location + "\\..\\..\\";

		// Token: 0x040022D7 RID: 8919
		private string softwareName;

		// Token: 0x040022D8 RID: 8920
		private Version softwareVersion;

		// Token: 0x040022D9 RID: 8921
		private string logTypeName;

		// Token: 0x040022DA RID: 8922
		private string logFilePrefix;

		// Token: 0x040022DB RID: 8923
		private string logComponent;

		// Token: 0x040022DC RID: 8924
		private bool isEnabled;

		// Token: 0x040022DD RID: 8925
		private string logFilePath;

		// Token: 0x040022DE RID: 8926
		private long ageQuota;

		// Token: 0x040022DF RID: 8927
		private long directorySizeQuota;

		// Token: 0x040022E0 RID: 8928
		private long perFileSizeQuota;

		// Token: 0x040022E1 RID: 8929
		private ProtocolLoggingLevel protocolLoggingLevel;
	}
}
