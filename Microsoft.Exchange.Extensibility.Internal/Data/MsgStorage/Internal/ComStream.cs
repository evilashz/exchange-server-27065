using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x02000098 RID: 152
	internal class ComStream : Stream
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x000177D4 File Offset: 0x000159D4
		internal ComStream(Interop.IStream iStream)
		{
			this.iStream = iStream;
			this.isDisposed = false;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000177EA File Offset: 0x000159EA
		public Interop.IStream IStream
		{
			get
			{
				return this.iStream;
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000177F4 File Offset: 0x000159F4
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("ComStream::Read");
			int result;
			fixed (byte* ptr = &buffer[offset])
			{
				result = this.Read(ptr, count);
			}
			return result;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00017850 File Offset: 0x00015A50
		public unsafe int Read(byte* pBuffer, int count)
		{
			this.CheckDisposed("ComStream::Read");
			int bytesRead = 0;
			Util.InvokeComCall(MsgStorageErrorCode.FailedRead, delegate
			{
				this.iStream.Read(pBuffer, count, out bytesRead);
			});
			return bytesRead;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000178A4 File Offset: 0x00015AA4
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("ComStream::Write");
			fixed (byte* ptr = &buffer[offset])
			{
				this.Write(ptr, count);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017920 File Offset: 0x00015B20
		public unsafe void Write(byte* pBuffer, int count)
		{
			this.CheckDisposed("ComStream::Write");
			Util.InvokeComCall(MsgStorageErrorCode.FailedWrite, delegate
			{
				int num = 0;
				this.iStream.Write(pBuffer, count, out num);
				if (num != count)
				{
					throw new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.FailedWrite(string.Empty));
				}
			});
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017994 File Offset: 0x00015B94
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("ComStream::Seek");
			long position = 0L;
			Util.InvokeComCall(MsgStorageErrorCode.FailedSeek, delegate
			{
				this.iStream.Seek(offset, (int)origin, out position);
			});
			return position;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000179E7 File Offset: 0x00015BE7
		public override long Length
		{
			get
			{
				this.CheckDisposed("ComStream::get_Length");
				return this.GetStat().cbSize;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000179FF File Offset: 0x00015BFF
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00017A15 File Offset: 0x00015C15
		public override long Position
		{
			get
			{
				this.CheckDisposed("ComStream::get_Position");
				return this.Seek(0L, SeekOrigin.Current);
			}
			set
			{
				this.CheckDisposed("ComStream::set_Position");
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00017A4C File Offset: 0x00015C4C
		public override void SetLength(long value)
		{
			this.CheckDisposed("ComStream::SetLength");
			Util.InvokeComCall(MsgStorageErrorCode.FailedWrite, delegate
			{
				this.iStream.SetSize(value);
			});
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017A98 File Offset: 0x00015C98
		public override void Flush()
		{
			this.CheckDisposed("ComStream.Flush()");
			Util.InvokeComCall(MsgStorageErrorCode.FailedWrite, delegate
			{
				this.iStream.Commit(0U);
			});
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00017AB7 File Offset: 0x00015CB7
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed("ComStream.get_CanRead");
				return true;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00017AC5 File Offset: 0x00015CC5
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed("ComStream.get_CanWrite");
				return true;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00017AD3 File Offset: 0x00015CD3
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("ComStream.get_CanWrite");
				return true;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017B14 File Offset: 0x00015D14
		private System.Runtime.InteropServices.ComTypes.STATSTG GetStat()
		{
			System.Runtime.InteropServices.ComTypes.STATSTG result = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			Util.InvokeComCall(MsgStorageErrorCode.FailedRead, delegate
			{
				System.Runtime.InteropServices.ComTypes.STATSTG result;
				this.iStream.Stat(out result, 1U);
				result = result;
			});
			return result;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00017B52 File Offset: 0x00015D52
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.iStream != null)
				{
					Marshal.ReleaseComObject(this.iStream);
					this.iStream = null;
				}
				GC.SuppressFinalize(this);
			}
			this.isDisposed = true;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017B7F File Offset: 0x00015D7F
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(methodName);
			}
		}

		// Token: 0x0400051B RID: 1307
		private Interop.IStream iStream;

		// Token: 0x0400051C RID: 1308
		private bool isDisposed;
	}
}
