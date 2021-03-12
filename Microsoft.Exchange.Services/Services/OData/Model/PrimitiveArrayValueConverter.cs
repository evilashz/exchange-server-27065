using System;
using System.Collections;
using System.Linq;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E9B RID: 3739
	internal class PrimitiveArrayValueConverter<T> : IODataPropertyValueConverter
	{
		// Token: 0x06006154 RID: 24916 RVA: 0x0012F7DC File Offset: 0x0012D9DC
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			ODataCollectionValue odataCollectionValue = odataPropertyValue as ODataCollectionValue;
			return (odataCollectionValue != null) ? odataCollectionValue.Items.Cast<T>().ToArray<T>() : null;
		}

		// Token: 0x06006155 RID: 24917 RVA: 0x0012F80C File Offset: 0x0012DA0C
		public object ToODataPropertyValue(object rawValue)
		{
			T[] array = (T[])rawValue;
			return new ODataCollectionValue
			{
				TypeName = typeof(T).MakeODataCollectionTypeName(),
				Items = (IEnumerable)rawValue
			};
		}
	}
}
