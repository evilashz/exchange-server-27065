using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007A9 RID: 1961
	internal class InvalidDelegateUserIdException : ServicePermanentException
	{
		// Token: 0x06003A9A RID: 15002 RVA: 0x000CF2FC File Offset: 0x000CD4FC
		public InvalidDelegateUserIdException() : base(ResponseCodeType.ErrorInvalidDelegateUserId, CoreResources.ErrorInvalidDelegateUserId(string.Empty))
		{
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000CF313 File Offset: 0x000CD513
		public InvalidDelegateUserIdException(string message) : base(ResponseCodeType.ErrorInvalidDelegateUserId, CoreResources.ErrorInvalidDelegateUserId(message))
		{
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000CF326 File Offset: 0x000CD526
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
