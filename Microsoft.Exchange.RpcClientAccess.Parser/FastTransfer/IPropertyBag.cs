using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000199 RID: 409
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPropertyBag
	{
		// Token: 0x0600080C RID: 2060
		AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag);

		// Token: 0x0600080D RID: 2061
		IEnumerable<AnnotatedPropertyValue> GetAnnotatedProperties();

		// Token: 0x0600080E RID: 2062
		void SetProperty(PropertyValue propertyValue);

		// Token: 0x0600080F RID: 2063
		void Delete(PropertyTag property);

		// Token: 0x06000810 RID: 2064
		Stream GetPropertyStream(PropertyTag property);

		// Token: 0x06000811 RID: 2065
		Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate);

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000812 RID: 2066
		ISession Session { get; }
	}
}
