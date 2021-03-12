using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CAB RID: 3243
	[Cmdlet("Set", "PublicFolderMoveRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPublicFolderMoveRequest : SetRequest<PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x06007C6A RID: 31850 RVA: 0x001FDA3B File Offset: 0x001FBC3B
		// (set) Token: 0x06007C6B RID: 31851 RVA: 0x001FDA5C File Offset: 0x001FBC5C
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

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x06007C6C RID: 31852 RVA: 0x001FDA74 File Offset: 0x001FBC74
		// (set) Token: 0x06007C6D RID: 31853 RVA: 0x001FDA7C File Offset: 0x001FBC7C
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

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x06007C6E RID: 31854 RVA: 0x001FDA85 File Offset: 0x001FBC85
		// (set) Token: 0x06007C6F RID: 31855 RVA: 0x001FDA8D File Offset: 0x001FBC8D
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

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x06007C70 RID: 31856 RVA: 0x001FDA96 File Offset: 0x001FBC96
		// (set) Token: 0x06007C71 RID: 31857 RVA: 0x001FDA9E File Offset: 0x001FBC9E
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

		// Token: 0x06007C72 RID: 31858 RVA: 0x001FDAA8 File Offset: 0x001FBCA8
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			if (base.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				changedValuesTracker.AppendLine(string.Format("SWRTC: {0} -> {1}", requestJob.SuspendWhenReadyToComplete, this.SuspendWhenReadyToComplete));
				requestJob.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
			}
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x001FDAF5 File Offset: 0x001FBCF5
		protected override void CheckIndexEntry()
		{
		}

		// Token: 0x04003D75 RID: 15733
		public const string ParameterSuspendWhenReadyToComplete = "SuspendWhenReadyToComplete";
	}
}
