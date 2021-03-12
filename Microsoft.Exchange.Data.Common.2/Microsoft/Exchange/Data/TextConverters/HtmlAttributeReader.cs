using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000228 RID: 552
	public struct HtmlAttributeReader
	{
		// Token: 0x0600169F RID: 5791 RVA: 0x000B2136 File Offset: 0x000B0336
		internal HtmlAttributeReader(HtmlReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000B213F File Offset: 0x000B033F
		public bool ReadNext()
		{
			return this.reader.AttributeReader_ReadNextAttribute();
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000B214C File Offset: 0x000B034C
		public HtmlAttributeId Id
		{
			get
			{
				return this.reader.AttributeReader_GetCurrentAttributeId();
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000B2159 File Offset: 0x000B0359
		public bool NameIsLong
		{
			get
			{
				return this.reader.AttributeReader_CurrentAttributeNameIsLong();
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000B2166 File Offset: 0x000B0366
		public string ReadName()
		{
			return this.reader.AttributeReader_ReadCurrentAttributeName();
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000B2173 File Offset: 0x000B0373
		public int ReadName(char[] buffer, int offset, int count)
		{
			return this.reader.AttributeReader_ReadCurrentAttributeName(buffer, offset, count);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000B2183 File Offset: 0x000B0383
		internal void WriteNameTo(ITextSink sink)
		{
			this.reader.AttributeReader_WriteCurrentAttributeNameTo(sink);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000B2191 File Offset: 0x000B0391
		public bool HasValue
		{
			get
			{
				return this.reader.AttributeReader_CurrentAttributeHasValue();
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000B219E File Offset: 0x000B039E
		public bool ValueIsLong
		{
			get
			{
				return this.reader.AttributeReader_CurrentAttributeValueIsLong();
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000B21AB File Offset: 0x000B03AB
		public string ReadValue()
		{
			return this.reader.AttributeReader_ReadCurrentAttributeValue();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000B21B8 File Offset: 0x000B03B8
		public int ReadValue(char[] buffer, int offset, int count)
		{
			return this.reader.AttributeReader_ReadCurrentAttributeValue(buffer, offset, count);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000B21C8 File Offset: 0x000B03C8
		internal void WriteValueTo(ITextSink sink)
		{
			this.reader.AttributeReader_WriteCurrentAttributeValueTo(sink);
		}

		// Token: 0x04001971 RID: 6513
		private HtmlReader reader;
	}
}
