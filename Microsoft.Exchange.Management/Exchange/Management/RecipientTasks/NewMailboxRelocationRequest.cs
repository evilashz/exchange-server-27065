using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C9A RID: 3226
	[Cmdlet("New", "MailboxRelocationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MailboxRelocationSplit")]
	public sealed class NewMailboxRelocationRequest : NewRequest<MailboxRelocationRequest>
	{
		// Token: 0x17002668 RID: 9832
		// (get) Token: 0x06007BE5 RID: 31717 RVA: 0x001FB798 File Offset: 0x001F9998
		// (set) Token: 0x06007BE6 RID: 31718 RVA: 0x001FB7AF File Offset: 0x001F99AF
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public MailboxOrMailUserIdParameter Mailbox
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17002669 RID: 9833
		// (get) Token: 0x06007BE7 RID: 31719 RVA: 0x001FB7C2 File Offset: 0x001F99C2
		// (set) Token: 0x06007BE8 RID: 31720 RVA: 0x001FB7D9 File Offset: 0x001F99D9
		[Parameter(Mandatory = false, ParameterSetName = "MailboxRelocationSplit")]
		public DatabaseIdParameter TargetDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["TargetDatabase"];
			}
			set
			{
				base.Fields["TargetDatabase"] = value;
			}
		}

		// Token: 0x1700266A RID: 9834
		// (get) Token: 0x06007BE9 RID: 31721 RVA: 0x001FB7EC File Offset: 0x001F99EC
		// (set) Token: 0x06007BEA RID: 31722 RVA: 0x001FB803 File Offset: 0x001F9A03
		[Parameter(Mandatory = true, ParameterSetName = "MailboxRelocationJoin")]
		public MailboxOrMailUserIdParameter TargetContainer
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["TargetContainer"];
			}
			set
			{
				base.Fields["TargetContainer"] = value;
			}
		}

		// Token: 0x1700266B RID: 9835
		// (get) Token: 0x06007BEB RID: 31723 RVA: 0x001FB816 File Offset: 0x001F9A16
		// (set) Token: 0x06007BEC RID: 31724 RVA: 0x001FB82D File Offset: 0x001F9A2D
		[Parameter(Mandatory = false)]
		public SkippableMoveComponent[] SkipMoving
		{
			get
			{
				return (SkippableMoveComponent[])base.Fields["SkipMoving"];
			}
			set
			{
				base.Fields["SkipMoving"] = value;
			}
		}

		// Token: 0x1700266C RID: 9836
		// (get) Token: 0x06007BED RID: 31725 RVA: 0x001FB840 File Offset: 0x001F9A40
		// (set) Token: 0x06007BEE RID: 31726 RVA: 0x001FB848 File Offset: 0x001F9A48
		private new Unlimited<int> LargeItemLimit
		{
			get
			{
				return base.LargeItemLimit;
			}
			set
			{
				base.LargeItemLimit = value;
			}
		}

		// Token: 0x1700266D RID: 9837
		// (get) Token: 0x06007BEF RID: 31727 RVA: 0x001FB851 File Offset: 0x001F9A51
		// (set) Token: 0x06007BF0 RID: 31728 RVA: 0x001FB859 File Offset: 0x001F9A59
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

		// Token: 0x1700266E RID: 9838
		// (get) Token: 0x06007BF1 RID: 31729 RVA: 0x001FB862 File Offset: 0x001F9A62
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxRelocation(base.RequestName);
			}
		}

		// Token: 0x06007BF2 RID: 31730 RVA: 0x001FB870 File Offset: 0x001F9A70
		protected override MailboxRelocationRequest ConvertToPresentationObject(TransactionalRequestJob dataObject)
		{
			if (dataObject.IndexEntries == null || dataObject.IndexEntries.Count < 1)
			{
				base.WriteError(new RequestIndexEntriesAbsentPermanentException(base.RequestName), ErrorCategory.InvalidArgument, this.Mailbox);
			}
			return new MailboxRelocationRequest(dataObject.IndexEntries[0]);
		}

		// Token: 0x06007BF3 RID: 31731 RVA: 0x001FB8BC File Offset: 0x001F9ABC
		protected override void CreateIndexEntries(TransactionalRequestJob dataObject)
		{
			RequestIndexEntryProvider.CreateAndPopulateRequestIndexEntries(dataObject, new RequestIndexId(this.mailbox.Id));
			if (base.ParameterSetName == "MailboxRelocationSplit")
			{
				RequestIndexEntryProvider.CreateAndPopulateRequestIndexEntries(dataObject, new RequestIndexId(this.sourceContainer.Id));
				return;
			}
			if (base.ParameterSetName == "MailboxRelocationJoin")
			{
				RequestIndexEntryProvider.CreateAndPopulateRequestIndexEntries(dataObject, new RequestIndexId(this.targetContainer.Id));
			}
		}

		// Token: 0x06007BF4 RID: 31732 RVA: 0x001FB930 File Offset: 0x001F9B30
		protected override void InternalBeginProcessing()
		{
			if (base.WorkloadType == RequestWorkloadType.None)
			{
				base.WorkloadType = RequestWorkloadType.SyncAggregation;
			}
			if (base.ParameterSetName == "MailboxRelocationJoin")
			{
				this.moveFlags |= RequestFlags.Join;
			}
			else if (base.ParameterSetName == "MailboxRelocationSplit")
			{
				this.moveFlags |= RequestFlags.Split;
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x001FB99C File Offset: 0x001F9B9C
		protected override void InternalStateReset()
		{
			this.mailbox = null;
			this.targetContainer = null;
			this.targetMailboxDatabase = null;
			this.sourceContainer = null;
			this.sourceDatabaseInformation = null;
			base.InternalStateReset();
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x001FB9CC File Offset: 0x001F9BCC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (!string.IsNullOrEmpty(base.Name))
				{
					base.ValidateName();
					base.RequestName = base.Name;
				}
				else
				{
					base.RequestName = base.ParameterSetName;
				}
				this.RetrieveSourceMailboxInformation();
				base.RescopeToOrgId(this.mailbox.OrganizationId);
				string parameterSetName;
				if ((parameterSetName = base.ParameterSetName) != null)
				{
					ADObjectId mdbId;
					ADObjectId mdbServerSite;
					RequestFlags requestFlags;
					if (!(parameterSetName == "MailboxRelocationJoin"))
					{
						if (!(parameterSetName == "MailboxRelocationSplit"))
						{
							goto IL_135;
						}
						if (this.mailbox.UnifiedMailbox == null || !ADRecipient.TryGetFromCrossTenantObjectId(this.mailbox.UnifiedMailbox, out this.sourceContainer).Succeeded)
						{
							base.WriteError(new MailboxRelocationSplitSourceNotInContainerException(this.mailbox.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
						}
						this.RetrieveTargetMailboxInformation();
						requestFlags = this.LocateAndChooseMdb(this.mailbox.Database, this.targetMailboxDatabase.Id, this.Mailbox, this.TargetDatabase, this.TargetDatabase, out mdbId, out mdbServerSite);
					}
					else
					{
						this.RetrieveTargetContainerAndMailboxInformation();
						requestFlags = this.LocateAndChooseMdb(this.mailbox.Database, this.targetContainer.Database, this.Mailbox, this.TargetContainer, this.TargetContainer, out mdbId, out mdbServerSite);
					}
					this.moveFlags |= requestFlags;
					base.MdbId = mdbId;
					base.MdbServerSite = mdbServerSite;
					base.Flags = this.moveFlags;
					RequestTaskHelper.ValidateNotImplicitSplit(this.moveFlags, this.mailbox, new Task.TaskErrorLoggingDelegate(base.WriteError), this.Mailbox);
					this.ValidateNoOtherActiveRequests();
					base.InternalValidate();
					return;
				}
				IL_135:
				throw new NotImplementedException();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x001FBB90 File Offset: 0x001F9D90
		protected override void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			base.SetRequestProperties(dataObject);
			dataObject.RequestType = MRSRequestType.MailboxRelocation;
			dataObject.JobType = MRSJobType.RequestJobE15_AutoResume;
			dataObject.UserId = this.mailbox.Id;
			dataObject.ExchangeGuid = this.mailbox.ExchangeGuid;
			RequestTaskHelper.ApplyOrganization(dataObject, this.mailbox.OrganizationId ?? OrganizationId.ForestWideOrgId);
			dataObject.UserOrgName = ((this.mailbox.OrganizationId != null && this.mailbox.OrganizationId.OrganizationalUnit != null) ? this.mailbox.OrganizationId.OrganizationalUnit.Name : this.mailbox.Id.DomainId.ToString());
			dataObject.DistinguishedName = this.mailbox.DistinguishedName;
			dataObject.DisplayName = this.mailbox.DisplayName;
			dataObject.Alias = this.mailbox.Alias;
			dataObject.User = this.mailbox;
			dataObject.DomainControllerToUpdate = this.mailbox.OriginatingServer;
			dataObject.SourceDatabase = ADObjectIdResolutionHelper.ResolveDN(this.mailbox.Database);
			dataObject.SourceVersion = this.sourceDatabaseInformation.Value.ServerVersion;
			this.ChooseTargetMailboxDatabase(dataObject);
			dataObject.PreserveMailboxSignature = false;
			dataObject.IncrementalSyncInterval = NewMailboxRelocationRequest.incrementalSyncInterval;
			dataObject.AllowLargeItems = true;
			RequestTaskHelper.SetSkipMoving(this.SkipMoving, dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x001FBD00 File Offset: 0x001F9F00
		private void RetrieveSourceMailboxInformation()
		{
			this.mailbox = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.Mailbox, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
			if (this.mailbox.Database == null)
			{
				base.WriteError(new MailboxLacksDatabasePermanentException(this.mailbox.ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
			}
			this.sourceDatabaseInformation = new DatabaseInformation?(MapiUtils.FindServerForMdb(this.mailbox.Database.ObjectGuid, null, null, FindServerFlags.None));
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x001FBDB4 File Offset: 0x001F9FB4
		private void RetrieveTargetContainerAndMailboxInformation()
		{
			this.targetContainer = RequestTaskHelper.ResolveADUser(base.RecipSession, base.GCSession, base.ServerSettings, this.TargetContainer, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
			if (this.targetContainer.UnifiedMailbox != null)
			{
				base.WriteError(new MailboxRelocationJoinTargetNotContainerOwnerException(this.targetContainer.ToString()), ErrorCategory.InvalidArgument, this.TargetContainer);
			}
			if (this.targetContainer.MailboxContainerGuid == null)
			{
				base.WriteError(new MailboxRelocationJoinTargetNotContainerException(this.targetContainer.ToString()), ErrorCategory.InvalidArgument, this.TargetContainer);
			}
			if (this.targetContainer.Database == null)
			{
				base.WriteError(new MailboxLacksDatabasePermanentException(this.targetContainer.ToString()), ErrorCategory.InvalidArgument, this.TargetContainer);
			}
			this.targetDatabaseInformation = new DatabaseInformation?(MapiUtils.FindServerForMdb(this.targetContainer.Database.ObjectGuid, null, null, FindServerFlags.None));
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x001FBEC4 File Offset: 0x001FA0C4
		private void RetrieveTargetMailboxInformation()
		{
			if (base.IsFieldSet("TargetDatabase"))
			{
				this.targetMailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.TargetDatabase, base.ConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.TargetDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.TargetDatabase.ToString())));
				MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.ConfigSession.SessionSettings, this.targetMailboxDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			else
			{
				this.targetMailboxDatabase = this.ChooseTargetMailboxDatabase(this.mailbox.Database);
			}
			DatabaseInformation value = MapiUtils.FindServerForMdb(this.targetMailboxDatabase.Id.ObjectGuid, null, null, FindServerFlags.None);
			this.targetDatabaseInformation = new DatabaseInformation?(value);
			if (!this.IsSupportedDatabaseVersion(value.ServerVersion, NewRequest<MailboxRelocationRequest>.DatabaseSide.Target))
			{
				base.WriteError(new DatabaseVersionUnsupportedPermanentException(this.targetMailboxDatabase.Identity.ToString(), value.ServerFqdn, new ServerVersion(value.ServerVersion).ToString()), ErrorCategory.InvalidArgument, this.Mailbox);
			}
			if (this.targetMailboxDatabase.Recovery)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorTargetDatabaseIsRecovery(this.targetMailboxDatabase.ToString())), ErrorCategory.InvalidArgument, this.Mailbox);
			}
			if (this.mailbox.MailboxProvisioningConstraint != null && this.targetMailboxDatabase.MailboxProvisioningAttributes != null && !this.mailbox.MailboxProvisioningConstraint.IsMatch(this.targetMailboxDatabase.MailboxProvisioningAttributes))
			{
				base.WriteError(new MailboxConstraintsMismatchException(this.mailbox.ToString(), this.targetMailboxDatabase.Name, this.mailbox.MailboxProvisioningConstraint.Value), ErrorCategory.InvalidData, this.Mailbox);
			}
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x001FC070 File Offset: 0x001FA270
		private void ChooseTargetMailboxDatabase(TransactionalRequestJob dataObject)
		{
			if (this.targetContainer != null)
			{
				dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetContainer.Database);
				dataObject.TargetContainerGuid = this.targetContainer.MailboxContainerGuid;
				dataObject.TargetUnifiedMailboxId = this.targetContainer.GetCrossTenantObjectId();
			}
			else if (this.targetMailboxDatabase != null)
			{
				dataObject.TargetDatabase = ADObjectIdResolutionHelper.ResolveDN(this.targetMailboxDatabase.Id);
				this.targetDatabaseInformation = new DatabaseInformation?(MapiUtils.FindServerForMdb(this.targetMailboxDatabase.Id.ObjectGuid, null, null, FindServerFlags.None));
			}
			dataObject.TargetVersion = this.targetDatabaseInformation.Value.ServerVersion;
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x001FC11C File Offset: 0x001FA31C
		private MailboxDatabase ChooseTargetMailboxDatabase(ADObjectId sourceMailboxDatabase)
		{
			return RequestTaskHelper.ChooseTargetMDB(new ADObjectId[]
			{
				sourceMailboxDatabase
			}, false, this.mailbox, base.DomainController, base.ScopeSet, new Action<LocalizedString>(base.WriteVerbose), new Action<LocalizedException, ExchangeErrorCategory, object>(base.WriteError), new Action<Exception, ErrorCategory, object>(base.WriteError), this.Mailbox);
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x001FC178 File Offset: 0x001FA378
		private void ValidateNoOtherActiveRequests()
		{
			string otherRequests = MailboxRequestIndexEntryHandler.GetOtherRequests(this.mailbox, null);
			if (!string.IsNullOrEmpty(otherRequests))
			{
				base.WriteError(new ObjectInvolvedInMultipleRelocationsPermanentException(MrsStrings.Mailbox, otherRequests), ErrorCategory.InvalidArgument, this.Mailbox);
			}
			ADRecipient adrecipient;
			if (this.mailbox.UnifiedMailbox != null && ADRecipient.TryGetFromCrossTenantObjectId(this.mailbox.UnifiedMailbox, out adrecipient).Succeeded)
			{
				otherRequests = MailboxRequestIndexEntryHandler.GetOtherRequests((ADUser)adrecipient, null);
				if (!string.IsNullOrEmpty(otherRequests))
				{
					base.WriteError(new ObjectInvolvedInMultipleRelocationsPermanentException(MrsStrings.SourceContainer, otherRequests), ErrorCategory.InvalidArgument, this.Mailbox);
				}
			}
			if (this.targetContainer != null)
			{
				otherRequests = MailboxRequestIndexEntryHandler.GetOtherRequests(this.targetContainer, null);
				if (!string.IsNullOrEmpty(otherRequests))
				{
					base.WriteError(new ObjectInvolvedInMultipleRelocationsPermanentException(MrsStrings.TargetContainer, otherRequests), ErrorCategory.InvalidArgument, this.Mailbox);
				}
			}
		}

		// Token: 0x04003D51 RID: 15697
		public const string DefaultMailboxRelocationRequestName = "MailboxRelocation";

		// Token: 0x04003D52 RID: 15698
		public const string TaskNoun = "MailboxRelocationRequest";

		// Token: 0x04003D53 RID: 15699
		public const string ParameterSetJoin = "MailboxRelocationJoin";

		// Token: 0x04003D54 RID: 15700
		public const string ParameterSetSplit = "MailboxRelocationSplit";

		// Token: 0x04003D55 RID: 15701
		public const string ParameterTargetContainer = "TargetContainer";

		// Token: 0x04003D56 RID: 15702
		private static readonly TimeSpan incrementalSyncInterval = TimeSpan.FromDays(1.0);

		// Token: 0x04003D57 RID: 15703
		private ADUser mailbox;

		// Token: 0x04003D58 RID: 15704
		private ADRecipient sourceContainer;

		// Token: 0x04003D59 RID: 15705
		private ADUser targetContainer;

		// Token: 0x04003D5A RID: 15706
		private MailboxDatabase targetMailboxDatabase;

		// Token: 0x04003D5B RID: 15707
		private DatabaseInformation? sourceDatabaseInformation;

		// Token: 0x04003D5C RID: 15708
		private DatabaseInformation? targetDatabaseInformation;

		// Token: 0x04003D5D RID: 15709
		private RequestFlags moveFlags = RequestFlags.IntraOrg;
	}
}
