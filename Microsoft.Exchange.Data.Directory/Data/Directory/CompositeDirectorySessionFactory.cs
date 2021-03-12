using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C7 RID: 199
	internal class CompositeDirectorySessionFactory : DirectorySessionFactory
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0002F070 File Offset: 0x0002D270
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new CompositeTenantConfigurationSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantConfigurationSession(consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath), false);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002F0A8 File Offset: 0x0002D2A8
		public override ITenantConfigurationSession CreateTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new CompositeTenantConfigurationSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantConfigurationSession(readOnly, consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(readOnly, consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath), false);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002F0E4 File Offset: 0x0002D2E4
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			bool cacheSessionForDeletingOnly = true;
			if (networkCredential == null && string.IsNullOrEmpty(domainController))
			{
				cacheSessionForDeletingOnly = false;
			}
			return new CompositeTenantConfigurationSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath), cacheSessionForDeletingOnly);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002F138 File Offset: 0x0002D338
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			bool cacheSessionForDeletingOnly = true;
			if (networkCredential == null && string.IsNullOrEmpty(domainController))
			{
				cacheSessionForDeletingOnly = false;
			}
			return new CompositeTenantConfigurationSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath), cacheSessionForDeletingOnly);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002F190 File Offset: 0x0002D390
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, Guid externalDirectoryOrganizationId, int callerFileLine, string memberName, string callerFilePath)
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
			if (adsessionSettings == null)
			{
				return null;
			}
			return new CompositeTenantConfigurationSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantConfigurationSession(consistencyMode, adsessionSettings, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(consistencyMode, adsessionSettings, callerFileLine, memberName, callerFilePath), false);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002F1D3 File Offset: 0x0002D3D3
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002F1E6 File Offset: 0x0002D3E6
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(readOnly, consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002F1FC File Offset: 0x0002D3FC
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002F220 File Offset: 0x0002D420
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			return DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002F248 File Offset: 0x0002D448
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			bool cacheSessionForDeletingOnly = true;
			if (networkCredential == null && string.IsNullOrEmpty(domainController))
			{
				cacheSessionForDeletingOnly = false;
			}
			return new CompositeTenantRecipientSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath), cacheSessionForDeletingOnly);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002F2A4 File Offset: 0x0002D4A4
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScopes, int callerFileLine, string memberName, string callerFilePath)
		{
			bool cacheSessionForDeletingOnly = true;
			if (networkCredential == null && string.IsNullOrEmpty(domainController))
			{
				cacheSessionForDeletingOnly = false;
			}
			return new CompositeTenantRecipientSession(DirectorySessionFactory.CacheSessionFactory.CreateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScopes, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.CreateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScopes, callerFileLine, memberName, callerFilePath), cacheSessionForDeletingOnly);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002F304 File Offset: 0x0002D504
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScopes, int callerFileLine, string memberName, string callerFilePath)
		{
			return DirectorySessionFactory.NonCacheSessionFactory.CreateRootOrgRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScopes, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002F330 File Offset: 0x0002D530
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return DirectorySessionFactory.NonCacheSessionFactory.CreateRootOrgRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002F358 File Offset: 0x0002D558
		public override IRecipientSession GetReducedRecipientSession(IRecipientSession baseSession, int callerFileLine, string memberName, string callerFilePath)
		{
			return new CompositeRecipientSession(DirectorySessionFactory.CacheSessionFactory.GetReducedRecipientSession(baseSession, callerFileLine, memberName, callerFilePath), DirectorySessionFactory.NonCacheSessionFactory.GetReducedRecipientSession(baseSession, callerFileLine, memberName, callerFilePath), true);
		}
	}
}
