using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x0200091A RID: 2330
	internal class OrganizationRelationshipTask : SessionTask
	{
		// Token: 0x060052CE RID: 21198 RVA: 0x00155A31 File Offset: 0x00153C31
		public OrganizationRelationshipTask() : base(HybridStrings.OrganizationRelationshipTaskName, 2)
		{
			this.addOnPremisesFedDomains = new List<string>();
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x060052CF RID: 21199 RVA: 0x00155A57 File Offset: 0x00153C57
		private SmtpDomain AutoDiscoverHybridDomain
		{
			get
			{
				return (from d in this.HybridDomains
				where d.AutoDiscover
				select d).FirstOrDefault<AutoDiscoverSmtpDomain>();
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x060052D0 RID: 21200 RVA: 0x00155A90 File Offset: 0x00153C90
		private SmtpDomain PreferredHybridDomain
		{
			get
			{
				AutoDiscoverSmtpDomain autoDiscoverSmtpDomain = (from d in this.HybridDomains
				where d.AutoDiscover
				select d).FirstOrDefault<AutoDiscoverSmtpDomain>();
				if (autoDiscoverSmtpDomain != null)
				{
					return autoDiscoverSmtpDomain;
				}
				return this.HybridDomains.First<AutoDiscoverSmtpDomain>();
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x060052D1 RID: 21201 RVA: 0x00155AE3 File Offset: 0x00153CE3
		private IEnumerable<AutoDiscoverSmtpDomain> HybridDomains
		{
			get
			{
				return from d in base.TaskContext.HybridConfigurationObject.Domains
				orderby d.Domain
				select d;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00155B17 File Offset: 0x00153D17
		private string TenantCoexistenceDomain
		{
			get
			{
				return base.TaskContext.Parameters.Get<string>("_hybridDomain");
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x060052D3 RID: 21203 RVA: 0x00155B30 File Offset: 0x00153D30
		private IFederationInformation TenantFederationInfo
		{
			get
			{
				if (this.RequiresFederationTrust() && this.tenantFederationInfo == null)
				{
					this.tenantFederationInfo = base.TenantSession.GetFederationInformation(this.PreferredHybridDomain.Domain);
					if (this.tenantFederationInfo == null)
					{
						this.tenantFederationInfo = base.TenantSession.GetFederationInformation(this.PreferredHybridDomain.Domain);
						if (this.tenantFederationInfo == null)
						{
							string text = string.Format("https://autodiscover.{0}/autodiscover/autodiscover.svc/WSSecurity", this.PreferredHybridDomain.Domain);
							string text2 = null;
							IFederatedOrganizationIdentifier federatedOrganizationIdentifier = base.OnPremisesSession.GetFederatedOrganizationIdentifier();
							if (federatedOrganizationIdentifier != null && federatedOrganizationIdentifier.AccountNamespace != null && !string.IsNullOrEmpty(federatedOrganizationIdentifier.AccountNamespace.Domain) && federatedOrganizationIdentifier.Domains != null)
							{
								text2 = federatedOrganizationIdentifier.AccountNamespace.Domain;
							}
							if (text2 != null && text != null)
							{
								this.tenantFederationInfo = base.OnPremisesSession.BuildFederationInformation(text2, text);
								base.AddLocalizedStringWarning(HybridStrings.WarningTenantGetFedInfoFailed(this.tenantFederationInfo.TargetAutodiscoverEpr));
							}
						}
					}
					if (this.tenantFederationInfo == null)
					{
						throw new LocalizedException(HybridStrings.ErrorFederationInfoNotFound(this.PreferredHybridDomain.Domain));
					}
				}
				return this.tenantFederationInfo;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x00155C4C File Offset: 0x00153E4C
		private IFederationInformation OnpremisesFederationInfo
		{
			get
			{
				if (this.RequiresFederationTrust() && this.onpremisesFederationInfo == null)
				{
					this.onpremisesFederationInfo = base.OnPremisesSession.GetFederationInformation(this.TenantCoexistenceDomain);
					if (this.onpremisesFederationInfo == null)
					{
						throw new LocalizedException(HybridStrings.WarningUnableToCommunicateWithAutoDiscoveryEP);
					}
				}
				return this.onpremisesFederationInfo;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x060052D5 RID: 21205 RVA: 0x00155C99 File Offset: 0x00153E99
		private IOrganizationConfig OnPremOrgConfig
		{
			get
			{
				return base.OnPremisesSession.GetOrganizationConfig();
			}
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00155CA6 File Offset: 0x00153EA6
		public override bool CheckPrereqs(ITaskContext taskContext)
		{
			if (!base.CheckPrereqs(taskContext))
			{
				base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.HybridInfoBasePrereqsFailed));
				return false;
			}
			return this.CheckTaskPrerequisites(taskContext);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00155CE0 File Offset: 0x00153EE0
		public override bool NeedsConfiguration(ITaskContext taskContext)
		{
			bool flag = base.NeedsConfiguration(taskContext);
			OrganizationRelationship existingOrgRel = taskContext.Parameters.Get<OrganizationRelationship>("_onPremOrgRel");
			OrganizationRelationship existingOrgRel2 = taskContext.Parameters.Get<OrganizationRelationship>("_tenantOrgRel");
			return flag || this.NeedProvisionOrganizationRelationship(base.OnPremisesSession, existingOrgRel, this.OnpremisesFederationInfo, new SmtpDomain[]
			{
				new SmtpDomain(this.TenantCoexistenceDomain)
			}, TaskCommon.GetOnPremOrgRelationshipName(this.OnPremOrgConfig)) || this.NeedProvisionOrganizationRelationship(base.TenantSession, existingOrgRel2, this.TenantFederationInfo, this.HybridDomains, TaskCommon.GetTenantOrgRelationshipName(this.OnPremOrgConfig)) || (this.RequiresFederationTrust() && (this.updateOnPremisesFedOrgId || this.updateTenantFedOrgId || this.addOnPremisesFedDomains.Count > 0));
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00155DA5 File Offset: 0x00153FA5
		public override bool Configure(ITaskContext taskContext)
		{
			return base.Configure(taskContext) && this.TaskConfigure(taskContext);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x00155DB9 File Offset: 0x00153FB9
		public override bool ValidateConfiguration(ITaskContext taskContext)
		{
			return base.ValidateConfiguration(taskContext) && this.CheckTaskPrerequisites(taskContext) && !this.NeedsConfiguration(taskContext);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x00155DF8 File Offset: 0x00153FF8
		private static bool VerifyAcceptedTokenIssuerUri(ICommonSession session, List<Uri> acceptedTokenIssuerUris)
		{
			IFederatedOrganizationIdentifier federatedOrganizationIdentifier = session.GetFederatedOrganizationIdentifier();
			if (federatedOrganizationIdentifier == null)
			{
				throw new LocalizedException(HybridStrings.ErrorFederationIDNotProvisioned);
			}
			IFederationTrust fedTrust = session.GetFederationTrust(federatedOrganizationIdentifier.DelegationTrustLink);
			if (fedTrust == null)
			{
				throw new LocalizedException(Strings.ErrorFederationTrustNotFound(federatedOrganizationIdentifier.DelegationTrustLink.Name));
			}
			return acceptedTokenIssuerUris.Any((Uri u) => u.Equals(fedTrust.TokenIssuerUri));
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00155E62 File Offset: 0x00154062
		private void Reset()
		{
			this.addOnPremisesFedDomains.Clear();
			this.accountNamespace = null;
			this.updateOnPremisesFedOrgId = false;
			this.updateTenantFedOrgId = false;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00155EB8 File Offset: 0x001540B8
		private bool CheckTaskPrerequisites(ITaskContext taskContext)
		{
			this.Reset();
			if (this.RequiresFederationTrust())
			{
				string value = Configuration.SignupDomainSuffix(taskContext.HybridConfigurationObject.ServiceInstance);
				foreach (AutoDiscoverSmtpDomain autoDiscoverSmtpDomain in this.HybridDomains)
				{
					if (!autoDiscoverSmtpDomain.Domain.EndsWith(value, StringComparison.InvariantCultureIgnoreCase) && autoDiscoverSmtpDomain.Domain.Length <= 32)
					{
						this.accountNamespace = autoDiscoverSmtpDomain.Domain;
						break;
					}
				}
				if (string.IsNullOrEmpty(this.accountNamespace))
				{
					base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorAccountNamespace));
					base.AddLocalizedStringError(HybridStrings.ErrorAccountNamespace);
					return false;
				}
				int num = base.OnPremisesSession.GetFederationTrust().Count<IFederationTrust>();
				if (num == 0)
				{
					base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, Strings.ErrorFederationTrustNotFound("")));
					base.AddLocalizedStringError(HybridStrings.ErrorNoFederationTrustFound);
					return false;
				}
				if (num > 1)
				{
					base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorMultipleFederationTrusts));
					base.AddLocalizedStringError(HybridStrings.ErrorMultipleFederationTrusts);
					return false;
				}
				IFederatedOrganizationIdentifier federatedOrganizationIdentifier = base.OnPremisesSession.GetFederatedOrganizationIdentifier();
				if (federatedOrganizationIdentifier == null)
				{
					this.updateOnPremisesFedOrgId = true;
				}
				else if (federatedOrganizationIdentifier.AccountNamespace == null || string.IsNullOrEmpty(federatedOrganizationIdentifier.AccountNamespace.Domain))
				{
					this.updateOnPremisesFedOrgId = true;
				}
				else
				{
					if (!federatedOrganizationIdentifier.Enabled)
					{
						base.Logger.LogInformation(HybridStrings.HybridInfoTaskLogTemplate(base.Name, HybridStrings.ErrorFederatedIdentifierDisabled));
						base.AddLocalizedStringError(HybridStrings.ErrorFederatedIdentifierDisabled);
						return false;
					}
					this.accountNamespace = federatedOrganizationIdentifier.AccountNamespace.Domain.Replace(FederatedOrganizationId.HybridConfigurationWellKnownSubDomain + ".", string.Empty);
					if (!string.Equals(TaskCommon.ToStringOrNull(federatedOrganizationIdentifier.DelegationTrustLink), Configuration.FederatedTrustIdentity))
					{
						this.updateOnPremisesFedOrgId = true;
					}
					else
					{
						this.updateOnPremisesFedOrgId = false;
						using (IEnumerator<string> enumerator2 = (from d in this.HybridDomains
						select d.Domain).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string hybridDomain = enumerator2.Current;
								if (!federatedOrganizationIdentifier.Domains.Any((FederatedDomain d) => string.Equals(d.Domain.Domain, hybridDomain, StringComparison.InvariantCultureIgnoreCase)))
								{
									this.addOnPremisesFedDomains.Add(hybridDomain);
								}
							}
						}
					}
					if (!TaskCommon.AreEqual(federatedOrganizationIdentifier.DefaultDomain, this.AutoDiscoverHybridDomain))
					{
						this.updateOnPremisesFedOrgId = true;
					}
				}
				if (this.updateOnPremisesFedOrgId)
				{
					foreach (string text in from d in this.HybridDomains
					select d.Domain)
					{
						if (!text.Equals(this.accountNamespace, StringComparison.InvariantCultureIgnoreCase))
						{
							this.addOnPremisesFedDomains.Add(text);
						}
					}
				}
				IFederatedOrganizationIdentifier federatedOrganizationIdentifier2 = base.TenantSession.GetFederatedOrganizationIdentifier();
				string text2 = (federatedOrganizationIdentifier2 != null && federatedOrganizationIdentifier2.DefaultDomain != null) ? federatedOrganizationIdentifier2.DefaultDomain.ToString() : null;
				if (text2 == null || !text2.Equals(this.TenantCoexistenceDomain, StringComparison.InvariantCultureIgnoreCase))
				{
					this.updateTenantFedOrgId = true;
				}
			}
			return true;
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00156264 File Offset: 0x00154464
		private bool TaskConfigure(ITaskContext taskContext)
		{
			IOrganizationConfig organizationConfig = taskContext.TenantSession.GetOrganizationConfig();
			if (organizationConfig.IsDehydrated)
			{
				try
				{
					taskContext.TenantSession.EnableOrganizationCustomization();
				}
				catch
				{
				}
			}
			if (this.RequiresFederationTrust())
			{
				if (this.updateOnPremisesFedOrgId)
				{
					IFederatedOrganizationIdentifier federatedOrganizationIdentifier = base.OnPremisesSession.GetFederatedOrganizationIdentifier();
					string text = (federatedOrganizationIdentifier != null && federatedOrganizationIdentifier.DelegationTrustLink != null) ? federatedOrganizationIdentifier.DelegationTrustLink.ToString() : Configuration.FederatedTrustIdentity;
					taskContext.OnPremisesSession.SetFederationTrustRefreshMetadata(text);
					SmtpDomain autoDiscoverHybridDomain = this.AutoDiscoverHybridDomain;
					string defaultDomain = (autoDiscoverHybridDomain != null && autoDiscoverHybridDomain.Domain != null) ? autoDiscoverHybridDomain.Domain : null;
					taskContext.OnPremisesSession.SetFederatedOrganizationIdentifier(this.accountNamespace, text, defaultDomain);
				}
				List<Uri> acceptedTokenIssuerUris = taskContext.Parameters.Get<List<Uri>>("_onPremAcceptedTokenIssuerUris");
				if (!OrganizationRelationshipTask.VerifyAcceptedTokenIssuerUri(base.OnPremisesSession, acceptedTokenIssuerUris))
				{
					throw new LocalizedException(HybridStrings.ErrorOnPremUsingConsumerLiveID);
				}
				acceptedTokenIssuerUris = taskContext.Parameters.Get<List<Uri>>("_tenantAcceptedTokenIssuerUris");
				if (!OrganizationRelationshipTask.VerifyAcceptedTokenIssuerUri(base.TenantSession, acceptedTokenIssuerUris))
				{
					throw new LocalizedException(HybridStrings.ErrorTenantUsingConsumerLiveID);
				}
				if (this.updateTenantFedOrgId)
				{
					base.TenantSession.SetFederatedOrganizationIdentifier(this.TenantCoexistenceDomain);
				}
				foreach (string domainName in this.addOnPremisesFedDomains)
				{
					taskContext.OnPremisesSession.AddFederatedDomain(domainName);
				}
			}
			OrganizationRelationship value = OrganizationRelationshipTask.ProvisionOrganizationRelationship(base.OnPremisesSession, taskContext.Parameters.Get<OrganizationRelationship>("_onPremOrgRel"), this.OnpremisesFederationInfo, new SmtpDomain[]
			{
				new SmtpDomain(this.TenantCoexistenceDomain)
			}, TaskCommon.GetOnPremOrgRelationshipName(this.OnPremOrgConfig));
			taskContext.Parameters.Set<OrganizationRelationship>("_onPremOrgRel", value);
			value = OrganizationRelationshipTask.ProvisionOrganizationRelationship(base.TenantSession, taskContext.Parameters.Get<OrganizationRelationship>("_tenantOrgRel"), this.TenantFederationInfo, this.HybridDomains, TaskCommon.GetTenantOrgRelationshipName(this.OnPremOrgConfig));
			taskContext.Parameters.Set<OrganizationRelationship>("_tenantOrgRel", value);
			return true;
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00156484 File Offset: 0x00154684
		private static OrganizationRelationship ProvisionOrganizationRelationship(ICommonSession session, OrganizationRelationship existingOrgRel, IFederationInformation federationInfo, IEnumerable<SmtpDomain> domains, string relationshipName)
		{
			if (existingOrgRel != null)
			{
				session.RemoveOrganizationRelationship(existingOrgRel.Identity.ToString());
			}
			string targetApplicationUri = (federationInfo != null) ? federationInfo.TargetApplicationUri : null;
			string targetAutodiscoverEpr = (federationInfo != null) ? federationInfo.TargetAutodiscoverEpr : null;
			session.NewOrganizationRelationship(relationshipName, targetApplicationUri, targetAutodiscoverEpr, domains);
			existingOrgRel = TaskCommon.GetOrganizationRelationship(session, relationshipName, from d in domains
			select d.Domain);
			if (existingOrgRel == null)
			{
				throw new LocalizedException(HybridStrings.ErrorOrgRelProvisionFailed(domains.First<SmtpDomain>().ToString()));
			}
			return existingOrgRel;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00156514 File Offset: 0x00154714
		private bool NeedProvisionOrganizationRelationship(ICommonSession session, OrganizationRelationship existingOrgRel, IFederationInformation federationInfo, IEnumerable<SmtpDomain> domains, string relationshipName)
		{
			return existingOrgRel == null || !TaskCommon.ContainsSame<SmtpDomain>(existingOrgRel.DomainNames, domains) || (this.RequiresFederationTrust() && (!string.Equals(TaskCommon.ToStringOrNull(existingOrgRel.TargetApplicationUri), federationInfo.TargetApplicationUri, StringComparison.InvariantCultureIgnoreCase) || !string.Equals(TaskCommon.ToStringOrNull(existingOrgRel.TargetAutodiscoverEpr), federationInfo.TargetAutodiscoverEpr, StringComparison.InvariantCultureIgnoreCase)));
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x00156574 File Offset: 0x00154774
		private bool RequiresFederationTrust()
		{
			return Configuration.RequiresFederationTrust(base.TaskContext.HybridConfigurationObject.ServiceInstance);
		}

		// Token: 0x04003016 RID: 12310
		private const int MaxAccountNamespaceLength = 32;

		// Token: 0x04003017 RID: 12311
		private List<string> addOnPremisesFedDomains;

		// Token: 0x04003018 RID: 12312
		private string accountNamespace;

		// Token: 0x04003019 RID: 12313
		private bool updateOnPremisesFedOrgId;

		// Token: 0x0400301A RID: 12314
		private bool updateTenantFedOrgId;

		// Token: 0x0400301B RID: 12315
		private IFederationInformation tenantFederationInfo;

		// Token: 0x0400301C RID: 12316
		private IFederationInformation onpremisesFederationInfo;
	}
}
