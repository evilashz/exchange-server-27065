using System;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002D2 RID: 722
	// (Invoke) Token: 0x0600201B RID: 8219
	public delegate void ConfigurationUpdateHandler<TCache>(TCache update) where TCache : class;
}
