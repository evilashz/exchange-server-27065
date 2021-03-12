using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ResumePublicFolderMigrationRequestCommand : MrsAccessorCommand
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x0004A235 File Offset: 0x00048435
		internal ResumePublicFolderMigrationRequestCommand() : base("Resume-PublicFolderMailboxMigrationRequest", ResumePublicFolderMigrationRequestCommand.IgnoreExceptionTypes, null)
		{
		}

		// Token: 0x17000543 RID: 1347
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x0004A248 File Offset: 0x00048448
		public bool SuspendWhenReadyToComplete
		{
			set
			{
				base.AddParameter("SuspendWhenReadyToComplete", value);
			}
		}

		// Token: 0x0400061A RID: 1562
		public const string CmdletName = "Resume-PublicFolderMailboxMigrationRequest";

		// Token: 0x0400061B RID: 1563
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};
	}
}
