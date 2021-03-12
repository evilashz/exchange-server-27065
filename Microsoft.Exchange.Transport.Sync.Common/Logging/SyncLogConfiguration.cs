using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000087 RID: 135
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncLogConfiguration
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x000156AE File Offset: 0x000138AE
		public SyncLogConfiguration() : this(SyncLoggingLevel.None)
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000156B8 File Offset: 0x000138B8
		public SyncLogConfiguration(SyncLoggingLevel syncLoggingLevel)
		{
			this.softwareVersion = Assembly.GetExecutingAssembly().GetName().Version;
			this.logFilePrefix = string.Empty;
			this.logComponent = "SyncLogs";
			this.logFilePath = Path.Combine(SyncLogConfiguration.exchangeInstallDirPath, SyncLogConfiguration.defaultRelativePath);
			this.ageQuotaInHours = 168L;
			this.directorySizeQuota = 256000L;
			this.perFileSizeQuota = 10240L;
			this.syncLoggingLevel = syncLoggingLevel;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00015736 File Offset: 0x00013936
		public string SoftwareName
		{
			get
			{
				return "Microsoft Exchange Server";
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001573D File Offset: 0x0001393D
		public Version SoftwareVersion
		{
			get
			{
				return this.softwareVersion;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00015745 File Offset: 0x00013945
		public string LogTypeName
		{
			get
			{
				return "Sync Logs";
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0001574C File Offset: 0x0001394C
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00015754 File Offset: 0x00013954
		public string LogFilePrefix
		{
			get
			{
				return this.logFilePrefix;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentNullOrEmpty("LogFilePrefix", value);
				this.logFilePrefix = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00015768 File Offset: 0x00013968
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00015770 File Offset: 0x00013970
		public string LogComponent
		{
			get
			{
				return this.logComponent;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentNullOrEmpty("LogComponent", value);
				this.logComponent = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00015784 File Offset: 0x00013984
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0001578C File Offset: 0x0001398C
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00015795 File Offset: 0x00013995
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0001579D File Offset: 0x0001399D
		public string LogFilePath
		{
			get
			{
				return this.logFilePath;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentNullOrEmpty("LogFilePath", value);
				this.logFilePath = Path.Combine(SyncLogConfiguration.exchangeInstallDirPath, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000157BB File Offset: 0x000139BB
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000157C3 File Offset: 0x000139C3
		public long AgeQuotaInHours
		{
			get
			{
				return this.ageQuotaInHours;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentLessThanZero("ageQuotaInHours", value);
				this.ageQuotaInHours = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000157D7 File Offset: 0x000139D7
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x000157DF File Offset: 0x000139DF
		public long DirectorySizeQuota
		{
			get
			{
				return this.directorySizeQuota;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentLessThanZero("DirectorySizeQuota", value);
				this.directorySizeQuota = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x000157F3 File Offset: 0x000139F3
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x000157FB File Offset: 0x000139FB
		public long PerFileSizeQuota
		{
			get
			{
				return this.perFileSizeQuota;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentLessThanZero("PerFileSizeQuota", value);
				this.perFileSizeQuota = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001580F File Offset: 0x00013A0F
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00015817 File Offset: 0x00013A17
		public SyncLoggingLevel SyncLoggingLevel
		{
			get
			{
				return this.syncLoggingLevel;
			}
			set
			{
				this.syncLoggingLevel = value;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00015820 File Offset: 0x00013A20
		public LogSchema CreateLogSchema(string[] fields)
		{
			return new LogSchema(this.SoftwareName, this.SoftwareVersion.ToString(), this.LogTypeName, fields);
		}

		// Token: 0x040001D5 RID: 469
		private const string SoftwareNameValue = "Microsoft Exchange Server";

		// Token: 0x040001D6 RID: 470
		private const string LogTypeNameValue = "Sync Logs";

		// Token: 0x040001D7 RID: 471
		private static readonly string exchangeInstallDirPath = Assembly.GetExecutingAssembly().Location + "\\..\\..\\";

		// Token: 0x040001D8 RID: 472
		private static readonly string defaultRelativePath = "TransportRoles\\Logs\\SyncLog";

		// Token: 0x040001D9 RID: 473
		private Version softwareVersion;

		// Token: 0x040001DA RID: 474
		private string logFilePrefix;

		// Token: 0x040001DB RID: 475
		private string logComponent;

		// Token: 0x040001DC RID: 476
		private bool enabled;

		// Token: 0x040001DD RID: 477
		private string logFilePath;

		// Token: 0x040001DE RID: 478
		private long ageQuotaInHours;

		// Token: 0x040001DF RID: 479
		private long directorySizeQuota;

		// Token: 0x040001E0 RID: 480
		private long perFileSizeQuota;

		// Token: 0x040001E1 RID: 481
		private SyncLoggingLevel syncLoggingLevel;
	}
}
