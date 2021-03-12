using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C8 RID: 456
	internal sealed class NspiBindRequest : MapiHttpRequest
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0001E712 File Offset: 0x0001C912
		public NspiBindRequest(NspiBindFlags flags, NspiState state, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001E729 File Offset: 0x0001C929
		public NspiBindRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiBindFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001E751 File Offset: 0x0001C951
		public NspiBindFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0001E759 File Offset: 0x0001C959
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001E761 File Offset: 0x0001C961
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400042D RID: 1069
		private readonly NspiBindFlags flags;

		// Token: 0x0400042E RID: 1070
		private readonly NspiState state;
	}
}
