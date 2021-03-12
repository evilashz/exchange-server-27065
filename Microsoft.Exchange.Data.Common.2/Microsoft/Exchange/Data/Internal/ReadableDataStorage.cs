using System;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000136 RID: 310
	internal abstract class ReadableDataStorage : DataStorage
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x0006B8A9 File Offset: 0x00069AA9
		public ReadableDataStorage()
		{
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C10 RID: 3088
		public abstract long Length { get; }

		// Token: 0x06000C11 RID: 3089
		public abstract int Read(long position, byte[] buffer, int offset, int count);

		// Token: 0x06000C12 RID: 3090 RVA: 0x0006B8B1 File Offset: 0x00069AB1
		public override Stream OpenReadStream(long start, long end)
		{
			base.ThrowIfDisposed();
			return new StreamOnReadableDataStorage(this, start, end);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0006B8C4 File Offset: 0x00069AC4
		public override long CopyContentToStream(long start, long end, Stream destStream, ref byte[] scratchBuffer)
		{
			base.ThrowIfDisposed();
			if (scratchBuffer == null || scratchBuffer.Length < 16384)
			{
				scratchBuffer = new byte[16384];
			}
			long num = 0L;
			long num2 = (end == long.MaxValue) ? long.MaxValue : (end - start);
			while (num2 != 0L)
			{
				int count = (int)Math.Min(num2, (long)scratchBuffer.Length);
				int num3 = this.Read(start, scratchBuffer, 0, count);
				if (num3 == 0)
				{
					break;
				}
				start += (long)num3;
				destStream.Write(scratchBuffer, 0, num3);
				num += (long)num3;
				if (num2 != 9223372036854775807L)
				{
					num2 -= (long)num3;
				}
			}
			return num;
		}
	}
}
