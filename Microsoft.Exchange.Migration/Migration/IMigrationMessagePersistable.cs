using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000C2 RID: 194
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationMessagePersistable : IMigrationPersistable, IMigrationSerializable
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A3C RID: 2620
		StoreObjectId MessageId { get; }
	}
}
