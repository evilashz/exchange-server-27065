using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog
{
	// Token: 0x020000F7 RID: 247
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncHealthLogConfiguration
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00023118 File Offset: 0x00021318
		public SyncHealthLogConfiguration()
		{
			this.syncHealthLogMaxAge = TimeSpan.FromDays(14.0);
			this.syncHealthLogMaxFile = 10485760L;
			this.syncHealthLogMaxDirectorySize = 10737418240L;
			this.syncHealthLogPath = Path.Combine(SyncHealthLogConfiguration.exchangeInstallDirPath, "TransportRoles\\Logs\\SyncHealth");
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0002316F File Offset: 0x0002136F
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x00023177 File Offset: 0x00021377
		public bool SyncHealthLogEnabled
		{
			get
			{
				return this.syncHealthLogEnabled;
			}
			set
			{
				this.syncHealthLogEnabled = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00023180 File Offset: 0x00021380
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00023188 File Offset: 0x00021388
		public string SyncHealthLogPath
		{
			get
			{
				return this.syncHealthLogPath;
			}
			set
			{
				this.syncHealthLogPath = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00023191 File Offset: 0x00021391
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x00023199 File Offset: 0x00021399
		public TimeSpan SyncHealthLogMaxAge
		{
			get
			{
				return this.syncHealthLogMaxAge;
			}
			set
			{
				this.syncHealthLogMaxAge = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x000231A2 File Offset: 0x000213A2
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x000231AA File Offset: 0x000213AA
		public long SyncHealthLogMaxFileSize
		{
			get
			{
				return this.syncHealthLogMaxFile;
			}
			set
			{
				this.syncHealthLogMaxFile = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x000231B3 File Offset: 0x000213B3
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x000231BB File Offset: 0x000213BB
		public long SyncHealthLogMaxDirectorySize
		{
			get
			{
				return this.syncHealthLogMaxDirectorySize;
			}
			set
			{
				this.syncHealthLogMaxDirectorySize = value;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000231C4 File Offset: 0x000213C4
		public static SyncHealthLogConfiguration CreateSyncHubHealthLogConfiguration(Server server)
		{
			SyncUtilities.ThrowIfArgumentNull("server", server);
			if (!server.IsHubTransportServer)
			{
				throw new ArgumentException("Should be Hub Transport Server", "server");
			}
			SyncHealthLogConfiguration syncHealthLogConfiguration = new SyncHealthLogConfiguration();
			syncHealthLogConfiguration.SyncHealthLogEnabled = server.TransportSyncHubHealthLogEnabled;
			syncHealthLogConfiguration.SyncHealthLogMaxAge = server.TransportSyncHubHealthLogMaxAge;
			syncHealthLogConfiguration.SyncHealthLogMaxDirectorySize = (long)((double)server.TransportSyncHubHealthLogMaxDirectorySize);
			syncHealthLogConfiguration.SyncHealthLogMaxFileSize = (long)((double)server.TransportSyncHubHealthLogMaxFileSize);
			if (server.TransportSyncHubHealthLogFilePath != null && !string.IsNullOrEmpty(server.TransportSyncHubHealthLogFilePath.PathName))
			{
				syncHealthLogConfiguration.SyncHealthLogPath = server.TransportSyncHubHealthLogFilePath.PathName;
			}
			else
			{
				syncHealthLogConfiguration.SyncHealthLogEnabled = false;
			}
			return syncHealthLogConfiguration;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00023278 File Offset: 0x00021478
		public static SyncHealthLogConfiguration CreateSyncMailboxHealthLogConfiguration(Server server)
		{
			SyncUtilities.ThrowIfArgumentNull("server", server);
			if (!server.IsMailboxServer)
			{
				throw new ArgumentException("Should be Mailbox Server", "server");
			}
			SyncHealthLogConfiguration syncHealthLogConfiguration = new SyncHealthLogConfiguration();
			syncHealthLogConfiguration.SyncHealthLogEnabled = server.TransportSyncMailboxHealthLogEnabled;
			syncHealthLogConfiguration.SyncHealthLogMaxAge = server.TransportSyncMailboxHealthLogMaxAge;
			syncHealthLogConfiguration.SyncHealthLogMaxDirectorySize = (long)((double)server.TransportSyncMailboxHealthLogMaxDirectorySize);
			syncHealthLogConfiguration.SyncHealthLogMaxFileSize = (long)((double)server.TransportSyncMailboxHealthLogMaxFileSize);
			if (server.TransportSyncMailboxHealthLogFilePath != null && !string.IsNullOrEmpty(server.TransportSyncMailboxHealthLogFilePath.PathName))
			{
				syncHealthLogConfiguration.SyncHealthLogPath = server.TransportSyncMailboxHealthLogFilePath.PathName;
			}
			else
			{
				syncHealthLogConfiguration.SyncHealthLogEnabled = false;
			}
			return syncHealthLogConfiguration;
		}

		// Token: 0x040003F6 RID: 1014
		private const string DefaultRelativeSyncHealthLogPath = "TransportRoles\\Logs\\SyncHealth";

		// Token: 0x040003F7 RID: 1015
		private static readonly string exchangeInstallDirPath = Assembly.GetExecutingAssembly().Location + "\\..\\..\\";

		// Token: 0x040003F8 RID: 1016
		private bool syncHealthLogEnabled;

		// Token: 0x040003F9 RID: 1017
		private string syncHealthLogPath;

		// Token: 0x040003FA RID: 1018
		private TimeSpan syncHealthLogMaxAge;

		// Token: 0x040003FB RID: 1019
		private long syncHealthLogMaxFile;

		// Token: 0x040003FC RID: 1020
		private long syncHealthLogMaxDirectorySize;
	}
}
