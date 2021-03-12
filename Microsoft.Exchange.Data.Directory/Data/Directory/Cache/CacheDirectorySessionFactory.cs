using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000094 RID: 148
	internal class CacheDirectorySessionFactory : DirectorySessionFactory
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x00026F70 File Offset: 0x00025170
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(sessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00026F90 File Offset: 0x00025190
		public override ITenantConfigurationSession CreateTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(sessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00026FB4 File Offset: 0x000251B4
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(sessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00026FD8 File Offset: 0x000251D8
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(sessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00026FFC File Offset: 0x000251FC
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, Guid externalDirectoryOrganizationId, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(ADSessionSettings.SessionSettingsFactory.Default.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId));
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00027026 File Offset: 0x00025226
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002702D File Offset: 0x0002522D
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00027034 File Offset: 0x00025234
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002703B File Offset: 0x0002523B
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00027044 File Offset: 0x00025244
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(sessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00027068 File Offset: 0x00025268
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScopes, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(sessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002708A File Offset: 0x0002528A
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScopes, int callerFileLine, string memberName, string callerFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00027091 File Offset: 0x00025291
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00027098 File Offset: 0x00025298
		public override IRecipientSession GetReducedRecipientSession(IRecipientSession baseSession, int callerFileLine, string memberName, string callerFilePath)
		{
			CacheDirectorySession cacheDirectorySession = new CacheDirectorySession(baseSession.SessionSettings);
			cacheDirectorySession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return cacheDirectorySession;
		}
	}
}
