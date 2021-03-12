using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C8 RID: 1992
	internal class InvalidProxySecurityContextException : ServicePermanentException
	{
		// Token: 0x06003AEF RID: 15087 RVA: 0x000CF9D5 File Offset: 0x000CDBD5
		public InvalidProxySecurityContextException(Exception innerException) : base((CoreResources.IDs)3616451054U, innerException)
		{
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000CF9E8 File Offset: 0x000CDBE8
		public InvalidProxySecurityContextException() : base((CoreResources.IDs)3616451054U)
		{
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000CF9FA File Offset: 0x000CDBFA
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
