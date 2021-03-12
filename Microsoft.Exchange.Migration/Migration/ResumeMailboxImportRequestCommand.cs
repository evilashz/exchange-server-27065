using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014C RID: 332
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ResumeMailboxImportRequestCommand : MrsAccessorCommand
	{
		// Token: 0x0600109C RID: 4252 RVA: 0x0004594D File Offset: 0x00043B4D
		public ResumeMailboxImportRequestCommand() : base("Resume-MailboxImportRequest", ResumeMailboxImportRequestCommand.IgnoreExceptionTypes, null)
		{
		}

		// Token: 0x040005DA RID: 1498
		private static readonly Type[] IgnoreExceptionTypes = new Type[]
		{
			typeof(CannotSetCompletedPermanentException)
		};
	}
}
