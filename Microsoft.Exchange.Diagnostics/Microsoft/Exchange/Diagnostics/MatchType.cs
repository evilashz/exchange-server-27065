using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000069 RID: 105
	// (Invoke) Token: 0x060001E2 RID: 482
	public delegate TResult MatchType<TResult, TParam>(Type baseType, Type type, TParam param) where TResult : class;
}
