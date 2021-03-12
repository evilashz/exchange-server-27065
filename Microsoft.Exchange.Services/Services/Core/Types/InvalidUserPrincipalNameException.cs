using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D9 RID: 2009
	internal sealed class InvalidUserPrincipalNameException : ServicePermanentException
	{
		// Token: 0x06003B25 RID: 15141 RVA: 0x000CFD23 File Offset: 0x000CDF23
		public InvalidUserPrincipalNameException() : base(CoreResources.IDs.ErrorInvalidUserPrincipalName)
		{
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000CFD35 File Offset: 0x000CDF35
		public InvalidUserPrincipalNameException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidUserPrincipalName, innerException)
		{
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000CFD48 File Offset: 0x000CDF48
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
