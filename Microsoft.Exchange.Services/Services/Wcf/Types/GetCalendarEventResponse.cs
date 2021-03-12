using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A22 RID: 2594
	[DataContract]
	public class GetCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x06004927 RID: 18727 RVA: 0x00102404 File Offset: 0x00100604
		internal GetCalendarEventResponse(ServiceResult<Event>[] results)
		{
			this.aggregatedResponseCode = ResponseCodeType.NoError;
			this.Events = new Event[results.Length];
			for (int i = 0; i < results.Length; i++)
			{
				this.Events[i] = results[i].Value;
				if (results[i].Code != ServiceResultCode.Success)
				{
					this.aggregatedResponseCode = results[i].Error.MessageKey;
					return;
				}
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x00102469 File Offset: 0x00100669
		// (set) Token: 0x06004929 RID: 18729 RVA: 0x00102471 File Offset: 0x00100671
		public Event[] Events { get; private set; }

		// Token: 0x0600492A RID: 18730 RVA: 0x0010247A File Offset: 0x0010067A
		Microsoft.Exchange.Services.Core.Types.ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return Microsoft.Exchange.Services.Core.Types.ResponseType.GetCalendarEventResponseMessage;
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x00102481 File Offset: 0x00100681
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			return this.aggregatedResponseCode;
		}

		// Token: 0x040029DD RID: 10717
		private readonly ResponseCodeType aggregatedResponseCode;
	}
}
