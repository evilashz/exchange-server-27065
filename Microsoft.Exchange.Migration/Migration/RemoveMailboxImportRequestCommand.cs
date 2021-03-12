using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014B RID: 331
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoveMailboxImportRequestCommand : MrsAccessorCommand
	{
		// Token: 0x0600109A RID: 4250 RVA: 0x000458F3 File Offset: 0x00043AF3
		internal RemoveMailboxImportRequestCommand() : base("Remove-MailboxImportRequest", RemoveMailboxImportRequestCommand.IgnoreExceptionTypes, RemoveMailboxImportRequestCommand.TransientExceptionTypes)
		{
		}

		// Token: 0x040005D8 RID: 1496
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(ManagementObjectNotFoundException)
		};

		// Token: 0x040005D9 RID: 1497
		private static readonly Type[] TransientExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletingPermanentException)
		};
	}
}
