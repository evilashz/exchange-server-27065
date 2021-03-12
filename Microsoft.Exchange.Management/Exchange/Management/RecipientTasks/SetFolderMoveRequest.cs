using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C6D RID: 3181
	[Cmdlet("Set", "FolderMoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetFolderMoveRequest : SetRequest<FolderMoveRequestIdParameter>
	{
		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x06007977 RID: 31095 RVA: 0x001EF39B File Offset: 0x001ED59B
		// (set) Token: 0x06007978 RID: 31096 RVA: 0x001EF3BC File Offset: 0x001ED5BC
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

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x06007979 RID: 31097 RVA: 0x001EF3D4 File Offset: 0x001ED5D4
		// (set) Token: 0x0600797A RID: 31098 RVA: 0x001EF3EB File Offset: 0x001ED5EB
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

		// Token: 0x17002598 RID: 9624
		// (get) Token: 0x0600797B RID: 31099 RVA: 0x001EF403 File Offset: 0x001ED603
		// (set) Token: 0x0600797C RID: 31100 RVA: 0x001EF41A File Offset: 0x001ED61A
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

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x0600797D RID: 31101 RVA: 0x001EF432 File Offset: 0x001ED632
		// (set) Token: 0x0600797E RID: 31102 RVA: 0x001EF43A File Offset: 0x001ED63A
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

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x0600797F RID: 31103 RVA: 0x001EF443 File Offset: 0x001ED643
		// (set) Token: 0x06007980 RID: 31104 RVA: 0x001EF44B File Offset: 0x001ED64B
		private new SwitchParameter RehomeRequest
		{
			get
			{
				return base.RehomeRequest;
			}
			set
			{
				base.RehomeRequest = value;
			}
		}

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x06007981 RID: 31105 RVA: 0x001EF454 File Offset: 0x001ED654
		// (set) Token: 0x06007982 RID: 31106 RVA: 0x001EF45C File Offset: 0x001ED65C
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

		// Token: 0x06007983 RID: 31107 RVA: 0x001EF468 File Offset: 0x001ED668
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			if (base.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				changedValuesTracker.AppendLine(string.Format("SWRTC: {0} -> {1}", requestJob.SuspendWhenReadyToComplete, this.SuspendWhenReadyToComplete));
				requestJob.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
			}
			if (base.IsFieldSet("CompleteAfter") && !RequestTaskHelper.CompareUtcTimeWithLocalTime(requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.CompleteAfter), this.CompleteAfter))
			{
				RequestTaskHelper.SetCompleteAfter(this.CompleteAfter, requestJob, changedValuesTracker);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				requestJob.IncrementalSyncInterval = this.IncrementalSyncInterval;
			}
		}

		// Token: 0x04003C5D RID: 15453
		public const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";
	}
}
