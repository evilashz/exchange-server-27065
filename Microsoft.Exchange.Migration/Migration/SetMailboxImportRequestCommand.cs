using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014D RID: 333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetMailboxImportRequestCommand : NewMailboxImportRequestCommandBase
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x00045987 File Offset: 0x00043B87
		public SetMailboxImportRequestCommand() : base("Set-MailboxImportRequest", new Type[0])
		{
		}

		// Token: 0x040005DB RID: 1499
		public const string CmdletName = "Set-MailboxImportRequest";
	}
}
