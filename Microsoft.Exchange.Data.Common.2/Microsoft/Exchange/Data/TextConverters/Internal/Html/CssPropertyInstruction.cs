using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E3 RID: 483
	internal struct CssPropertyInstruction
	{
		// Token: 0x060014EC RID: 5356 RVA: 0x000967E7 File Offset: 0x000949E7
		public CssPropertyInstruction(PropertyId propertyId, PropertyValueParsingMethod parsingMethod, MultiPropertyParsingMethod multiPropertyParsingMethod)
		{
			this.propertyId = propertyId;
			this.parsingMethod = parsingMethod;
			this.multiPropertyParsingMethod = multiPropertyParsingMethod;
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000967FE File Offset: 0x000949FE
		public PropertyId PropertyId
		{
			get
			{
				return this.propertyId;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00096806 File Offset: 0x00094A06
		public PropertyValueParsingMethod ParsingMethod
		{
			get
			{
				return this.parsingMethod;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0009680E File Offset: 0x00094A0E
		public MultiPropertyParsingMethod MultiPropertyParsingMethod
		{
			get
			{
				return this.multiPropertyParsingMethod;
			}
		}

		// Token: 0x0400145D RID: 5213
		private PropertyId propertyId;

		// Token: 0x0400145E RID: 5214
		private PropertyValueParsingMethod parsingMethod;

		// Token: 0x0400145F RID: 5215
		private MultiPropertyParsingMethod multiPropertyParsingMethod;
	}
}
