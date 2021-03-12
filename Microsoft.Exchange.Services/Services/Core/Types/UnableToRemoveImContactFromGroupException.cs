using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008AA RID: 2218
	internal sealed class UnableToRemoveImContactFromGroupException : ServicePermanentException
	{
		// Token: 0x06003F14 RID: 16148 RVA: 0x000DAA53 File Offset: 0x000D8C53
		public UnableToRemoveImContactFromGroupException() : base((CoreResources.IDs)3162641137U)
		{
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x000DAA65 File Offset: 0x000D8C65
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
