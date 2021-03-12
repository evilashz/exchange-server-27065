using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000876 RID: 2166
	[Serializable]
	internal sealed class SendMeetingInvitationsOrCancellationsRequiredException : ServicePermanentException
	{
		// Token: 0x06003E45 RID: 15941 RVA: 0x000D8445 File Offset: 0x000D6645
		public SendMeetingInvitationsOrCancellationsRequiredException() : base((CoreResources.IDs)3422864683U)
		{
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x000D8457 File Offset: 0x000D6657
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
