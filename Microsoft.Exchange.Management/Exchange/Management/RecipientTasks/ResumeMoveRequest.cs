using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C7D RID: 3197
	[Cmdlet("Resume", "MoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeMoveRequest : SetMoveRequestBase
	{
		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x06007ABD RID: 31421 RVA: 0x001F6DCC File Offset: 0x001F4FCC
		// (set) Token: 0x06007ABE RID: 31422 RVA: 0x001F6DF2 File Offset: 0x001F4FF2
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x001F6E0A File Offset: 0x001F500A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageResumeRequest(base.LocalADUser.ToString());
			}
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x001F6E1C File Offset: 0x001F501C
		protected override void ValidateMoveRequest(TransactionalRequestJob moveRequest)
		{
			base.ValidateMoveRequestIsActive(moveRequest);
			base.ValidateMoveRequestProtectionStatus(moveRequest);
			base.ValidateMoveRequestIsSettable(moveRequest);
			if (!moveRequest.Suspend)
			{
				base.WriteVerbose(Strings.MoveNotSuspended(base.LocalADUser.ToString()));
			}
			if (RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Cleanup))
			{
				base.WriteError(new SuspendWhenReadyToCompleteCannotBeUsedDuringCompletionException(moveRequest.Identity.ToString()), ErrorCategory.InvalidArgument, moveRequest.Identity);
			}
			if (moveRequest.JobType >= MRSJobType.RequestJobE15_AutoResume && this.SuspendWhenReadyToComplete)
			{
				base.WriteError(new SuspendWhenReadyToCompleteCannotBeUsedOnAutoResumeJobsException(moveRequest.Identity.ToString()), ErrorCategory.InvalidArgument, moveRequest.Identity);
			}
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x001F6EBC File Offset: 0x001F50BC
		protected override void ModifyMoveRequest(TransactionalRequestJob moveRequest)
		{
			this.mdbGuid = moveRequest.WorkItemQueueMdb.ObjectGuid;
			if (base.LocalADUser != null)
			{
				moveRequest.DomainControllerToUpdate = base.LocalADUser.OriginatingServer;
			}
			moveRequest.PoisonCount = 0;
			if (moveRequest.Suspend)
			{
				moveRequest.Suspend = false;
				moveRequest.Message = LocalizedString.Empty;
				DateTime? timestamp = moveRequest.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter);
				moveRequest.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, timestamp);
				moveRequest.TotalRetryCount = 0;
				LocalizedString msg;
				if (this.SuspendWhenReadyToComplete)
				{
					moveRequest.SuspendWhenReadyToComplete = true;
					msg = MrsStrings.ReportRequestResumedWithSuspendWhenReadyToComplete(base.ExecutingUserIdentity);
				}
				else
				{
					msg = MrsStrings.ReportMoveRequestResumed(base.ExecutingUserIdentity);
				}
				ReportData reportData = new ReportData(moveRequest.ExchangeGuid, moveRequest.ReportVersion);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				reportData.Append(msg, connectivityRec);
				reportData.Flush(base.MRProvider.SystemMailbox);
			}
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x001F6FA0 File Offset: 0x001F51A0
		protected override void PostSaveAction()
		{
			using (MailboxReplicationServiceClient mailboxReplicationServiceClient = this.DataObject.CreateMRSClient(base.ConfigSession, this.mdbGuid, base.UnreachableMrsServers))
			{
				mailboxReplicationServiceClient.RefreshMoveRequest(base.LocalADUser.ExchangeGuid, this.mdbGuid, MoveRequestNotification.SuspendResume);
			}
			base.WriteVerbose(Strings.ResumeSuccessInformationalMessage(base.LocalADUser.ToString()));
		}

		// Token: 0x04003CC2 RID: 15554
		private Guid mdbGuid;
	}
}
