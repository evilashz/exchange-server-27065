using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D81 RID: 3457
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IIdCompressor
	{
		// Token: 0x06007721 RID: 30497
		byte[] Compress(byte[] streamIn, byte compressorId, out int outBytesRequired);

		// Token: 0x06007722 RID: 30498
		MemoryStream Decompress(byte[] input, int maxLength);
	}
}
