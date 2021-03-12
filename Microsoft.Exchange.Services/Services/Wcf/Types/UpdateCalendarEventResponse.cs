using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A3C RID: 2620
	[DataContract]
	public class UpdateCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x060049FD RID: 18941 RVA: 0x0010308C File Offset: 0x0010128C
		internal UpdateCalendarEventResponse(ServiceResult<Event>[] results)
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

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x001030F1 File Offset: 0x001012F1
		// (set) Token: 0x060049FF RID: 18943 RVA: 0x001030F9 File Offset: 0x001012F9
		public Event[] Events { get; private set; }

		// Token: 0x06004A00 RID: 18944 RVA: 0x00103102 File Offset: 0x00101302
		Microsoft.Exchange.Services.Core.Types.ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return Microsoft.Exchange.Services.Core.Types.ResponseType.UpdateCalendarEventResponseMessage;
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x00103109 File Offset: 0x00101309
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			return this.aggregatedResponseCode;
		}

		// Token: 0x04002A39 RID: 10809
		private readonly ResponseCodeType aggregatedResponseCode;
	}
}
