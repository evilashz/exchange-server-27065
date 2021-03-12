using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002B8 RID: 696
	internal struct StyleBuilder
	{
		// Token: 0x06001BB0 RID: 7088 RVA: 0x000D4F0C File Offset: 0x000D310C
		internal StyleBuilder(FormatConverter converter, int handle)
		{
			this.converter = converter;
			this.handle = handle;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000D4F1C File Offset: 0x000D311C
		public void SetProperty(PropertyId propertyId, PropertyValue value)
		{
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, value);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000D4F34 File Offset: 0x000D3134
		public void SetProperties(Property[] properties, int propertyCount)
		{
			for (int i = 0; i < propertyCount; i++)
			{
				this.SetProperty(properties[i].Id, properties[i].Value);
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000D4F6B File Offset: 0x000D316B
		public void SetStringProperty(PropertyId propertyId, StringValue value)
		{
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, value.PropertyValue);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000D4F88 File Offset: 0x000D3188
		public void SetStringProperty(PropertyId propertyId, string value)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, value);
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, stringValue.PropertyValue);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000D4FBC File Offset: 0x000D31BC
		public void SetStringProperty(PropertyId propertyId, BufferString value)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, value);
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, stringValue.PropertyValue);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000D4FF0 File Offset: 0x000D31F0
		public void SetStringProperty(PropertyId propertyId, char[] buffer, int offset, int count)
		{
			StringValue stringValue = this.converter.RegisterStringValue(false, new BufferString(buffer, offset, count));
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, stringValue.PropertyValue);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000D502C File Offset: 0x000D322C
		public void SetMultiValueProperty(PropertyId propertyId, MultiValue value)
		{
			value.AddRef();
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, value.PropertyValue);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000D5050 File Offset: 0x000D3250
		public void SetMultiValueProperty(PropertyId propertyId, out MultiValueBuilder multiValueBuilder)
		{
			MultiValue multiValue = this.converter.RegisterMultiValue(false, out multiValueBuilder);
			this.converter.StyleBuildHelper.SetProperty(0, propertyId, multiValue.PropertyValue);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000D5084 File Offset: 0x000D3284
		public void Flush()
		{
			this.converter.StyleBuildHelper.GetPropertyList(out this.converter.Store.Styles.Plane(this.handle)[this.converter.Store.Styles.Index(this.handle)].PropertyList, out this.converter.Store.Styles.Plane(this.handle)[this.converter.Store.Styles.Index(this.handle)].FlagProperties, out this.converter.Store.Styles.Plane(this.handle)[this.converter.Store.Styles.Index(this.handle)].PropertyMask);
		}

		// Token: 0x0400211C RID: 8476
		private FormatConverter converter;

		// Token: 0x0400211D RID: 8477
		private int handle;
	}
}
