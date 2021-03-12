using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A1E RID: 2590
	[DataContract]
	public class ExpandCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x06004910 RID: 18704 RVA: 0x0010226B File Offset: 0x0010046B
		internal ExpandCalendarEventResponse(ServiceResult<ExpandedEvent> serviceResult)
		{
			this.serviceResult = serviceResult;
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06004911 RID: 18705 RVA: 0x0010227A File Offset: 0x0010047A
		public ExpandedEvent Result
		{
			get
			{
				return this.serviceResult.Value;
			}
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00102287 File Offset: 0x00100487
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return ResponseType.ExpandCalendarEventResponseMessage;
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00102290 File Offset: 0x00100490
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			ResponseCodeType result = ResponseCodeType.NoError;
			if (this.serviceResult.Code != ServiceResultCode.Success)
			{
				result = this.serviceResult.Error.MessageKey;
			}
			return result;
		}

		// Token: 0x040029D6 RID: 10710
		private readonly ServiceResult<ExpandedEvent> serviceResult;
	}
}
