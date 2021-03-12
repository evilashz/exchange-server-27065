using System;
using System.IO;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C10 RID: 3088
	internal class DotStuffingStream : Stream
	{
		// Token: 0x06004392 RID: 17298 RVA: 0x000B5A9F File Offset: 0x000B3C9F
		public DotStuffingStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x000B5AA9 File Offset: 0x000B3CA9
		public DotStuffingStream(Stream stream, bool rejectBareLinefeeds)
		{
			this.stream = stream;
			this.rejectBareLinefeeds = rejectBareLinefeeds;
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000B5ABF File Offset: 0x000B3CBF
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06004395 RID: 17301 RVA: 0x000B5AC2 File Offset: 0x000B3CC2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000B5AC5 File Offset: 0x000B3CC5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x000B5AC8 File Offset: 0x000B3CC8
		public override bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x000B5ACB File Offset: 0x000B3CCB
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x000B5AD2 File Offset: 0x000B3CD2
		// (set) Token: 0x0600439A RID: 17306 RVA: 0x000B5ADA File Offset: 0x000B3CDA
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x0600439B RID: 17307 RVA: 0x000B5AE1 File Offset: 0x000B3CE1
		// (set) Token: 0x0600439C RID: 17308 RVA: 0x000B5AE8 File Offset: 0x000B3CE8
		public override int ReadTimeout
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x0600439D RID: 17309 RVA: 0x000B5AEF File Offset: 0x000B3CEF
		// (set) Token: 0x0600439E RID: 17310 RVA: 0x000B5AF6 File Offset: 0x000B3CF6
		public override int WriteTimeout
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000B5AFD File Offset: 0x000B3CFD
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000B5B04 File Offset: 0x000B3D04
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000B5B0B File Offset: 0x000B3D0B
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000B5B12 File Offset: 0x000B3D12
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000B5B19 File Offset: 0x000B3D19
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000B5B20 File Offset: 0x000B3D20
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (this.state == DotStuffingStream.State.EndOfData)
			{
				return 0;
			}
			int num = (size - 6) * 3 / 4;
			int num2 = offset + size - num;
			int bytesFilled = this.stream.Read(buffer, num2, num);
			return this.DotStuffBuffer(buffer, offset, size, num2, bytesFilled);
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000B5B60 File Offset: 0x000B3D60
		public override int ReadByte()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000B5B67 File Offset: 0x000B3D67
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000B5B6E File Offset: 0x000B3D6E
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000B5B75 File Offset: 0x000B3D75
		public override void Write(byte[] buffer, int offset, int size)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000B5B7C File Offset: 0x000B3D7C
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000B5B83 File Offset: 0x000B3D83
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Flush();
				this.stream.Dispose();
				this.stream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000B5BB4 File Offset: 0x000B3DB4
		private int DotStuffBuffer(byte[] buffer, int offset, int size, int readOffset, int bytesFilled)
		{
			int num = 0;
			for (int i = readOffset; i < readOffset + bytesFilled; i++)
			{
				byte b = buffer[i];
				buffer[offset + num++] = b;
				switch (this.state)
				{
				case DotStuffingStream.State.BeginningOfLine:
					if (b == 46)
					{
						buffer[offset + num++] = 46;
						this.state = DotStuffingStream.State.Data;
					}
					else if (b == 13)
					{
						this.state = DotStuffingStream.State.CarriageReturnSeen;
					}
					else
					{
						this.state = DotStuffingStream.State.Data;
					}
					break;
				case DotStuffingStream.State.Data:
					if (b == 13)
					{
						this.state = DotStuffingStream.State.CarriageReturnSeen;
					}
					else if (b == 10 && this.rejectBareLinefeeds)
					{
						throw new BareLinefeedException();
					}
					break;
				case DotStuffingStream.State.CarriageReturnSeen:
					if (b == 10)
					{
						this.state = DotStuffingStream.State.BeginningOfLine;
					}
					else if (b == 13)
					{
						this.state = DotStuffingStream.State.CarriageReturnSeen;
					}
					else
					{
						this.state = DotStuffingStream.State.Data;
					}
					break;
				default:
					throw new InvalidOperationException();
				}
			}
			if (this.stream.Position == this.stream.Length)
			{
				int num2;
				switch (this.state)
				{
				case DotStuffingStream.State.BeginningOfLine:
					num2 = 2;
					break;
				case DotStuffingStream.State.Data:
					num2 = 0;
					break;
				case DotStuffingStream.State.CarriageReturnSeen:
					num2 = 1;
					break;
				default:
					throw new InvalidOperationException();
				}
				this.state = DotStuffingStream.State.EndOfData;
				int num3 = DotStuffingStream.EodSequence.Length - num2;
				Buffer.BlockCopy(DotStuffingStream.EodSequence, num2, buffer, offset + num, num3);
				num += num3;
			}
			this.position += (long)num;
			return num;
		}

		// Token: 0x04003996 RID: 14742
		private static readonly byte[] EodSequence = new byte[]
		{
			13,
			10,
			46,
			13,
			10
		};

		// Token: 0x04003997 RID: 14743
		private Stream stream;

		// Token: 0x04003998 RID: 14744
		private DotStuffingStream.State state;

		// Token: 0x04003999 RID: 14745
		private long position;

		// Token: 0x0400399A RID: 14746
		private bool rejectBareLinefeeds;

		// Token: 0x02000C11 RID: 3089
		private enum State
		{
			// Token: 0x0400399C RID: 14748
			BeginningOfLine,
			// Token: 0x0400399D RID: 14749
			Data,
			// Token: 0x0400399E RID: 14750
			CarriageReturnSeen,
			// Token: 0x0400399F RID: 14751
			EndOfData
		}
	}
}
