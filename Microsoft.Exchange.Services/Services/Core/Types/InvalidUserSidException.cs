using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007DA RID: 2010
	internal sealed class InvalidUserSidException : ServicePermanentException
	{
		// Token: 0x06003B28 RID: 15144 RVA: 0x000CFD4F File Offset: 0x000CDF4F
		public InvalidUserSidException() : base(CoreResources.IDs.ErrorInvalidUserSid)
		{
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000CFD61 File Offset: 0x000CDF61
		public InvalidUserSidException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidUserSid, innerException)
		{
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x000CFD74 File Offset: 0x000CDF74
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
