using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006AB RID: 1707
	[Serializable]
	internal sealed class AffectedTaskOccurrencesRequiredException : ServicePermanentException
	{
		// Token: 0x060034B6 RID: 13494 RVA: 0x000BE09F File Offset: 0x000BC29F
		public AffectedTaskOccurrencesRequiredException() : base((CoreResources.IDs)2684918840U)
		{
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000BE0B1 File Offset: 0x000BC2B1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
