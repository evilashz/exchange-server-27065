using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000731 RID: 1841
	internal sealed class CrossMailboxMoveCopyException : ServicePermanentException
	{
		// Token: 0x060037A6 RID: 14246 RVA: 0x000C5B96 File Offset: 0x000C3D96
		public CrossMailboxMoveCopyException() : base((CoreResources.IDs)2832845860U)
		{
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000C5BA8 File Offset: 0x000C3DA8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
