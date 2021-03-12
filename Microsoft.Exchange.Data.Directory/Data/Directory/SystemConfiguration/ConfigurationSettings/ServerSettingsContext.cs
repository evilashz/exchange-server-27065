using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000665 RID: 1637
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerSettingsContext : SettingsContextBase
	{
		// Token: 0x06004C7E RID: 19582 RVA: 0x0011AAFE File Offset: 0x00118CFE
		static ServerSettingsContext()
		{
			SettingsContextBase.DefaultContextGetter = (() => ServerSettingsContext.LocalServer);
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0011AB44 File Offset: 0x00118D44
		private ServerSettingsContext() : base(null)
		{
			Server localServer = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localServer = Microsoft.Exchange.Data.Directory.SystemConfiguration.LocalServer.GetServer();
			});
			this.Initialize(localServer, Process.GetCurrentProcess().MainModule.ModuleName);
			this.DateCreated = DateTime.UtcNow;
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0011AB9D File Offset: 0x00118D9D
		public ServerSettingsContext(Server server, string processName, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.Initialize(server, processName);
			this.DateCreated = DateTime.UtcNow;
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0011ABB9 File Offset: 0x00118DB9
		public ServerSettingsContext(string serverName, string processName, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.serverName = serverName;
			this.processName = processName;
			this.DateCreated = DateTime.UtcNow;
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0011ABDB File Offset: 0x00118DDB
		public ServerSettingsContext(string serverName, string serverRole, string processName, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.serverName = serverName;
			this.serverRole = serverRole;
			this.processName = processName;
			this.DateCreated = DateTime.UtcNow;
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x0011AC08 File Offset: 0x00118E08
		public static ServerSettingsContext LocalServer
		{
			get
			{
				ServerSettingsContext result;
				lock (ServerSettingsContext.lockObj)
				{
					if (ServerSettingsContext.localServer == null || ServerSettingsContext.localServer.CacheExpired)
					{
						ServerSettingsContext.localServer = new ServerSettingsContext();
					}
					result = ServerSettingsContext.localServer;
				}
				return result;
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x0011AC68 File Offset: 0x00118E68
		public override Guid? ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x0011AC70 File Offset: 0x00118E70
		public override string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x0011AC78 File Offset: 0x00118E78
		public override ServerVersion ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x0011AC80 File Offset: 0x00118E80
		public override string ServerRole
		{
			get
			{
				return this.serverRole;
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x0011AC88 File Offset: 0x00118E88
		public override string ProcessName
		{
			get
			{
				return this.processName;
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x0011AC90 File Offset: 0x00118E90
		private bool CacheExpired
		{
			get
			{
				return this.DateCreated + TimeSpan.FromMinutes(15.0) < DateTime.UtcNow;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x0011ACB5 File Offset: 0x00118EB5
		// (set) Token: 0x06004C8B RID: 19595 RVA: 0x0011ACBD File Offset: 0x00118EBD
		private DateTime DateCreated { get; set; }

		// Token: 0x06004C8C RID: 19596 RVA: 0x0011ACC8 File Offset: 0x00118EC8
		private void Initialize(Server server, string processName)
		{
			if (server != null)
			{
				this.serverGuid = new Guid?(server.Guid);
				this.serverName = server.Name;
				this.serverVersion = server.AdminDisplayVersion;
				this.serverRole = ExchangeServer.ConvertE15ServerRoleToOutput(server.CurrentServerRole).ToString();
			}
			else
			{
				this.serverName = NativeHelpers.GetLocalComputerFqdn(false);
			}
			this.processName = processName;
		}

		// Token: 0x0400345D RID: 13405
		private static ServerSettingsContext localServer;

		// Token: 0x0400345E RID: 13406
		private static object lockObj = new object();

		// Token: 0x0400345F RID: 13407
		private Guid? serverGuid;

		// Token: 0x04003460 RID: 13408
		private string serverName;

		// Token: 0x04003461 RID: 13409
		private ServerVersion serverVersion;

		// Token: 0x04003462 RID: 13410
		private string serverRole;

		// Token: 0x04003463 RID: 13411
		private string processName;
	}
}
