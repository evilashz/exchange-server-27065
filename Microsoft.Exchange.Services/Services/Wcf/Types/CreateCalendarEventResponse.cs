using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A1A RID: 2586
	[DataContract]
	public class CreateCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x060048F8 RID: 18680 RVA: 0x001020B0 File Offset: 0x001002B0
		internal CreateCalendarEventResponse(ServiceResult<Event>[] results)
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

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x00102115 File Offset: 0x00100315
		// (set) Token: 0x060048FA RID: 18682 RVA: 0x0010211D File Offset: 0x0010031D
		public Event[] Events { get; private set; }

		// Token: 0x060048FB RID: 18683 RVA: 0x00102126 File Offset: 0x00100326
		Microsoft.Exchange.Services.Core.Types.ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return Microsoft.Exchange.Services.Core.Types.ResponseType.CreateCalendarEventResponseMessage;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0010212D File Offset: 0x0010032D
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			return this.aggregatedResponseCode;
		}

		// Token: 0x040029CE RID: 10702
		private readonly ResponseCodeType aggregatedResponseCode;
	}
}
