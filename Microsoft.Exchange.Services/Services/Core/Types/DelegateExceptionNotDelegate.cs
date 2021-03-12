using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000739 RID: 1849
	internal class DelegateExceptionNotDelegate : ServicePermanentException
	{
		// Token: 0x060037B7 RID: 14263 RVA: 0x000C5C64 File Offset: 0x000C3E64
		public DelegateExceptionNotDelegate() : base((CoreResources.IDs)2410622290U)
		{
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000C5C76 File Offset: 0x000C3E76
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
