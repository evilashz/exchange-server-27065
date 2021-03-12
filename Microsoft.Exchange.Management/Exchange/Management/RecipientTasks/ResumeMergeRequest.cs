using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C88 RID: 3208
	[Cmdlet("Resume", "MergeRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeMergeRequest : ResumeRequest<MergeRequestIdParameter>
	{
		// Token: 0x17002640 RID: 9792
		// (get) Token: 0x06007B70 RID: 31600 RVA: 0x001FA559 File Offset: 0x001F8759
		// (set) Token: 0x06007B71 RID: 31601 RVA: 0x001FA561 File Offset: 0x001F8761
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public override SwitchParameter SuspendWhenReadyToComplete
		{
			get
			{
				return base.SuspendWhenReadyToComplete;
			}
			set
			{
				base.SuspendWhenReadyToComplete = value;
			}
		}

		// Token: 0x06007B72 RID: 31602 RVA: 0x001FA56C File Offset: 0x001F876C
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			if (!requestJob.SuspendWhenReadyToComplete && this.SuspendWhenReadyToComplete)
			{
				base.WriteError(new SuspendWhenReadyToCompleteNotSupportedException(requestJob.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (requestJob.SuspendWhenReadyToComplete && requestJob.StatusDetail == RequestState.AutoSuspended && !this.SuspendWhenReadyToComplete)
			{
				base.WriteError(new IncrementalMergesRequireSuspendWhenReadyToCompleteException(requestJob.Name), ErrorCategory.InvalidArgument, this.Identity);
			}
			base.ValidateRequest(requestJob);
		}

		// Token: 0x06007B73 RID: 31603 RVA: 0x001FA5E4 File Offset: 0x001F87E4
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			if (requestJob.Suspend)
			{
				DateTime? timestamp = requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter);
				requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, timestamp);
			}
			base.ModifyRequest(requestJob);
		}
	}
}
