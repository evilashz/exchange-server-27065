using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF7 RID: 3575
	internal static class ODataComplexValueExtensions
	{
		// Token: 0x06005C8F RID: 23695 RVA: 0x001208F0 File Offset: 0x0011EAF0
		public static T GetPropertyValue<T>(this ODataComplexValue odataComplexValue, string propertyName, T defaultValue = default(T))
		{
			ArgumentValidator.ThrowIfNull("odataComplexValue", odataComplexValue);
			ArgumentValidator.ThrowIfNullOrEmpty("propertyName", propertyName);
			ODataProperty odataProperty = odataComplexValue.Properties.SingleOrDefault((ODataProperty x) => x.Name.Equals(propertyName));
			if (odataProperty == null)
			{
				return defaultValue;
			}
			return (T)((object)odataProperty.Value);
		}
	}
}
