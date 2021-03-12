using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E74 RID: 3700
	internal class LocationODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x0600604B RID: 24651 RVA: 0x0012CB58 File Offset: 0x0012AD58
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			if (odataPropertyValue == null)
			{
				return null;
			}
			ODataComplexValue odataComplexValue = (ODataComplexValue)odataPropertyValue;
			return new Location
			{
				DisplayName = odataComplexValue.GetPropertyValue("DisplayName", null)
			};
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0012CB8C File Offset: 0x0012AD8C
		public object ToODataPropertyValue(object rawValue)
		{
			if (rawValue == null)
			{
				return null;
			}
			Location location = (Location)rawValue;
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = typeof(Location).FullName;
			List<ODataProperty> properties = new List<ODataProperty>
			{
				new ODataProperty
				{
					Name = "DisplayName",
					Value = location.DisplayName
				}
			};
			odataComplexValue.Properties = properties;
			return odataComplexValue;
		}
	}
}
