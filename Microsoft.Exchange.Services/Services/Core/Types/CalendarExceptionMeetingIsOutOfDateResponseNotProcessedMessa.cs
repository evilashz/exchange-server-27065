using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006FD RID: 1789
	internal class CalendarExceptionMeetingIsOutOfDateResponseNotProcessedMessageSent : ServicePermanentException
	{
		// Token: 0x06003643 RID: 13891 RVA: 0x000C229A File Offset: 0x000C049A
		public CalendarExceptionMeetingIsOutOfDateResponseNotProcessedMessageSent() : base((CoreResources.IDs)3407017993U)
		{
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000C22AC File Offset: 0x000C04AC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
