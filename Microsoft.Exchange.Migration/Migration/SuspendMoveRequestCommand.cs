using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000169 RID: 361
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SuspendMoveRequestCommand : MrsAccessorCommand
	{
		// Token: 0x06001169 RID: 4457 RVA: 0x00049765 File Offset: 0x00047965
		public SuspendMoveRequestCommand() : base("Suspend-MoveRequest", SuspendMoveRequestCommand.IgnoreExceptionTypes, SuspendMoveRequestCommand.TransientExceptionTypes)
		{
		}

		// Token: 0x04000609 RID: 1545
		public const string CmdletName = "Suspend-MoveRequest";

		// Token: 0x0400060A RID: 1546
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};

		// Token: 0x0400060B RID: 1547
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
