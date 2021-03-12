using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200081B RID: 2075
	internal sealed class NameResolutionNoMailboxException : ServicePermanentException
	{
		// Token: 0x06003C33 RID: 15411 RVA: 0x000D5A0A File Offset: 0x000D3C0A
		public NameResolutionNoMailboxException() : base(CoreResources.IDs.ErrorNameResolutionNoMailbox)
		{
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x000D5A1C File Offset: 0x000D3C1C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
