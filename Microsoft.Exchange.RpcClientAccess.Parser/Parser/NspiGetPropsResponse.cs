using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D3 RID: 467
	internal sealed class NspiGetPropsResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x0001EE98 File Offset: 0x0001D098
		public NspiGetPropsResponse(uint returnCode, uint codePage, PropertyValue[] propertyValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.codePage = codePage;
			this.propertyValues = propertyValues;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001EEB4 File Offset: 0x0001D0B4
		public NspiGetPropsResponse(Reader reader) : base(reader)
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

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001EEFE File Offset: 0x0001D0FE
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001EF06 File Offset: 0x0001D106
		public PropertyValue[] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001EF10 File Offset: 0x0001D110
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

		// Token: 0x0400044C RID: 1100
		private readonly uint codePage;

		// Token: 0x0400044D RID: 1101
		private readonly PropertyValue[] propertyValues;
	}
}
