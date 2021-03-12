using System;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E08 RID: 3592
	internal interface IODataPropertyValueConverter
	{
		// Token: 0x06005D24 RID: 23844
		object FromODataPropertyValue(object odataPropertyValue);

		// Token: 0x06005D25 RID: 23845
		object ToODataPropertyValue(object rawValue);
	}
}
