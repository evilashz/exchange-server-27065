using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000784 RID: 1924
	[Serializable]
	internal sealed class FaultInjectionPermanentException : ServicePermanentException
	{
		// Token: 0x0600396F RID: 14703 RVA: 0x000CB3EA File Offset: 0x000C95EA
		public FaultInjectionPermanentException(ResponseCodeType responseCode, string soapAction) : base(responseCode, CoreResources.ErrorInternalServerErrorFaultInjection(responseCode.ToString(), soapAction))
		{
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003970 RID: 14704 RVA: 0x000CB404 File Offset: 0x000C9604
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
