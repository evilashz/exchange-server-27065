using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200022F RID: 559
	public struct HtmlTagContextAttribute
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x000B4267 File Offset: 0x000B2467
		internal HtmlTagContextAttribute(HtmlTagContext tagContext, int attributeIndexAndCookie)
		{
			this.tagContext = tagContext;
			this.attributeIndexAndCookie = attributeIndexAndCookie;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x000B4277 File Offset: 0x000B2477
		public bool IsNull
		{
			get
			{
				return this.tagContext == null;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x000B4282 File Offset: 0x000B2482
		public HtmlAttributeId Id
		{
			get
			{
				this.AssertValid();
				return this.tagContext.GetAttributeNameIdImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie));
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x000B42A0 File Offset: 0x000B24A0
		public string Name
		{
			get
			{
				this.AssertValid();
				return this.tagContext.GetAttributeNameImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie));
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x000B42BE File Offset: 0x000B24BE
		public string Value
		{
			get
			{
				this.AssertValid();
				return this.tagContext.GetAttributeValueImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie));
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x000B42DC File Offset: 0x000B24DC
		internal HtmlAttributeParts Parts
		{
			get
			{
				this.AssertValid();
				return this.tagContext.GetAttributePartsImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie));
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000B42FA File Offset: 0x000B24FA
		public int ReadValue(char[] buffer, int offset, int count)
		{
			this.AssertValid();
			return this.tagContext.ReadAttributeValueImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie), buffer, offset, count);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000B431B File Offset: 0x000B251B
		public void Write()
		{
			this.AssertValid();
			this.tagContext.WriteAttributeImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie), true, true);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000B433B File Offset: 0x000B253B
		public void WriteName()
		{
			this.AssertValid();
			this.tagContext.WriteAttributeImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie), true, false);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000B435B File Offset: 0x000B255B
		public void WriteValue()
		{
			this.AssertValid();
			this.tagContext.WriteAttributeImpl(HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie), false, true);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000B437C File Offset: 0x000B257C
		public override string ToString()
		{
			if (this.tagContext != null)
			{
				return HtmlTagContext.ExtractIndex(this.attributeIndexAndCookie).ToString();
			}
			return "null";
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000B43AA File Offset: 0x000B25AA
		private void AssertValid()
		{
			if (this.tagContext == null)
			{
				throw new InvalidOperationException(TextConvertersStrings.AttributeNotInitialized);
			}
			this.tagContext.AssertAttributeValid(this.attributeIndexAndCookie);
		}

		// Token: 0x04001A73 RID: 6771
		public static readonly HtmlTagContextAttribute Null = default(HtmlTagContextAttribute);

		// Token: 0x04001A74 RID: 6772
		private HtmlTagContext tagContext;

		// Token: 0x04001A75 RID: 6773
		private int attributeIndexAndCookie;
	}
}
