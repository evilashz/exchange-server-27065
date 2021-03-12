using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000877 RID: 2167
	[Serializable]
	internal sealed class SendMeetingInvitationsRequiredException : ServicePermanentException
	{
		// Token: 0x06003E47 RID: 15943 RVA: 0x000D845E File Offset: 0x000D665E
		public SendMeetingInvitationsRequiredException() : base((CoreResources.IDs)3383701276U)
		{
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x000D8470 File Offset: 0x000D6670
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
