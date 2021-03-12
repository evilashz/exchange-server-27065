using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B9 RID: 1977
	internal sealed class InvalidLogonTypeException : ServicePermanentException
	{
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000CF663 File Offset: 0x000CD863
		public InvalidLogonTypeException() : base((CoreResources.IDs)3522975510U)
		{
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x000CF675 File Offset: 0x000CD875
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
