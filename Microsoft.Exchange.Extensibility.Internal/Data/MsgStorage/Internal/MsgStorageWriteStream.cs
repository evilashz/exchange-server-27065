using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x02000094 RID: 148
	internal class MsgStorageWriteStream : Stream
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0001685C File Offset: 0x00014A5C
		internal MsgStorageWriteStream(Stream innerStream, int addStringTerminators)
		{
			Util.ThrowOnNullArgument(innerStream, "innerStream");
			if (addStringTerminators < 0 || addStringTerminators > 2)
			{
				throw new ArgumentException("addStringTerminators must be in the range 0 - 2");
			}
			this.innerStream = innerStream;
			this.offset = 0L;
			this.length = 0L;
			this.addStringTerminators = addStringTerminators;
			this.terminatorsFound = 0;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000168B2 File Offset: 0x00014AB2
		internal void AddOnCloseNotifier(MsgStorageWriteStream.OnCloseDelegate onCloseDelegate)
		{
			this.CheckDisposed("MsgStorageWriteStream::AddOnCloseNotifier");
			this.onCloseDelegate = (MsgStorageWriteStream.OnCloseDelegate)Delegate.Combine(this.onCloseDelegate, onCloseDelegate);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000168D6 File Offset: 0x00014AD6
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("MsgStorageWriteStream::Read");
			throw new NotSupportedException("MsgStorageWriteStream::Read");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000168F0 File Offset: 0x00014AF0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("MsgStorageWriteStream::Write");
			this.innerStream.Write(buffer, offset, count);
			if (this.addStringTerminators != 0)
			{
				int num = (count < this.addStringTerminators) ? count : this.addStringTerminators;
				int num2 = 0;
				while (num2 != num && buffer[count - num2 - 1] == 0)
				{
					num2++;
				}
				if (num2 < count)
				{
					this.terminatorsFound = num2;
				}
				else
				{
					this.terminatorsFound += num2;
				}
			}
			this.offset += (long)count;
			if (this.offset > this.length)
			{
				this.length = this.offset;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001698A File Offset: 0x00014B8A
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("MsgStorageWriteStream::Seek");
			this.offset = this.innerStream.Seek(offset, origin);
			return this.offset;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000169B0 File Offset: 0x00014BB0
		public override long Length
		{
			get
			{
				this.CheckDisposed("MsgStorageWriteStream::get_Length");
				return this.length;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000169C3 File Offset: 0x00014BC3
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000169D6 File Offset: 0x00014BD6
		public override long Position
		{
			get
			{
				this.CheckDisposed("MsgStorageWriteStream::get_Position");
				return this.offset;
			}
			set
			{
				this.CheckDisposed("MsgStorageWriteStream::set_Position");
				this.innerStream.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000169F1 File Offset: 0x00014BF1
		public override void SetLength(long value)
		{
			this.CheckDisposed("ComStream::SetLength");
			this.innerStream.SetLength(value);
			this.length = value;
			this.offset = this.innerStream.Position;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016A22 File Offset: 0x00014C22
		public override void Flush()
		{
			this.CheckDisposed("ComStream::Flush");
			this.innerStream.Flush();
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00016A3A File Offset: 0x00014C3A
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed("ComStream::get_CanRead");
				return false;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00016A48 File Offset: 0x00014C48
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed("ComStream::get_CanWrite");
				return true;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00016A56 File Offset: 0x00014C56
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("ComStream::get_CanSeek");
				return true;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00016A64 File Offset: 0x00014C64
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				if (this.innerStream != null)
				{
					Exception onCloseException = null;
					try
					{
						if (this.terminatorsFound < this.addStringTerminators)
						{
							this.Write(MsgStorageWriteStream.StringTerminators, 0, this.addStringTerminators);
						}
					}
					catch (COMException ex)
					{
						onCloseException = ex;
					}
					catch (IOException ex2)
					{
						onCloseException = ex2;
					}
					if (this.onCloseDelegate != null)
					{
						this.onCloseDelegate(this, onCloseException);
						this.onCloseDelegate = null;
					}
					this.innerStream.Dispose();
					this.innerStream = null;
				}
				GC.SuppressFinalize(this);
			}
			this.isDisposed = true;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00016B0C File Offset: 0x00014D0C
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(methodName);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016B20 File Offset: 0x00014D20
		// Note: this type is marked as 'beforefieldinit'.
		static MsgStorageWriteStream()
		{
			byte[] stringTerminators = new byte[2];
			MsgStorageWriteStream.StringTerminators = stringTerminators;
		}

		// Token: 0x04000509 RID: 1289
		private static readonly byte[] StringTerminators;

		// Token: 0x0400050A RID: 1290
		private Stream innerStream;

		// Token: 0x0400050B RID: 1291
		private int addStringTerminators;

		// Token: 0x0400050C RID: 1292
		private int terminatorsFound;

		// Token: 0x0400050D RID: 1293
		private long offset;

		// Token: 0x0400050E RID: 1294
		private long length;

		// Token: 0x0400050F RID: 1295
		private MsgStorageWriteStream.OnCloseDelegate onCloseDelegate;

		// Token: 0x04000510 RID: 1296
		private bool isDisposed;

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x060004DB RID: 1243
		internal delegate void OnCloseDelegate(MsgStorageWriteStream stream, Exception onCloseException);
	}
}
