using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000A3 RID: 163
	[ClassInterface(ClassInterfaceType.None)]
	internal class LockBytesOnStream : Interop.ILockBytes
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x00017BA4 File Offset: 0x00015DA4
		public LockBytesOnStream(Stream stream)
		{
			Util.ThrowOnNullArgument(stream, "stream");
			if (!stream.CanSeek)
			{
				throw new ArgumentException(MsgStorageStrings.StreamNotSeakable("LockBytesOnStream::ctr"));
			}
			this.stream = stream;
			this.streamLock = new object();
			this.offset = 0L;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00017BF4 File Offset: 0x00015DF4
		public void ReadAt(long offset, byte[] buffer, int bufferSize, out int totalBytesRead)
		{
			lock (this.streamLock)
			{
				if (this.offset != offset)
				{
					this.offset = this.stream.Seek(offset, SeekOrigin.Begin);
				}
				totalBytesRead = 0;
				int num = 0;
				int num2 = bufferSize;
				int num3;
				do
				{
					num3 = this.stream.Read(buffer, num, num2);
					totalBytesRead += num3;
					this.offset += (long)num3;
					num += num3;
					num2 -= num3;
				}
				while (totalBytesRead < bufferSize && num3 != 0);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00017C8C File Offset: 0x00015E8C
		public void WriteAt(long offset, byte[] buffer, int bufferSize, out int bytesWritten)
		{
			lock (this.streamLock)
			{
				if (this.offset != offset)
				{
					this.offset = this.stream.Seek(offset, SeekOrigin.Begin);
				}
				this.stream.Write(buffer, 0, bufferSize);
				bytesWritten = bufferSize;
				this.offset += (long)bytesWritten;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00017D08 File Offset: 0x00015F08
		public void Flush()
		{
			lock (this.streamLock)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00017D50 File Offset: 0x00015F50
		public void SetSize(long newSize)
		{
			lock (this.streamLock)
			{
				this.stream.SetLength(newSize);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00017D98 File Offset: 0x00015F98
		public void LockRegion(long offset, long length, uint lockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00017D9F File Offset: 0x00015F9F
		public void UnlockRegion(long offset, long length, int lockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00017DA8 File Offset: 0x00015FA8
		public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG statStg, uint statFlag)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			filetime.dwHighDateTime = 0;
			filetime.dwLowDateTime = 0;
			statStg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			statStg.atime = filetime;
			statStg.mtime = filetime;
			statStg.ctime = filetime;
			statStg.type = 2;
			statStg.cbSize = this.stream.Length;
			statStg.grfLocksSupported = 0;
			statStg.clsid = Guid.Empty;
			statStg.pwcsName = null;
		}

		// Token: 0x04000541 RID: 1345
		private Stream stream;

		// Token: 0x04000542 RID: 1346
		private object streamLock;

		// Token: 0x04000543 RID: 1347
		private long offset;
	}
}
