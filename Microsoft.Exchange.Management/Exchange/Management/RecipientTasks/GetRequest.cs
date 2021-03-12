using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C5E RID: 3166
	public abstract class GetRequest<TIdentity, TDataObject> : GetObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : MRSRequestIdParameter where TDataObject : RequestBase, new()
	{
		// Token: 0x06007824 RID: 30756 RVA: 0x001E96B2 File Offset: 0x001E78B2
		public GetRequest()
		{
			this.userId = null;
			this.queueId = null;
			this.GCSession = null;
			this.RecipSession = null;
			this.ConfigSession = null;
			this.CurrentOrgConfigSession = null;
			this.indexProvider = null;
		}

		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x06007825 RID: 30757 RVA: 0x001E96EB File Offset: 0x001E78EB
		// (set) Token: 0x06007826 RID: 30758 RVA: 0x001E970C File Offset: 0x001E790C
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public RequestStatus Status
		{
			get
			{
				return (RequestStatus)(base.Fields["Status"] ?? RequestStatus.None);
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x06007827 RID: 30759 RVA: 0x001E9724 File Offset: 0x001E7924
		// (set) Token: 0x06007828 RID: 30760 RVA: 0x001E973B File Offset: 0x001E793B
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public string BatchName
		{
			get
			{
				return (string)base.Fields["BatchName"];
			}
			set
			{
				base.Fields["BatchName"] = value;
			}
		}

		// Token: 0x17002521 RID: 9505
		// (get) Token: 0x06007829 RID: 30761 RVA: 0x001E974E File Offset: 0x001E794E
		// (set) Token: 0x0600782A RID: 30762 RVA: 0x001E9765 File Offset: 0x001E7965
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17002522 RID: 9506
		// (get) Token: 0x0600782B RID: 30763 RVA: 0x001E9778 File Offset: 0x001E7978
		// (set) Token: 0x0600782C RID: 30764 RVA: 0x001E9799 File Offset: 0x001E7999
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool Suspend
		{
			get
			{
				return (bool)(base.Fields["Suspend"] ?? false);
			}
			set
			{
				base.Fields["Suspend"] = value;
			}
		}

		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x0600782D RID: 30765 RVA: 0x001E97B1 File Offset: 0x001E79B1
		// (set) Token: 0x0600782E RID: 30766 RVA: 0x001E97D2 File Offset: 0x001E79D2
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public bool HighPriority
		{
			get
			{
				return (bool)(base.Fields["HighPriority"] ?? false);
			}
			set
			{
				base.Fields["HighPriority"] = value;
			}
		}

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x0600782F RID: 30767 RVA: 0x001E97EA File Offset: 0x001E79EA
		// (set) Token: 0x06007830 RID: 30768 RVA: 0x001E9801 File Offset: 0x001E7A01
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
		public DatabaseIdParameter RequestQueue
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["RequestQueue"];
			}
			set
			{
				base.Fields["RequestQueue"] = value;
			}
		}

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x06007831 RID: 30769 RVA: 0x001E9814 File Offset: 0x001E7A14
		// (set) Token: 0x06007832 RID: 30770 RVA: 0x001E982B File Offset: 0x001E7A2B
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "Filtering")]
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

		// Token: 0x17002526 RID: 9510
		// (get) Token: 0x06007833 RID: 30771 RVA: 0x001E983E File Offset: 0x001E7A3E
		// (set) Token: 0x06007834 RID: 30772 RVA: 0x001E9846 File Offset: 0x001E7A46
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17002527 RID: 9511
		// (get) Token: 0x06007835 RID: 30773 RVA: 0x001E984F File Offset: 0x001E7A4F
		// (set) Token: 0x06007836 RID: 30774 RVA: 0x001E9857 File Offset: 0x001E7A57
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x001E9860 File Offset: 0x001E7A60
		// (set) Token: 0x06007838 RID: 30776 RVA: 0x001E9877 File Offset: 0x001E7A77
		[Parameter(Mandatory = false, ParameterSetName = "Filtering", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields["AccountPartition"];
			}
			set
			{
				base.Fields["AccountPartition"] = value;
			}
		}

		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x06007839 RID: 30777 RVA: 0x001E988A File Offset: 0x001E7A8A
		// (set) Token: 0x0600783A RID: 30778 RVA: 0x001E98A1 File Offset: 0x001E7AA1
		internal MailboxOrMailUserIdParameter InternalMailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x0600783B RID: 30779 RVA: 0x001E98B4 File Offset: 0x001E7AB4
		// (set) Token: 0x0600783C RID: 30780 RVA: 0x001E98BC File Offset: 0x001E7ABC
		internal IRecipientSession GCSession { get; set; }

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x0600783D RID: 30781 RVA: 0x001E98C5 File Offset: 0x001E7AC5
		// (set) Token: 0x0600783E RID: 30782 RVA: 0x001E98CD File Offset: 0x001E7ACD
		internal IRecipientSession RecipSession { get; set; }

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x0600783F RID: 30783 RVA: 0x001E98D6 File Offset: 0x001E7AD6
		// (set) Token: 0x06007840 RID: 30784 RVA: 0x001E98DE File Offset: 0x001E7ADE
		internal IConfigurationSession CurrentOrgConfigSession { get; set; }

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x06007841 RID: 30785 RVA: 0x001E98E7 File Offset: 0x001E7AE7
		// (set) Token: 0x06007842 RID: 30786 RVA: 0x001E98EF File Offset: 0x001E7AEF
		internal ITopologyConfigurationSession ConfigSession { get; set; }

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x06007843 RID: 30787 RVA: 0x001E98F8 File Offset: 0x001E7AF8
		protected virtual RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(RequestIndexLocation.AD);
			}
		}

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x06007844 RID: 30788
		protected abstract MRSRequestType RequestType { get; }

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x06007845 RID: 30789 RVA: 0x001E9900 File Offset: 0x001E7B00
		protected override ObjectId RootId
		{
			get
			{
				if (this.AccountPartition == null)
				{
					return ADHandler.GetRootId(this.CurrentOrgConfigSession, this.RequestType);
				}
				return null;
			}
		}

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x06007846 RID: 30790 RVA: 0x001E991D File Offset: 0x001E7B1D
		protected override bool DeepSearch
		{
			get
			{
				return this.AccountPartition != null;
			}
		}

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x06007847 RID: 30791 RVA: 0x001E992B File Offset: 0x001E7B2B
		protected override QueryFilter InternalFilter
		{
			get
			{
				return this.InternalFilterBuilder();
			}
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x001E9934 File Offset: 0x001E7B34
		protected virtual RequestIndexEntryQueryFilter InternalFilterBuilder()
		{
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter();
			if (this.IsFieldSet("BatchName"))
			{
				requestIndexEntryQueryFilter.BatchName = (this.BatchName ?? string.Empty);
			}
			if (this.IsFieldSet("Name"))
			{
				requestIndexEntryQueryFilter.RequestName = (this.Name ?? string.Empty);
			}
			if (this.IsFieldSet("Status"))
			{
				requestIndexEntryQueryFilter.Status = this.Status;
			}
			if (this.IsFieldSet("Suspend"))
			{
				if (this.Suspend)
				{
					requestIndexEntryQueryFilter.Flags |= RequestFlags.Suspend;
				}
				else
				{
					requestIndexEntryQueryFilter.NotFlags |= RequestFlags.Suspend;
				}
			}
			if (this.IsFieldSet("HighPriority"))
			{
				if (this.HighPriority)
				{
					requestIndexEntryQueryFilter.Flags |= RequestFlags.HighPriority;
				}
				else
				{
					requestIndexEntryQueryFilter.NotFlags |= RequestFlags.HighPriority;
				}
			}
			if (this.queueId != null)
			{
				requestIndexEntryQueryFilter.RequestQueueId = this.queueId;
			}
			if (this.userId != null)
			{
				requestIndexEntryQueryFilter.MailboxId = this.userId;
				requestIndexEntryQueryFilter.LooseMailboxSearch = true;
			}
			return requestIndexEntryQueryFilter;
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x001E9A4C File Offset: 0x001E7C4C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Organization != null)
			{
				IConfigurationSession session = RequestTaskHelper.CreateOrganizationFindingSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, session, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
			}
		}

		// Token: 0x0600784A RID: 30794 RVA: 0x001E9AC3 File Offset: 0x001E7CC3
		protected override void InternalStateReset()
		{
			this.userId = null;
			this.queueId = null;
			base.InternalStateReset();
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x001E9ADC File Offset: 0x001E7CDC
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings adsessionSettings;
			if (this.AccountPartition != null)
			{
				PartitionId partitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
				adsessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			}
			else
			{
				adsessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			}
			if (MapiTaskHelper.IsDatacenter || MapiTaskHelper.IsDatacenterDedicated)
			{
				adsessionSettings.IncludeSoftDeletedObjects = true;
				adsessionSettings.IncludeInactiveMailbox = true;
			}
			this.CurrentOrgConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 479, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequest.cs");
			if (this.AccountPartition == null)
			{
				adsessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
			}
			this.GCSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 493, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequest.cs");
			this.RecipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 500, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequest.cs");
			this.ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 506, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\GetRequest.cs");
			if (this.indexProvider != null)
			{
				this.indexProvider = null;
			}
			this.indexProvider = new RequestIndexEntryProvider(this.GCSession, this.CurrentOrgConfigSession);
			return this.indexProvider;
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x001E9C38 File Offset: 0x001E7E38
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.InternalMailbox != null)
			{
				ADUser aduser = RequestTaskHelper.ResolveADUser(this.RecipSession, this.GCSession, base.ServerSettings, this.InternalMailbox, base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
				this.userId = aduser.Id;
			}
			if (this.RequestQueue != null)
			{
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.RequestQueue, this.ConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.RequestQueue.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.RequestQueue.ToString())));
				this.queueId = mailboxDatabase.Id;
			}
			if (this.Organization != null && this.AccountPartition != null)
			{
				base.WriteError(new TaskException(Strings.ErrorIncompatibleParameters("Organization", "AccountPartition")), ErrorCategory.InvalidArgument, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x001E9D54 File Offset: 0x001E7F54
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.Identity != null)
				{
					TIdentity identity = this.Identity;
					if (identity.OrganizationId != null)
					{
						IDirectorySession recipSession = this.RecipSession;
						TIdentity identity2 = this.Identity;
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(recipSession, identity2.OrganizationId))
						{
							IDirectorySession recipSession2 = this.RecipSession;
							TIdentity identity3 = this.Identity;
							this.RecipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(recipSession2, identity3.OrganizationId, true);
						}
						IDirectorySession currentOrgConfigSession = this.CurrentOrgConfigSession;
						TIdentity identity4 = this.Identity;
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(currentOrgConfigSession, identity4.OrganizationId))
						{
							IDirectorySession currentOrgConfigSession2 = this.CurrentOrgConfigSession;
							TIdentity identity5 = this.Identity;
							this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(currentOrgConfigSession2, identity5.OrganizationId, true);
							this.indexProvider.ConfigSession = this.CurrentOrgConfigSession;
						}
					}
					TIdentity identity6 = this.Identity;
					if (!string.IsNullOrEmpty(identity6.MailboxName))
					{
						IRecipientSession recipSession3 = this.RecipSession;
						IRecipientSession gcsession = this.GCSession;
						ADServerSettings serverSettings = base.ServerSettings;
						TIdentity identity7 = this.Identity;
						ADUser aduser = RequestTaskHelper.ResolveADUser(recipSession3, gcsession, serverSettings, new MailboxOrMailUserIdParameter(identity7.MailboxName), base.OptionalIdentityData, this.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), false);
						if (aduser != null)
						{
							TIdentity identity8 = this.Identity;
							identity8.MailboxId = aduser.Id;
							if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.RecipSession, aduser))
							{
								this.RecipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.RecipSession, aduser.OrganizationId, true);
							}
							if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.CurrentOrgConfigSession, aduser))
							{
								this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.CurrentOrgConfigSession, aduser.OrganizationId, true);
								this.indexProvider.ConfigSession = this.CurrentOrgConfigSession;
							}
						}
					}
					TIdentity identity9 = this.Identity;
					if (!string.IsNullOrEmpty(identity9.OrganizationName))
					{
						IConfigurationSession configurationSession = RequestTaskHelper.CreateOrganizationFindingSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
						TIdentity identity10 = this.Identity;
						IIdentityParameter id = new OrganizationIdParameter(identity10.OrganizationName);
						IConfigDataProvider session = configurationSession;
						ObjectId rootID = null;
						TIdentity identity11 = this.Identity;
						LocalizedString? notFoundError = new LocalizedString?(Strings.ErrorOrganizationNotFound(identity11.OrganizationName));
						TIdentity identity12 = this.Identity;
						ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(id, session, rootID, notFoundError, new LocalizedString?(Strings.ErrorOrganizationNotUnique(identity12.OrganizationName)));
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.RecipSession, adorganizationalUnit))
						{
							this.RecipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.RecipSession, adorganizationalUnit.OrganizationId, true);
						}
						if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.CurrentOrgConfigSession, adorganizationalUnit))
						{
							this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.CurrentOrgConfigSession, adorganizationalUnit.OrganizationId, true);
							this.indexProvider.ConfigSession = this.CurrentOrgConfigSession;
						}
					}
					TIdentity identity13 = this.Identity;
					identity13.SetDefaultIndex(this.DefaultRequestIndexId);
				}
				base.InternalProcessRecord();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600784E RID: 30798 RVA: 0x001EA094 File Offset: 0x001E8294
		protected override bool IsKnownException(Exception exception)
		{
			return RequestTaskHelper.IsKnownExceptionHandler(exception, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x0600784F RID: 30799 RVA: 0x001EA0B3 File Offset: 0x001E82B3
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x04003BFE RID: 15358
		public const string ParameterStatus = "Status";

		// Token: 0x04003BFF RID: 15359
		public const string ParameterBatchName = "BatchName";

		// Token: 0x04003C00 RID: 15360
		public const string ParameterName = "Name";

		// Token: 0x04003C01 RID: 15361
		public const string ParameterMailbox = "Mailbox";

		// Token: 0x04003C02 RID: 15362
		public const string ParameterRequestQueue = "RequestQueue";

		// Token: 0x04003C03 RID: 15363
		public const string ParameterSuspend = "Suspend";

		// Token: 0x04003C04 RID: 15364
		public const string ParameterHighPriority = "HighPriority";

		// Token: 0x04003C05 RID: 15365
		public const string ParameterOrganization = "Organization";

		// Token: 0x04003C06 RID: 15366
		public const string ParameterAccountPartition = "AccountPartition";

		// Token: 0x04003C07 RID: 15367
		public const string FiltersSet = "Filtering";

		// Token: 0x04003C08 RID: 15368
		private ADObjectId userId;

		// Token: 0x04003C09 RID: 15369
		private ADObjectId queueId;

		// Token: 0x04003C0A RID: 15370
		private RequestIndexEntryProvider indexProvider;
	}
}
