using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000045 RID: 69
	internal sealed class CountWriter : Writer
	{
		// Token: 0x060001DC RID: 476 RVA: 0x000071FB File Offset: 0x000053FB
		internal CountWriter()
		{
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007203 File Offset: 0x00005403
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000720C File Offset: 0x0000540C
		public override long Position
		{
			get
			{
				return (long)((ulong)this.currentPosition);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException(string.Format("Cannot move to position {0}.", value));
				}
				this.currentPosition = (uint)value;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007231 File Offset: 0x00005431
		public override void WriteByte(byte value)
		{
			this.AdvancePosition(1U);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000723A File Offset: 0x0000543A
		protected override void InternalWriteBytes(byte[] value, int offset, int count)
		{
			this.AdvancePosition((uint)count);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007243 File Offset: 0x00005443
		public override void WriteDouble(double value)
		{
			this.AdvancePosition(8U);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000724C File Offset: 0x0000544C
		public override void WriteSingle(float value)
		{
			this.AdvancePosition(4U);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007255 File Offset: 0x00005455
		public override void WriteGuid(Guid value)
		{
			this.AdvancePosition(16U);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000725F File Offset: 0x0000545F
		public override void WriteInt32(int value)
		{
			this.AdvancePosition(4U);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007268 File Offset: 0x00005468
		public override void WriteInt64(long value)
		{
			this.AdvancePosition(8U);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007271 File Offset: 0x00005471
		public override void WriteInt16(short value)
		{
			this.AdvancePosition(2U);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000727A File Offset: 0x0000547A
		public override void WriteUInt32(uint value)
		{
			this.AdvancePosition(4U);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007283 File Offset: 0x00005483
		public override void WriteUInt64(ulong value)
		{
			this.AdvancePosition(8U);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000728C File Offset: 0x0000548C
		public override void WriteUInt16(ushort value)
		{
			this.AdvancePosition(2U);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007295 File Offset: 0x00005495
		public override void SkipArraySegment(ArraySegment<byte> buffer)
		{
			this.AdvancePosition((uint)buffer.Count);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000072A4 File Offset: 0x000054A4
		protected override void InternalWriteString(string value, int length, Encoding encoding)
		{
			this.AdvancePosition((uint)length);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000072AD File Offset: 0x000054AD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CountWriter>(this);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000072B5 File Offset: 0x000054B5
		private void AdvancePosition(uint count)
		{
			this.currentPosition += count;
		}

		// Token: 0x040000D4 RID: 212
		private uint currentPosition;
	}
}
