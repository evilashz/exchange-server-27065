using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C7F RID: 3199
	[Cmdlet("Suspend", "MoveRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SuspendMoveRequest : SetMoveRequestBase
	{
		// Token: 0x06007AF6 RID: 31478 RVA: 0x001F82EF File Offset: 0x001F64EF
		public SuspendMoveRequest()
		{
			this.moveIsOffline = false;
		}

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x06007AF7 RID: 31479 RVA: 0x001F82FE File Offset: 0x001F64FE
		// (set) Token: 0x06007AF8 RID: 31480 RVA: 0x001F8315 File Offset: 0x001F6515
		[Parameter(Mandatory = false)]
		public string SuspendComment
		{
			get
			{
				return (string)base.Fields["SuspendComment"];
			}
			set
			{
				base.Fields["SuspendComment"] = value;
			}
		}

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x06007AF9 RID: 31481 RVA: 0x001F8328 File Offset: 0x001F6528
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.moveIsOffline)
				{
					return Strings.ConfirmationMessageSuspendOfflineMoveRequest(base.LocalADUser.ToString());
				}
				return Strings.ConfirmationMessageSuspendOnlineMoveRequest(base.LocalADUser.ToString());
			}
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x001F8354 File Offset: 0x001F6554
		protected override void ValidateMoveRequest(TransactionalRequestJob moveRequest)
		{
			this.moveIsOffline = moveRequest.IsOffline;
			base.ValidateMoveRequestIsActive(moveRequest);
			base.ValidateMoveRequestProtectionStatus(moveRequest);
			base.ValidateMoveRequestIsSettable(moveRequest);
			if (moveRequest.RequestJobState == JobProcessingState.InProgress && RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Cleanup) && moveRequest.IdleTime < TimeSpan.FromMinutes(60.0))
			{
				base.WriteError(new CannotModifyCompletingRequestPermanentException(base.LocalADUser.ToString()), ErrorCategory.InvalidArgument, base.Identity);
			}
			if (moveRequest.Suspend)
			{
				base.WriteVerbose(Strings.MoveAlreadySuspended(base.LocalADUser.ToString()));
			}
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x001F83F0 File Offset: 0x001F65F0
		protected override void ModifyMoveRequest(TransactionalRequestJob moveRequest)
		{
			this.mdbGuid = moveRequest.WorkItemQueueMdb.ObjectGuid;
			if (base.LocalADUser != null)
			{
				moveRequest.DomainControllerToUpdate = base.LocalADUser.OriginatingServer;
			}
			moveRequest.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(DateTime.MaxValue));
			if (!moveRequest.Suspend)
			{
				moveRequest.Suspend = true;
				if (!string.IsNullOrEmpty(this.SuspendComment))
				{
					moveRequest.Message = MrsStrings.MoveRequestMessageInformational(new LocalizedString(this.SuspendComment));
				}
				ReportData reportData = new ReportData(moveRequest.ExchangeGuid, moveRequest.ReportVersion);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				reportData.Append(MrsStrings.ReportMoveRequestSuspended(base.ExecutingUserIdentity), connectivityRec);
				reportData.Flush(base.MRProvider.SystemMailbox);
			}
		}

		// Token: 0x06007AFC RID: 31484 RVA: 0x001F84B0 File Offset: 0x001F66B0
		protected override void PostSaveAction()
		{
			using (MailboxReplicationServiceClient mailboxReplicationServiceClient = this.DataObject.CreateMRSClient(base.ConfigSession, this.mdbGuid, base.UnreachableMrsServers))
			{
				mailboxReplicationServiceClient.RefreshMoveRequest(base.LocalADUser.ExchangeGuid, this.mdbGuid, MoveRequestNotification.SuspendResume);
			}
			base.WriteVerbose(Strings.SuspendSuccessInformationalMessage(base.LocalADUser.ToString()));
		}

		// Token: 0x04003CC8 RID: 15560
		public const string ParameterSuspendComment = "SuspendComment";

		// Token: 0x04003CC9 RID: 15561
		private Guid mdbGuid;

		// Token: 0x04003CCA RID: 15562
		private bool moveIsOffline;
	}
}
