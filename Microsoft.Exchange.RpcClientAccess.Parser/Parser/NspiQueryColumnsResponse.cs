using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001DD RID: 477
	internal sealed class NspiQueryColumnsResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x0001F555 File Offset: 0x0001D755
		public NspiQueryColumnsResponse(uint returnCode, PropertyTag[] columns, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.columns = columns;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001F566 File Offset: 0x0001D766
		public NspiQueryColumnsResponse(Reader reader) : base(reader)
		{
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0001F583 File Offset: 0x0001D783
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001F58B File Offset: 0x0001D78B
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000465 RID: 1125
		private readonly PropertyTag[] columns;
	}
}
