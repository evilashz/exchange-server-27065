using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000826 RID: 2086
	internal sealed class NonProvisionedException : ServicePermanentException
	{
		// Token: 0x06003C63 RID: 15459 RVA: 0x000D5C46 File Offset: 0x000D3E46
		public NonProvisionedException(bool mowa) : base(ResponseCodeType.ErrorMailboxStoreUnavailable, mowa ? CoreResources.IDs.MowaNotProvisioned : CoreResources.IDs.DowaNotProvisioned)
		{
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000D5C67 File Offset: 0x000D3E67
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
