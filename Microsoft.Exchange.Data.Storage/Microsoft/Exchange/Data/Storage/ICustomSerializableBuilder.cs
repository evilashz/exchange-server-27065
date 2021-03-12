using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E0E RID: 3598
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICustomSerializableBuilder : ICustomSerializable
	{
		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06007C20 RID: 31776
		// (set) Token: 0x06007C21 RID: 31777
		ushort TypeId { get; set; }

		// Token: 0x06007C22 RID: 31778
		ICustomSerializable BuildObject();
	}
}
