using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISubscriptionId : ISnapshotId, IMigrationSerializable
	{
		// Token: 0x060002F4 RID: 756
		string ToString();

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002F5 RID: 757
		MigrationType MigrationType { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002F6 RID: 758
		IMailboxData MailboxData { get; }
	}
}
