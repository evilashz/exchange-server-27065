using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006F6 RID: 1782
	internal class CalendarExceptionInvalidTimeZone : ServicePermanentException
	{
		// Token: 0x06003631 RID: 13873 RVA: 0x000C1FEF File Offset: 0x000C01EF
		public CalendarExceptionInvalidTimeZone(Exception exception) : base(CoreResources.IDs.ErrorCalendarInvalidTimeZone, exception)
		{
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000C2002 File Offset: 0x000C0202
		public CalendarExceptionInvalidTimeZone(Enum messageId) : base(ResponseCodeType.ErrorCalendarInvalidTimeZone, messageId)
		{
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000C200D File Offset: 0x000C020D
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
