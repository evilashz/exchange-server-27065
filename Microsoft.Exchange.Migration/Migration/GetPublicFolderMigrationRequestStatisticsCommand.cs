using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016C RID: 364
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPublicFolderMigrationRequestStatisticsCommand : MrsAccessorCommand
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x0004A0F0 File Offset: 0x000482F0
		public GetPublicFolderMigrationRequestStatisticsCommand(object identity) : base("Get-PublicFolderMailboxMigrationRequestStatistics", null, null)
		{
			base.Identity = identity;
		}

		// Token: 0x0400060D RID: 1549
		public const string CmdletName = "Get-PublicFolderMailboxMigrationRequestStatistics";
	}
}
