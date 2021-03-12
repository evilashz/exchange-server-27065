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
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA8 RID: 3240
	[Cmdlet("New", "PublicFolderMoveRequest", SupportsShouldProcess = true)]
	public sealed class NewPublicFolderMoveRequest : NewRequest<PublicFolderMoveRequest>
	{
		// Token: 0x1700268A RID: 9866
		// (get) Token: 0x06007C50 RID: 31824 RVA: 0x001FD32F File Offset: 0x001FB52F
		// (set) Token: 0x06007C51 RID: 31825 RVA: 0x001FD346 File Offset: 0x001FB546
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700268B RID: 9867
		// (get) Token: 0x06007C52 RID: 31826 RVA: 0x001FD359 File Offset: 0x001FB559
		// (set) Token: 0x06007C53 RID: 31827 RVA: 0x001FD370 File Offset: 0x001FB570
		[ValidateNotNull]
		[Parameter(Mandatory = true)]
		public PublicFolderIdParameter[] Folders
		{
			get
			{
				return (PublicFolderIdParameter[])base.Fields["SourceFolder"];
			}
			set
			{
				base.Fields["SourceFolder"] = value;
			}
		}

		// Token: 0x1700268C RID: 9868
		// (get) Token: 0x06007C54 RID: 31828 RVA: 0x001FD383 File Offset: 0x001FB583
		// (set) Token: 0x06007C55 RID: 31829 RVA: 0x001FD39A File Offset: 0x001FB59A
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

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x06007C56 RID: 31830 RVA: 0x001FD3AD File Offset: 0x001FB5AD
		// (set) Token: 0x06007C57 RID: 31831 RVA: 0x001FD3D3 File Offset: 0x001FB5D3
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

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x06007C58 RID: 31832 RVA: 0x001FD3EB File Offset: 0x001FB5EB
		// (set) Token: 0x06007C59 RID: 31833 RVA: 0x001FD411 File Offset: 0x001FB611
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

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x06007C5A RID: 31834 RVA: 0x001FD429 File Offset: 0x001FB629
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPublicFolderMoveRequest((this.DataObject == null) ? base.RequestName : this.DataObject.ToString());
			}
		}

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x06007C5B RID: 31835 RVA: 0x001FD44B File Offset: 0x001FB64B
		// (set) Token: 0x06007C5C RID: 31836 RVA: 0x001FD453 File Offset: 0x001FB653
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

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x06007C5D RID: 31837 RVA: 0x001FD45C File Offset: 0x001FB65C
		// (set) Token: 0x06007C5E RID: 31838 RVA: 0x001FD464 File Offset: 0x001FB664
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

		// Token: 0x06007C5F RID: 31839 RVA: 0x001FD46D File Offset: 0x001FB66D
		protected override void InternalStateReset()
		{
			this.sourceMailboxUser = null;
			this.targetMailboxUser = null;
			base.InternalStateReset();
		}

		// Token: 0x06007C60 RID: 31840 RVA: 0x001FD484 File Offset: 0x001FB684
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (MapiTaskHelper.IsDatacenter)
				{
					base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, this.Organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
					base.RescopeToOrgId(base.CurrentOrganizationId);
					base.OrganizationId = base.CurrentOrganizationId;
				}
				else
				{
					base.OrganizationId = OrganizationId.ForestWideOrgId;
				}
				this.DisallowPublicFolderMoveDuringFinalization();
				this.targetMailboxUser = (ADUser)base.GetDataObject<ADUser>(this.TargetMailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.TargetMailbox.ToString())), ExchangeErrorCategory.Client);
				TenantPublicFolderConfigurationCache.Instance.RemoveValue(base.CurrentOrganizationId);
				TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(base.CurrentOrganizationId);
				if (value.GetLocalMailboxRecipient(this.targetMailboxUser.ExchangeGuid) == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotMovePublicFolderIntoNonPublicFolderMailbox), ErrorCategory.InvalidArgument, this.targetMailboxUser);
				}
				string text = this.GetSourceMailboxGuid().ToString();
				this.sourceMailboxUser = (ADUser)base.GetDataObject<ADUser>(MailboxIdParameter.Parse(text), base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(text)), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(text)), ExchangeErrorCategory.Client);
				if (this.sourceMailboxUser.ExchangeGuid == this.targetMailboxUser.ExchangeGuid)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotMovePublicFolderIntoSameMailbox), ErrorCategory.InvalidArgument, this.targetMailboxUser);
				}
				if (!string.IsNullOrEmpty(base.Name))
				{
					base.ValidateName();
					base.RequestName = base.Name;
				}
				else
				{
					base.RequestName = "PublicFolderMove";
				}
				ADObjectId mdbId = null;
				ADObjectId mdbServerSite = null;
				base.Flags = (RequestFlags.IntraOrg | this.LocateAndChooseMdb(value.GetLocalMailboxRecipient(this.sourceMailboxUser.ExchangeGuid).Database, value.GetLocalMailboxRecipient(this.targetMailboxUser.ExchangeGuid).Database, this.sourceMailboxUser, this.targetMailboxUser, this.targetMailboxUser, out mdbId, out mdbServerSite));
				if (base.WorkloadType == RequestWorkloadType.None)
				{
					base.WorkloadType = RequestWorkloadType.Local;
				}
				base.MdbId = mdbId;
				base.MdbServerSite = mdbServerSite;
				base.WriteVerbose(Strings.RequestQueueIdentified(base.MdbId.Name));
				this.CheckRequestNameAvailability(null, null, false, MRSRequestType.PublicFolderMove, this.Organization, false);
				base.WriteVerbose(Strings.RequestNameAvailabilityComplete);
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007C61 RID: 31841 RVA: 0x001FD708 File Offset: 0x001FB908
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.PublicFolderMove;
			dataObject.WorkloadType = base.WorkloadType;
			RequestTaskHelper.ApplyOrganization(dataObject, base.OrganizationId ?? OrganizationId.ForestWideOrgId);
			dataObject.FolderList = this.folderList;
			dataObject.SourceUserId = this.sourceMailboxUser.Id;
			dataObject.TargetUserId = this.targetMailboxUser.Id;
			dataObject.SourceExchangeGuid = this.sourceMailboxUser.ExchangeGuid;
			dataObject.TargetExchangeGuid = this.targetMailboxUser.ExchangeGuid;
			dataObject.ExchangeGuid = this.targetMailboxUser.ExchangeGuid;
			dataObject.SourceDatabase = this.sourceMailboxUser.Database;
			dataObject.TargetDatabase = this.targetMailboxUser.Database;
			dataObject.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
			dataObject.AllowLargeItems = this.AllowLargeItems;
			dataObject.PreserveMailboxSignature = false;
		}

		// Token: 0x06007C62 RID: 31842 RVA: 0x001FD7F0 File Offset: 0x001FB9F0
		protected override PublicFolderMoveRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries != null && dataObject.IndexEntries.Count >= 1)
			{
				return new PublicFolderMoveRequest(dataObject.IndexEntries[0]);
			}
			base.WriteError(new RequestIndexEntriesAbsentPermanentException(dataObject.ToString()), ErrorCategory.InvalidArgument, this.Organization);
			return null;
		}

		// Token: 0x06007C63 RID: 31843 RVA: 0x001FD83E File Offset: 0x001FBA3E
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is StorageTransientException || exception is StoragePermanentException;
		}

		// Token: 0x06007C64 RID: 31844 RVA: 0x001FD85C File Offset: 0x001FBA5C
		private void DisallowPublicFolderMoveDuringFinalization()
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled && CommonUtils.IsPublicFolderMailboxesLockedForNewConnectionsFlagSet(base.CurrentOrganizationId))
			{
				base.WriteError(new RecipientTaskException(new LocalizedString(ServerStrings.PublicFoldersCannotBeMovedDuringMigration)), ErrorCategory.InvalidOperation, this.targetMailboxUser);
			}
		}

		// Token: 0x06007C65 RID: 31845 RVA: 0x001FD8B8 File Offset: 0x001FBAB8
		private Guid GetSourceMailboxGuid()
		{
			Guid guid = Guid.Empty;
			base.WriteVerbose(Strings.DetermineSourceMailbox);
			using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "New-PublicFolderMoveRequest", Guid.Empty))
			{
				foreach (PublicFolderIdParameter publicFolderIdParameter in this.Folders)
				{
					PublicFolder publicFolder = (PublicFolder)base.GetDataObject<PublicFolder>(publicFolderIdParameter, publicFolderDataProvider, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(publicFolderIdParameter.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(publicFolderIdParameter.ToString())));
					if (publicFolderDataProvider.PublicFolderSession.IsWellKnownFolder(publicFolder.InternalFolderIdentity.ObjectId))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotMoveWellKnownPublicFolders), ErrorCategory.InvalidArgument, publicFolderIdParameter);
					}
					if (guid == Guid.Empty)
					{
						guid = publicFolder.ContentMailboxGuid;
					}
					else if (guid != publicFolder.ContentMailboxGuid)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotMovePublicFoldersFromDifferentSourceMailbox), ErrorCategory.InvalidArgument, publicFolderIdParameter);
					}
					if (publicFolder.EntryId == null || publicFolder.DumpsterEntryId == null)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotMovePublicFoldersWithNullEntryId), ErrorCategory.InvalidData, publicFolderIdParameter);
					}
					this.folderList.Add(new MoveFolderInfo(publicFolder.EntryId, false));
					this.folderList.Add(new MoveFolderInfo(publicFolder.DumpsterEntryId, false));
				}
			}
			return guid;
		}

		// Token: 0x04003D6B RID: 15723
		private const string DefaultPublicFolderMoveRequestName = "PublicFolderMove";

		// Token: 0x04003D6C RID: 15724
		internal const string TaskNoun = "PublicFolderMoveRequest";

		// Token: 0x04003D6D RID: 15725
		internal const string ParameterOrganization = "Organization";

		// Token: 0x04003D6E RID: 15726
		internal const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x04003D6F RID: 15727
		internal const string ParameterSourceFolder = "SourceFolder";

		// Token: 0x04003D70 RID: 15728
		internal const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";

		// Token: 0x04003D71 RID: 15729
		internal const string ParameterAllowLargeItems = "AllowLargeItems";

		// Token: 0x04003D72 RID: 15730
		private ADUser sourceMailboxUser;

		// Token: 0x04003D73 RID: 15731
		private ADUser targetMailboxUser;

		// Token: 0x04003D74 RID: 15732
		private List<MoveFolderInfo> folderList = new List<MoveFolderInfo>();
	}
}
