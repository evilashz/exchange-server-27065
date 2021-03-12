using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200079F RID: 1951
	internal sealed class ImpersonateUserDeniedException : ServicePermanentException
	{
		// Token: 0x06003A81 RID: 14977 RVA: 0x000CF0E7 File Offset: 0x000CD2E7
		public ImpersonateUserDeniedException() : base(CoreResources.IDs.ErrorImpersonateUserDenied)
		{
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000CF0F9 File Offset: 0x000CD2F9
		public ImpersonateUserDeniedException(Exception innerException) : base(CoreResources.IDs.ErrorImpersonateUserDenied, innerException)
		{
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003A83 RID: 14979 RVA: 0x000CF10C File Offset: 0x000CD30C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
