using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A7 RID: 2215
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TooManyKeywordsException : ServicePermanentException
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x000D9FEF File Offset: 0x000D81EF
		public TooManyKeywordsException(Exception innerException) : base((CoreResources.IDs)3032287327U, innerException)
		{
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000DA002 File Offset: 0x000D8202
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
