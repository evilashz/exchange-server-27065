using System;
using System.IO;
using System.Threading;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200004C RID: 76
	internal abstract class DataStorage : RefCountable
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000FF10 File Offset: 0x0000E110
		internal bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000FF18 File Offset: 0x0000E118
		public static long CopyStreamToStream(Stream srcStream, Stream destStream, long lengthToCopy, ref byte[] scratchBuffer)
		{
			if (scratchBuffer == null || scratchBuffer.Length < 16384)
			{
				scratchBuffer = new byte[16384];
			}
			long num = 0L;
			while (lengthToCopy != 0L)
			{
				int count = (int)Math.Min(lengthToCopy, (long)scratchBuffer.Length);
				int num2 = srcStream.Read(scratchBuffer, 0, count);
				if (num2 == 0)
				{
					break;
				}
				if (destStream != null)
				{
					destStream.Write(scratchBuffer, 0, num2);
				}
				num += (long)num2;
				if (lengthToCopy != 9223372036854775807L)
				{
					lengthToCopy -= (long)num2;
				}
			}
			return num;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000FF8A File Offset: 0x0000E18A
		public static Stream NewEmptyReadStream()
		{
			return new StreamOnReadableDataStorage(null, 0L, 0L);
		}

		// Token: 0x060002C8 RID: 712
		public abstract Stream OpenReadStream(long start, long end);

		// Token: 0x060002C9 RID: 713 RVA: 0x0000FF98 File Offset: 0x0000E198
		public virtual long CopyContentToStream(long start, long end, Stream destStream, ref byte[] scratchBuffer)
		{
			base.ThrowIfDisposed();
			if (destStream == null && end != 9223372036854775807L)
			{
				return end - start;
			}
			long result;
			using (Stream stream = this.OpenReadStream(start, end))
			{
				result = DataStorage.CopyStreamToStream(stream, destStream, long.MaxValue, ref scratchBuffer);
			}
			return result;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		internal virtual void SetReadOnly(bool makeReadOnly)
		{
			base.ThrowIfDisposed();
			if (makeReadOnly == this.isReadOnly)
			{
				return;
			}
			if (makeReadOnly)
			{
				this.readOnlySemaphore = new SemaphoreSlim(1);
			}
			else
			{
				this.readOnlySemaphore = null;
			}
			this.isReadOnly = makeReadOnly;
		}

		// Token: 0x04000259 RID: 601
		protected bool isReadOnly;

		// Token: 0x0400025A RID: 602
		protected SemaphoreSlim readOnlySemaphore;
	}
}
