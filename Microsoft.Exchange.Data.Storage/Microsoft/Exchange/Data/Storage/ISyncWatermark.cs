using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E5B RID: 3675
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncWatermark : ICustomSerializableBuilder, ICustomSerializable, IComparable, ICloneable
	{
		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x06007F42 RID: 32578
		bool IsNew { get; }
	}
}
