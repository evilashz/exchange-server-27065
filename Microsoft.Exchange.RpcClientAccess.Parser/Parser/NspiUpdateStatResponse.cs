using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E9 RID: 489
	internal sealed class NspiUpdateStatResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x0001FFCD File Offset: 0x0001E1CD
		public NspiUpdateStatResponse(uint returnCode, NspiState state, int? delta, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.state = state;
			this.delta = delta;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001FFE6 File Offset: 0x0001E1E6
		public NspiUpdateStatResponse(Reader reader) : base(reader)
		{
			this.state = reader.ReadNspiState();
			this.delta = reader.ReadNullableInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002000E File Offset: 0x0001E20E
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00020016 File Offset: 0x0001E216
		public int? Delta
		{
			get
			{
				return this.delta;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002001E File Offset: 0x0001E21E
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteNspiState(this.state);
			writer.WriteNullableInt32(this.delta);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400048A RID: 1162
		private readonly NspiState state;

		// Token: 0x0400048B RID: 1163
		private readonly int? delta;
	}
}
