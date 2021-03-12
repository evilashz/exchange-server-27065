using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E3 RID: 483
	internal sealed class NspiResortRestrictionResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x0001FB7A File Offset: 0x0001DD7A
		public NspiResortRestrictionResponse(uint returnCode, NspiState state, int[] ephemeralIds, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.state = state;
			this.ephemeralIds = ephemeralIds;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001FB93 File Offset: 0x0001DD93
		public NspiResortRestrictionResponse(Reader reader) : base(reader)
		{
			this.state = reader.ReadNspiState();
			this.ephemeralIds = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0001FBC4 File Offset: 0x0001DDC4
		public int[] EphemeralIds
		{
			get
			{
				return this.ephemeralIds;
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteNspiState(this.state);
			writer.WriteNullableSizeAndIntegerArray(this.ephemeralIds, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400047B RID: 1147
		private readonly NspiState state;

		// Token: 0x0400047C RID: 1148
		private readonly int[] ephemeralIds;
	}
}
