using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Dkm;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleConnectExchangeGroupKeyFactory
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x0003D0F1 File Offset: 0x0003B2F1
		private PeopleConnectExchangeGroupKeyFactory()
		{
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003D0F9 File Offset: 0x0003B2F9
		public static IExchangeGroupKey Create()
		{
			if (PeopleConnectRegistryReader.Read().DogfoodInEnterprise)
			{
				return new NullExchangeGroupKey();
			}
			return new ExchangeGroupKey(null, "Microsoft Exchange DKM");
		}
	}
}
