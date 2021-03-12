using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007CB RID: 1995
	[Serializable]
	internal sealed class InvalidReferenceItemException : ServicePermanentException
	{
		// Token: 0x06003AF7 RID: 15095 RVA: 0x000CFA46 File Offset: 0x000CDC46
		public InvalidReferenceItemException() : base((CoreResources.IDs)2519519915U)
		{
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000CFA58 File Offset: 0x000CDC58
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
