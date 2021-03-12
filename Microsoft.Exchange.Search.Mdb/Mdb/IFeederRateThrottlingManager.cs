using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000011 RID: 17
	internal interface IFeederRateThrottlingManager
	{
		// Token: 0x06000058 RID: 88
		double ThrottlingRateContinue(double currentRate);

		// Token: 0x06000059 RID: 89
		double ThrottlingRateStart();
	}
}
