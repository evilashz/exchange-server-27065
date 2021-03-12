using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000726 RID: 1830
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ClientIntentNotFoundException : ServicePermanentException
	{
		// Token: 0x06003784 RID: 14212 RVA: 0x000C595B File Offset: 0x000C3B5B
		public ClientIntentNotFoundException() : base((CoreResources.IDs)2851949310U)
		{
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000C596D File Offset: 0x000C3B6D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
