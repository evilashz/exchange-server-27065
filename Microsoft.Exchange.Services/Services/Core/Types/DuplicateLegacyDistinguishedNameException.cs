using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000757 RID: 1879
	internal sealed class DuplicateLegacyDistinguishedNameException : ServicePermanentException
	{
		// Token: 0x0600383C RID: 14396 RVA: 0x000C71A3 File Offset: 0x000C53A3
		public DuplicateLegacyDistinguishedNameException(string duplicateLegacyDN, Exception innerException) : base((CoreResources.IDs)3584287689U, CoreResources.ErrorDuplicateLegacyDistinguishedNameFound(duplicateLegacyDN), innerException)
		{
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000C71BC File Offset: 0x000C53BC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
