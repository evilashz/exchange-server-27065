using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000024 RID: 36
	internal interface ITokenBucketFactory
	{
		// Token: 0x060000F1 RID: 241
		ITokenBucket Create(Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> rechargeInterval);

		// Token: 0x060000F2 RID: 242
		ITokenBucket Create(ITokenBucket template, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> rechargeInterval);
	}
}
