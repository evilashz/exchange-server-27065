using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000732 RID: 1842
	internal sealed class CrossSiteRequestException : ServicePermanentException
	{
		// Token: 0x060037A8 RID: 14248 RVA: 0x000C5BAF File Offset: 0x000C3DAF
		public CrossSiteRequestException(string smtpAddress) : base(ResponseCodeType.ErrorCrossSiteRequest, CoreResources.IDs.ErrorCrossSiteRequest)
		{
			base.ConstantValues.Add("AutodiscoverSmtpAddress", smtpAddress);
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000C5BD4 File Offset: 0x000C3DD4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}

		// Token: 0x04001EF8 RID: 7928
		private const string AutodiscoverSmtpAddress = "AutodiscoverSmtpAddress";
	}
}
