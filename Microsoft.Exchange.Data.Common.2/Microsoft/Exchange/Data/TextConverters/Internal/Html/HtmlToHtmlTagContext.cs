using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001DE RID: 478
	internal class HtmlToHtmlTagContext : HtmlTagContext
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0009668C File Offset: 0x0009488C
		public HtmlToHtmlTagContext(HtmlToHtmlConverter converter)
		{
			this.converter = converter;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0009669C File Offset: 0x0009489C
		internal override string GetTagNameImpl()
		{
			if (base.TagNameIndex > HtmlNameIndex.Unknown)
			{
				if (!base.TagParts.Begin)
				{
					return string.Empty;
				}
				return HtmlNameData.Names[(int)base.TagNameIndex].Name;
			}
			else
			{
				if (base.TagParts.Name)
				{
					return this.converter.InternalToken.Name.GetString(int.MaxValue);
				}
				return string.Empty;
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00096711 File Offset: 0x00094911
		internal override HtmlAttributeId GetAttributeNameIdImpl(int attributeIndex)
		{
			return this.converter.GetAttributeNameId(attributeIndex);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0009671F File Offset: 0x0009491F
		internal override HtmlAttributeParts GetAttributePartsImpl(int attributeIndex)
		{
			return this.converter.GetAttributeParts(attributeIndex);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0009672D File Offset: 0x0009492D
		internal override string GetAttributeNameImpl(int attributeIndex)
		{
			return this.converter.GetAttributeName(attributeIndex);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0009673B File Offset: 0x0009493B
		internal override string GetAttributeValueImpl(int attributeIndex)
		{
			return this.converter.GetAttributeValue(attributeIndex);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00096749 File Offset: 0x00094949
		internal override int ReadAttributeValueImpl(int attributeIndex, char[] buffer, int offset, int count)
		{
			return this.converter.ReadAttributeValue(attributeIndex, buffer, offset, count);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0009675B File Offset: 0x0009495B
		internal override void WriteTagImpl(bool copyTagAttributes)
		{
			this.converter.WriteTag(copyTagAttributes);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00096769 File Offset: 0x00094969
		internal override void WriteAttributeImpl(int attributeIndex, bool writeName, bool writeValue)
		{
			this.converter.WriteAttribute(attributeIndex, writeName, writeValue);
		}

		// Token: 0x04001455 RID: 5205
		private HtmlToHtmlConverter converter;
	}
}
