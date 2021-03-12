using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E8 RID: 488
	internal sealed class NspiUpdateStatRequest : MapiHttpRequest
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0001FF35 File Offset: 0x0001E135
		public NspiUpdateStatRequest(NspiUpdateStatFlags flags, NspiState state, bool deltaRequested, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.deltaRequested = deltaRequested;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001FF54 File Offset: 0x0001E154
		public NspiUpdateStatRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiUpdateStatFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.deltaRequested = reader.ReadBool();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001FF88 File Offset: 0x0001E188
		public NspiUpdateStatFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001FF90 File Offset: 0x0001E190
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0001FF98 File Offset: 0x0001E198
		public bool DeltaRequested
		{
			get
			{
				return this.deltaRequested;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteBool(this.deltaRequested);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000487 RID: 1159
		private readonly NspiUpdateStatFlags flags;

		// Token: 0x04000488 RID: 1160
		private readonly NspiState state;

		// Token: 0x04000489 RID: 1161
		private readonly bool deltaRequested;
	}
}
