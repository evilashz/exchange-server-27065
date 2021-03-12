using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007DC RID: 2012
	internal sealed class InvalidWatermarkException : ServicePermanentException
	{
		// Token: 0x06003B31 RID: 15153 RVA: 0x000CFE14 File Offset: 0x000CE014
		public InvalidWatermarkException() : base((CoreResources.IDs)3312780993U)
		{
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000CFE26 File Offset: 0x000CE026
		public InvalidWatermarkException(Exception innerException) : base((CoreResources.IDs)3312780993U, innerException)
		{
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003B33 RID: 15155 RVA: 0x000CFE39 File Offset: 0x000CE039
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
