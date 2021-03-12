using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C9D RID: 3229
	[Cmdlet("Set", "MailboxRelocationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxRelocationRequest : SetRequest<MailboxRelocationRequestIdParameter>
	{
		// Token: 0x1700266F RID: 9839
		// (get) Token: 0x06007C03 RID: 31747 RVA: 0x001FC294 File Offset: 0x001FA494
		// (set) Token: 0x06007C04 RID: 31748 RVA: 0x001FC2B0 File Offset: 0x001FA4B0
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x17002670 RID: 9840
		// (get) Token: 0x06007C05 RID: 31749 RVA: 0x001FC2C3 File Offset: 0x001FA4C3
		// (set) Token: 0x06007C06 RID: 31750 RVA: 0x001FC2CB File Offset: 0x001FA4CB
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

		// Token: 0x17002671 RID: 9841
		// (get) Token: 0x06007C07 RID: 31751 RVA: 0x001FC2D4 File Offset: 0x001FA4D4
		// (set) Token: 0x06007C08 RID: 31752 RVA: 0x001FC2DC File Offset: 0x001FA4DC
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

		// Token: 0x06007C09 RID: 31753 RVA: 0x001FC2E8 File Offset: 0x001FA4E8
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			base.ModifyRequestInternal(requestJob, changedValuesTracker);
			if (base.IsFieldSet("SkipMoving"))
			{
				RequestJobInternalFlags requestJobInternalFlags = requestJob.RequestJobInternalFlags;
				RequestTaskHelper.SetSkipMoving(this.SkipMoving, requestJob, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
				changedValuesTracker.AppendLine(string.Format("InternalFlags: {0} -> {1}", requestJobInternalFlags, requestJob.RequestJobInternalFlags));
			}
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x001FC34C File Offset: 0x001FA54C
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			base.ValidateRequest(requestJob);
			if (base.IsFieldSet("LargeItemLimit") && requestJob.AllowLargeItems)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorIncompatibleParameters("AllowLargeItems", "LargeItemLimit")), ErrorCategory.InvalidArgument, this.Identity);
			}
		}
	}
}
