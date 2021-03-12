using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000036 RID: 54
	internal interface IServiceClient<FunctionalInterfaceType> : IDisposable
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B4 RID: 436
		Uri ServiceEndpoint { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B5 RID: 437
		FunctionalInterfaceType FunctionalInterface { get; }

		// Token: 0x060001B6 RID: 438
		bool Connect();
	}
}
