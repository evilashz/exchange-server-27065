using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E2 RID: 482
	internal sealed class NspiResortRestrictionRequest : MapiHttpRequest
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
		public NspiResortRestrictionRequest(NspiResortRestrictionFlags flags, NspiState state, int[] ephemeralIds, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.ephemeralIds = ephemeralIds;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001FAFF File Offset: 0x0001DCFF
		public NspiResortRestrictionRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiResortRestrictionFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.ephemeralIds = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001FB34 File Offset: 0x0001DD34
		public NspiResortRestrictionFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001FB44 File Offset: 0x0001DD44
		public int[] EphemeralIds
		{
			get
			{
				return this.ephemeralIds;
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001FB4C File Offset: 0x0001DD4C
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteNullableSizeAndIntegerArray(this.ephemeralIds, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000478 RID: 1144
		private readonly NspiResortRestrictionFlags flags;

		// Token: 0x04000479 RID: 1145
		private readonly NspiState state;

		// Token: 0x0400047A RID: 1146
		private readonly int[] ephemeralIds;
	}
}
