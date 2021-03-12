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
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000095 RID: 149
	[Cmdlet("Get", "AuditLogSearch", DefaultParameterSetName = "Identity")]
	public sealed class GetAuditLogSearch : GetObjectWithIdentityTaskBase<AuditLogSearchIdParameter, AuditLogSearchBase>
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00013E1E File Offset: 0x0001201E
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00013E35 File Offset: 0x00012035
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00013E48 File Offset: 0x00012048
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00013E5F File Offset: 0x0001205F
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string Type
		{
			get
			{
				return (string)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00013E72 File Offset: 0x00012072
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00013E89 File Offset: 0x00012089
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime? CreatedAfter
		{
			get
			{
				return (ExDateTime?)base.Fields["CreatedAfter"];
			}
			set
			{
				base.Fields["CreatedAfter"] = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00013EA1 File Offset: 0x000120A1
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00013EB8 File Offset: 0x000120B8
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ExDateTime? CreatedBefore
		{
			get
			{
				return (ExDateTime?)base.Fields["CreatedBefore"];
			}
			set
			{
				base.Fields["CreatedBefore"] = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00013ED0 File Offset: 0x000120D0
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00013EE7 File Offset: 0x000120E7
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateRange(1, 250000)]
		public int ResultSize
		{
			get
			{
				return (int)base.Fields["ResultSize"];
			}
			set
			{
				base.Fields["ResultSize"] = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00013EFF File Offset: 0x000120FF
		protected override ObjectId RootId
		{
			get
			{
				return AdminAuditLogConfig.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013F0C File Offset: 0x0001210C
		protected override void InternalValidate()
		{
			if (this.CreatedAfter != null && this.CreatedBefore != null && this.CreatedAfter >= this.CreatedBefore)
			{
				base.WriteError(new ArgumentException(Strings.ErrorDateRangeInvalid, "CreatedAfter"), ErrorCategory.InvalidArgument, null);
			}
			if (this.CreatedAfter != null && this.CreatedAfter > ExDateTime.Now)
			{
				base.WriteError(new ArgumentException(Strings.ErrorCreatedAfterLaterThanToday, "CreatedAfter"), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields["ResultSize"] == null)
			{
				this.ResultSize = 1000;
			}
			if (base.Fields["Type"] != null && !Enum.IsDefined(typeof(AuditLogType), ((string)base.Fields["Type"]).ToLower()))
			{
				base.WriteError(new ArgumentException(Strings.AuditLogSearchInvalidSearchType((string)base.Fields["Type"]), "Type"), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00014070 File Offset: 0x00012270
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			SearchFilter filter = this.CreateSearchFilter();
			try
			{
				this.WriteResult<AuditLogSearchBase>(((AuditLogSearchEwsDataProvider)base.DataSession).FindIds<AuditLogSearchBase>(filter, null != this.Identity, this.ResultSize, this.RootId));
			}
			catch (LocalizedException)
			{
				base.WriteError(new FailedToRetrieveAuditLogSearchException(), ErrorCategory.ResourceUnavailable, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000140F4 File Offset: 0x000122F4
		protected override IConfigDataProvider CreateSession()
		{
			OrganizationId organizationId = this.ResolveOrganizationId();
			ADUser tenantArbitrationMailbox;
			try
			{
				tenantArbitrationMailbox = AdminAuditLogHelper.GetTenantArbitrationMailbox(organizationId);
			}
			catch (ObjectNotFoundException innerException)
			{
				TaskLogger.Trace("ObjectNotFoundException occurred when getting Exchange principal from the discovery mailbox user.", new object[0]);
				throw new AdminAuditLogSearchException(Strings.AuditLogSearchArbitrationMailboxNotFound(organizationId.ToString()), innerException);
			}
			catch (NonUniqueRecipientException innerException2)
			{
				TaskLogger.Trace("More than one tenant arbitration mailbox found for the current organization.", new object[0]);
				throw new AdminAuditLogSearchException(Strings.AuditLogSearchNonUniqueArbitrationMailbox(organizationId.ToString()), innerException2);
			}
			ExchangePrincipal primaryMailbox = ExchangePrincipal.FromADUser(ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), tenantArbitrationMailbox, RemotingOptions.AllowCrossSite);
			return new AuditLogSearchEwsDataProvider(primaryMailbox);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001418C File Offset: 0x0001238C
		private OrganizationId ResolveOrganizationId()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 217, "ResolveOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AuditLogSearch\\GetAuditLogSearch.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.CurrentOrganizationId;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001423C File Offset: 0x0001243C
		private SearchFilter CreateSearchFilter()
		{
			List<SearchFilter> list = new List<SearchFilter>();
			if (this.Type != null)
			{
				list.Add(new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "IPM.AuditLogSearch." + this.Type, 1, 1));
			}
			if (this.Identity != null)
			{
				list.Add(new SearchFilter.IsEqualTo(AuditLogSearchBaseEwsSchema.Identity.StorePropertyDefinition, this.Identity.GetId().Guid.ToString()));
			}
			if (this.CreatedAfter != null)
			{
				if (!this.CreatedAfter.Value.HasTimeZone)
				{
					ExDateTime exDateTime = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.CreatedAfter.Value.UniversalTime)[0];
					list.Add(new SearchFilter.IsGreaterThanOrEqualTo(AuditLogSearchBaseEwsSchema.CreationTime.StorePropertyDefinition, exDateTime.UniversalTime));
				}
				else
				{
					list.Add(new SearchFilter.IsGreaterThanOrEqualTo(AuditLogSearchBaseEwsSchema.CreationTime.StorePropertyDefinition, this.CreatedAfter.Value.UniversalTime));
				}
			}
			if (this.CreatedBefore != null)
			{
				if (!this.CreatedBefore.Value.HasTimeZone)
				{
					ExDateTime exDateTime2 = ExDateTime.Create(ExTimeZone.CurrentTimeZone, this.CreatedBefore.Value.UniversalTime)[0];
					list.Add(new SearchFilter.IsLessThanOrEqualTo(AuditLogSearchBaseEwsSchema.CreationTime.StorePropertyDefinition, exDateTime2.UniversalTime));
				}
				else
				{
					list.Add(new SearchFilter.IsLessThanOrEqualTo(AuditLogSearchBaseEwsSchema.CreationTime.StorePropertyDefinition, this.CreatedBefore.Value.UniversalTime));
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new SearchFilter.SearchFilterCollection(0, list.ToArray());
		}

		// Token: 0x0400026E RID: 622
		private const int DefaultSearchResultSize = 1000;
	}
}
