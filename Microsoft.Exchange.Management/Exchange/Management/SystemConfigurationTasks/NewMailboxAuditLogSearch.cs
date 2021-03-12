using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000097 RID: 151
	[Cmdlet("New", "MailboxAuditLogSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewMailboxAuditLogSearch : NewAuditLogSearchBase<MailboxAuditLogSearch>
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00014BB8 File Offset: 0x00012DB8
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00014BCF File Offset: 0x00012DCF
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxIdParameter> Mailboxes
		{
			get
			{
				return (MultiValuedProperty<MailboxIdParameter>)base.Fields["Mailboxes"];
			}
			set
			{
				base.Fields["Mailboxes"] = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00014BE2 File Offset: 0x00012DE2
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00014BF9 File Offset: 0x00012DF9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<AuditScopes> LogonTypes
		{
			get
			{
				return (MultiValuedProperty<AuditScopes>)base.Fields["LogonTypes"];
			}
			set
			{
				base.Fields["LogonTypes"] = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00014C0C File Offset: 0x00012E0C
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00014C23 File Offset: 0x00012E23
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxAuditOperations> Operations
		{
			get
			{
				return (MultiValuedProperty<MailboxAuditOperations>)base.Fields["Operations"];
			}
			set
			{
				base.Fields["Operations"] = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00014C36 File Offset: 0x00012E36
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00014C5C File Offset: 0x00012E5C
		[Parameter(Mandatory = false)]
		public SwitchParameter ShowDetails
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowDetails"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowDetails"] = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00014C74 File Offset: 0x00012E74
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxAuditLogSearch(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00014C88 File Offset: 0x00012E88
		protected override void InternalValidate()
		{
			if (NewMailboxAuditLogSearch.MailboxAuditLogSearchRequestThreshold == null)
			{
				int? num = NewAuditLogSearchBase<MailboxAuditLogSearch>.ReadIntegerAppSetting("AsyncMailboxAuditLogSearchRequestThreshold");
				if (num == null || num < 1)
				{
					NewMailboxAuditLogSearch.MailboxAuditLogSearchRequestThreshold = new int?(50);
				}
				else
				{
					NewMailboxAuditLogSearch.MailboxAuditLogSearchRequestThreshold = num;
				}
			}
			NewMailboxAuditLogSearch.SearchDataProvider searchDataProvider = (NewMailboxAuditLogSearch.SearchDataProvider)base.DataSession;
			if (searchDataProvider.GetAuditLogSearchCount() >= NewMailboxAuditLogSearch.MailboxAuditLogSearchRequestThreshold.Value)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotCreateAuditLogSearchDueToSearchQuota), ErrorCategory.QuotaExceeded, null);
			}
			base.InternalValidate();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00014D1D File Offset: 0x00012F1D
		internal override IConfigDataProvider InternalCreateSearchDataProvider(ExchangePrincipal principal, OrganizationId organizationId)
		{
			return new NewMailboxAuditLogSearch.SearchDataProvider(principal, organizationId);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00014D28 File Offset: 0x00012F28
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			this.DataObject.Mailboxes = MailboxAuditLogSearch.ConvertTo(this.recipientSession, this.Mailboxes, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.LogonTypes != null)
			{
				this.DataObject.LogonTypesUserInput = this.LogonTypes;
			}
			if (this.Operations != null)
			{
				this.DataObject.OperationsUserInput = this.Operations;
			}
			this.DataObject.ShowDetails = this.ShowDetails;
			this.DataObject.Validate(new Task.TaskErrorLoggingDelegate(base.WriteError));
			return this.DataObject;
		}

		// Token: 0x04000271 RID: 625
		private const string MailboxAuditLogSearchRequestThresholdKey = "AsyncMailboxAuditLogSearchRequestThreshold";

		// Token: 0x04000272 RID: 626
		private const int DefaultMailboxAuditLogSearchRequestThreshold = 50;

		// Token: 0x04000273 RID: 627
		private static int? MailboxAuditLogSearchRequestThreshold = null;

		// Token: 0x02000098 RID: 152
		private class SearchDataProvider : AuditLogSearchEwsDataProvider
		{
			// Token: 0x06000500 RID: 1280 RVA: 0x00014DEA File Offset: 0x00012FEA
			public SearchDataProvider(ExchangePrincipal primaryMailbox, OrganizationId organizationId) : base(primaryMailbox)
			{
				this.organizationId = organizationId;
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x00014DFC File Offset: 0x00012FFC
			public override void Save(IConfigurable instance)
			{
				MailboxAuditLogSearch mailboxAuditLogSearch = (MailboxAuditLogSearch)instance;
				MailboxAuditLogSearch mailboxAuditLogSearch2 = new MailboxAuditLogSearch();
				mailboxAuditLogSearch2.Identity = (AuditLogSearchId)mailboxAuditLogSearch.Identity;
				mailboxAuditLogSearch2.Name = mailboxAuditLogSearch.Name;
				mailboxAuditLogSearch2.StartDateUtc = new DateTime?(mailboxAuditLogSearch.StartDateUtc.Value);
				mailboxAuditLogSearch2.EndDateUtc = new DateTime?(mailboxAuditLogSearch.EndDateUtc.Value);
				mailboxAuditLogSearch2.StatusMailRecipients = NewAuditLogSearchBase<MailboxAuditLogSearch>.GetMultiValuedSmptAddressAsStrings(mailboxAuditLogSearch.StatusMailRecipients);
				mailboxAuditLogSearch2.CreatedBy = mailboxAuditLogSearch.CreatedBy;
				mailboxAuditLogSearch2.CreatedByEx = mailboxAuditLogSearch.CreatedByEx;
				mailboxAuditLogSearch2.Mailboxes = mailboxAuditLogSearch.Mailboxes;
				mailboxAuditLogSearch2.LogonTypes = mailboxAuditLogSearch.LogonTypes;
				mailboxAuditLogSearch2.Operations = mailboxAuditLogSearch.Operations;
				mailboxAuditLogSearch2.ShowDetails = new bool?(mailboxAuditLogSearch.ShowDetails);
				if (mailboxAuditLogSearch.ExternalAccess != null)
				{
					mailboxAuditLogSearch2.ExternalAccess = (mailboxAuditLogSearch.ExternalAccess.Value ? bool.TrueString : bool.FalseString);
				}
				base.Save(mailboxAuditLogSearch2);
				AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
				{
					QueueType = AuditQueueType.AsyncMailboxSearch,
					EventType = QueueEventType.Queue,
					CorrelationId = mailboxAuditLogSearch2.Identity.Guid.ToString(),
					OrganizationId = this.organizationId,
					QueueLength = ((this.defaultFolder != null) ? (this.defaultFolder.TotalCount + 1) : 1)
				};
				auditQueuesOpticsLogData.Log();
				instance.ResetChangeTracking();
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x00014F6C File Offset: 0x0001316C
			public override IConfigurable Read<T>(ObjectId identity)
			{
				AuditLogSearchId auditLogSearchId = identity as AuditLogSearchId;
				if (auditLogSearchId != null)
				{
					SearchFilter filter = new SearchFilter.IsEqualTo(AuditLogSearchBaseEwsSchema.Identity.StorePropertyDefinition, auditLogSearchId.Guid.ToString());
					using (IEnumerator<MailboxAuditLogSearch> enumerator = this.FindInFolder<MailboxAuditLogSearch>(filter, this.GetDefaultFolder()).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							MailboxAuditLogSearch mailboxAuditLogSearch = enumerator.Current;
							MailboxAuditLogSearch mailboxAuditLogSearch2 = new MailboxAuditLogSearch();
							mailboxAuditLogSearch2.SetId(mailboxAuditLogSearch.Identity);
							mailboxAuditLogSearch2.Name = mailboxAuditLogSearch.Name;
							mailboxAuditLogSearch2.StartDateUtc = new DateTime?(mailboxAuditLogSearch.StartDateUtc.Value);
							mailboxAuditLogSearch2.EndDateUtc = new DateTime?(mailboxAuditLogSearch.EndDateUtc.Value);
							mailboxAuditLogSearch2.StatusMailRecipients = NewAuditLogSearchBase<MailboxAuditLogSearch>.GetMultiValuedStringsAsSmptAddresses(mailboxAuditLogSearch.StatusMailRecipients);
							mailboxAuditLogSearch2.CreatedBy = mailboxAuditLogSearch.CreatedBy;
							mailboxAuditLogSearch2.CreatedByEx = mailboxAuditLogSearch.CreatedByEx;
							mailboxAuditLogSearch2.Mailboxes = mailboxAuditLogSearch.Mailboxes;
							mailboxAuditLogSearch2.LogonTypes = mailboxAuditLogSearch.LogonTypes;
							mailboxAuditLogSearch2.Operations = mailboxAuditLogSearch.Operations;
							mailboxAuditLogSearch2.ShowDetails = (mailboxAuditLogSearch.ShowDetails != null && mailboxAuditLogSearch.ShowDetails.Value);
							bool value;
							if (!string.IsNullOrEmpty(mailboxAuditLogSearch.ExternalAccess) && bool.TryParse(mailboxAuditLogSearch.ExternalAccess, out value))
							{
								mailboxAuditLogSearch2.ExternalAccess = new bool?(value);
							}
							return mailboxAuditLogSearch2;
						}
					}
				}
				return null;
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x000150FC File Offset: 0x000132FC
			protected override FolderId GetDefaultFolder()
			{
				if (this.defaultFolder == null)
				{
					this.defaultFolder = base.GetOrCreateFolder("MailboxAuditLogSearch");
				}
				return this.defaultFolder.Id;
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x00015124 File Offset: 0x00013324
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

			// Token: 0x04000274 RID: 628
			private OrganizationId organizationId;

			// Token: 0x04000275 RID: 629
			private Folder defaultFolder;
		}
	}
}
