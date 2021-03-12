using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D7 RID: 471
	internal sealed class NspiGetTemplateInfoResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		public NspiGetTemplateInfoResponse(uint returnCode, uint codePage, PropertyValue[] propertyValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.codePage = codePage;
			this.propertyValues = propertyValues;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		public NspiGetTemplateInfoResponse(Reader reader) : base(reader)
		{
			this.codePage = reader.ReadUInt32();
			Encoding asciiEncoding;
			if (!String8Encodings.TryGetEncoding((int)this.codePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			this.propertyValues = reader.ReadNullableCountAndPropertyValueList(asciiEncoding, WireFormatStyle.Nspi);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001F242 File Offset: 0x0001D442
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0001F24A File Offset: 0x0001D44A
		public PropertyValue[] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001F254 File Offset: 0x0001D454
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			Encoding asciiEncoding;
			if (!String8Encodings.TryGetEncoding((int)this.codePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			writer.WriteUInt32(this.codePage);
			writer.WriteNullableCountAndPropertyValueList(this.propertyValues, asciiEncoding, WireFormatStyle.Nspi);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000459 RID: 1113
		private readonly uint codePage;

		// Token: 0x0400045A RID: 1114
		private readonly PropertyValue[] propertyValues;
	}
}
