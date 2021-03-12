using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000875 RID: 2165
	[Serializable]
	internal sealed class SendMeetingCancellationsRequiredException : ServicePermanentException
	{
		// Token: 0x06003E43 RID: 15939 RVA: 0x000D842C File Offset: 0x000D662C
		public SendMeetingCancellationsRequiredException() : base(CoreResources.IDs.ErrorSendMeetingCancellationsRequired)
		{
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x000D843E File Offset: 0x000D663E
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
