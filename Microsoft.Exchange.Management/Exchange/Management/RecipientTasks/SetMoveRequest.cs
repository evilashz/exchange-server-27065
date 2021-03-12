using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C7E RID: 3198
	[Cmdlet("Set", "MoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMoveRequest : SetMoveRequestBase
	{
		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x06007AC4 RID: 31428 RVA: 0x001F7020 File Offset: 0x001F5220
		// (set) Token: 0x06007AC5 RID: 31429 RVA: 0x001F7041 File Offset: 0x001F5241
		[Parameter(Mandatory = false)]
		public bool SuspendWhenReadyToComplete
		{
			get
			{
				return (bool)(base.Fields["SuspendWhenReadyToComplete"] ?? false);
			}
			set
			{
				base.Fields["SuspendWhenReadyToComplete"] = value;
			}
		}

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x06007AC6 RID: 31430 RVA: 0x001F7059 File Offset: 0x001F5259
		// (set) Token: 0x06007AC7 RID: 31431 RVA: 0x001F7070 File Offset: 0x001F5270
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public Fqdn RemoteGlobalCatalog
		{
			get
			{
				return (Fqdn)base.Fields["RemoteGlobalCatalog"];
			}
			set
			{
				base.Fields["RemoteGlobalCatalog"] = value;
			}
		}

		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x06007AC8 RID: 31432 RVA: 0x001F7083 File Offset: 0x001F5283
		// (set) Token: 0x06007AC9 RID: 31433 RVA: 0x001F70A8 File Offset: 0x001F52A8
		[Parameter(Mandatory = false)]
		public Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["BadItemLimit"] ?? RequestTaskHelper.UnlimitedZero);
			}
			set
			{
				base.Fields["BadItemLimit"] = value;
			}
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x06007ACA RID: 31434 RVA: 0x001F70C0 File Offset: 0x001F52C0
		// (set) Token: 0x06007ACB RID: 31435 RVA: 0x001F70E5 File Offset: 0x001F52E5
		[Parameter(Mandatory = false)]
		public Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["LargeItemLimit"] ?? RequestTaskHelper.UnlimitedZero);
			}
			set
			{
				base.Fields["LargeItemLimit"] = value;
			}
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x06007ACC RID: 31436 RVA: 0x001F70FD File Offset: 0x001F52FD
		// (set) Token: 0x06007ACD RID: 31437 RVA: 0x001F7123 File Offset: 0x001F5323
		[Parameter(Mandatory = false)]
		public SwitchParameter AcceptLargeDataLoss
		{
			get
			{
				return (SwitchParameter)(base.Fields["AcceptLargeDataLoss"] ?? false);
			}
			set
			{
				base.Fields["AcceptLargeDataLoss"] = value;
			}
		}

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x06007ACE RID: 31438 RVA: 0x001F713B File Offset: 0x001F533B
		// (set) Token: 0x06007ACF RID: 31439 RVA: 0x001F7152 File Offset: 0x001F5352
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public Fqdn RemoteHostName
		{
			get
			{
				return (Fqdn)base.Fields["RemoteHostName"];
			}
			set
			{
				base.Fields["RemoteHostName"] = value;
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x06007AD0 RID: 31440 RVA: 0x001F7165 File Offset: 0x001F5365
		// (set) Token: 0x06007AD1 RID: 31441 RVA: 0x001F717C File Offset: 0x001F537C
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public PSCredential RemoteCredential
		{
			get
			{
				return (PSCredential)base.Fields["RemoteCredential"];
			}
			set
			{
				base.Fields["RemoteCredential"] = value;
			}
		}

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x06007AD2 RID: 31442 RVA: 0x001F718F File Offset: 0x001F538F
		// (set) Token: 0x06007AD3 RID: 31443 RVA: 0x001F71B0 File Offset: 0x001F53B0
		[Parameter(Mandatory = false)]
		public bool Protect
		{
			get
			{
				return (bool)(base.Fields["Protect"] ?? false);
			}
			set
			{
				base.Fields["Protect"] = value;
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x06007AD4 RID: 31444 RVA: 0x001F71C8 File Offset: 0x001F53C8
		// (set) Token: 0x06007AD5 RID: 31445 RVA: 0x001F71E9 File Offset: 0x001F53E9
		[Parameter(Mandatory = false)]
		public bool IgnoreRuleLimitErrors
		{
			get
			{
				return (bool)(base.Fields["IgnoreRuleLimitErrors"] ?? false);
			}
			set
			{
				base.Fields["IgnoreRuleLimitErrors"] = value;
			}
		}

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x06007AD6 RID: 31446 RVA: 0x001F7201 File Offset: 0x001F5401
		// (set) Token: 0x06007AD7 RID: 31447 RVA: 0x001F7218 File Offset: 0x001F5418
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x06007AD8 RID: 31448 RVA: 0x001F722B File Offset: 0x001F542B
		// (set) Token: 0x06007AD9 RID: 31449 RVA: 0x001F724D File Offset: 0x001F544D
		[Parameter(Mandatory = false)]
		public RequestPriority Priority
		{
			get
			{
				return (RequestPriority)(base.Fields["Priority"] ?? RequestPriority.Normal);
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17002611 RID: 9745
		// (get) Token: 0x06007ADA RID: 31450 RVA: 0x001F7265 File Offset: 0x001F5465
		// (set) Token: 0x06007ADB RID: 31451 RVA: 0x001F728A File Offset: 0x001F548A
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)(base.Fields["CompletedRequestAgeLimit"] ?? RequestTaskHelper.DefaultCompletedRequestAgeLimit);
			}
			set
			{
				base.Fields["CompletedRequestAgeLimit"] = value;
			}
		}

		// Token: 0x17002612 RID: 9746
		// (get) Token: 0x06007ADC RID: 31452 RVA: 0x001F72A2 File Offset: 0x001F54A2
		// (set) Token: 0x06007ADD RID: 31453 RVA: 0x001F72C3 File Offset: 0x001F54C3
		[Parameter(Mandatory = false)]
		public bool PreventCompletion
		{
			get
			{
				return (bool)(base.Fields["PreventCompletion"] ?? false);
			}
			set
			{
				base.Fields["PreventCompletion"] = value;
			}
		}

		// Token: 0x17002613 RID: 9747
		// (get) Token: 0x06007ADE RID: 31454 RVA: 0x001F72DB File Offset: 0x001F54DB
		// (set) Token: 0x06007ADF RID: 31455 RVA: 0x001F72F7 File Offset: 0x001F54F7
		[Parameter(Mandatory = false)]
		public SkippableMoveComponent[] SkipMoving
		{
			get
			{
				return (SkippableMoveComponent[])(base.Fields["SkipMoving"] ?? null);
			}
			set
			{
				base.Fields["SkipMoving"] = value;
			}
		}

		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x06007AE0 RID: 31456 RVA: 0x001F730A File Offset: 0x001F550A
		// (set) Token: 0x06007AE1 RID: 31457 RVA: 0x001F7326 File Offset: 0x001F5526
		[Parameter(Mandatory = false)]
		public InternalMrsFlag[] InternalFlags
		{
			get
			{
				return (InternalMrsFlag[])(base.Fields["InternalFlags"] ?? null);
			}
			set
			{
				base.Fields["InternalFlags"] = value;
			}
		}

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x06007AE2 RID: 31458 RVA: 0x001F7339 File Offset: 0x001F5539
		// (set) Token: 0x06007AE3 RID: 31459 RVA: 0x001F7350 File Offset: 0x001F5550
		[Parameter(Mandatory = false)]
		public DateTime? StartAfter
		{
			get
			{
				return (DateTime?)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x06007AE4 RID: 31460 RVA: 0x001F7368 File Offset: 0x001F5568
		// (set) Token: 0x06007AE5 RID: 31461 RVA: 0x001F737F File Offset: 0x001F557F
		[Parameter(Mandatory = false)]
		public DateTime? CompleteAfter
		{
			get
			{
				return (DateTime?)base.Fields["CompleteAfter"];
			}
			set
			{
				base.Fields["CompleteAfter"] = value;
			}
		}

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x06007AE6 RID: 31462 RVA: 0x001F7397 File Offset: 0x001F5597
		// (set) Token: 0x06007AE7 RID: 31463 RVA: 0x001F73AE File Offset: 0x001F55AE
		[Parameter(Mandatory = false)]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)base.Fields["IncrementalSyncInterval"];
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x06007AE8 RID: 31464 RVA: 0x001F73C6 File Offset: 0x001F55C6
		// (set) Token: 0x06007AE9 RID: 31465 RVA: 0x001F73DD File Offset: 0x001F55DD
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
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

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x001F73F0 File Offset: 0x001F55F0
		// (set) Token: 0x06007AEB RID: 31467 RVA: 0x001F7407 File Offset: 0x001F5607
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter ArchiveTargetDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["ArchiveTargetDatabase"];
			}
			set
			{
				base.Fields["ArchiveTargetDatabase"] = value;
			}
		}

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x06007AEC RID: 31468 RVA: 0x001F741A File Offset: 0x001F561A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRequest(base.LocalADUser.ToString());
			}
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x001F742C File Offset: 0x001F562C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			RequestTaskHelper.ValidateItemLimits(this.BadItemLimit, this.LargeItemLimit, this.AcceptLargeDataLoss, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), base.ExecutingUserIdentity);
			if (this.BatchName != null && this.BatchName.Length > 255)
			{
				base.WriteError(new ParameterLengthExceededPermanentException("BatchName", 255), ErrorCategory.InvalidArgument, this.BatchName);
			}
			if (this.TargetDatabase != null)
			{
				this.specifiedTargetMDB = this.LocateAndVerifyMdb(this.TargetDatabase, out this.newTargetServerVersion);
			}
			if (this.ArchiveTargetDatabase != null)
			{
				this.specifiedArchiveTargetMDB = this.LocateAndVerifyMdb(this.ArchiveTargetDatabase, out this.newArchiveTargetServerVersion);
			}
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x001F74EC File Offset: 0x001F56EC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.InternalValidate();
				TransactionalRequestJob dataObject = this.DataObject;
				if (base.IsFieldSet("TargetDatabase"))
				{
					ServerVersion serverVersion = new ServerVersion(MapiUtils.FindServerForMdb(dataObject.TargetDatabase.ObjectGuid, null, null, FindServerFlags.None).ServerVersion);
					if (this.newTargetServerVersion.Major != serverVersion.Major || this.newTargetServerVersion.Minor != serverVersion.Minor)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotRetargetToDifferentVersionServerThanOriginal(this.newTargetServerVersion.ToString(), serverVersion.ToString())), ErrorCategory.InvalidArgument, base.Identity);
					}
				}
				if (base.IsFieldSet("ArchiveTargetDatabase"))
				{
					ServerVersion serverVersion2 = new ServerVersion(MapiUtils.FindServerForMdb(dataObject.TargetArchiveDatabase.ObjectGuid, null, null, FindServerFlags.None).ServerVersion);
					if (this.newArchiveTargetServerVersion.Major != serverVersion2.Major || this.newArchiveTargetServerVersion.Minor != serverVersion2.Minor)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorCannotRetargetToDifferentVersionArchiveServerThanOriginal(this.newArchiveTargetServerVersion.ToString(), serverVersion2.ToString())), ErrorCategory.InvalidArgument, base.Identity);
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x001F7640 File Offset: 0x001F5840
		protected override void ValidateMoveRequest(TransactionalRequestJob moveRequest)
		{
			base.ValidateMoveRequestIsActive(moveRequest);
			base.ValidateMoveRequestProtectionStatus(moveRequest);
			base.ValidateMoveRequestIsSettable(moveRequest);
			if (base.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				if (moveRequest.IsOffline)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorNoOfflineSuspendWhenReadyToComplete(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
				}
				if (RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Completion))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotAutoSuspendMoveAlreadyCompleting(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
				}
			}
			if (base.IsFieldSet("RemoteCredential") && moveRequest.RequestStyle == RequestStyle.IntraOrg)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNoRemoteCredentialSettingForLocalMove(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (base.IsFieldSet("RemoteGlobalCatalog") && moveRequest.RequestStyle == RequestStyle.IntraOrg)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNoRemoteGlobalCatalogSettingForLocalMove(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (base.IsFieldSet("RemoteHostName") && moveRequest.RequestStyle == RequestStyle.IntraOrg)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNoRemoteHostNameSettingForLocalMove(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (base.IsFieldSet("BadItemLimit") && this.BadItemLimit < new Unlimited<int>(moveRequest.BadItemsEncountered))
			{
				base.WriteError(new BadItemLimitAlreadyExceededPermanentException(moveRequest.Name, moveRequest.BadItemsEncountered, this.BadItemLimit.ToString()), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (base.IsFieldSet("LargeItemLimit") && moveRequest.AllowLargeItems)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorIncompatibleParameters("AllowLargeItems", "LargeItemLimit")), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (base.IsFieldSet("LargeItemLimit") && this.LargeItemLimit < new Unlimited<int>(moveRequest.LargeItemsEncountered))
			{
				base.WriteError(new LargeItemLimitAlreadyExceededPermanentException(moveRequest.Name, moveRequest.LargeItemsEncountered, this.LargeItemLimit.ToString()), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (this.PreventCompletion)
			{
				if (moveRequest.IsOffline)
				{
					base.WriteError(new CannotPreventCompletionForOfflineMovePermanentException(), ErrorCategory.InvalidArgument, this.PreventCompletion);
				}
				if (RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Completion))
				{
					base.WriteError(new CannotPreventCompletionForCompletingMovePermanentException(), ErrorCategory.InvalidArgument, this.PreventCompletion);
				}
				if (moveRequest.JobType >= MRSJobType.RequestJobE15_AutoResume)
				{
					base.WriteError(new SuspendWRTCPreventCompletionCannotBeSetOnNewRequestsException(), ErrorCategory.InvalidArgument, this.PreventCompletion);
				}
			}
			DateTime? timestamp = moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter);
			bool flag = RequestTaskHelper.CompareUtcTimeWithLocalTime(timestamp, this.StartAfter);
			DateTime? timestamp2 = moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter);
			bool flag2 = RequestTaskHelper.CompareUtcTimeWithLocalTime(timestamp2, this.CompleteAfter);
			bool flag3 = base.IsFieldSet("StartAfter") && !flag;
			bool flag4 = base.IsFieldSet("CompleteAfter") && !flag2;
			if (flag3 || flag4 || base.IsFieldSet("IncrementalSyncInterval"))
			{
				this.CheckMoveStatusWithStartAfterAndCompleteAfterIncrementalSyncInterval(moveRequest);
			}
			if (flag3)
			{
				this.CheckMoveStatusInQueuedForStartAfterSet(moveRequest);
			}
			bool flag5 = base.IsFieldSet("SuspendWhenReadyToComplete") ? this.SuspendWhenReadyToComplete : moveRequest.SuspendWhenReadyToComplete;
			if (flag5 && moveRequest.JobType >= MRSJobType.RequestJobE15_AutoResume)
			{
				base.WriteError(new SuspendWRTCPreventCompletionCannotBeSetOnNewRequestsException(), ErrorCategory.InvalidArgument, this.SuspendWhenReadyToComplete);
			}
			DateTime utcNow = DateTime.UtcNow;
			if (flag3 && this.StartAfter != null)
			{
				RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.Value.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), utcNow);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.IsFieldSet("StartAfter") && flag)
			{
				this.WriteWarning(Strings.WarningScheduledTimeIsUnchanged("StartAfter"));
			}
			if (base.IsFieldSet("CompleteAfter") && flag2)
			{
				this.WriteWarning(Strings.WarningScheduledTimeIsUnchanged("CompleteAfter"));
			}
			if (base.IsFieldSet("TargetDatabase") || base.IsFieldSet("ArchiveTargetDatabase"))
			{
				if (!moveRequest.TargetIsLocal)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotRetargetOutboundMoves(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
				}
				if (!RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Queued))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCanRetargetOnlyQueuedMoves(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
				}
				if (base.IsFieldSet("TargetDatabase") && moveRequest.ArchiveOnly)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotRetargetPrimaryForArchiveOnlyMoves(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
				}
				if (base.IsFieldSet("ArchiveTargetDatabase") && moveRequest.PrimaryOnly)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotRetargetArchiveForPrimaryOnlyMoves(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
				}
			}
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x001F7B21 File Offset: 0x001F5D21
		private void CheckMoveStatusInQueuedForStartAfterSet(TransactionalRequestJob moveRequest)
		{
			if (!RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Queued))
			{
				base.WriteError(new ErrorStartAfterCanBeSetOnlyInQueuedException(), ErrorCategory.InvalidArgument, base.Identity);
			}
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x001F7B44 File Offset: 0x001F5D44
		private void CheckMoveStatusWithStartAfterAndCompleteAfterIncrementalSyncInterval(TransactionalRequestJob moveRequest)
		{
			if (moveRequest.IsOffline)
			{
				base.WriteError(new StartAfterOrCompleteAfterCannotBeSetForOfflineMovesException(), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Completion) && !RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.IncrementalSync))
			{
				base.WriteError(new StartAfterOrCompleteAfterCannotBeSetWhenJobCompletingException(), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (moveRequest.JobType < MRSJobType.RequestJobE15_AutoResume)
			{
				base.WriteError(new StartAfterOrCompleteAfterCannotBeSetOnLegacyRequestsException(), ErrorCategory.InvalidArgument, base.Identity);
			}
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x001F7BB8 File Offset: 0x001F5DB8
		protected override void ModifyMoveRequest(TransactionalRequestJob moveRequest)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Set-MoveRequest changed values:");
			this.mdbGuid = moveRequest.WorkItemQueueMdb.ObjectGuid;
			if (base.LocalADUser != null)
			{
				moveRequest.DomainControllerToUpdate = base.LocalADUser.OriginatingServer;
			}
			if (base.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				stringBuilder.AppendLine(string.Format("SWRTC: {0} -> {1}", moveRequest.SuspendWhenReadyToComplete, this.SuspendWhenReadyToComplete));
				moveRequest.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
			}
			if (base.IsFieldSet("RemoteCredential"))
			{
				string remoteCredentialUsername = moveRequest.RemoteCredentialUsername;
				moveRequest.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, null);
				stringBuilder.AppendLine(string.Format("RemoteCredential: {0}:<pwd> -> {1}:<pwd>", remoteCredentialUsername, moveRequest.RemoteCredentialUsername));
				if ((moveRequest.Flags & RequestFlags.RemoteLegacy) != RequestFlags.None)
				{
					if (moveRequest.Direction == RequestDirection.Pull)
					{
						moveRequest.SourceCredential = moveRequest.RemoteCredential;
					}
					else
					{
						moveRequest.TargetCredential = moveRequest.RemoteCredential;
					}
				}
			}
			if (base.IsFieldSet("RemoteGlobalCatalog"))
			{
				string arg;
				if ((moveRequest.Flags & RequestFlags.RemoteLegacy) != RequestFlags.None)
				{
					if (moveRequest.Direction == RequestDirection.Pull)
					{
						arg = moveRequest.SourceDCName;
						moveRequest.SourceDCName = this.RemoteGlobalCatalog;
					}
					else
					{
						arg = moveRequest.TargetDCName;
						moveRequest.TargetDCName = this.RemoteGlobalCatalog;
					}
				}
				else
				{
					arg = moveRequest.RemoteDomainControllerToUpdate;
					moveRequest.RemoteDomainControllerToUpdate = this.RemoteGlobalCatalog;
				}
				stringBuilder.AppendLine(string.Format("RemoteGC: {0} -> {1}", arg, this.RemoteGlobalCatalog));
			}
			if (base.IsFieldSet("RemoteHostName"))
			{
				stringBuilder.AppendLine(string.Format("RemoteHostName: {0} -> {1}", moveRequest.RemoteHostName, this.RemoteHostName));
				moveRequest.RemoteHostName = this.RemoteHostName;
			}
			if (base.IsFieldSet("BadItemLimit"))
			{
				stringBuilder.AppendLine(string.Format("BadItemLimit: {0} -> {1}", moveRequest.BadItemLimit, this.BadItemLimit));
				moveRequest.BadItemLimit = this.BadItemLimit;
			}
			if (base.IsFieldSet("LargeItemLimit"))
			{
				stringBuilder.AppendLine(string.Format("LargeItemLimit: {0} -> {1}", moveRequest.LargeItemLimit, this.LargeItemLimit));
				moveRequest.LargeItemLimit = this.LargeItemLimit;
			}
			if (base.IsFieldSet("Protect"))
			{
				stringBuilder.AppendLine(string.Format("Protect: {0} -> {1}", moveRequest.Protect, this.Protect));
				moveRequest.Protect = this.Protect;
			}
			if (base.IsFieldSet("IgnoreRuleLimitErrors"))
			{
				stringBuilder.AppendLine(string.Format("IgnoreRuleLimitErrors: {0} -> {1}", moveRequest.IgnoreRuleLimitErrors, this.IgnoreRuleLimitErrors));
				moveRequest.IgnoreRuleLimitErrors = this.IgnoreRuleLimitErrors;
			}
			if (base.IsFieldSet("BatchName"))
			{
				stringBuilder.AppendLine(string.Format("BatchName: {0} -> {1}", moveRequest.BatchName, this.BatchName));
				moveRequest.BatchName = this.BatchName;
			}
			if (base.IsFieldSet("Priority"))
			{
				stringBuilder.AppendLine(string.Format("Priority: {0} -> {1}", moveRequest.Priority, this.Priority));
				moveRequest.Priority = this.Priority;
			}
			if (base.IsFieldSet("CompletedRequestAgeLimit"))
			{
				stringBuilder.AppendLine(string.Format("CompletedRequestAgeLimit: {0} -> {1}", moveRequest.CompletedRequestAgeLimit, this.CompletedRequestAgeLimit));
				moveRequest.CompletedRequestAgeLimit = this.CompletedRequestAgeLimit;
			}
			if (base.IsFieldSet("PreventCompletion"))
			{
				stringBuilder.AppendLine(string.Format("PreventCompletion: {0} -> {1}", moveRequest.PreventCompletion, this.PreventCompletion));
				moveRequest.PreventCompletion = this.PreventCompletion;
			}
			if (base.IsFieldSet("StartAfter") && !RequestTaskHelper.CompareUtcTimeWithLocalTime(moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter), this.StartAfter))
			{
				RequestTaskHelper.SetStartAfter(this.StartAfter, moveRequest, stringBuilder);
			}
			if (base.IsFieldSet("CompleteAfter") && !RequestTaskHelper.CompareUtcTimeWithLocalTime(moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter), this.CompleteAfter))
			{
				RequestTaskHelper.SetCompleteAfter(this.CompleteAfter, moveRequest, stringBuilder);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				moveRequest.IncrementalSyncInterval = this.IncrementalSyncInterval;
			}
			RequestTaskHelper.ValidateStartAfterCompleteAfterWithSuspendWhenReadyToComplete(moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter), moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter), moveRequest.SuspendWhenReadyToComplete, new Task.TaskErrorLoggingDelegate(base.WriteError));
			RequestTaskHelper.ValidateStartAfterComesBeforeCompleteAfter(moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter), moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter), new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (base.IsFieldSet("SkipMoving"))
			{
				RequestJobInternalFlags requestJobInternalFlags = moveRequest.RequestJobInternalFlags;
				RequestTaskHelper.SetSkipMoving(this.SkipMoving, moveRequest, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
				stringBuilder.AppendLine(string.Format("InternalFlags: {0} -> {1}", requestJobInternalFlags, moveRequest.RequestJobInternalFlags));
			}
			if (base.IsFieldSet("InternalFlags"))
			{
				RequestJobInternalFlags requestJobInternalFlags2 = moveRequest.RequestJobInternalFlags;
				RequestTaskHelper.SetInternalFlags(this.InternalFlags, moveRequest, new Task.TaskErrorLoggingDelegate(base.WriteError));
				stringBuilder.AppendLine(string.Format("InternalFlags: {0} -> {1}", requestJobInternalFlags2, moveRequest.RequestJobInternalFlags));
			}
			ReportData reportData = new ReportData(moveRequest.ExchangeGuid, moveRequest.ReportVersion);
			ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
			reportData.Append(MrsStrings.ReportMoveRequestSet(base.ExecutingUserIdentity), connectivityRec);
			reportData.AppendDebug(stringBuilder.ToString());
			if (this.AcceptLargeDataLoss)
			{
				reportData.Append(MrsStrings.ReportLargeAmountOfDataLossAccepted2(moveRequest.BadItemLimit.ToString(), moveRequest.LargeItemLimit.ToString(), base.ExecutingUserIdentity));
			}
			if (base.IsFieldSet("TargetDatabase") || base.IsFieldSet("ArchiveTargetDatabase"))
			{
				moveRequest.RehomeRequest = true;
				if (base.IsFieldSet("TargetDatabase"))
				{
					moveRequest.TargetDatabase = this.specifiedTargetMDB.Id;
				}
				if (base.IsFieldSet("ArchiveTargetDatabase"))
				{
					moveRequest.TargetArchiveDatabase = this.specifiedArchiveTargetMDB.Id;
				}
			}
			reportData.Flush(base.MRProvider.SystemMailbox);
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x001F81E8 File Offset: 0x001F63E8
		protected override void PostSaveAction()
		{
			using (MailboxReplicationServiceClient mailboxReplicationServiceClient = this.DataObject.CreateMRSClient(base.ConfigSession, this.mdbGuid, base.UnreachableMrsServers))
			{
				mailboxReplicationServiceClient.RefreshMoveRequest(base.LocalADUser.ExchangeGuid, this.mdbGuid, MoveRequestNotification.Updated);
			}
		}

		// Token: 0x06007AF4 RID: 31476 RVA: 0x001F8248 File Offset: 0x001F6448
		private MailboxDatabase LocateAndVerifyMdb(DatabaseIdParameter databaseId, out ServerVersion targetServerVersion)
		{
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseId, base.ConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(databaseId.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(databaseId.ToString())));
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mailboxDatabase.Id.ObjectGuid, null, null, FindServerFlags.None);
			targetServerVersion = new ServerVersion(databaseInformation.ServerVersion);
			this.EnsureSupportedServerVersion(databaseInformation.ServerVersion);
			return mailboxDatabase;
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x001F82BC File Offset: 0x001F64BC
		private void EnsureSupportedServerVersion(int serverVersion)
		{
			if (serverVersion < Server.E15MinVersion)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMovingToOldExchangeDatabaseUnsupported), ErrorCategory.InvalidArgument, this.TargetDatabase);
			}
		}

		// Token: 0x04003CC3 RID: 15555
		private Guid mdbGuid;

		// Token: 0x04003CC4 RID: 15556
		private MailboxDatabase specifiedTargetMDB;

		// Token: 0x04003CC5 RID: 15557
		private MailboxDatabase specifiedArchiveTargetMDB;

		// Token: 0x04003CC6 RID: 15558
		private ServerVersion newTargetServerVersion;

		// Token: 0x04003CC7 RID: 15559
		private ServerVersion newArchiveTargetServerVersion;
	}
}
