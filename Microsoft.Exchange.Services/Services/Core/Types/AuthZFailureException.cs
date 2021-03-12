using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006DE RID: 1758
	internal sealed class AuthZFailureException : ServicePermanentException
	{
		// Token: 0x060035F3 RID: 13811 RVA: 0x000C1B19 File Offset: 0x000BFD19
		internal AuthZFailureException(AuthzException innerException) : base(CoreResources.IDs.ErrorImpersonationFailed, innerException)
		{
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000C1B2C File Offset: 0x000BFD2C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
