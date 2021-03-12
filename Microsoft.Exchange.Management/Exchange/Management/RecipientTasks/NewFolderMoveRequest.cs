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
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C65 RID: 3173
	[Cmdlet("New", "FolderMoveRequest", SupportsShouldProcess = true)]
	public sealed class NewFolderMoveRequest : NewRequest<FolderMoveRequest>
	{
		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x060078ED RID: 30957 RVA: 0x001EC8D2 File Offset: 0x001EAAD2
		// (set) Token: 0x060078EE RID: 30958 RVA: 0x001EC8E9 File Offset: 0x001EAAE9
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public MailboxFolderIdParameter[] Folders
		{
			get
			{
				return (MailboxFolderIdParameter[])base.Fields["Folders"];
			}
			set
			{
				base.Fields["Folders"] = value;
			}
		}

		// Token: 0x1700256C RID: 9580
		// (get) Token: 0x060078EF RID: 30959 RVA: 0x001EC8FC File Offset: 0x001EAAFC
		// (set) Token: 0x060078F0 RID: 30960 RVA: 0x001EC913 File Offset: 0x001EAB13
		[ValidateNotNull]
		[Parameter(Mandatory = true)]
		public MailboxIdParameter SourceMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["SourceMailbox"];
			}
			set
			{
				base.Fields["SourceMailbox"] = value;
			}
		}

		// Token: 0x1700256D RID: 9581
		// (get) Token: 0x060078F1 RID: 30961 RVA: 0x001EC926 File Offset: 0x001EAB26
		// (set) Token: 0x060078F2 RID: 30962 RVA: 0x001EC93D File Offset: 0x001EAB3D
		[Parameter(Mandatory = true)]
		[ValidateNotNull]
		public MailboxIdParameter TargetMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x1700256E RID: 9582
		// (get) Token: 0x060078F3 RID: 30963 RVA: 0x001EC950 File Offset: 0x001EAB50
		// (set) Token: 0x060078F4 RID: 30964 RVA: 0x001EC976 File Offset: 0x001EAB76
		[Parameter(Mandatory = false)]
		public SwitchParameter SuspendWhenReadyToComplete
		{
			get
			{
				return (SwitchParameter)(base.Fields["SuspendWhenReadyToComplete"] ?? false);
			}
			set
			{
				base.Fields["SuspendWhenReadyToComplete"] = value;
			}
		}

		// Token: 0x1700256F RID: 9583
		// (get) Token: 0x060078F5 RID: 30965 RVA: 0x001EC98E File Offset: 0x001EAB8E
		// (set) Token: 0x060078F6 RID: 30966 RVA: 0x001EC9B4 File Offset: 0x001EABB4
		[Parameter(Mandatory = false)]
		public SwitchParameter AllowLargeItems
		{
			get
			{
				return (SwitchParameter)(base.Fields["AllowLargeItems"] ?? false);
			}
			set
			{
				base.Fields["AllowLargeItems"] = value;
			}
		}

		// Token: 0x17002570 RID: 9584
		// (get) Token: 0x060078F7 RID: 30967 RVA: 0x001EC9CC File Offset: 0x001EABCC
		// (set) Token: 0x060078F8 RID: 30968 RVA: 0x001EC9E3 File Offset: 0x001EABE3
		[Parameter(Mandatory = false)]
		public DateTime CompleteAfter
		{
			get
			{
				return (DateTime)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x17002571 RID: 9585
		// (get) Token: 0x060078F9 RID: 30969 RVA: 0x001EC9FB File Offset: 0x001EABFB
		// (set) Token: 0x060078FA RID: 30970 RVA: 0x001ECA20 File Offset: 0x001EAC20
		[Parameter(Mandatory = false)]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)(base.Fields["IncrementalSyncInterval"] ?? NewFolderMoveRequest.defaultIncrementalSyncIntervalForMove);
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x17002572 RID: 9586
		// (get) Token: 0x060078FB RID: 30971 RVA: 0x001ECA38 File Offset: 0x001EAC38
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewFolderMoveRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x17002573 RID: 9587
		// (get) Token: 0x060078FC RID: 30972 RVA: 0x001ECA5A File Offset: 0x001EAC5A
		// (set) Token: 0x060078FD RID: 30973 RVA: 0x001ECA62 File Offset: 0x001EAC62
		private new SkippableMergeComponent[] SkipMerging
		{
			get
			{
				return base.SkipMerging;
			}
			set
			{
				base.SkipMerging = value;
			}
		}

		// Token: 0x17002574 RID: 9588
		// (get) Token: 0x060078FE RID: 30974 RVA: 0x001ECA6B File Offset: 0x001EAC6B
		// (set) Token: 0x060078FF RID: 30975 RVA: 0x001ECA73 File Offset: 0x001EAC73
		private new string BatchName
		{
			get
			{
				return base.BatchName;
			}
			set
			{
				base.BatchName = value;
			}
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x001ECA7C File Offset: 0x001EAC7C
		protected override void InternalStateReset()
		{
			this.sourceMailboxUser = null;
			this.targetMailboxUser = null;
			base.InternalStateReset();
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x001ECA94 File Offset: 0x001EAC94
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.IsFieldSet("CompleteAfter") && base.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				DateTime? completeAfter = base.IsFieldSet("CompleteAfter") ? new DateTime?(this.CompleteAfter) : null;
				RequestTaskHelper.ValidateStartAfterCompleteAfterWithSuspendWhenReadyToComplete(null, completeAfter, this.SuspendWhenReadyToComplete.ToBool(), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.IsFieldSet("IncrementalSyncInterval") && base.IsFieldSet("SuspendWhenReadyToComplete") && this.SuspendWhenReadyToComplete.ToBool())
			{
				base.WriteError(new SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException(), ErrorCategory.InvalidArgument, this.SuspendWhenReadyToComplete);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x001ECB74 File Offset: 0x001EAD74
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				this.sourceMailboxUser = (ADUser)base.GetDataObject<ADUser>(this.SourceMailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.SourceMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.SourceMailbox.ToString())), ExchangeErrorCategory.Client);
				this.targetMailboxUser = (ADUser)base.GetDataObject<ADUser>(this.TargetMailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.TargetMailbox.ToString())), ExchangeErrorCategory.Client);
				if (!object.Equals(this.sourceMailboxUser.OrganizationId, this.targetMailboxUser.OrganizationId))
				{
					base.WriteError(new UsersNotInSameOrganizationPermanentException(this.sourceMailboxUser.ToString(), this.targetMailboxUser.ToString()), ErrorCategory.InvalidArgument, this.TargetMailbox);
				}
				base.RescopeToOrgId(this.sourceMailboxUser.OrganizationId);
				using (MailboxFolderDataProvider mailboxFolderDataProvider = new MailboxFolderDataProvider(base.OrgWideSessionSettings, this.sourceMailboxUser, "New-FolderMoveRequest"))
				{
					foreach (MailboxFolderIdParameter mailboxFolderIdParameter in this.Folders)
					{
						mailboxFolderIdParameter.InternalMailboxFolderId = new Microsoft.Exchange.Data.Storage.Management.MailboxFolderId(this.sourceMailboxUser.Id, mailboxFolderIdParameter.RawFolderStoreId, mailboxFolderIdParameter.RawFolderPath);
						MailboxFolder mailboxFolder = (MailboxFolder)base.GetDataObject<MailboxFolder>(mailboxFolderIdParameter, mailboxFolderDataProvider, null, new LocalizedString?(Strings.ErrorMailboxFolderNotFound(mailboxFolderIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxFolderNotUnique(mailboxFolderIdParameter.ToString())));
						string entryId = string.Empty;
						if (mailboxFolder.InternalFolderIdentity != null && mailboxFolder.InternalFolderIdentity.ObjectId != null)
						{
							entryId = mailboxFolder.InternalFolderIdentity.ObjectId.ToHexEntryId();
						}
						this.folderList.Add(new MoveFolderInfo(entryId, false));
					}
				}
				if (!string.IsNullOrEmpty(base.Name))
				{
					base.ValidateName();
					base.RequestName = base.Name;
				}
				else
				{
					base.RequestName = "FolderMove";
				}
				ADObjectId mdbId = null;
				ADObjectId mdbServerSite = null;
				base.Flags = (RequestFlags.IntraOrg | this.LocateAndChooseMdb(this.sourceMailboxUser.Database, this.targetMailboxUser.Database, this.sourceMailboxUser, this.targetMailboxUser, this.targetMailboxUser, out mdbId, out mdbServerSite));
				if (base.WorkloadType == RequestWorkloadType.None)
				{
					base.WorkloadType = RequestWorkloadType.Local;
				}
				base.MdbId = mdbId;
				base.MdbServerSite = mdbServerSite;
				base.WriteVerbose(Strings.RequestQueueIdentified(base.MdbId.Name));
				this.CheckRequestNameAvailability(base.RequestName, null, false, MRSRequestType.FolderMove, this.TargetMailbox, false);
				base.WriteVerbose(Strings.FolderMoveRequestCheckComplete);
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x001ECE58 File Offset: 0x001EB058
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.FolderMove;
			dataObject.WorkloadType = base.WorkloadType;
			dataObject.FolderList = this.folderList;
			dataObject.SourceUser = this.sourceMailboxUser;
			dataObject.SourceUserId = this.sourceMailboxUser.Id;
			dataObject.TargetUser = this.targetMailboxUser;
			dataObject.TargetUserId = this.targetMailboxUser.Id;
			dataObject.SourceExchangeGuid = this.sourceMailboxUser.ExchangeGuid;
			dataObject.TargetExchangeGuid = this.targetMailboxUser.ExchangeGuid;
			dataObject.ExchangeGuid = this.targetMailboxUser.ExchangeGuid;
			dataObject.SourceDatabase = this.sourceMailboxUser.Database;
			dataObject.TargetDatabase = this.targetMailboxUser.Database;
			dataObject.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
			dataObject.AllowLargeItems = this.AllowLargeItems;
			if (base.IsFieldSet("CompleteAfter"))
			{
				RequestTaskHelper.SetCompleteAfter(new DateTime?(this.CompleteAfter), dataObject, null);
			}
			dataObject.IncrementalSyncInterval = this.IncrementalSyncInterval;
			dataObject.PreserveMailboxSignature = false;
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x001ECF6F File Offset: 0x001EB16F
		protected override FolderMoveRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new FolderMoveRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(dataObject.ToString()), ErrorCategory.InvalidArgument, null);
			return null;
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x001ECFAD File Offset: 0x001EB1AD
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is StorageTransientException || exception is StoragePermanentException;
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x001ECFCC File Offset: 0x001EB1CC
		protected override string CheckRequestNameAvailability(string name, ADObjectId identifyingObjectId, bool objectIsMailbox, MRSRequestType requestType, object errorObject, bool wildcardedSearch)
		{
			string text = string.Format("{0}*", name);
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter(wildcardedSearch ? text : name, identifyingObjectId, requestType, new RequestIndexId(RequestIndexLocation.AD), objectIsMailbox);
			requestIndexEntryQueryFilter.WildcardedNameSearch = wildcardedSearch;
			ObjectId rootId = ADHandler.GetRootId(base.CurrentOrgConfigSession, requestType);
			IEnumerable<FolderMoveRequest> enumerable = ((RequestJobProvider)base.DataSession).IndexProvider.FindPaged<FolderMoveRequest>(requestIndexEntryQueryFilter, rootId, rootId == null, null, 10);
			string result;
			using (IEnumerator<FolderMoveRequest> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					bool flag = true;
					while (this.IsNewRequestAllowed(enumerator.Current))
					{
						if (!enumerator.MoveNext())
						{
							IL_93:
							if (!flag)
							{
								base.WriteError(new NameMustBeUniquePermanentException(name, (identifyingObjectId == null) ? string.Empty : identifyingObjectId.ToString()), ErrorCategory.InvalidArgument, errorObject);
								return null;
							}
							return name;
						}
					}
					flag = false;
					goto IL_93;
				}
				result = name;
			}
			return result;
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x001ED0B8 File Offset: 0x001EB2B8
		private bool IsNewRequestAllowed(FolderMoveRequest request)
		{
			return !request.Name.Equals(base.Name, StringComparison.OrdinalIgnoreCase) && (((request.SourceMailbox == null || !request.SourceMailbox.Equals(this.sourceMailboxUser.Id)) && (request.TargetMailbox == null || !request.TargetMailbox.Equals(this.targetMailboxUser.Id))) || request.Status == RequestStatus.Completed || request.Status == RequestStatus.CompletedWithWarning || request.Status == RequestStatus.Failed);
		}

		// Token: 0x04003C34 RID: 15412
		private const string DefaultFolderMoveRequestName = "FolderMove";

		// Token: 0x04003C35 RID: 15413
		internal const string TaskNoun = "FolderMoveRequest";

		// Token: 0x04003C36 RID: 15414
		internal const string ParameterSourceMailbox = "SourceMailbox";

		// Token: 0x04003C37 RID: 15415
		internal const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003C38 RID: 15416
		internal const string ParameterFolders = "Folders";

		// Token: 0x04003C39 RID: 15417
		internal const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";

		// Token: 0x04003C3A RID: 15418
		internal const string ParameterAllowLargeItems = "AllowLargeItems";

		// Token: 0x04003C3B RID: 15419
		private static TimeSpan defaultIncrementalSyncIntervalForMove = TimeSpan.FromDays(1.0);

		// Token: 0x04003C3C RID: 15420
		private ADUser sourceMailboxUser;

		// Token: 0x04003C3D RID: 15421
		private ADUser targetMailboxUser;

		// Token: 0x04003C3E RID: 15422
		private List<MoveFolderInfo> folderList = new List<MoveFolderInfo>();
	}
}
