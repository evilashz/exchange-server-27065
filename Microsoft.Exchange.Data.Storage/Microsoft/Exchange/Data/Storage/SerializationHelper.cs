using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E41 RID: 3649
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SerializationHelper
	{
		// Token: 0x06007E99 RID: 32409 RVA: 0x0022C770 File Offset: 0x0022A970
		public static void Compress(MemoryStream readStream, MemoryStream writeStream)
		{
			if (readStream == null || readStream.Length == 0L)
			{
				return;
			}
			readStream.Seek(0L, SeekOrigin.Begin);
			using (GZipStream gzipStream = new GZipStream(writeStream, CompressionMode.Compress, true))
			{
				readStream.CopyTo(gzipStream);
			}
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x0022C7C4 File Offset: 0x0022A9C4
		public static void Decompress(MemoryStream readStream, MemoryStream writeStream, byte[] transferBuffer)
		{
			if (readStream == null || readStream.Length == 0L)
			{
				return;
			}
			using (GZipStream gzipStream = new GZipStream(readStream, CompressionMode.Decompress, true))
			{
				try
				{
					Util.StreamHandler.CopyStreamData(gzipStream, writeStream, null, 0, transferBuffer);
				}
				catch (InvalidDataException innerException)
				{
					throw new CustomSerializationInvalidDataException(innerException);
				}
			}
		}
	}
}
