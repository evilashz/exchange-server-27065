using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001CD RID: 461
	internal sealed class NspiDnToEphResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x0001E962 File Offset: 0x0001CB62
		public NspiDnToEphResponse(uint returnCode, int[] ephemeralIds, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.ephemeralIds = ephemeralIds;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001E973 File Offset: 0x0001CB73
		public NspiDnToEphResponse(Reader reader) : base(reader)
		{
			this.ephemeralIds = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0001E990 File Offset: 0x0001CB90
		public int[] EphemeralIds
		{
			get
			{
				return this.ephemeralIds;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001E998 File Offset: 0x0001CB98
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteNullableSizeAndIntegerArray(this.ephemeralIds, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000437 RID: 1079
		private readonly int[] ephemeralIds;
	}
}
