using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal sealed class ActiveDirectoryConnectivityContext
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000275F File Offset: 0x0000095F
		internal static ActiveDirectoryConnectivityContext CreateForActiveDirectoryConnectivity(TestActiveDirectoryConnectivityTask instance, MonitoringData monitoringData, string domainController)
		{
			return new ActiveDirectoryConnectivityContext(instance, monitoringData, domainController);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000276C File Offset: 0x0000096C
		private ActiveDirectoryConnectivityContext(TestActiveDirectoryConnectivityTask instance, MonitoringData monitoringData, string domainController)
		{
			this.instance = instance;
			this.monitoringData = monitoringData;
			this.domainController = domainController;
			this.logger = new TaskLoggerAdaptor(this.Instance);
			if (this.MonitoringData != null)
			{
				this.logger.Append(new StringLogger());
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000027BD File Offset: 0x000009BD
		internal MonitoringData MonitoringData
		{
			get
			{
				return this.monitoringData;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000027C5 File Offset: 0x000009C5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000027E0 File Offset: 0x000009E0
		internal string CurrentDomainController
		{
			get
			{
				if (string.IsNullOrEmpty(this.domainController))
				{
					return "Auto-Selected by ADDriver";
				}
				return this.domainController;
			}
			set
			{
				this.domainController = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027E9 File Offset: 0x000009E9
		internal DirectoryEntry CurrentDCDirectoryEntry
		{
			get
			{
				if (this.currentDCDirectoryEntry == null)
				{
					this.currentDCDirectoryEntry = ActiveDirectoryConnectivityContext.GetDirectoryEntry(this.CurrentDomainController);
				}
				return this.currentDCDirectoryEntry;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000280A File Offset: 0x00000A0A
		internal bool UseADDriver
		{
			get
			{
				return this.Instance.UseADDriver;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002817 File Offset: 0x00000A17
		internal TestActiveDirectoryConnectivityTask Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002820 File Offset: 0x00000A20
		internal IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
					this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.domainController, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, ConfigScopes.TenantSubTree, 166, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ActiveDirectory\\ActiveDirectoryConnectivityContext.cs");
				}
				return this.recipientSession;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002878 File Offset: 0x00000A78
		internal IConfigurationSession SystemConfigurationSession
		{
			get
			{
				if (this.systemConfigurationSession == null)
				{
					this.systemConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.CurrentDomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 188, "SystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ActiveDirectory\\ActiveDirectoryConnectivityContext.cs");
				}
				return this.systemConfigurationSession;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000028BF File Offset: 0x00000ABF
		internal ITopologyConfigurationSession SafeSystemConfigurationSession
		{
			get
			{
				if (this.safeSystemConfigurationSession == null)
				{
					this.safeSystemConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 207, "SafeSystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ActiveDirectory\\ActiveDirectoryConnectivityContext.cs");
				}
				return this.safeSystemConfigurationSession;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028F4 File Offset: 0x00000AF4
		internal void WriteVerbose(LocalizedString message)
		{
			this.logger.WriteVerbose(message);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002902 File Offset: 0x00000B02
		internal void WriteWarning(LocalizedString message)
		{
			this.logger.WriteWarning(message);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002910 File Offset: 0x00000B10
		internal void WriteDebug(LocalizedString message)
		{
			this.logger.WriteDebug(message);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000291E File Offset: 0x00000B1E
		internal string GetDiagnosticInfo(string prefix)
		{
			return this.logger.GetDiagnosticInfo(prefix);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000292C File Offset: 0x00000B2C
		internal static DirectoryEntry GetDirectoryEntry(string directoryServer)
		{
			string str = "LDAP://";
			str += ((!string.IsNullOrEmpty(directoryServer)) ? (directoryServer + "/") : string.Empty);
			string path = string.Empty;
			using (DirectoryEntry directoryEntry = new DirectoryEntry(str + "rootDSE"))
			{
				if (directoryEntry == null)
				{
					return null;
				}
				string str2 = directoryEntry.Properties["defaultNamingContext"].Value.ToString();
				path = str + str2;
			}
			return new DirectoryEntry(path);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal static T EnsureSingleObject<T>(Func<IEnumerable<T>> getObjects) where T : class
		{
			T t = default(T);
			foreach (T t2 in getObjects())
			{
				if (t != null)
				{
					throw new DataValidationException(new ObjectValidationError(Strings.MoreThanOneObjects(typeof(T).ToString()), null, null));
				}
				t = t2;
			}
			return t;
		}

		// Token: 0x0400001F RID: 31
		private readonly TestActiveDirectoryConnectivityTask instance;

		// Token: 0x04000020 RID: 32
		private readonly MonitoringData monitoringData;

		// Token: 0x04000021 RID: 33
		private readonly ChainedLogger logger;

		// Token: 0x04000022 RID: 34
		private IRecipientSession recipientSession;

		// Token: 0x04000023 RID: 35
		private IConfigurationSession systemConfigurationSession;

		// Token: 0x04000024 RID: 36
		private ITopologyConfigurationSession safeSystemConfigurationSession;

		// Token: 0x04000025 RID: 37
		private string domainController;

		// Token: 0x04000026 RID: 38
		private DirectoryEntry currentDCDirectoryEntry;
	}
}
