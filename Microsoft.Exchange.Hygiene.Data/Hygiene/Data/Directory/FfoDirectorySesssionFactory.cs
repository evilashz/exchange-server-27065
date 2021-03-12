using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000D6 RID: 214
	internal class FfoDirectorySesssionFactory : DirectorySessionFactory
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00019206 File Offset: 0x00017406
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new FfoTenantConfigurationSession(consistencyMode, sessionSettings);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001920F File Offset: 0x0001740F
		public override ITenantConfigurationSession CreateTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new FfoTenantConfigurationSession(readOnly, consistencyMode, sessionSettings);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00019219 File Offset: 0x00017419
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new FfoTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00019227 File Offset: 0x00017427
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			return new FfoTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00019238 File Offset: 0x00017438
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, Guid externalDirectoryOrganizationId, int callerFileLine, string memberName, string callerFilePath)
		{
			ADObjectId tenantId = new ADObjectId(DalHelper.GetTenantDistinguishedName(externalDirectoryOrganizationId.ToString()), externalDirectoryOrganizationId);
			return new FfoTenantConfigurationSession(tenantId);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00019264 File Offset: 0x00017464
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new ADTopologyConfigurationSession(consistencyMode, sessionSettings);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001926D File Offset: 0x0001746D
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new ADTopologyConfigurationSession(readOnly, consistencyMode, sessionSettings);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00019277 File Offset: 0x00017477
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new ADTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00019285 File Offset: 0x00017485
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			return new ADTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00019295 File Offset: 0x00017495
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new FfoTenantRecipientSession(true, readOnly, consistencyMode, networkCredential, sessionSettings);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000192A5 File Offset: 0x000174A5
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			return new FfoTenantRecipientSession(true, readOnly, consistencyMode, networkCredential, sessionSettings);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000192B5 File Offset: 0x000174B5
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			return new ADRootOrganizationRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000192C7 File Offset: 0x000174C7
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			return new ADRootOrganizationRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000192DC File Offset: 0x000174DC
		public override IRecipientSession GetReducedRecipientSession(IRecipientSession baseSession, int callerFileLine, string memberName, string callerFilePath)
		{
			IRecipientSession recipientSession;
			if (baseSession is ADRootOrganizationRecipientSession)
			{
				ADRootOrganizationRecipientSession adrootOrganizationRecipientSession = new ADRootOrganizationRecipientSession(baseSession.DomainController, null, CultureInfo.CurrentCulture.LCID, true, baseSession.ConsistencyMode, baseSession.NetworkCredential, baseSession.SessionSettings);
				adrootOrganizationRecipientSession.EnableReducedRecipientSession();
				recipientSession = adrootOrganizationRecipientSession;
			}
			else
			{
				FfoTenantRecipientSession ffoTenantRecipientSession = new FfoTenantRecipientSession(baseSession.UseConfigNC, true, baseSession.ConsistencyMode, baseSession.NetworkCredential, baseSession.SessionSettings);
				ffoTenantRecipientSession.EnableReducedRecipientSession();
				recipientSession = ffoTenantRecipientSession;
			}
			recipientSession.UseGlobalCatalog = baseSession.UseGlobalCatalog;
			return recipientSession;
		}
	}
}
