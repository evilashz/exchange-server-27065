using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DFB RID: 3579
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICustomSerializable
	{
		// Token: 0x06007AFC RID: 31484
		void DeserializeData(BinaryReader reader, ComponentDataPool componentDataPool);

		// Token: 0x06007AFD RID: 31485
		void SerializeData(BinaryWriter writer, ComponentDataPool componentDataPool);
	}
}
