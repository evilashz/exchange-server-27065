using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000682 RID: 1666
	internal interface IExpirationWindowProvider<T>
	{
		// Token: 0x06001E30 RID: 7728
		TimeSpan GetExpirationWindow(T value);
	}
}
