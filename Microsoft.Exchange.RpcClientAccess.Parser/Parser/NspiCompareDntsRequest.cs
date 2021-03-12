using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001CA RID: 458
	internal sealed class NspiCompareDntsRequest : MapiHttpRequest
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0001E7D3 File Offset: 0x0001C9D3
		public NspiCompareDntsRequest(NspiCompareDNTsFlags flags, NspiState state, int ephemeralId1, int ephemeralId2, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.ephemeralId1 = ephemeralId1;
			this.ephemeralId2 = ephemeralId2;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001E7FA File Offset: 0x0001C9FA
		public NspiCompareDntsRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiCompareDNTsFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.ephemeralId1 = reader.ReadInt32();
			this.ephemeralId2 = reader.ReadInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001E83A File Offset: 0x0001CA3A
		public NspiCompareDNTsFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001E842 File Offset: 0x0001CA42
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0001E84A File Offset: 0x0001CA4A
		public int EphemeralId1
		{
			get
			{
				return this.ephemeralId1;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001E852 File Offset: 0x0001CA52
		public int EphemeralId2
		{
			get
			{
				return this.ephemeralId2;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001E85A File Offset: 0x0001CA5A
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteInt32(this.ephemeralId1);
			writer.WriteInt32(this.ephemeralId2);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000430 RID: 1072
		private readonly NspiCompareDNTsFlags flags;

		// Token: 0x04000431 RID: 1073
		private readonly NspiState state;

		// Token: 0x04000432 RID: 1074
		private readonly int ephemeralId1;

		// Token: 0x04000433 RID: 1075
		private readonly int ephemeralId2;
	}
}
