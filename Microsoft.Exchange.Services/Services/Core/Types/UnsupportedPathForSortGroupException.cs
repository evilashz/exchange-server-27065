using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008B3 RID: 2227
	internal sealed class UnsupportedPathForSortGroupException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003F42 RID: 16194 RVA: 0x000DB29F File Offset: 0x000D949F
		public UnsupportedPathForSortGroupException(PropertyPath offendingPath) : base(CoreResources.IDs.ErrorUnsupportedPathForSortGroup, offendingPath)
		{
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x000DB2B2 File Offset: 0x000D94B2
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
