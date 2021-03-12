using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007BF RID: 1983
	internal sealed class InvalidPercentCompleteValueException : ServicePermanentException
	{
		// Token: 0x06003AD6 RID: 15062 RVA: 0x000CF81D File Offset: 0x000CDA1D
		public InvalidPercentCompleteValueException() : base((CoreResources.IDs)3035123300U)
		{
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000CF82F File Offset: 0x000CDA2F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
