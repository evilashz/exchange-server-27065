using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB8 RID: 3256
	[Cmdlet("Resume", "PublicFolderMailboxMigrationRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumePublicFolderMailboxMigrationRequest : ResumeRequest<PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x06007CF3 RID: 31987 RVA: 0x001FF494 File Offset: 0x001FD694
		// (set) Token: 0x06007CF4 RID: 31988 RVA: 0x001FF49C File Offset: 0x001FD69C
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

		// Token: 0x06007CF5 RID: 31989 RVA: 0x001FF4A8 File Offset: 0x001FD6A8
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			base.ModifyRequest(requestJob);
			if (base.IsFieldSet("SuspendWhenReadyToComplete"))
			{
				requestJob.SuspendWhenReadyToComplete = this.SuspendWhenReadyToComplete;
				requestJob.PreventCompletion = this.SuspendWhenReadyToComplete;
				requestJob.AllowedToFinishMove = !this.SuspendWhenReadyToComplete;
			}
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x001FF4FF File Offset: 0x001FD6FF
		protected override void CheckIndexEntry()
		{
		}

		// Token: 0x04003DA9 RID: 15785
		private const string TaskNoun = "PublicFolderMailboxMigrationRequest";
	}
}
