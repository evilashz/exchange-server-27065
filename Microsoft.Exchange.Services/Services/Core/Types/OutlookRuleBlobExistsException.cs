using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000588 RID: 1416
	internal sealed class OutlookRuleBlobExistsException : ServicePermanentException
	{
		// Token: 0x0600275D RID: 10077 RVA: 0x000A7410 File Offset: 0x000A5610
		public OutlookRuleBlobExistsException() : base(ResponseCodeType.ErrorOutlookRuleBlobExists, CoreResources.IDs.RuleErrorOutlookRuleBlobExists)
		{
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000A7427 File Offset: 0x000A5627
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
