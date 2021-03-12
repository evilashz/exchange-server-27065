using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000041 RID: 65
	internal sealed class BufferWriter : Writer
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x000067F7 File Offset: 0x000049F7
		internal BufferWriter(ArraySegment<byte> buffer)
		{
			Util.ThrowOnNullArgument(buffer, "buffer");
			this.buffer = buffer;
			this.bufferRelativePosition = 0U;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000681D File Offset: 0x00004A1D
		internal BufferWriter(byte[] buffer) : this(new ArraySegment<byte>(buffer))
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000682B File Offset: 0x00004A2B
		internal BufferWriter(int size) : this(new byte[size])
		{
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006839 File Offset: 0x00004A39
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006844 File Offset: 0x00004A44
		public override long Position
		{
			get
			{
				return (long)((ulong)this.bufferRelativePosition);
			}
			set
			{
				if (value < 0L || value > (long)this.buffer.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Cannot move to position {0}. It is out of range for our buffer of size {1}", value, this.buffer.Count));
				}
				this.bufferRelativePosition = (uint)value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000689C File Offset: 0x00004A9C
		public uint AvailableSpace
		{
			get
			{
				return (uint)(this.buffer.Count - (int)this.bufferRelativePosition);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000068C0 File Offset: 0x00004AC0
		private int BufferAbsolutePosition
		{
			get
			{
				return (int)((ulong)this.bufferRelativePosition + (ulong)((long)this.buffer.Offset));
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static byte[] Serialize(BufferWriter.SerializeDelegate serializeDelegate)
		{
			uint num = 0U;
			using (CountWriter countWriter = new CountWriter())
			{
				serializeDelegate(countWriter);
				num = (uint)countWriter.Position;
			}
			byte[] result = new byte[num];
			using (Writer writer = new BufferWriter(result))
			{
				serializeDelegate(writer);
			}
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006958 File Offset: 0x00004B58
		public int CopyFrom(Stream source, int count)
		{
			Util.ThrowOnNullArgument(source, "source");
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.CheckValidRange((uint)count);
			int num = source.Read(this.buffer.Array, this.BufferAbsolutePosition, count);
			this.bufferRelativePosition += (uint)num;
			return num;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000069B4 File Offset: 0x00004BB4
		public BufferWriter SubWriter()
		{
			return new BufferWriter(this.buffer.SubSegment((int)this.bufferRelativePosition, (int)((long)this.buffer.Count - (long)((ulong)this.bufferRelativePosition))));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000069F0 File Offset: 0x00004BF0
		public override void WriteByte(byte value)
		{
			this.CheckValidRange(1U);
			this.buffer.Array[this.BufferAbsolutePosition] = value;
			this.bufferRelativePosition += 1U;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006A28 File Offset: 0x00004C28
		public override void WriteDouble(double value)
		{
			this.CheckValidRange(8U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 8U;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006A68 File Offset: 0x00004C68
		public override void WriteSingle(float value)
		{
			this.CheckValidRange(4U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 4U;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public override void WriteGuid(Guid value)
		{
			this.CheckValidRange(16U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 16U;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006AE8 File Offset: 0x00004CE8
		public override void WriteInt32(int value)
		{
			this.CheckValidRange(4U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 4U;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006B28 File Offset: 0x00004D28
		public override void WriteInt64(long value)
		{
			this.CheckValidRange(8U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 8U;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006B68 File Offset: 0x00004D68
		public override void WriteInt16(short value)
		{
			this.CheckValidRange(2U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 2U;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public override void WriteUInt32(uint value)
		{
			this.CheckValidRange(4U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 4U;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public override void WriteUInt64(ulong value)
		{
			this.CheckValidRange(8U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 8U;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006C28 File Offset: 0x00004E28
		public override void WriteUInt16(ushort value)
		{
			this.CheckValidRange(2U);
			ExBitConverter.Write(value, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 2U;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006C68 File Offset: 0x00004E68
		public override void SkipArraySegment(ArraySegment<byte> buffer)
		{
			if (BufferWriter.verifySkippedArraySegment)
			{
				if (buffer.Array != this.buffer.Array)
				{
					throw new ArgumentException("The ArraySegment being skipped is not the current buffer in the Writer.");
				}
				if (buffer.Offset != this.BufferAbsolutePosition)
				{
					throw new ArgumentException("The ArraySegment being skipped is not in the current Writer's buffer position.");
				}
			}
			this.CheckValidRange((uint)buffer.Count);
			this.bufferRelativePosition += (uint)buffer.Count;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006CD9 File Offset: 0x00004ED9
		internal static void VerifySkippedArraySegment(bool verify)
		{
			BufferWriter.verifySkippedArraySegment = verify;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006CE4 File Offset: 0x00004EE4
		protected override void InternalWriteBytes(byte[] value, int offset, int count)
		{
			this.CheckValidRange((uint)count);
			Buffer.BlockCopy(value, offset, this.buffer.Array, this.BufferAbsolutePosition, count);
			this.bufferRelativePosition += (uint)count;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006D24 File Offset: 0x00004F24
		protected override void InternalWriteString(string value, int length, Encoding encoding)
		{
			this.CheckValidRange((uint)length);
			encoding.GetBytes(value, 0, value.Length, this.buffer.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += (uint)length;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006D69 File Offset: 0x00004F69
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BufferWriter>(this);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006D71 File Offset: 0x00004F71
		private void ThrowInvalidRange()
		{
			throw new BufferTooSmallException();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006D78 File Offset: 0x00004F78
		private void CheckValidRange(uint size)
		{
			if (size > this.AvailableSpace)
			{
				this.ThrowInvalidRange();
			}
		}

		// Token: 0x040000C9 RID: 201
		private static bool verifySkippedArraySegment = true;

		// Token: 0x040000CA RID: 202
		private readonly ArraySegment<byte> buffer;

		// Token: 0x040000CB RID: 203
		private uint bufferRelativePosition;

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x060001D1 RID: 465
		public delegate void SerializeDelegate(Writer writer);
	}
}
