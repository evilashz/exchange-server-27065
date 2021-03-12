using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200038F RID: 911
	internal sealed class StreamWriter : Writer
	{
		// Token: 0x0600160F RID: 5647 RVA: 0x00038E68 File Offset: 0x00037068
		internal StreamWriter(Stream stream)
		{
			Util.ThrowOnNullArgument(stream, "stream");
			this.writer = new BinaryWriter(stream, Encoding.Unicode);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x00038E8C File Offset: 0x0003708C
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x00038E9E File Offset: 0x0003709E
		public override long Position
		{
			get
			{
				return this.writer.BaseStream.Position;
			}
			set
			{
				this.writer.BaseStream.Position = value;
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00038EB4 File Offset: 0x000370B4
		public override void WriteByte(byte value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00038EFC File Offset: 0x000370FC
		protected override void InternalWriteBytes(byte[] value, int offset, int count)
		{
			try
			{
				this.writer.Write(value, offset, count);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00038F44 File Offset: 0x00037144
		public override void WriteDouble(double value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00038F8C File Offset: 0x0003718C
		public override void WriteSingle(float value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00038FD4 File Offset: 0x000371D4
		public override void WriteGuid(Guid value)
		{
			if (this.charBytes == null)
			{
				this.charBytes = new byte[256];
			}
			int count = ExBitConverter.Write(value, this.charBytes, 0);
			base.WriteBytes(this.charBytes, 0, count);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00039018 File Offset: 0x00037218
		public override void WriteInt32(int value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00039060 File Offset: 0x00037260
		public override void WriteInt64(long value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000390A8 File Offset: 0x000372A8
		public override void WriteInt16(short value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000390F0 File Offset: 0x000372F0
		public override void WriteUInt32(uint value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00039138 File Offset: 0x00037338
		public override void WriteUInt64(ulong value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00039180 File Offset: 0x00037380
		public override void WriteUInt16(ushort value)
		{
			try
			{
				this.writer.Write(value);
			}
			catch (IOException)
			{
				throw new BufferTooSmallException();
			}
			catch (NotSupportedException)
			{
				throw new BufferTooSmallException();
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000391C8 File Offset: 0x000373C8
		public override void SkipArraySegment(ArraySegment<byte> buffer)
		{
			throw new NotSupportedException("StreamWriter does not support SkipArraySegment.");
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000391D4 File Offset: 0x000373D4
		protected override void InternalWriteString(string value, int length, Encoding encoding)
		{
			base.CheckDisposed();
			if (this.charBytes == null)
			{
				this.charBytes = new byte[256];
			}
			if (length <= 256)
			{
				encoding.GetBytes(value, 0, value.Length, this.charBytes, 0);
				base.WriteBytes(this.charBytes, 0, length);
				return;
			}
			this.WriteLargeBufferString(value, encoding);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00039234 File Offset: 0x00037434
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamWriter>(this);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0003923C File Offset: 0x0003743C
		protected override void InternalDispose()
		{
			if (this.writer != null)
			{
				((IDisposable)this.writer).Dispose();
			}
			base.InternalDispose();
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00039258 File Offset: 0x00037458
		private unsafe void WriteLargeBufferString(string value, Encoding encoding)
		{
			int num = 0;
			int i = value.Length;
			Encoder encoder = encoding.GetEncoder();
			int num2 = 256 / encoding.GetMaxByteCount(1);
			while (i > 0)
			{
				int num3 = (i > num2) ? num2 : i;
				int bytes;
				fixed (char* ptr = value)
				{
					fixed (byte* ptr2 = this.charBytes)
					{
						bytes = encoder.GetBytes(ptr + num, num3, ptr2, 256, num3 == i);
					}
				}
				base.WriteBytes(this.charBytes, 0, bytes);
				num += num3;
				i -= num3;
			}
		}

		// Token: 0x04000B75 RID: 2933
		private const int LargeByteBufferSize = 256;

		// Token: 0x04000B76 RID: 2934
		private readonly BinaryWriter writer;

		// Token: 0x04000B77 RID: 2935
		private byte[] charBytes;
	}
}
