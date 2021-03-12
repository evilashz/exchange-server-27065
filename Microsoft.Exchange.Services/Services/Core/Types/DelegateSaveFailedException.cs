using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200073D RID: 1853
	internal class DelegateSaveFailedException : ServicePermanentException
	{
		// Token: 0x060037C8 RID: 14280 RVA: 0x000C5D05 File Offset: 0x000C3F05
		public DelegateSaveFailedException(CoreResources.IDs errorCode) : base(errorCode)
		{
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000C5D13 File Offset: 0x000C3F13
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
