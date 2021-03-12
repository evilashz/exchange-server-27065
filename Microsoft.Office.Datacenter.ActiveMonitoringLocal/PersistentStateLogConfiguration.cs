using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PersistentStateLogConfiguration : ILogConfiguration
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00020884 File Offset: 0x0001EA84
		public PersistentStateLogConfiguration(string logType, long maxLogDirectorySizeInBytes, long maxLogFileSizeInBytes)
		{
			this.logPrefix = logType;
			this.logType = logType;
			this.logComponentValue = logType;
			this.IsLoggingEnabled = Settings.IsPersistentStateEnabled;
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
							this.LogPath = "%systemdrive%\\Program Files\\Microsoft\\Exchange Server\\V15";
						}
					}
					catch (Exception)
					{
						this.LogPath = "%systemdrive%\\Program Files\\Microsoft\\Exchange Server\\V15";
					}
				}
				this.LogPath = Path.Combine(Path.Combine(this.LogPath, "Logging\\Monitoring\\PersistentState"), logType);
				this.MaxLogAge = TimeSpan.FromDays((double)Settings.MaxLogAge);
				this.MaxLogDirectorySizeInBytes = maxLogDirectorySizeInBytes;
				this.MaxLogFileSizeInBytes = maxLogFileSizeInBytes;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00020958 File Offset: 0x0001EB58
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00020960 File Offset: 0x0001EB60
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00020969 File Offset: 0x0001EB69
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002096C File Offset: 0x0001EB6C
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00020974 File Offset: 0x0001EB74
		public string LogPath { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002097D File Offset: 0x0001EB7D
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00020985 File Offset: 0x0001EB85
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0002098E File Offset: 0x0001EB8E
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00020996 File Offset: 0x0001EB96
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002099F File Offset: 0x0001EB9F
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x000209A7 File Offset: 0x0001EBA7
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x000209B0 File Offset: 0x0001EBB0
		public string LogComponent
		{
			get
			{
				return this.logComponentValue;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000209B8 File Offset: 0x0001EBB8
		public string LogPrefix
		{
			get
			{
				return this.logPrefix;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000209C0 File Offset: 0x0001EBC0
		public string LogType
		{
			get
			{
				return this.logType;
			}
		}

		// Token: 0x040005F6 RID: 1526
		private const string DefaultLogDirectoryPath = "%systemdrive%\\Program Files\\Microsoft\\Exchange Server\\V15";

		// Token: 0x040005F7 RID: 1527
		private readonly string logPrefix;

		// Token: 0x040005F8 RID: 1528
		private readonly string logType;

		// Token: 0x040005F9 RID: 1529
		private readonly string logComponentValue;
	}
}
