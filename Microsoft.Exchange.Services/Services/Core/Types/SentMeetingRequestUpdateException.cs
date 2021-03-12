using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000879 RID: 2169
	[Serializable]
	internal sealed class SentMeetingRequestUpdateException : ServicePermanentException
	{
		// Token: 0x06003E49 RID: 15945 RVA: 0x000D8477 File Offset: 0x000D6677
		public SentMeetingRequestUpdateException() : base((CoreResources.IDs)3080514177U)
		{
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x000D8489 File Offset: 0x000D6689
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
