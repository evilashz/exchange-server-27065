using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SuspendPublicFolderMigrationRequestCommand : MrsAccessorCommand
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x0004A2CF File Offset: 0x000484CF
		internal SuspendPublicFolderMigrationRequestCommand(MRSSubscriptionId identity) : base("Suspend-PublicFolderMailboxMigrationRequest", SuspendPublicFolderMigrationRequestCommand.IgnoreExceptionTypes, SuspendPublicFolderMigrationRequestCommand.TransientExceptionTypes)
		{
			base.Identity = identity;
		}

		// Token: 0x0400061E RID: 1566
		public const string CmdletName = "Suspend-PublicFolderMailboxMigrationRequest";

		// Token: 0x0400061F RID: 1567
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};

		// Token: 0x04000620 RID: 1568
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
