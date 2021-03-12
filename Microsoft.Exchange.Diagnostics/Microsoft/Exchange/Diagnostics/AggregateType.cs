using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200006A RID: 106
	// (Invoke) Token: 0x060001E6 RID: 486
	public delegate void AggregateType<TResult>(Type baseType, Type type, List<TResult> results) where TResult : class;
}
