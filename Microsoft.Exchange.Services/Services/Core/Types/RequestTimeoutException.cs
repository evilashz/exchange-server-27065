using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000862 RID: 2146
	[Serializable]
	internal sealed class RequestTimeoutException : ServicePermanentException
	{
		// Token: 0x06003DAA RID: 15786 RVA: 0x000D7B56 File Offset: 0x000D5D56
		public RequestTimeoutException() : base(ResponseCodeType.ErrorTimeoutExpired, (CoreResources.IDs)3285224352U)
		{
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003DAB RID: 15787 RVA: 0x000D7B6D File Offset: 0x000D5D6D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000D7B74 File Offset: 0x000D5D74
		internal override bool StopsBatchProcessing
		{
			get
			{
				return true;
			}
		}
	}
}
