using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000099 RID: 153
	[Cmdlet("New", "AdminAuditLogSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewAdminAuditLogSearch : NewAuditLogSearchBase<AdminAuditLogSearch>
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001515C File Offset: 0x0001335C
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00015173 File Offset: 0x00013373
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> Cmdlets
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["Cmdlets"];
			}
			set
			{
				base.Fields["Cmdlets"] = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00015186 File Offset: 0x00013386
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0001519D File Offset: 0x0001339D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["Parameters"];
			}
			set
			{
				base.Fields["Parameters"] = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x000151B0 File Offset: 0x000133B0
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x000151C7 File Offset: 0x000133C7
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ObjectIds
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ObjectIds"];
			}
			set
			{
				base.Fields["ObjectIds"] = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000151DA File Offset: 0x000133DA
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x000151F1 File Offset: 0x000133F1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SecurityPrincipalIdParameter> UserIds
		{
			get
			{
				return (MultiValuedProperty<SecurityPrincipalIdParameter>)base.Fields["UserIds"];
			}
			set
			{
				base.Fields["UserIds"] = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00015204 File Offset: 0x00013404
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAdminAuditLogSearch(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00015218 File Offset: 0x00013418
		protected override void InternalValidate()
		{
			if (NewAdminAuditLogSearch.AdminAuditLogSearchRequestThreshold == null)
			{
				int? num = NewAuditLogSearchBase<AdminAuditLogSearch>.ReadIntegerAppSetting("AsyncAdminAuditLogSearchRequestThreshold");
				if (num == null || num < 1)
				{
					NewAdminAuditLogSearch.AdminAuditLogSearchRequestThreshold = new int?(50);
				}
				else
				{
					NewAdminAuditLogSearch.AdminAuditLogSearchRequestThreshold = num;
				}
			}
			NewAdminAuditLogSearch.SearchDataProvider searchDataProvider = (NewAdminAuditLogSearch.SearchDataProvider)base.DataSession;
			if (searchDataProvider.GetAuditLogSearchCount() >= NewAdminAuditLogSearch.AdminAuditLogSearchRequestThreshold.Value)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotCreateAuditLogSearchDueToSearchQuota), ErrorCategory.QuotaExceeded, null);
			}
			base.InternalValidate();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000152AD File Offset: 0x000134AD
		internal override IConfigDataProvider InternalCreateSearchDataProvider(ExchangePrincipal principal, OrganizationId organizationId)
		{
			return new NewAdminAuditLogSearch.SearchDataProvider(principal, organizationId);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000152B8 File Offset: 0x000134B8
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			if (this.Cmdlets != null)
			{
				this.DataObject.Cmdlets = this.Cmdlets;
			}
			if (this.Parameters != null)
			{
				this.DataObject.Parameters = this.Parameters;
			}
			if (this.ObjectIds != null)
			{
				this.DataObject.ObjectIds = this.ObjectIds;
			}
			if (this.UserIds != null)
			{
				this.DataObject.UserIdsUserInput = this.UserIds;
			}
			this.DataObject.Succeeded = null;
			this.DataObject.StartIndex = 0;
			this.DataObject.ResultSize = 50000;
			this.DataObject.RedactDatacenterAdmins = !AdminAuditExternalAccessDeterminer.IsExternalAccess(base.SessionSettings.ExecutingUserIdentityName, base.SessionSettings.ExecutingUserOrganizationId, base.SessionSettings.CurrentOrganizationId);
			AdminAuditLogHelper.SetResolveUsers(this.DataObject, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			this.DataObject.Validate(new Task.TaskErrorLoggingDelegate(base.WriteError));
			return this.DataObject;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000153E1 File Offset: 0x000135E1
		protected override void InternalProcessRecord()
		{
			if (AdminAuditLogHelper.ShouldIssueWarning(base.CurrentOrganizationId))
			{
				this.WriteWarning(Strings.WarningNewAdminAuditLogSearchOnPreE15(base.CurrentOrganizationId.ToString()));
				return;
			}
			base.InternalProcessRecord();
		}

		// Token: 0x04000276 RID: 630
		private const string AdminAuditLogSearchRequestThresholdKey = "AsyncAdminAuditLogSearchRequestThreshold";

		// Token: 0x04000277 RID: 631
		private const int DefaultAdminAuditLogSearchRequestThreshold = 50;

		// Token: 0x04000278 RID: 632
		private static int? AdminAuditLogSearchRequestThreshold = null;

		// Token: 0x0200009A RID: 154
		private class SearchDataProvider : AuditLogSearchEwsDataProvider
		{
			// Token: 0x06000514 RID: 1300 RVA: 0x00015422 File Offset: 0x00013622
			public SearchDataProvider(ExchangePrincipal primaryMailbox, OrganizationId organizationId) : base(primaryMailbox)
			{
				this.organizationId = organizationId;
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x00015434 File Offset: 0x00013634
			public override void Save(IConfigurable instance)
			{
				AdminAuditLogSearch adminAuditLogSearch = (AdminAuditLogSearch)instance;
				AdminAuditLogSearch adminAuditLogSearch2 = new AdminAuditLogSearch();
				adminAuditLogSearch2.Identity = (AuditLogSearchId)adminAuditLogSearch.Identity;
				adminAuditLogSearch2.Name = adminAuditLogSearch.Name;
				adminAuditLogSearch2.StartDateUtc = new DateTime?(adminAuditLogSearch.StartDateUtc.Value);
				adminAuditLogSearch2.EndDateUtc = new DateTime?(adminAuditLogSearch.EndDateUtc.Value);
				adminAuditLogSearch2.StatusMailRecipients = NewAuditLogSearchBase<AdminAuditLogSearch>.GetMultiValuedSmptAddressAsStrings(adminAuditLogSearch.StatusMailRecipients);
				adminAuditLogSearch2.CreatedBy = adminAuditLogSearch.CreatedBy;
				adminAuditLogSearch2.CreatedByEx = adminAuditLogSearch.CreatedByEx;
				adminAuditLogSearch2.Cmdlets = adminAuditLogSearch.Cmdlets;
				adminAuditLogSearch2.Parameters = adminAuditLogSearch.Parameters;
				adminAuditLogSearch2.ObjectIds = adminAuditLogSearch.ObjectIds;
				if (adminAuditLogSearch.ExternalAccess != null)
				{
					adminAuditLogSearch2.ExternalAccess = (adminAuditLogSearch.ExternalAccess.Value ? bool.TrueString : bool.FalseString);
				}
				adminAuditLogSearch2.UserIds = adminAuditLogSearch.UserIds;
				adminAuditLogSearch2.ResolvedUsers = adminAuditLogSearch.ResolvedUsers;
				adminAuditLogSearch2.RedactDatacenterAdmins = adminAuditLogSearch.RedactDatacenterAdmins;
				base.Save(adminAuditLogSearch2);
				AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
				{
					QueueType = AuditQueueType.AsyncAdminSearch,
					EventType = QueueEventType.Queue,
					CorrelationId = adminAuditLogSearch2.Identity.Guid.ToString(),
					OrganizationId = this.organizationId,
					QueueLength = ((this.defaultFolder != null) ? (this.defaultFolder.TotalCount + 1) : 1)
				};
				auditQueuesOpticsLogData.Log();
				instance.ResetChangeTracking();
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x000155B8 File Offset: 0x000137B8
			public override IConfigurable Read<T>(ObjectId identity)
			{
				AuditLogSearchId auditLogSearchId = identity as AuditLogSearchId;
				if (auditLogSearchId != null)
				{
					SearchFilter filter = new SearchFilter.IsEqualTo(AuditLogSearchBaseEwsSchema.Identity.StorePropertyDefinition, auditLogSearchId.Guid.ToString());
					using (IEnumerator<AdminAuditLogSearch> enumerator = this.FindInFolder<AdminAuditLogSearch>(filter, this.GetDefaultFolder()).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							AdminAuditLogSearch adminAuditLogSearch = enumerator.Current;
							AdminAuditLogSearch adminAuditLogSearch2 = new AdminAuditLogSearch();
							adminAuditLogSearch2.SetId(adminAuditLogSearch.Identity);
							adminAuditLogSearch2.Name = adminAuditLogSearch.Name;
							adminAuditLogSearch2.StartDateUtc = new DateTime?(adminAuditLogSearch.StartDateUtc.Value);
							adminAuditLogSearch2.EndDateUtc = new DateTime?(adminAuditLogSearch.EndDateUtc.Value);
							adminAuditLogSearch2.StatusMailRecipients = NewAuditLogSearchBase<AdminAuditLogSearch>.GetMultiValuedStringsAsSmptAddresses(adminAuditLogSearch.StatusMailRecipients);
							adminAuditLogSearch2.CreatedBy = adminAuditLogSearch.CreatedBy;
							adminAuditLogSearch2.CreatedByEx = adminAuditLogSearch.CreatedByEx;
							adminAuditLogSearch2.Cmdlets = adminAuditLogSearch.Cmdlets;
							adminAuditLogSearch2.Parameters = adminAuditLogSearch.Parameters;
							adminAuditLogSearch2.ObjectIds = adminAuditLogSearch.ObjectIds;
							bool value;
							if (!string.IsNullOrEmpty(adminAuditLogSearch.ExternalAccess) && bool.TryParse(adminAuditLogSearch.ExternalAccess, out value))
							{
								adminAuditLogSearch2.ExternalAccess = new bool?(value);
							}
							adminAuditLogSearch2.UserIds = adminAuditLogSearch.UserIds;
							adminAuditLogSearch2.ResolvedUsers = adminAuditLogSearch.ResolvedUsers;
							return adminAuditLogSearch2;
						}
					}
				}
				return null;
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x00015738 File Offset: 0x00013938
			protected override FolderId GetDefaultFolder()
			{
				if (this.defaultFolder == null)
				{
					this.defaultFolder = base.GetOrCreateFolder("AdminAuditLogSearch");
				}
				return this.defaultFolder.Id;
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x00015760 File Offset: 0x00013960
			public int GetAuditLogSearchCount()
			{
				int result;
				try
				{
					this.GetDefaultFolder();
					result = this.defaultFolder.TotalCount;
				}
				catch (LocalizedException)
				{
					result = 0;
				}
				return result;
			}

			// Token: 0x04000279 RID: 633
			private OrganizationId organizationId;

			// Token: 0x0400027A RID: 634
			private Folder defaultFolder;
		}
	}
}
