using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200038E RID: 910
	internal sealed class StreamReader : Reader
	{
		// Token: 0x060015FD RID: 5629 RVA: 0x00038D31 File Offset: 0x00036F31
		internal StreamReader(Stream stream)
		{
			Util.ThrowOnNullArgument(stream, "stream");
			this.reader = new BinaryReader(stream, Encoding.Unicode);
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00038D55 File Offset: 0x00036F55
		public override long Length
		{
			get
			{
				return this.reader.BaseStream.Length;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00038D67 File Offset: 0x00036F67
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x00038D79 File Offset: 0x00036F79
		public override long Position
		{
			get
			{
				return this.reader.BaseStream.Position;
			}
			set
			{
				this.reader.BaseStream.Position = value;
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00038D8C File Offset: 0x00036F8C
		protected override byte InternalReadByte()
		{
			return this.reader.ReadByte();
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00038D99 File Offset: 0x00036F99
		protected override double InternalReadDouble()
		{
			return this.reader.ReadDouble();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00038DA6 File Offset: 0x00036FA6
		protected override short InternalReadInt16()
		{
			return this.reader.ReadInt16();
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00038DB3 File Offset: 0x00036FB3
		protected override ushort InternalReadUInt16()
		{
			return this.reader.ReadUInt16();
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00038DC0 File Offset: 0x00036FC0
		protected override int InternalReadInt32()
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00038DCD File Offset: 0x00036FCD
		protected override uint InternalReadUInt32()
		{
			return this.reader.ReadUInt32();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00038DDA File Offset: 0x00036FDA
		protected override long InternalReadInt64()
		{
			return this.reader.ReadInt64();
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00038DE7 File Offset: 0x00036FE7
		protected override ulong InternalReadUInt64()
		{
			return this.reader.ReadUInt64();
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00038DF4 File Offset: 0x00036FF4
		protected override float InternalReadSingle()
		{
			return this.reader.ReadSingle();
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00038E01 File Offset: 0x00037001
		protected override ArraySegment<byte> InternalReadArraySegment(uint count)
		{
			return new ArraySegment<byte>(this.reader.ReadBytes((int)count));
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00038E14 File Offset: 0x00037014
		protected override ArraySegment<byte> InternalReadArraySegmentForString(int maxCount)
		{
			int count = this.reader.Read(base.StringBuffer, 0, maxCount);
			return new ArraySegment<byte>(base.StringBuffer, 0, count);
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00038E42 File Offset: 0x00037042
		protected override bool NeedsStagingAreaForFixedLengthStrings
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00038E45 File Offset: 0x00037045
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamReader>(this);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00038E4D File Offset: 0x0003704D
		protected override void InternalDispose()
		{
			if (this.reader != null)
			{
				((IDisposable)this.reader).Dispose();
			}
			base.InternalDispose();
		}

		// Token: 0x04000B74 RID: 2932
		private readonly BinaryReader reader;
	}
}
