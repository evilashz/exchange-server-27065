using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D83 RID: 3459
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PassthruCompressor : IIdCompressor
	{
		// Token: 0x06007734 RID: 30516 RVA: 0x0020E370 File Offset: 0x0020C570
		public byte[] Compress(byte[] streamIn, byte compressorId, out int outBytesRequired)
		{
			outBytesRequired = 0;
			return null;
		}

		// Token: 0x06007735 RID: 30517 RVA: 0x0020E378 File Offset: 0x0020C578
		public MemoryStream Decompress(byte[] input, int maxLength)
		{
			if (input.Length > maxLength)
			{
				throw new InvalidIdMalformedException();
			}
			MemoryStream memoryStream = new MemoryStream(input.Length - 1);
			memoryStream.Write(input, 1, input.Length - 1);
			memoryStream.Position = 0L;
			return memoryStream;
		}
	}
}
