using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Data.Transport.Interop
{
	// Token: 0x02000007 RID: 7
	internal class InvocationContext : IProxyCallback, IStream, IDisposable
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002374 File Offset: 0x00000574
		internal InvocationContext(ComProxy.AsyncCompletionCallback asyncComplete, ComArguments propertyBag, MailItem mailItem)
		{
			this.asyncComplete = asyncComplete;
			this.bag = propertyBag;
			this.mailItem = mailItem;
			if (this.mailItem != null)
			{
				this.readStream = this.mailItem.GetMimeReadStream();
				if (this.readStream == null)
				{
					throw new InvalidOperationException("Can't open read stream.");
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023C8 File Offset: 0x000005C8
		void IProxyCallback.AsyncCompletion()
		{
			if (this.asyncComplete == null)
			{
				throw new InvalidOperationException();
			}
			this.asyncComplete(this.bag);
			this.asyncComplete = null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023F0 File Offset: 0x000005F0
		void IProxyCallback.SetWriteStream([MarshalAs(UnmanagedType.Interface)] IStream writeStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023F7 File Offset: 0x000005F7
		void IProxyCallback.PutProperty(int id, byte[] value)
		{
			this.bag[id] = value;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002406 File Offset: 0x00000606
		void IProxyCallback.GetProperty(int id, out byte[] value)
		{
			value = this.bag[id];
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002416 File Offset: 0x00000616
		void IStream.Clone(out IStream ppstm)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000241D File Offset: 0x0000061D
		void IStream.Commit(int grfCommitFlags)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002424 File Offset: 0x00000624
		unsafe void IStream.CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			if (pstm == null)
			{
				throw new ArgumentException();
			}
			try
			{
				if (this.mailItem == null)
				{
					throw new InvalidOperationException();
				}
				int num = (cb > 16384L) ? 16384 : ((int)cb);
				byte[] array = new byte[num];
				int num2 = 0;
				uint num3 = 0U;
				IntPtr pcbWritten2 = new IntPtr((void*)(&num3));
				while ((long)num2 < cb)
				{
					int num4 = this.readStream.Read(array, 0, num);
					if (num4 == 0)
					{
						break;
					}
					pstm.Write(array, num4, pcbWritten2);
					if ((ulong)num3 != (ulong)((long)num4))
					{
						throw new InvalidOperationException("not all bytes were written to the stream.");
					}
					num2 += num4;
				}
				if (pcbRead != IntPtr.Zero)
				{
					Marshal.WriteInt64(pcbRead, (long)num2);
				}
				if (pcbWritten != IntPtr.Zero)
				{
					Marshal.WriteInt64(pcbWritten, (long)num2);
				}
			}
			finally
			{
				Marshal.ReleaseComObject(pstm);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024F4 File Offset: 0x000006F4
		void IStream.LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024FB File Offset: 0x000006FB
		void IStream.Read([Out] byte[] pv, int cb, IntPtr pcbRead)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002502 File Offset: 0x00000702
		void IStream.Revert()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002509 File Offset: 0x00000709
		void IStream.Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002510 File Offset: 0x00000710
		void IStream.SetSize(long libNewSize)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002517 File Offset: 0x00000717
		void IStream.Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
		{
			if (this.mailItem == null)
			{
				throw new InvalidOperationException();
			}
			pstatstg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			pstatstg.type = 2;
			pstatstg.cbSize = this.readStream.Length;
			pstatstg.grfMode = 0;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000254D File Offset: 0x0000074D
		void IStream.UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002554 File Offset: 0x00000754
		void IStream.Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000255B File Offset: 0x0000075B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000256A File Offset: 0x0000076A
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				if (this.readStream != null)
				{
					this.readStream.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002594 File Offset: 0x00000794
		~InvocationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x04000008 RID: 8
		private const long CopytoBlockSize = 16384L;

		// Token: 0x04000009 RID: 9
		private Stream readStream;

		// Token: 0x0400000A RID: 10
		private MailItem mailItem;

		// Token: 0x0400000B RID: 11
		private ComProxy.AsyncCompletionCallback asyncComplete;

		// Token: 0x0400000C RID: 12
		private ComArguments bag;

		// Token: 0x0400000D RID: 13
		private bool disposed;
	}
}
