using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationPersistable : IMigrationSerializable
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003B0 RID: 944
		StoreObjectId StoreObjectId { get; }
	}
}
