using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016F RID: 367
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemovePublicFolderMigrationRequestCommand : MrsAccessorCommand
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x0004A1D3 File Offset: 0x000483D3
		internal RemovePublicFolderMigrationRequestCommand(MRSSubscriptionId identity) : base("Remove-PublicFolderMailboxMigrationRequest", RemovePublicFolderMigrationRequestCommand.IgnoreExceptionTypes, RemovePublicFolderMigrationRequestCommand.TransientExceptionTypes)
		{
			base.Identity = identity;
		}

		// Token: 0x04000617 RID: 1559
		public const string CmdletName = "Remove-PublicFolderMailboxMigrationRequest";

		// Token: 0x04000618 RID: 1560
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(ManagementObjectNotFoundException)
		};

		// Token: 0x04000619 RID: 1561
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
