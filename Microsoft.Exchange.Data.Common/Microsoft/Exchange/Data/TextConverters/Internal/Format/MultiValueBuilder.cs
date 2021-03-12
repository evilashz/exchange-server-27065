using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002B9 RID: 697
	internal struct MultiValueBuilder
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x000D5161 File Offset: 0x000D3361
		internal MultiValueBuilder(FormatConverter converter, int handle)
		{
			this.converter = converter;
			this.handle = handle;
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x000D5171 File Offset: 0x000D3371
		public int Count
		{
			get
			{
				return this.converter.MultiValueBuildHelper.Count;
			}
		}

		// Token: 0x17000726 RID: 1830
		public PropertyValue this[int i]
		{
			get
			{
				return this.converter.MultiValueBuildHelper[i];
			}
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000D5196 File Offset: 0x000D3396
		public void AddValue(PropertyValue value)
		{
			this.converter.MultiValueBuildHelper.AddValue(value);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000D51A9 File Offset: 0x000D33A9
		public void AddStringValue(StringValue value)
		{
			this.converter.MultiValueBuildHelper.AddValue(value.PropertyValue);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000D51C4 File Offset: 0x000D33C4
		public void AddStringValue(string value)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, value);
			this.converter.MultiValueBuildHelper.AddValue(stringValue.PropertyValue);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000D51F8 File Offset: 0x000D33F8
		public void AddStringValue(char[] buffer, int offset, int count)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, new BufferString(buffer, offset, count));
			this.converter.MultiValueBuildHelper.AddValue(stringValue.PropertyValue);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000D5234 File Offset: 0x000D3434
		public void Flush()
		{
			this.converter.Store.MultiValues.Plane(this.handle)[this.converter.Store.MultiValues.Index(this.handle)].Values = this.converter.MultiValueBuildHelper.GetValues();
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000D5291 File Offset: 0x000D3491
		public void Cancel()
		{
			this.converter.MultiValueBuildHelper.Cancel();
		}

		// Token: 0x0400211E RID: 8478
		private FormatConverter converter;

		// Token: 0x0400211F RID: 8479
		private int handle;
	}
}
