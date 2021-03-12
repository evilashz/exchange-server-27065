using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000123 RID: 291
	internal interface IMultivaluedProperty<T> : IProperty, IEnumerable<T>, IEnumerable
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000F3E RID: 3902
		int Count { get; }
	}
}
