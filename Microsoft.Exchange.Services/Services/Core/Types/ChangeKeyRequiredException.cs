using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000720 RID: 1824
	internal sealed class ChangeKeyRequiredException : ServicePermanentException
	{
		// Token: 0x06003751 RID: 14161 RVA: 0x000C56BE File Offset: 0x000C38BE
		public ChangeKeyRequiredException() : base((CoreResources.IDs)3941855338U)
		{
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x000C56D0 File Offset: 0x000C38D0
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
