using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200091F RID: 2335
	internal class UpgradeConfigurationFrom14Task : SessionTask
	{
		// Token: 0x060052FE RID: 21246 RVA: 0x00156F84 File Offset: 0x00155184
		public UpgradeConfigurationFrom14Task(ExchangeObjectVersion currentVersion) : base(HybridStrings.HybridUpgradeFrom14TaskName, 1)
		{
			this.currentVersion = currentVersion;
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00156FB4 File Offset: 0x001551B4
		public override bool NeedsConfiguration(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoHybridConfigurationObjectVersion(taskContext.HybridConfigurationObject.ExchangeVersion));
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoHybridConfigurationEngineVersion(this.currentVersion));
			bool flag = base.NeedsConfiguration(taskContext) || (taskContext.HybridConfigurationObject.ExchangeVersion.ExchangeBuild.Major == this.upgradeFrom.ExchangeBuild.Major && this.currentVersion.ExchangeBuild.Major == this.upgradeTo.ExchangeBuild.Major);
			if (!flag)
			{
				taskContext.Logger.LogInformation(HybridStrings.HybridInfoNoNeedToUpgrade);
			}
			return flag;
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0015706E File Offset: 0x0015526E
		public override bool Configure(ITaskContext taskContext)
		{
			if (!base.Configure(taskContext))
			{
				return false;
			}
			this.PromptUserForPermissionToUpgrade(taskContext);
			this.EnsureAllTransportServersAreCurrent(taskContext);
			this.EnsureTenantHasAlreadyBeenUpgraded(taskContext);
			this.UpgradeFopeConnectors(taskContext);
			this.RemoveUnnecessaryRemoteDomains(taskContext);
			this.ClearUnusedConfigurationAttributes(taskContext);
			this.UpgradeHybridConfigurationObjectVersion(taskContext);
			return true;
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x001570AD File Offset: 0x001552AD
		public override bool ValidateConfiguration(ITaskContext taskContext)
		{
			if (!base.ValidateConfiguration(taskContext))
			{
				return false;
			}
			this.ValidateFopeConnectorsAreUpgraded(taskContext);
			this.ValidateUnnecessaryRemoteDomainsAreRemoved(taskContext);
			this.ValidateUnusedConfigurationAttributesAreCleared(taskContext);
			this.ValidateHybridConfigurationObjectVersionUpgraded(taskContext);
			return true;
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x001570D8 File Offset: 0x001552D8
		private void PromptUserForPermissionToUpgrade(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoCheckForPermissionToUpgrade);
			if (!taskContext.Parameters.Get<bool>("_forceUpgrade") && !taskContext.UI.ShouldContinue(HybridStrings.HybridUpgradePrompt))
			{
				throw new LocalizedException(HybridStrings.ErrorHybridMustBeUpgraded);
			}
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x0015712C File Offset: 0x0015532C
		private void EnsureAllTransportServersAreCurrent(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoVerifyTransportServers);
			List<ADObjectId> list = taskContext.HybridConfigurationObject.SendingTransportServers.ToList<ADObjectId>();
			list.AddRange(taskContext.HybridConfigurationObject.ReceivingTransportServers);
			foreach (ADObjectId adobjectId in list)
			{
				IExchangeServer exchangeServer = taskContext.OnPremisesSession.GetExchangeServer(adobjectId.Name);
				ServerVersion adminDisplayVersion = exchangeServer.AdminDisplayVersion;
				if (adminDisplayVersion.Major < (int)this.upgradeTo.ExchangeBuild.Major)
				{
					throw new LocalizedException(HybridStrings.ErrorHybridUpgradeNotAllTransportServersProperVersion);
				}
			}
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x001571E8 File Offset: 0x001553E8
		private void EnsureTenantHasAlreadyBeenUpgraded(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoVerifyTenantHasBeenUpgraded);
			IOrganizationConfig organizationConfig = taskContext.TenantSession.GetOrganizationConfig();
			if (organizationConfig.AdminDisplayVersion.ExchangeBuild < this.upgradeTo.ExchangeBuild || organizationConfig.IsUpgradingOrganization)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridTenenatUpgradeRequired);
			}
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0015725C File Offset: 0x0015545C
		private void UpgradeFopeConnectors(ITaskContext taskContext)
		{
			MultiValuedProperty<SmtpDomain> multiValuedProperty = new MultiValuedProperty<SmtpDomain>();
			foreach (SmtpDomain item in base.TaskContext.HybridConfigurationObject.Domains)
			{
				multiValuedProperty.Add(item);
			}
			IOrganizationConfig organizationConfig = base.OnPremisesSession.GetOrganizationConfig();
			List<string> domains = new List<string>();
			OrganizationRelationship organizationRelationship = TaskCommon.GetOrganizationRelationship(base.OnPremisesSession, Configuration.OnPremGetOrgRel, domains);
			OrganizationRelationship organizationRelationship2 = TaskCommon.GetOrganizationRelationship(base.TenantSession, Configuration.TenantGetOrgRel, domains);
			if (organizationRelationship2 == null || organizationRelationship == null)
			{
				throw new LocalizedException(HybridStrings.InvalidOrganizationRelationship);
			}
			string onPremOrgRelationshipName = TaskCommon.GetOnPremOrgRelationshipName(organizationConfig);
			string tenantOrgRelationshipName = TaskCommon.GetTenantOrgRelationshipName(organizationConfig);
			SessionParameters sessionParameters = new SessionParameters();
			SessionParameters sessionParameters2 = new SessionParameters();
			sessionParameters.Set("Name", onPremOrgRelationshipName);
			sessionParameters2.Set("Name", tenantOrgRelationshipName);
			base.OnPremisesSession.SetOrganizationRelationship(organizationRelationship.Identity, sessionParameters);
			base.TenantSession.SetOrganizationRelationship(organizationRelationship2.Identity, sessionParameters2);
			organizationRelationship2 = TaskCommon.GetOrganizationRelationship(base.TenantSession, tenantOrgRelationshipName, domains);
			if (organizationRelationship2 == null)
			{
				throw new LocalizedException(HybridStrings.InvalidOrganizationRelationship);
			}
			IInboundConnector inboundConnector = base.TenantSession.GetInboundConnectors().FirstOrDefault((IInboundConnector x) => x.ConnectorSource == TenantConnectorSource.HybridWizard);
			if (inboundConnector == null)
			{
				throw new LocalizedException(HybridStrings.ErrorNoInboundConnector);
			}
			base.TenantSession.RenameInboundConnector(inboundConnector, Configuration.InboundConnectorName(organizationConfig.Guid.ToString()));
			IOutboundConnector outboundConnector = base.TenantSession.GetOutboundConnectors().FirstOrDefault((IOutboundConnector x) => x.ConnectorSource == TenantConnectorSource.HybridWizard);
			if (outboundConnector == null)
			{
				throw new LocalizedException(HybridStrings.ErrorNoOutboundConnector);
			}
			base.TenantSession.RenameOutboundConnector(outboundConnector, Configuration.OutboundConnectorName(organizationConfig.Guid.ToString()));
			base.TenantSession.NewOnPremisesOrganization(organizationConfig, multiValuedProperty, inboundConnector, outboundConnector, organizationRelationship2);
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0015746C File Offset: 0x0015566C
		private void ValidateFopeConnectorsAreUpgraded(ITaskContext taskContext)
		{
			IOrganizationConfig organizationConfig = base.OnPremisesSession.GetOrganizationConfig();
			IOnPremisesOrganization onPremisesOrganization = base.TenantSession.GetOnPremisesOrganization(organizationConfig.Guid);
			IInboundConnector inboundConnector = base.TenantSession.GetInboundConnector(onPremisesOrganization.InboundConnector.ToString());
			IOutboundConnector outboundConnector = base.TenantSession.GetOutboundConnector(onPremisesOrganization.OutboundConnector.ToString());
			if (inboundConnector.ConnectorSource != TenantConnectorSource.HybridWizard || outboundConnector.ConnectorSource != TenantConnectorSource.HybridWizard)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridOnPremisesOrganizationWasNotCreatedWithUpgradedConnectors);
			}
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x00157518 File Offset: 0x00155718
		private void RemoveUnnecessaryRemoteDomains(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoRemovingUnnecessaryRemoteDomains);
			foreach (DomainContentConfig domainContentConfig in from x in taskContext.OnPremisesSession.GetRemoteDomain()
			where (x.Name ?? string.Empty).StartsWith("Hybrid Domain -")
			select x)
			{
				taskContext.OnPremisesSession.RemoveRemoteDomain(domainContentConfig.Identity);
			}
			foreach (DomainContentConfig domainContentConfig2 in from x in taskContext.TenantSession.GetRemoteDomain()
			where (x.Name ?? string.Empty).StartsWith("Hybrid Domain -")
			select x)
			{
				taskContext.TenantSession.RemoveRemoteDomain(domainContentConfig2.Identity);
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x00157650 File Offset: 0x00155850
		private void ValidateUnnecessaryRemoteDomainsAreRemoved(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoValidatingUnnecessaryRemoteDomainsAreRemoved);
			DomainContentConfig domainContentConfig = (from x in taskContext.OnPremisesSession.GetRemoteDomain()
			where (x.Name ?? string.Empty).StartsWith("Hybrid Domain -")
			select x).FirstOrDefault<DomainContentConfig>();
			if (domainContentConfig != null)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridOnPremRemoteDomainNotRemoved(domainContentConfig.Name));
			}
			DomainContentConfig domainContentConfig2 = (from x in taskContext.TenantSession.GetRemoteDomain()
			where (x.Name ?? string.Empty).StartsWith("Hybrid Domain -")
			select x).FirstOrDefault<DomainContentConfig>();
			if (domainContentConfig2 != null)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridTenantRemoteDomainNotRemoved(domainContentConfig2.Name));
			}
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x00157700 File Offset: 0x00155900
		private void ClearUnusedConfigurationAttributes(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoClearingUnusedHybridConfigurationProperties);
			taskContext.HybridConfigurationObject.ClientAccessServers.Clear();
			taskContext.HybridConfigurationObject.ExternalIPAddresses.Clear();
			try
			{
				taskContext.HybridConfigurationObject.Session.Save(taskContext.HybridConfigurationObject);
			}
			catch (InvalidOperationException)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridUpgradedTo2013);
			}
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00157778 File Offset: 0x00155978
		private void ValidateUnusedConfigurationAttributesAreCleared(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoValidateUnusedConfigurationAttributesAreCleared);
			if (taskContext.HybridConfigurationObject.ClientAccessServers.Count != 0)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridClientAccessServersNotCleared);
			}
			if (taskContext.HybridConfigurationObject.ExternalIPAddresses.Count != 0)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridExternalIPAddressesNotCleared);
			}
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x001577D4 File Offset: 0x001559D4
		private void UpgradeHybridConfigurationObjectVersion(ITaskContext taskContext)
		{
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoUpdatingHybridConfigurationVersion);
			taskContext.HybridConfigurationObject.SetExchangeVersion(this.upgradeTo);
			try
			{
				taskContext.HybridConfigurationObject.Session.Save(taskContext.HybridConfigurationObject);
			}
			catch (InvalidOperationException)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridUpgradedTo2013);
			}
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0015783C File Offset: 0x00155A3C
		private void ValidateHybridConfigurationObjectVersionUpgraded(ITaskContext taskContext)
		{
			if (taskContext.HybridConfigurationObject.ExchangeVersion != this.upgradeTo)
			{
				throw new LocalizedException(HybridStrings.ErrorHybridConfigurationVersionNotUpdated);
			}
		}

		// Token: 0x04003034 RID: 12340
		public const string E14RemoteDomainPrefix = "Hybrid Domain -";

		// Token: 0x04003035 RID: 12341
		private readonly ExchangeObjectVersion upgradeFrom = ExchangeObjectVersion.Exchange2010;

		// Token: 0x04003036 RID: 12342
		private readonly ExchangeObjectVersion upgradeTo = ExchangeObjectVersion.Exchange2012;

		// Token: 0x04003037 RID: 12343
		private readonly ExchangeObjectVersion currentVersion;
	}
}
