using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000DE RID: 222
	[Cmdlet("Get", "SiteMailbox", DefaultParameterSetName = "TeamMailboxIW")]
	public sealed class GetSiteMailbox : GetRecipientWithAddressListBase<RecipientIdParameter, ADUser>
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0003D032 File Offset: 0x0003B232
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x0003D03A File Offset: 0x0003B23A
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public new RecipientIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003D043 File Offset: 0x0003B243
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x0003D04B File Offset: 0x0003B24B
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public new OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003D054 File Offset: 0x0003B254
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0003D07A File Offset: 0x0003B27A
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public SwitchParameter DeletedSiteMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["DeletedSiteMailbox"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DeletedSiteMailbox"] = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0003D092 File Offset: 0x0003B292
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x0003D09A File Offset: 0x0003B29A
		[Parameter(Mandatory = false)]
		public new string Anr
		{
			get
			{
				return base.Anr;
			}
			set
			{
				base.Anr = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0003D0A3 File Offset: 0x0003B2A3
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x0003D0C9 File Offset: 0x0003B2C9
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public SwitchParameter BypassOwnerCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassOwnerCheck"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BypassOwnerCheck"] = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0003D0E1 File Offset: 0x0003B2E1
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x0003D0E9 File Offset: 0x0003B2E9
		private new PSCredential Credential { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0003D0F2 File Offset: 0x0003B2F2
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x0003D0FA File Offset: 0x0003B2FA
		private new string Filter { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x0003D103 File Offset: 0x0003B303
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x0003D10B File Offset: 0x0003B30B
		private new SwitchParameter IgnoreDefaultScope { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0003D114 File Offset: 0x0003B314
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x0003D11C File Offset: 0x0003B31C
		private new OrganizationalUnitIdParameter OrganizationalUnit { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0003D125 File Offset: 0x0003B325
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x0003D12D File Offset: 0x0003B32D
		private new string SortBy { get; set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x0003D136 File Offset: 0x0003B336
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<TeamMailboxSchema>();
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0003D13D File Offset: 0x0003B33D
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetSiteMailbox.SortPropertiesArray;
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003D190 File Offset: 0x0003B390
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.ParameterSetName == "TeamMailboxIW" && !base.TryGetExecutingUserId(out this.executingUserId))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxCannotIdentifyTheUser), ExchangeErrorCategory.Client, this.Identity);
			}
			if (GetSiteMailbox.isMultiTenant && base.CurrentOrganizationId != null && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
			{
				ITenantConfigurationSession tenantConfigSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CurrentOrganizationId), 164, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\TeamMailbox\\GetSiteMailbox.cs");
				ExchangeConfigurationUnit exchangeConfigurationUnit = base.ProvisioningCache.TryAddAndGetOrganizationData<ExchangeConfigurationUnit>(CannedProvisioningCacheKeys.OrganizationCUContainer, base.CurrentOrganizationId, delegate()
				{
					ADObjectId configurationUnit = this.CurrentOrganizationId.ConfigurationUnit;
					IConfigurationSession configurationSession = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(tenantConfigSession, this.CurrentOrganizationId, true);
					return configurationSession.Read<ExchangeConfigurationUnit>(configurationUnit);
				});
				if (exchangeConfigurationUnit != null)
				{
					this.isHybridTenant = exchangeConfigurationUnit.IsDirSyncRunning;
				}
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0003D274 File Offset: 0x0003B474
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.TeamMailbox)
				});
				if (this.DeletedSiteMailbox && !this.isHybridTenant)
				{
					QueryFilter queryFilter2 = new AmbiguousNameResolutionFilter("MDEL:");
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					});
				}
				else
				{
					QueryFilter queryFilter3 = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUser),
						new OrFilter(new QueryFilter[]
						{
							new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientDisplayType, RecipientDisplayType.SyncedTeamMailboxUser),
							new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientDisplayType, RecipientDisplayType.ACLableSyncedTeamMailboxUser)
						})
					});
					queryFilter = new OrFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter3
					});
					if (base.ParameterSetName == "TeamMailboxIW")
					{
						QueryFilter queryFilter4 = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.DelegateListLink, this.executingUserId);
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							queryFilter4
						});
					}
				}
				if (base.InternalFilter != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						base.InternalFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003D3EC File Offset: 0x0003B5EC
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			ADUser aduser = dataObject as ADUser;
			if (aduser == null)
			{
				return true;
			}
			if (this.DeletedSiteMailbox)
			{
				return !TeamMailbox.IsPendingDeleteSiteMailbox(aduser);
			}
			if (TeamMailbox.IsPendingDeleteSiteMailbox(aduser))
			{
				return true;
			}
			if (!TeamMailbox.IsLocalTeamMailbox(aduser) && !TeamMailbox.IsRemoteTeamMailbox(aduser))
			{
				return true;
			}
			if (base.ParameterSetName == "TeamMailboxIW")
			{
				TeamMailbox teamMailbox = TeamMailbox.FromDataObject((ADUser)dataObject);
				return !teamMailbox.OwnersAndMembers.Contains(this.executingUserId);
			}
			return false;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003D46C File Offset: 0x0003B66C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			TeamMailbox teamMailbox = TeamMailbox.FromDataObject((ADUser)dataObject);
			teamMailbox.SetMyRole(this.executingUserId);
			if (this.executingUserId == null)
			{
				teamMailbox.ShowInMyClient = false;
			}
			else if (base.ExecutingUserOrganizationId == base.CurrentOrganizationId)
			{
				if (teamMailbox.Active)
				{
					ADUser aduser = (ADUser)base.GetDataObject<ADUser>(new MailboxIdParameter(this.executingUserId), base.DataSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.executingUserId.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.executingUserId.ToString())));
					teamMailbox.ShowInMyClient = aduser.TeamMailboxShowInClientList.Contains(teamMailbox.Id);
				}
				else
				{
					teamMailbox.ShowInMyClient = false;
				}
			}
			return teamMailbox;
		}

		// Token: 0x040002FF RID: 767
		private static readonly bool isMultiTenant = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

		// Token: 0x04000300 RID: 768
		private bool isHybridTenant;

		// Token: 0x04000301 RID: 769
		private ADObjectId executingUserId;

		// Token: 0x04000302 RID: 770
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			TeamMailboxSchema.DisplayName
		};
	}
}
