using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017A RID: 378
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoveSyncRequestCommand : MrsAccessorCommand
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x0004B02D File Offset: 0x0004922D
		internal RemoveSyncRequestCommand() : base("Remove-SyncRequest", RemoveSyncRequestCommand.IgnoreExceptionTypes, RemoveSyncRequestCommand.TransientExceptionTypes)
		{
		}

		// Token: 0x04000635 RID: 1589
		public const string CmdletName = "Remove-SyncRequest";

		// Token: 0x04000636 RID: 1590
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(ManagementObjectNotFoundException)
		};

		// Token: 0x04000637 RID: 1591
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
