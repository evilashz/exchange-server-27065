using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationDataRow
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37
		MigrationType MigrationType { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38
		MigrationUserRecipientType RecipientType { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000027 RID: 39
		string Identifier { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40
		string LocalMailboxIdentifier { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41
		int CursorPosition { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42
		bool SupportsRemoteIdentifier { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43
		string RemoteIdentifier { get; }
	}
}
