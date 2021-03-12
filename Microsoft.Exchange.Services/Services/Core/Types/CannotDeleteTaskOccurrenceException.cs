using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000714 RID: 1812
	internal sealed class CannotDeleteTaskOccurrenceException : ServicePermanentException
	{
		// Token: 0x06003730 RID: 14128 RVA: 0x000C5550 File Offset: 0x000C3750
		public CannotDeleteTaskOccurrenceException() : base((CoreResources.IDs)3049158008U)
		{
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000C5562 File Offset: 0x000C3762
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
