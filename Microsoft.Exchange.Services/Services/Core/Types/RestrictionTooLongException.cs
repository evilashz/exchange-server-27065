using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000868 RID: 2152
	internal sealed class RestrictionTooLongException : ServicePermanentException
	{
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000D7CD6 File Offset: 0x000D5ED6
		public RestrictionTooLongException() : base((CoreResources.IDs)3143473274U)
		{
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x000D7CE8 File Offset: 0x000D5EE8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
