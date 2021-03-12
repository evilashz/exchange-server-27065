using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007FE RID: 2046
	internal sealed class MailTipsDisabledException : ServicePermanentException
	{
		// Token: 0x06003BEC RID: 15340 RVA: 0x000D4E5A File Offset: 0x000D305A
		public MailTipsDisabledException() : base(ResponseCodeType.ErrorMailTipsDisabled, CoreResources.IDs.ErrorMailTipsDisabled)
		{
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x000D4E71 File Offset: 0x000D3071
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
