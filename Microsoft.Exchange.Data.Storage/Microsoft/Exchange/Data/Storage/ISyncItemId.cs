using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E59 RID: 3673
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncItemId : ICustomSerializableBuilder, ICustomSerializable
	{
		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x06007F3E RID: 32574
		object NativeId { get; }
	}
}
