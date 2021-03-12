using System;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000406 RID: 1030
	internal interface IServiceLocator
	{
		// Token: 0x0600242B RID: 9259
		TServiceType GetService<TServiceType>();
	}
}
