using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007B4 RID: 1972
	internal sealed class InvalidImContactIdException : ServicePermanentException
	{
		// Token: 0x06003AB9 RID: 15033 RVA: 0x000CF497 File Offset: 0x000CD697
		public InvalidImContactIdException() : base((CoreResources.IDs)3485828594U)
		{
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000CF4A9 File Offset: 0x000CD6A9
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
