using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C5C RID: 3164
	[Cmdlet("Get", "SecurityPrincipal", DefaultParameterSetName = "Identity")]
	public sealed class GetSecurityPrincipal : GetMultitenancySystemConfigurationObjectTask<ExtendedSecurityPrincipalIdParameter, ExtendedSecurityPrincipal>
	{
		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x060077FD RID: 30717 RVA: 0x001E8EF0 File Offset: 0x001E70F0
		// (set) Token: 0x060077FE RID: 30718 RVA: 0x001E8F07 File Offset: 0x001E7107
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ExtendedOrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (ExtendedOrganizationalUnitIdParameter)base.Fields["OrganizationalUnit"];
			}
			set
			{
				base.Fields["OrganizationalUnit"] = value;
			}
		}

		// Token: 0x17002515 RID: 9493
		// (get) Token: 0x060077FF RID: 30719 RVA: 0x001E8F1A File Offset: 0x001E711A
		// (set) Token: 0x06007800 RID: 30720 RVA: 0x001E8F31 File Offset: 0x001E7131
		[ValidateNotNullOrEmpty]
		[Parameter]
		public SmtpDomain IncludeDomainLocalFrom
		{
			get
			{
				return (SmtpDomain)base.Fields["IncludeDomainLocalFrom"];
			}
			set
			{
				base.Fields["IncludeDomainLocalFrom"] = value;
			}
		}

		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x06007801 RID: 30721 RVA: 0x001E8F44 File Offset: 0x001E7144
		// (set) Token: 0x06007802 RID: 30722 RVA: 0x001E8F4C File Offset: 0x001E714C
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x06007803 RID: 30723 RVA: 0x001E8F55 File Offset: 0x001E7155
		// (set) Token: 0x06007804 RID: 30724 RVA: 0x001E8F6C File Offset: 0x001E716C
		[Parameter]
		public MultiValuedProperty<SecurityPrincipalType> Types
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalType>)base.Fields["Types"];
			}
			set
			{
				base.Fields["Types"] = value;
			}
		}

		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x06007805 RID: 30725 RVA: 0x001E8F7F File Offset: 0x001E717F
		// (set) Token: 0x06007806 RID: 30726 RVA: 0x001E8FA5 File Offset: 0x001E71A5
		[Parameter]
		public SwitchParameter RoleGroupAssignable
		{
			get
			{
				return (SwitchParameter)(base.Fields["RoleGroupAssignable"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RoleGroupAssignable"] = value;
			}
		}

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x06007807 RID: 30727 RVA: 0x001E8FBD File Offset: 0x001E71BD
		// (set) Token: 0x06007808 RID: 30728 RVA: 0x001E8FD4 File Offset: 0x001E71D4
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, ADRecipientProperties.Instance);
				this.inputFilter = monadFilter.InnerFilter;
				base.OptionalIdentityData.AdditionalFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x06007809 RID: 30729 RVA: 0x001E901C File Offset: 0x001E721C
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x0600780A RID: 30730 RVA: 0x001E901F File Offset: 0x001E721F
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x0600780B RID: 30731 RVA: 0x001E9028 File Offset: 0x001E7228
		protected override QueryFilter InternalFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					this.inputFilter,
					base.InternalFilter
				});
			}
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x001E9054 File Offset: 0x001E7254
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.NetCredential, base.SessionSettings, 177, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\GetSecurityPrincipal.cs");
			tenantOrRootOrgRecipientSession.EnforceDefaultScope = true;
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = (this.rootId == null);
			return DirectorySessionFactory.Default.GetReducedRecipientSession(tenantOrRootOrgRecipientSession, 187, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\GetSecurityPrincipal.cs");
		}

		// Token: 0x0600780D RID: 30733 RVA: 0x001E90C4 File Offset: 0x001E72C4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.OrganizationalUnit != null)
			{
				this.organizationalUnit = (ExtendedOrganizationalUnit)base.GetDataObject<ExtendedOrganizationalUnit>(this.OrganizationalUnit, this.ConfigurationSession, null, null, new LocalizedString?(Strings.ErrorManagementObjectAmbiguous(this.OrganizationalUnit.ToString())));
				this.rootId = this.organizationalUnit.Id;
			}
			if (this.IncludeDomainLocalFrom != null)
			{
				this.includeDomainLocalFrom = ADForest.GetLocalForest(this.ConfigurationSession.DomainController).FindDomainByFqdn(this.IncludeDomainLocalFrom.Domain);
				if (this.includeDomainLocalFrom == null)
				{
					base.WriteError(new DomainNotFoundException(this.IncludeDomainLocalFrom.Domain), ErrorCategory.InvalidArgument, this.IncludeDomainLocalFrom);
					TaskLogger.LogExit();
					return;
				}
			}
			if (this.Types == null || this.Types.Count == 0)
			{
				this.types = new MultiValuedProperty<SecurityPrincipalType>();
				this.types.Add(SecurityPrincipalType.WellknownSecurityPrincipal);
				this.types.Add(SecurityPrincipalType.User);
				this.types.Add(SecurityPrincipalType.Computer);
				this.types.Add(SecurityPrincipalType.Group);
			}
			else
			{
				this.types = this.Types;
			}
			if (this.Identity != null)
			{
				this.Identity.IncludeDomainLocalFrom = this.includeDomainLocalFrom;
				this.Identity.Types = this.types;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600780E RID: 30734 RVA: 0x001E9218 File Offset: 0x001E7418
		protected override IEnumerable<ExtendedSecurityPrincipal> GetPagedData()
		{
			ADObjectId includeDomailLocalFrom = (this.includeDomainLocalFrom != null) ? this.includeDomainLocalFrom.Id : null;
			return ExtendedSecurityPrincipalSearchHelper.PerformSearch(new ExtendedSecurityPrincipalSearcher(this.FindObjects), base.DataSession, this.RootId as ADObjectId, includeDomailLocalFrom, this.types);
		}

		// Token: 0x0600780F RID: 30735 RVA: 0x001E9265 File Offset: 0x001E7465
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			if (!this.ShouldSkipObject(dataObject))
			{
				this.CompleteDataObject((ExtendedSecurityPrincipal)dataObject);
				base.WriteResult(dataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007810 RID: 30736 RVA: 0x001E928D File Offset: 0x001E748D
		private void CompleteDataObject(ExtendedSecurityPrincipal dataObject)
		{
			dataObject.UserFriendlyName = SecurityPrincipalIdParameter.GetFriendlyUserName(dataObject.SID, null);
		}

		// Token: 0x06007811 RID: 30737 RVA: 0x001E92A4 File Offset: 0x001E74A4
		private bool ShouldSkipObject(IConfigurable dataObject)
		{
			if (this.RoleGroupAssignable)
			{
				ExtendedSecurityPrincipal extendedSecurityPrincipal = (ExtendedSecurityPrincipal)dataObject;
				if (Array.IndexOf<RecipientTypeDetails>(GetSecurityPrincipal.AllowedRecipientTypeDetails, extendedSecurityPrincipal.RecipientTypeDetails) == -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007812 RID: 30738 RVA: 0x001E92DC File Offset: 0x001E74DC
		private IEnumerable<ExtendedSecurityPrincipal> FindObjects(IConfigDataProvider session, ADObjectId rootId, QueryFilter targetFilter)
		{
			return session.FindPaged<ExtendedSecurityPrincipal>(QueryFilter.AndTogether(new QueryFilter[]
			{
				targetFilter,
				this.InternalFilter
			}), rootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
		}

		// Token: 0x04003BF5 RID: 15349
		private QueryFilter inputFilter;

		// Token: 0x04003BF6 RID: 15350
		private ExtendedOrganizationalUnit organizationalUnit;

		// Token: 0x04003BF7 RID: 15351
		private ADDomain includeDomainLocalFrom;

		// Token: 0x04003BF8 RID: 15352
		private ObjectId rootId;

		// Token: 0x04003BF9 RID: 15353
		private MultiValuedProperty<SecurityPrincipalType> types;

		// Token: 0x04003BFA RID: 15354
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.None,
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.SharedMailbox,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.LegacyMailbox,
			RecipientTypeDetails.MailUser,
			(RecipientTypeDetails)((ulong)int.MinValue),
			RecipientTypeDetails.RemoteSharedMailbox,
			RecipientTypeDetails.RemoteTeamMailbox,
			RecipientTypeDetails.MailUniversalSecurityGroup,
			RecipientTypeDetails.User,
			RecipientTypeDetails.UniversalSecurityGroup,
			RecipientTypeDetails.LinkedUser,
			RecipientTypeDetails.RoleGroup,
			RecipientTypeDetails.AllUniqueRecipientTypes
		};
	}
}
