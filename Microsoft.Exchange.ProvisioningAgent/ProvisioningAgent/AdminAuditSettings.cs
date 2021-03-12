using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Win32;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000002 RID: 2
	internal class AdminAuditSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static AdminAuditSettings Instance
		{
			get
			{
				return AdminAuditSettings.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		public bool BypassForwardSync { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		public int SessionCacheSize { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F9 File Offset: 0x000002F9
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002101 File Offset: 0x00000301
		public TimeSpan SessionExpirationTime { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002112 File Offset: 0x00000312
		public int MaxNumberOfMailboxSessionsPerMailbox { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		private AdminAuditSettings()
		{
			this.LoadSettings();
			this.CheckTestDomain();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002130 File Offset: 0x00000330
		private void LoadSettings()
		{
			this.BypassForwardSync = AdminAuditSettings.DefaultBypassForwardSync;
			this.SessionCacheSize = AdminAuditSettings.DefaultSessionCacheSize;
			this.SessionExpirationTime = AdminAuditSettings.DefaultSessionExpirationTime;
			this.MaxNumberOfMailboxSessionsPerMailbox = AdminAuditSettings.DefaultMaxNumberOfMailboxSessionsPerMailbox;
			Exception ex = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(AdminAuditSettings.AdminAuditLogKeyRoot))
				{
					if (registryKey == null)
					{
						return;
					}
					this.BypassForwardSync = ((int)registryKey.GetValue(AdminAuditSettings.BypassForwardSyncValueName, AdminAuditSettings.DefaultBypassForwardSync) != 0);
					int num = (int)registryKey.GetValue(AdminAuditSettings.SessionCacheSizeValueName, AdminAuditSettings.DefaultSessionCacheSize);
					if (num > 0)
					{
						this.SessionCacheSize = num;
					}
					num = (int)registryKey.GetValue(AdminAuditSettings.MaxNumberOfMailboxSessionsPerMailboxName, AdminAuditSettings.DefaultMaxNumberOfMailboxSessionsPerMailbox);
					if (num > 0)
					{
						this.MaxNumberOfMailboxSessionsPerMailbox = num;
					}
					this.SessionExpirationTime = TimeSpan.Parse((string)registryKey.GetValue(AdminAuditSettings.SessionExpirationTimeValueName, AdminAuditSettings.DefaultSessionExpirationTime.ToString()));
				}
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (InvalidCastException ex3)
			{
				ex = ex3;
			}
			catch (FormatException ex4)
			{
				ex = ex4;
			}
			catch (IOException ex5)
			{
				ex = ex5;
			}
			catch (UnauthorizedAccessException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				ExTraceGlobals.AdminAuditLogTracer.TraceError<Exception>(0L, "Error occured when reading settings from registry. Exception: {0}", ex);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B4 File Offset: 0x000004B4
		private void CheckTestDomain()
		{
			ADObjectId rootDomainNamingContextForLocalForest = ADSession.GetRootDomainNamingContextForLocalForest();
			string text = rootDomainNamingContextForLocalForest.DomainId.ToCanonicalName().ToLower();
			if (text.Contains("extest.microsoft.com"))
			{
				this.SessionCacheSize = Math.Min(this.SessionCacheSize, AdminAuditSettings.TestDomainMaxCacheSize);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly string AuditLogKeyName = "AuditLog";

		// Token: 0x04000002 RID: 2
		private static readonly string AdminAuditLogKeyName = "AdminAuditLog";

		// Token: 0x04000003 RID: 3
		private static readonly string AdminAuditLogKeyRoot = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\{1}\\{2}", "v15", AdminAuditSettings.AuditLogKeyName, AdminAuditSettings.AdminAuditLogKeyName);

		// Token: 0x04000004 RID: 4
		private static readonly string BypassForwardSyncValueName = "BypassForwardSync";

		// Token: 0x04000005 RID: 5
		private static readonly string SessionCacheSizeValueName = "SessionCacheSize";

		// Token: 0x04000006 RID: 6
		private static readonly string SessionExpirationTimeValueName = "SessionExpirationTime";

		// Token: 0x04000007 RID: 7
		private static readonly string MaxNumberOfMailboxSessionsPerMailboxName = "MaxNumberOfMailboxSessionsPerMailbox";

		// Token: 0x04000008 RID: 8
		private static readonly bool DefaultBypassForwardSync = false;

		// Token: 0x04000009 RID: 9
		private static readonly int DefaultSessionCacheSize = 1000;

		// Token: 0x0400000A RID: 10
		private static readonly TimeSpan DefaultSessionExpirationTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400000B RID: 11
		private static readonly int DefaultMaxNumberOfMailboxSessionsPerMailbox = 5;

		// Token: 0x0400000C RID: 12
		private static readonly int TestDomainMaxCacheSize = 3;

		// Token: 0x0400000D RID: 13
		private static AdminAuditSettings instance = new AdminAuditSettings();
	}
}
