using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetSyncRequestCommand : NewSyncRequestCommandBase
	{
		// Token: 0x060011CF RID: 4559 RVA: 0x0004B0BF File Offset: 0x000492BF
		public SetSyncRequestCommand() : base("Set-SyncRequest", new Type[0])
		{
		}

		// Token: 0x0400063A RID: 1594
		public const string CmdletName = "Set-SyncRequest";
	}
}
