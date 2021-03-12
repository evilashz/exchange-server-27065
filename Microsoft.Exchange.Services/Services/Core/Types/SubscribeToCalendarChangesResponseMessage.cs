using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200055E RID: 1374
	public class SubscribeToCalendarChangesResponseMessage : ResponseMessage
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x000A66FB File Offset: 0x000A48FB
		public SubscribeToCalendarChangesResponseMessage()
		{
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000A6703 File Offset: 0x000A4903
		internal SubscribeToCalendarChangesResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
