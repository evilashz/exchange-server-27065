using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001D3 RID: 467
	internal class HtmlFormatOutputCallbackContext : HtmlTagContext
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x0009286A File Offset: 0x00090A6A
		public HtmlFormatOutputCallbackContext(HtmlFormatOutput formatOutput)
		{
			this.formatOutput = formatOutput;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00092886 File Offset: 0x00090A86
		public new void InitializeTag(bool isEndTag, HtmlNameIndex tagNameIndex, bool tagDropped)
		{
			base.InitializeTag(isEndTag, tagNameIndex, tagDropped);
			this.countAttributes = 0;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00092898 File Offset: 0x00090A98
		public void InitializeFragment(bool isEmptyElementTag)
		{
			base.InitializeFragment(isEmptyElementTag, this.countAttributes, (this.countAttributes == 0) ? HtmlFormatOutputCallbackContext.CompleteTagWithoutAttributesParts : HtmlFormatOutputCallbackContext.CompleteTagWithAttributesParts);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000928BB File Offset: 0x00090ABB
		internal void Reset()
		{
			this.countAttributes = 0;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x000928C4 File Offset: 0x00090AC4
		internal void AddAttribute(HtmlNameIndex nameIndex, string value)
		{
			this.attributes[this.countAttributes].NameIndex = nameIndex;
			this.attributes[this.countAttributes].Value = value;
			this.attributes[this.countAttributes].ReadIndex = 0;
			this.countAttributes++;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00092924 File Offset: 0x00090B24
		internal override string GetTagNameImpl()
		{
			return HtmlNameData.Names[(int)base.TagNameIndex].Name;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0009293B File Offset: 0x00090B3B
		internal override HtmlAttributeId GetAttributeNameIdImpl(int attributeIndex)
		{
			return HtmlNameData.Names[(int)this.attributes[attributeIndex].NameIndex].PublicAttributeId;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0009295D File Offset: 0x00090B5D
		internal override HtmlAttributeParts GetAttributePartsImpl(int attributeIndex)
		{
			return HtmlFormatOutputCallbackContext.CompleteAttributeParts;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00092964 File Offset: 0x00090B64
		internal override string GetAttributeNameImpl(int attributeIndex)
		{
			return HtmlNameData.Names[(int)this.attributes[attributeIndex].NameIndex].Name;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00092986 File Offset: 0x00090B86
		internal override string GetAttributeValueImpl(int attributeIndex)
		{
			return this.attributes[attributeIndex].Value;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0009299C File Offset: 0x00090B9C
		internal override int ReadAttributeValueImpl(int attributeIndex, char[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this.attributes[attributeIndex].Value.Length - this.attributes[attributeIndex].ReadIndex);
			if (num != 0)
			{
				this.attributes[attributeIndex].Value.CopyTo(this.attributes[attributeIndex].ReadIndex, buffer, offset, num);
				HtmlFormatOutputCallbackContext.AttributeDescriptor[] array = this.attributes;
				array[attributeIndex].ReadIndex = array[attributeIndex].ReadIndex + num;
			}
			return num;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00092A20 File Offset: 0x00090C20
		internal override void WriteTagImpl(bool copyTagAttributes)
		{
			this.formatOutput.Writer.WriteTagBegin(base.TagNameIndex, null, base.IsEndTag, false, false);
			if (copyTagAttributes)
			{
				for (int i = 0; i < this.countAttributes; i++)
				{
					this.WriteAttributeImpl(i, true, true);
				}
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00092A6C File Offset: 0x00090C6C
		internal override void WriteAttributeImpl(int attributeIndex, bool writeName, bool writeValue)
		{
			if (writeName)
			{
				this.formatOutput.Writer.WriteAttributeName(this.attributes[attributeIndex].NameIndex);
			}
			if (writeValue)
			{
				this.formatOutput.Writer.WriteAttributeValue(this.attributes[attributeIndex].Value);
			}
		}

		// Token: 0x040013EE RID: 5102
		private const int MaxCallbackAttributes = 10;

		// Token: 0x040013EF RID: 5103
		private static readonly HtmlAttributeParts CompleteAttributeParts = new HtmlAttributeParts(HtmlToken.AttrPartMajor.Complete, HtmlToken.AttrPartMinor.CompleteNameWithCompleteValue);

		// Token: 0x040013F0 RID: 5104
		private static readonly HtmlTagParts CompleteTagWithAttributesParts = new HtmlTagParts(HtmlToken.TagPartMajor.Complete, HtmlToken.TagPartMinor.CompleteNameWithAttributes);

		// Token: 0x040013F1 RID: 5105
		private static readonly HtmlTagParts CompleteTagWithoutAttributesParts = new HtmlTagParts(HtmlToken.TagPartMajor.Complete, HtmlToken.TagPartMinor.CompleteName);

		// Token: 0x040013F2 RID: 5106
		private HtmlFormatOutput formatOutput;

		// Token: 0x040013F3 RID: 5107
		private int countAttributes;

		// Token: 0x040013F4 RID: 5108
		private HtmlFormatOutputCallbackContext.AttributeDescriptor[] attributes = new HtmlFormatOutputCallbackContext.AttributeDescriptor[10];

		// Token: 0x020001D4 RID: 468
		private struct AttributeDescriptor
		{
			// Token: 0x040013F5 RID: 5109
			public HtmlNameIndex NameIndex;

			// Token: 0x040013F6 RID: 5110
			public string Value;

			// Token: 0x040013F7 RID: 5111
			public int ReadIndex;
		}
	}
}
