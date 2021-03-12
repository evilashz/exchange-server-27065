using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014E RID: 334
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SuspendMailboxImportRequestCommand : MrsAccessorCommand
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0004599A File Offset: 0x00043B9A
		public SuspendMailboxImportRequestCommand() : base("Suspend-MailboxImportRequest", SuspendMailboxImportRequestCommand.IgnoreExceptionTypes, SuspendMailboxImportRequestCommand.TransientExceptionTypes)
		{
		}

		// Token: 0x040005DC RID: 1500
		public const string CmdletName = "Suspend-MailboxImportRequest";

		// Token: 0x040005DD RID: 1501
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};

		// Token: 0x040005DE RID: 1502
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
