using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C6A RID: 3178
	public abstract class ResumeRequest<TIdentity> : SetRequestBase<TIdentity> where TIdentity : MRSRequestIdParameter
	{
		// Token: 0x0600794C RID: 31052 RVA: 0x001EE8B1 File Offset: 0x001ECAB1
		public ResumeRequest()
		{
		}

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x0600794D RID: 31053 RVA: 0x001EE8B9 File Offset: 0x001ECAB9
		// (set) Token: 0x0600794E RID: 31054 RVA: 0x001EE8C1 File Offset: 0x001ECAC1
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNull]
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

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x0600794F RID: 31055 RVA: 0x001EE8CA File Offset: 0x001ECACA
		// (set) Token: 0x06007950 RID: 31056 RVA: 0x001EE8F0 File Offset: 0x001ECAF0
		public virtual SwitchParameter SuspendWhenReadyToComplete
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

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x06007951 RID: 31057 RVA: 0x001EE908 File Offset: 0x001ECB08
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageResumeRequest(base.RequestName);
			}
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x001EE918 File Offset: 0x001ECB18
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			base.ValidateRequest(requestJob);
			base.ValidateRequestIsActive(requestJob);
			base.ValidateRequestProtectionStatus(requestJob);
			base.ValidateRequestIsRunnable(requestJob);
			base.ValidateRequestIsNotCancelled(requestJob);
			if (!requestJob.Suspend)
			{
				base.WriteVerbose(Strings.RequestNotSuspended(requestJob.Name));
			}
			if (RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.Completion) && !RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.IncrementalSync))
			{
				base.WriteError(new SuspendWhenReadyToCompleteCannotBeUsedDuringCompletionException(requestJob.Name), ErrorCategory.InvalidArgument, requestJob.Identity);
			}
			using (MailboxReplicationServiceClient mailboxReplicationServiceClient = requestJob.CreateMRSClient(base.ConfigSession, requestJob.WorkItemQueueMdb.ObjectGuid, base.UnreachableMrsServers))
			{
				LocalizedString message = requestJob.Message;
				requestJob.Message = LocalizedString.Empty;
				try
				{
					List<ReportEntry> entries = null;
					using (mailboxReplicationServiceClient.ValidateAndPopulateRequestJob(requestJob, out entries))
					{
						RequestTaskHelper.WriteReportEntries(requestJob.Name, entries, requestJob.Identity, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
				finally
				{
					requestJob.Message = message;
				}
			}
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x001EEA54 File Offset: 0x001ECC54
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
			requestJob.PoisonCount = 0;
			if (requestJob.Suspend)
			{
				requestJob.Suspend = false;
				requestJob.Message = LocalizedString.Empty;
				requestJob.TotalRetryCount = 0;
				LocalizedString msg;
				if (this.SuspendWhenReadyToComplete)
				{
					requestJob.SuspendWhenReadyToComplete = true;
					msg = MrsStrings.ReportRequestResumedWithSuspendWhenReadyToComplete(base.ExecutingUserIdentity);
				}
				else
				{
					msg = MrsStrings.ReportRequestResumed(base.ExecutingUserIdentity);
				}
				ReportData reportData = new ReportData(requestJob.RequestGuid, requestJob.ReportVersion);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				reportData.Append(msg, connectivityRec);
				reportData.Flush(base.RJProvider.SystemMailbox);
			}
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x001EEB33 File Offset: 0x001ECD33
		protected override void PostSaveAction()
		{
			RequestTaskHelper.TickleMRS(this.DataObject, MoveRequestNotification.SuspendResume, this.mdbGuid, base.ConfigSession, base.UnreachableMrsServers);
			base.WriteVerbose(Strings.ResumeSuccessInformationalMessage(this.DataObject.Name));
		}

		// Token: 0x04003C59 RID: 15449
		public const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";

		// Token: 0x04003C5A RID: 15450
		private Guid mdbGuid;
	}
}
