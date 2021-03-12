using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000804 RID: 2052
	[Serializable]
	internal sealed class MessageTrackingFatalException : ServicePermanentException
	{
		// Token: 0x06003BF6 RID: 15350 RVA: 0x000D4F54 File Offset: 0x000D3154
		public MessageTrackingFatalException() : base(CoreResources.IDs.ErrorMessageTrackingPermanentError)
		{
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000D4F66 File Offset: 0x000D3166
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
