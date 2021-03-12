using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A15 RID: 2581
	internal class ILockBytesOverStream : ILockBytes
	{
		// Token: 0x06003854 RID: 14420 RVA: 0x0008FC51 File Offset: 0x0008DE51
		public ILockBytesOverStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("The passed in stream must be seekable", "stream");
			}
			this.stream = stream;
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0008FC88 File Offset: 0x0008DE88
		public void ReadAt(ulong offset, byte[] buffer, int count, out int bytesRead)
		{
			if (buffer.Length < count)
			{
				throw new ArgumentException("Requesting more bytes from the stream than will fit in the supplied buffer", "count");
			}
			int i = count;
			bytesRead = 0;
			this.stream.Seek((long)offset, SeekOrigin.Begin);
			while (i > 0)
			{
				int num = this.stream.Read(buffer, bytesRead, i);
				if (num == 0)
				{
					return;
				}
				i -= num;
				bytesRead += num;
			}
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0008FCE5 File Offset: 0x0008DEE5
		public void WriteAt(ulong offset, byte[] buffer, int count, out int written)
		{
			this.stream.Seek((long)offset, SeekOrigin.Begin);
			this.stream.Write(buffer, 0, count);
			written = count;
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x0008FD07 File Offset: 0x0008DF07
		public void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x0008FD14 File Offset: 0x0008DF14
		public void SetSize(ulong length)
		{
			this.stream.SetLength((long)length);
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x0008FD22 File Offset: 0x0008DF22
		public void LockRegion(ulong libOffset, ulong cb, int dwLockType)
		{
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x0008FD24 File Offset: 0x0008DF24
		public void UnlockRegion(ulong libOffset, ulong cb, int dwLockType)
		{
		}

		// Token: 0x0600385B RID: 14427 RVA: 0x0008FD26 File Offset: 0x0008DF26
		public void Stat(out STATSTG pstatstg, STATFLAG grfStatFlag)
		{
			pstatstg = default(STATSTG);
			pstatstg.type = 2;
			pstatstg.cbSize = this.stream.Length;
			pstatstg.grfLocksSupported = 2;
		}

		// Token: 0x04002F98 RID: 12184
		private Stream stream;
	}
}
