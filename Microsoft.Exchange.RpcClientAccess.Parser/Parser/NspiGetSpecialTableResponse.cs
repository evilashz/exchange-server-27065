using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D5 RID: 469
	internal sealed class NspiGetSpecialTableResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x0001EFF2 File Offset: 0x0001D1F2
		public NspiGetSpecialTableResponse(uint returnCode, uint codePage, uint? version, PropertyValue[][] propertyValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.codePage = codePage;
			this.version = version;
			this.propertyValues = propertyValues;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001F014 File Offset: 0x0001D214
		public NspiGetSpecialTableResponse(Reader reader) : base(reader)
		{
			this.codePage = reader.ReadUInt32();
			this.version = reader.ReadNullableUInt32();
			Encoding asciiEncoding;
			if (!String8Encodings.TryGetEncoding((int)this.codePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			this.propertyValues = reader.ReadNullableCountAndPropertyValueListList(asciiEncoding, WireFormatStyle.Nspi);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001F06A File Offset: 0x0001D26A
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001F072 File Offset: 0x0001D272
		public uint? Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001F07A File Offset: 0x0001D27A
		public PropertyValue[][] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001F084 File Offset: 0x0001D284
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			Encoding asciiEncoding;
			if (!String8Encodings.TryGetEncoding((int)this.codePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			writer.WriteUInt32(this.codePage);
			writer.WriteNullableUInt32(this.version);
			writer.WriteNullableCountAndPropertyValueListList(this.propertyValues, asciiEncoding, WireFormatStyle.Nspi);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000451 RID: 1105
		private readonly uint codePage;

		// Token: 0x04000452 RID: 1106
		private readonly uint? version;

		// Token: 0x04000453 RID: 1107
		private readonly PropertyValue[][] propertyValues;
	}
}
