using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000DF RID: 223
	public abstract class TeamMailboxDiagnosticsBase : DataAccessTask<ADUser>
	{
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0003D57E File Offset: 0x0003B77E
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x0003D595 File Offset: 0x0003B795
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public RecipientIdParameter Identity
		{
			get
			{
				return (RecipientIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x0003D5CE File Offset: 0x0003B7CE
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

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x0003D5E6 File Offset: 0x0003B7E6
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x0003D5FD File Offset: 0x0003B7FD
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0003D610 File Offset: 0x0003B810
		internal Dictionary<ADUser, ExchangePrincipal> TMPrincipals
		{
			get
			{
				return this.tmPrincipals;
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003D618 File Offset: 0x0003B818
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 121, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\TeamMailbox\\TeamMailboxDiagnosticsBase.cs");
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003D660 File Offset: 0x0003B860
		protected override void InternalValidate()
		{
			OptionalIdentityData optionalIdentityData = new OptionalIdentityData();
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.TeamMailbox);
			queryFilter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
			if (base.ParameterSetName != "TeamMailboxITPro")
			{
				ADObjectId propertyValue = null;
				if (!base.TryGetExecutingUserId(out propertyValue))
				{
					base.WriteError(new InvalidOperationException(Strings.CouldNotGetExecutingUser), ErrorCategory.InvalidOperation, null);
				}
				QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.DelegateListLink, propertyValue);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter3
				});
				if (this.additionalConstrainedIdentity != null)
				{
					queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.DelegateListLink, this.additionalConstrainedIdentity);
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter3
					});
				}
			}
			optionalIdentityData.AdditionalFilter = queryFilter;
			LocalizedString? localizedString = null;
			IEnumerable<ADUser> dataObjects = base.GetDataObjects<ADUser>(this.Identity, base.DataSession, null, optionalIdentityData, out localizedString);
			foreach (ADUser aduser in dataObjects)
			{
				ExchangePrincipal value = ExchangePrincipal.FromADUser(((IRecipientSession)base.DataSession).SessionSettings, aduser, RemotingOptions.AllowCrossSite);
				this.tmPrincipals.Add(aduser, value);
			}
			if (this.tmPrincipals.Count == 0)
			{
				base.WriteError(new InvalidOperationException(Strings.CouldNotLocateAnyTeamMailbox), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003D800 File Offset: 0x0003BA00
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			TaskLogger.LogExit();
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003D820 File Offset: 0x0003BA20
		private OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 228, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\TeamMailbox\\TeamMailboxDiagnosticsBase.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x04000308 RID: 776
		private Dictionary<ADUser, ExchangePrincipal> tmPrincipals = new Dictionary<ADUser, ExchangePrincipal>();

		// Token: 0x04000309 RID: 777
		protected ADObjectId additionalConstrainedIdentity;

		// Token: 0x020000E0 RID: 224
		public enum TargetType : uint
		{
			// Token: 0x0400030B RID: 779
			All,
			// Token: 0x0400030C RID: 780
			Document,
			// Token: 0x0400030D RID: 781
			Membership,
			// Token: 0x0400030E RID: 782
			Maintenance
		}
	}
}
