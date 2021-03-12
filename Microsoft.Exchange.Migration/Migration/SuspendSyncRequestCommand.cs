using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017D RID: 381
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SuspendSyncRequestCommand : MrsAccessorCommand
	{
		// Token: 0x060011D0 RID: 4560 RVA: 0x0004B0D2 File Offset: 0x000492D2
		public SuspendSyncRequestCommand() : base("Suspend-SyncRequest", SuspendSyncRequestCommand.IgnoreExceptionTypes, SuspendSyncRequestCommand.TransientExceptionTypes)
		{
		}

		// Token: 0x0400063B RID: 1595
		public const string CmdletName = "Suspend-SyncRequest";

		// Token: 0x0400063C RID: 1596
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};

		// Token: 0x0400063D RID: 1597
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
