using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200003F RID: 63
	internal sealed class BufferReader : Reader
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005B34 File Offset: 0x00003D34
		internal BufferReader(ArraySegment<byte> arraySegment)
		{
			this.arraySegment = arraySegment;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005B44 File Offset: 0x00003D44
		public static TResult Parse<TResult>(ArraySegment<byte> arraySegment, Func<Reader, TResult> parser)
		{
			TResult result;
			using (BufferReader bufferReader = new BufferReader(arraySegment))
			{
				result = parser(bufferReader);
			}
			return result;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005B80 File Offset: 0x00003D80
		public BufferReader SubReader(int count)
		{
			return new BufferReader(this.arraySegment.SubSegment(this.bufferRelativePosition, count));
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005B9C File Offset: 0x00003D9C
		private int BufferAbsolutePosition
		{
			get
			{
				return this.arraySegment.Offset + this.bufferRelativePosition;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public override long Length
		{
			get
			{
				return (long)this.arraySegment.Count;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005BDC File Offset: 0x00003DDC
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public override long Position
		{
			get
			{
				return (long)this.bufferRelativePosition;
			}
			set
			{
				if (value < 0L || value > (long)this.arraySegment.Count)
				{
					throw new BufferParseException(string.Format("Invalid position. Position requested = {0}. Size of underlying buffer = {1}", value, this.arraySegment.Count));
				}
				this.bufferRelativePosition = (int)value;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005C40 File Offset: 0x00003E40
		protected override byte InternalReadByte()
		{
			byte result = this.arraySegment.Array[this.BufferAbsolutePosition];
			this.bufferRelativePosition++;
			return result;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005C74 File Offset: 0x00003E74
		protected override double InternalReadDouble()
		{
			double result = BitConverter.ToDouble(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 8;
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005CAC File Offset: 0x00003EAC
		protected override short InternalReadInt16()
		{
			short result = BitConverter.ToInt16(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 2;
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005CE4 File Offset: 0x00003EE4
		protected override ushort InternalReadUInt16()
		{
			ushort result = BitConverter.ToUInt16(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 2;
			return result;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005D1C File Offset: 0x00003F1C
		protected override int InternalReadInt32()
		{
			int result = BitConverter.ToInt32(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 4;
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005D54 File Offset: 0x00003F54
		protected override uint InternalReadUInt32()
		{
			uint result = BitConverter.ToUInt32(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 4;
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005D8C File Offset: 0x00003F8C
		protected override long InternalReadInt64()
		{
			long result = BitConverter.ToInt64(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 8;
			return result;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005DC4 File Offset: 0x00003FC4
		protected override ulong InternalReadUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 8;
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005DFC File Offset: 0x00003FFC
		protected override float InternalReadSingle()
		{
			float result = BitConverter.ToSingle(this.arraySegment.Array, this.BufferAbsolutePosition);
			this.bufferRelativePosition += 4;
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005E34 File Offset: 0x00004034
		protected override ArraySegment<byte> InternalReadArraySegment(uint count)
		{
			ArraySegment<byte> result = this.arraySegment.SubSegment(this.bufferRelativePosition, (int)count);
			this.bufferRelativePosition += (int)count;
			return result;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005E64 File Offset: 0x00004064
		protected override ArraySegment<byte> InternalReadArraySegmentForString(int maxCount)
		{
			int count = Math.Min(maxCount, this.arraySegment.Count - this.bufferRelativePosition);
			return this.InternalReadArraySegment((uint)count);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005E94 File Offset: 0x00004094
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BufferReader>(this);
		}

		// Token: 0x040000C5 RID: 197
		private readonly ArraySegment<byte> arraySegment;

		// Token: 0x040000C6 RID: 198
		private int bufferRelativePosition;
	}
}
