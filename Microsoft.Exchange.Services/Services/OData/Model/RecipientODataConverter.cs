using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E77 RID: 3703
	internal class RecipientODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x06006053 RID: 24659 RVA: 0x0012CCE3 File Offset: 0x0012AEE3
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			return RecipientODataConverter.ODataValueToRecipient((ODataComplexValue)odataPropertyValue);
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x0012CCF0 File Offset: 0x0012AEF0
		public object ToODataPropertyValue(object rawValue)
		{
			return RecipientODataConverter.RecipientToODataValue((Recipient)rawValue);
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x0012CD00 File Offset: 0x0012AF00
		internal static ODataValue RecipientToODataValue(Recipient recipient)
		{
			if (recipient == null)
			{
				return null;
			}
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = typeof(Recipient).FullName;
			List<ODataProperty> properties = new List<ODataProperty>
			{
				new ODataProperty
				{
					Name = "Name",
					Value = recipient.Name
				},
				new ODataProperty
				{
					Name = "Address",
					Value = recipient.Address
				}
			};
			odataComplexValue.Properties = properties;
			return odataComplexValue;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x0012CD8C File Offset: 0x0012AF8C
		internal static Recipient ODataValueToRecipient(ODataComplexValue complexValue)
		{
			if (complexValue == null)
			{
				return null;
			}
			return new Recipient
			{
				Name = complexValue.GetPropertyValue("Name", null),
				Address = complexValue.GetPropertyValue("Address", null)
			};
		}
	}
}
