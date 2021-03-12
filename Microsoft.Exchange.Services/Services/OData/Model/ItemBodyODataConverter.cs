using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E71 RID: 3697
	internal class ItemBodyODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x06006041 RID: 24641 RVA: 0x0012C988 File Offset: 0x0012AB88
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			if (odataPropertyValue == null)
			{
				return null;
			}
			ODataComplexValue odataComplexValue = (ODataComplexValue)odataPropertyValue;
			return new ItemBody
			{
				ContentType = EnumConverter.FromODataEnumValue<BodyType>(odataComplexValue.GetPropertyValue("ContentType", null)),
				Content = odataComplexValue.GetPropertyValue("Content", null)
			};
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0012C9D4 File Offset: 0x0012ABD4
		public object ToODataPropertyValue(object rawValue)
		{
			if (rawValue == null)
			{
				return null;
			}
			ItemBody itemBody = (ItemBody)rawValue;
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = itemBody.GetType().FullName;
			List<ODataProperty> properties = new List<ODataProperty>
			{
				new ODataProperty
				{
					Name = "ContentType",
					Value = EnumConverter.ToODataEnumValue(itemBody.ContentType)
				},
				new ODataProperty
				{
					Name = "Content",
					Value = itemBody.Content
				}
			};
			odataComplexValue.Properties = properties;
			return odataComplexValue;
		}
	}
}
