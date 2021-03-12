using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E2 RID: 482
	internal struct HtmlTagInstruction
	{
		// Token: 0x060014E7 RID: 5351 RVA: 0x000967A8 File Offset: 0x000949A8
		public HtmlTagInstruction(FormatContainerType containerType, int defaultStyle, int inheritanceMaskIndex, HtmlAttributeInstruction[] attributeInstructions)
		{
			this.containerType = containerType;
			this.defaultStyle = defaultStyle;
			this.inheritanceMaskIndex = inheritanceMaskIndex;
			this.attributeInstructions = attributeInstructions;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x000967C7 File Offset: 0x000949C7
		public FormatContainerType ContainerType
		{
			get
			{
				return this.containerType;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x000967CF File Offset: 0x000949CF
		public int DefaultStyle
		{
			get
			{
				return this.defaultStyle;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x000967D7 File Offset: 0x000949D7
		public int InheritanceMaskIndex
		{
			get
			{
				return this.inheritanceMaskIndex;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x000967DF File Offset: 0x000949DF
		public HtmlAttributeInstruction[] AttributeInstructions
		{
			get
			{
				return this.attributeInstructions;
			}
		}

		// Token: 0x04001459 RID: 5209
		private FormatContainerType containerType;

		// Token: 0x0400145A RID: 5210
		private int defaultStyle;

		// Token: 0x0400145B RID: 5211
		private int inheritanceMaskIndex;

		// Token: 0x0400145C RID: 5212
		private HtmlAttributeInstruction[] attributeInstructions;
	}
}
