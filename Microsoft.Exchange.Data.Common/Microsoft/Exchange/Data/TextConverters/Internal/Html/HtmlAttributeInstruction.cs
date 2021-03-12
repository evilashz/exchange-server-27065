using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E1 RID: 481
	internal struct HtmlAttributeInstruction
	{
		// Token: 0x060014E3 RID: 5347 RVA: 0x00096779 File Offset: 0x00094979
		public HtmlAttributeInstruction(HtmlNameIndex attributeNameId, PropertyId propertyId, PropertyValueParsingMethod parsingMethod)
		{
			this.attributeNameId = attributeNameId;
			this.propertyId = propertyId;
			this.parsingMethod = parsingMethod;
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00096790 File Offset: 0x00094990
		public HtmlNameIndex AttributeNameId
		{
			get
			{
				return this.attributeNameId;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00096798 File Offset: 0x00094998
		public PropertyId PropertyId
		{
			get
			{
				return this.propertyId;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000967A0 File Offset: 0x000949A0
		public PropertyValueParsingMethod ParsingMethod
		{
			get
			{
				return this.parsingMethod;
			}
		}

		// Token: 0x04001456 RID: 5206
		private HtmlNameIndex attributeNameId;

		// Token: 0x04001457 RID: 5207
		private PropertyId propertyId;

		// Token: 0x04001458 RID: 5208
		private PropertyValueParsingMethod parsingMethod;
	}
}
