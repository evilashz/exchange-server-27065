using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D1 RID: 465
	internal sealed class NspiGetPropListResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0001EDAB File Offset: 0x0001CFAB
		public NspiGetPropListResponse(uint returnCode, PropertyTag[] propertyTags, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.propertyTags = propertyTags;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		public NspiGetPropListResponse(Reader reader) : base(reader)
		{
			this.propertyTags = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0001EDD9 File Offset: 0x0001CFD9
		public PropertyTag[] PropertyTags
		{
			get
			{
				return this.propertyTags;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001EDE1 File Offset: 0x0001CFE1
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteNullableCountAndPropertyTagArray(this.propertyTags, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000448 RID: 1096
		private readonly PropertyTag[] propertyTags;
	}
}
