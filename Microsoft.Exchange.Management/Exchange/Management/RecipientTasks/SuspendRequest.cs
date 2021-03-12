using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C6E RID: 3182
	public abstract class SuspendRequest<TIdentity> : SetRequestBase<TIdentity> where TIdentity : MRSRequestIdParameter
	{
		// Token: 0x06007985 RID: 31109 RVA: 0x001EF50A File Offset: 0x001ED70A
		public SuspendRequest()
		{
		}

		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x06007986 RID: 31110 RVA: 0x001EF512 File Offset: 0x001ED712
		// (set) Token: 0x06007987 RID: 31111 RVA: 0x001EF51A File Offset: 0x001ED71A
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override TIdentity Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x1700259D RID: 9629
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x001EF523 File Offset: 0x001ED723
		// (set) Token: 0x06007989 RID: 31113 RVA: 0x001EF53A File Offset: 0x001ED73A
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

		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x0600798A RID: 31114 RVA: 0x001EF54D File Offset: 0x001ED74D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSuspendRequest(base.RequestName);
			}
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x001EF55C File Offset: 0x001ED75C
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			base.ValidateRequest(requestJob);
			base.ValidateRequestIsActive(requestJob);
			base.ValidateRequestProtectionStatus(requestJob);
			base.ValidateRequestIsRunnable(requestJob);
			base.ValidateRequestIsNotCancelled(requestJob);
			if (requestJob.RequestJobState == JobProcessingState.InProgress && RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.Completion) && !RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.IncrementalSync) && requestJob.IdleTime < TimeSpan.FromMinutes(60.0))
			{
				base.WriteError(new CannotModifyCompletingRequestPermanentException(base.RequestName), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (requestJob.Suspend)
			{
				base.WriteVerbose(Strings.RequestAlreadySuspended(base.RequestName));
			}
			if (!string.IsNullOrEmpty(this.SuspendComment) && this.SuspendComment.Length > 4096)
			{
				base.WriteError(new ParameterLengthExceededPermanentException("SuspendComment", 4096), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x001EF644 File Offset: 0x001ED844
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			this.mdbGuid = requestJob.WorkItemQueueMdb.ObjectGuid;
			if (requestJob.TargetUser != null)
			{
				requestJob.DomainControllerToUpdate = requestJob.TargetUser.OriginatingServer;
			}
			else if (requestJob.SourceUser != null)
			{
				requestJob.DomainControllerToUpdate = requestJob.SourceUser.OriginatingServer;
			}
			if (!requestJob.Suspend)
			{
				requestJob.Suspend = true;
				if (!string.IsNullOrEmpty(this.SuspendComment))
				{
					requestJob.Message = MrsStrings.MoveRequestMessageInformational(new LocalizedString(this.SuspendComment));
				}
				ReportData reportData = new ReportData(requestJob.RequestGuid, requestJob.ReportVersion);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				reportData.Append(MrsStrings.ReportRequestSuspended(base.ExecutingUserIdentity), connectivityRec);
				reportData.Flush(base.RJProvider.SystemMailbox);
			}
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x001EF709 File Offset: 0x001ED909
		protected override void PostSaveAction()
		{
			RequestTaskHelper.TickleMRS(this.DataObject, MoveRequestNotification.SuspendResume, this.mdbGuid, base.ConfigSession, base.UnreachableMrsServers);
			base.WriteVerbose(Strings.SuspendSuccessInformationalMessage(base.RequestName));
		}

		// Token: 0x04003C5E RID: 15454
		public const string ParameterSuspendComment = "SuspendComment";

		// Token: 0x04003C5F RID: 15455
		private Guid mdbGuid;
	}
}
