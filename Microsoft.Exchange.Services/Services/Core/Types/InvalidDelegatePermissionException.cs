using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A8 RID: 1960
	internal class InvalidDelegatePermissionException : ServicePermanentException
	{
		// Token: 0x06003A98 RID: 15000 RVA: 0x000CF2E3 File Offset: 0x000CD4E3
		public InvalidDelegatePermissionException() : base((CoreResources.IDs)3537364541U)
		{
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000CF2F5 File Offset: 0x000CD4F5
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
