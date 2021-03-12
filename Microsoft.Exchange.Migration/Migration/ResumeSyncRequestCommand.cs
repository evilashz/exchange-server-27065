using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017B RID: 379
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ResumeSyncRequestCommand : MrsAccessorCommand
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x0004B085 File Offset: 0x00049285
		public ResumeSyncRequestCommand() : base("Resume-SyncRequest", ResumeSyncRequestCommand.IgnoreExceptionTypes, null)
		{
		}

		// Token: 0x04000638 RID: 1592
		public const string CmdletName = "Resume-SyncRequest";

		// Token: 0x04000639 RID: 1593
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};
	}
}
