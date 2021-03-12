using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200079E RID: 1950
	internal class ImGroupLimitReachedException : ServicePermanentException
	{
		// Token: 0x06003A7F RID: 14975 RVA: 0x000CF0C4 File Offset: 0x000CD2C4
		public ImGroupLimitReachedException() : base(ResponseCodeType.ErrorImGroupLimitReached, CoreResources.ErrorImGroupLimitReached(Global.UnifiedContactStoreConfiguration.MaxImGroups))
		{
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000CF0E0 File Offset: 0x000CD2E0
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
