using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetPublicFolderMigrationRequestCommand : NewPublicFolderMigrationRequestCommandBase
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x0004A283 File Offset: 0x00048483
		public SetPublicFolderMigrationRequestCommand(ISubscriptionId id) : base("Set-PublicFolderMailboxMigrationRequest", SetPublicFolderMigrationRequestCommand.ExceptionsToIgnore)
		{
			MigrationUtil.ThrowOnNullArgument(id, "id");
			base.Identity = id;
		}

		// Token: 0x0400061C RID: 1564
		public const string CmdletName = "Set-PublicFolderMailboxMigrationRequest";

		// Token: 0x0400061D RID: 1565
		private static readonly Type[] ExceptionsToIgnore = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};
	}
}
