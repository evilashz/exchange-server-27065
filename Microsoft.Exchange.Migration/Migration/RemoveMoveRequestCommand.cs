using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000166 RID: 358
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoveMoveRequestCommand : MrsAccessorCommand
	{
		// Token: 0x06001163 RID: 4451 RVA: 0x000496AF File Offset: 0x000478AF
		internal RemoveMoveRequestCommand() : base("Remove-MoveRequest", RemoveMoveRequestCommand.IgnoreExceptionTypes, RemoveMoveRequestCommand.TransientExceptionTypes)
		{
		}

		// Token: 0x04000605 RID: 1541
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(ManagementObjectNotFoundException)
		};

		// Token: 0x04000606 RID: 1542
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
