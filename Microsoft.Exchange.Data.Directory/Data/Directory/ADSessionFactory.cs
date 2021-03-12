using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200007E RID: 126
	internal class ADSessionFactory : DirectorySessionFactory
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0002115C File Offset: 0x0001F35C
		[Conditional("DEBUG")]
		private void EnsureSessionSettingsScope(ADSessionSettings sessionSettings, ADSessionFactory.IntendedUse intendedUse)
		{
			if (sessionSettings.ConfigScopes == ConfigScopes.TenantSubTree)
			{
				return;
			}
			bool flag = OrganizationId.ForestWideOrgId.Equals(sessionSettings.CurrentOrganizationId);
			bool flag2 = sessionSettings.ConfigScopes == ConfigScopes.AllTenants || (sessionSettings.ConfigScopes == ConfigScopes.TenantLocal && !flag);
			if (flag2 != (intendedUse == ADSessionFactory.IntendedUse.Tenant))
			{
				string message = string.Format("Invalid ADSessionFactory usage: the sessionSettings passed has ConfigScopes={0}, CurrentOrganizationId is {1}root org. Intended use: {2}", sessionSettings.ConfigScopes, flag ? string.Empty : "not ", intendedUse);
				ExTraceGlobals.GetConnectionTracer.TraceDebug((long)this.GetHashCode(), message);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000211EC File Offset: 0x0001F3EC
		internal static bool UseAggregateSession(ADSessionSettings sessionSettings)
		{
			ADServerSettings externalServerSettings = ADSessionSettings.ExternalServerSettings;
			ADDriverContext processADContext = ADSessionSettings.GetProcessADContext();
			bool flag = processADContext != null && processADContext.Mode == ContextMode.Setup;
			bool flag2 = externalServerSettings != null && externalServerSettings.ForceADInTemplateScope;
			bool flag3 = !ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("ConsumerMailboxScenarioDisabled");
			return flag3 && TemplateTenantConfiguration.IsTemplateTenant(sessionSettings.CurrentOrganizationId) && !sessionSettings.ForceADInTemplateScope && !flag2 && !flag;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00021254 File Offset: 0x0001F454
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTenantConfigurationSession adtenantConfigurationSession = new ADTenantConfigurationSession(consistencyMode, sessionSettings);
			adtenantConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtenantConfigurationSession;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00021278 File Offset: 0x0001F478
		public override ITenantConfigurationSession CreateTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTenantConfigurationSession adtenantConfigurationSession = new ADTenantConfigurationSession(readOnly, consistencyMode, sessionSettings);
			adtenantConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtenantConfigurationSession;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002129C File Offset: 0x0001F49C
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTenantConfigurationSession adtenantConfigurationSession = new ADTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings);
			adtenantConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtenantConfigurationSession;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000212C4 File Offset: 0x0001F4C4
		public override ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTenantConfigurationSession adtenantConfigurationSession = new ADTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
			adtenantConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtenantConfigurationSession;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000212F0 File Offset: 0x0001F4F0
		public override ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, Guid externalDirectoryOrganizationId, int callerFileLine, string memberName, string callerFilePath)
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
			if (adsessionSettings == null)
			{
				return null;
			}
			return this.CreateTenantConfigurationSession(consistencyMode, adsessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00021318 File Offset: 0x0001F518
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTopologyConfigurationSession adtopologyConfigurationSession = new ADTopologyConfigurationSession(consistencyMode, sessionSettings);
			adtopologyConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtopologyConfigurationSession;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002133C File Offset: 0x0001F53C
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTopologyConfigurationSession adtopologyConfigurationSession = new ADTopologyConfigurationSession(readOnly, consistencyMode, sessionSettings);
			adtopologyConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtopologyConfigurationSession;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00021360 File Offset: 0x0001F560
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTopologyConfigurationSession adtopologyConfigurationSession = new ADTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings);
			adtopologyConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtopologyConfigurationSession;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00021388 File Offset: 0x0001F588
		public override ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			ADTopologyConfigurationSession adtopologyConfigurationSession = new ADTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
			adtopologyConfigurationSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adtopologyConfigurationSession;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000213B4 File Offset: 0x0001F5B4
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			DirectorySessionBase directorySessionBase = ADSessionFactory.UseAggregateSession(sessionSettings) ? new AggregateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings) : new ADTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings);
			directorySessionBase.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return (ITenantRecipientSession)directorySessionBase;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00021400 File Offset: 0x0001F600
		public override ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			DirectorySessionBase directorySessionBase = ADSessionFactory.UseAggregateSession(sessionSettings) ? new AggregateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScope) : new ADTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
			directorySessionBase.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return (ITenantRecipientSession)directorySessionBase;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00021450 File Offset: 0x0001F650
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, int callerFileLine, string memberName, string callerFilePath)
		{
			ADRootOrganizationRecipientSession adrootOrganizationRecipientSession = new ADRootOrganizationRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScope);
			adrootOrganizationRecipientSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adrootOrganizationRecipientSession;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00021480 File Offset: 0x0001F680
		public override IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, int callerFileLine, string memberName, string callerFilePath)
		{
			ADRootOrganizationRecipientSession adrootOrganizationRecipientSession = new ADRootOrganizationRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings);
			adrootOrganizationRecipientSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adrootOrganizationRecipientSession;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000214AC File Offset: 0x0001F6AC
		public override IRecipientSession GetReducedRecipientSession(IRecipientSession baseSession, int callerFileLine, string memberName, string callerFilePath)
		{
			if (!(baseSession is ADRecipientObjectSession) && !(baseSession is CompositeTenantRecipientSession) && !(baseSession is CompositeRecipientSession))
			{
				throw new ArgumentException("baseSession");
			}
			ADRecipientObjectSession adrecipientObjectSession;
			if (baseSession is ADRootOrganizationRecipientSession)
			{
				adrecipientObjectSession = new ADRootOrganizationRecipientSession(baseSession.DomainController, null, CultureInfo.CurrentCulture.LCID, true, baseSession.ConsistencyMode, baseSession.NetworkCredential, baseSession.SessionSettings);
			}
			else
			{
				adrecipientObjectSession = new ADTenantRecipientSession(baseSession.DomainController, null, CultureInfo.CurrentCulture.LCID, true, baseSession.ConsistencyMode, baseSession.NetworkCredential, baseSession.SessionSettings);
			}
			adrecipientObjectSession.EnableReducedRecipientSession();
			adrecipientObjectSession.UseGlobalCatalog = baseSession.UseGlobalCatalog;
			adrecipientObjectSession.SetCallerInfo(callerFilePath, memberName, callerFileLine);
			return adrecipientObjectSession;
		}

		// Token: 0x0200007F RID: 127
		private enum IntendedUse
		{
			// Token: 0x04000272 RID: 626
			Tenant,
			// Token: 0x04000273 RID: 627
			RootOrg
		}
	}
}
