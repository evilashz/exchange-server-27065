using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A20 RID: 2592
	[DataContract]
	public class ForwardCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x0600491D RID: 18717 RVA: 0x0010233F File Offset: 0x0010053F
		internal ForwardCalendarEventResponse(ServiceResult<VoidResult> serviceResult)
		{
			this.serviceResult = serviceResult;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x0010234E File Offset: 0x0010054E
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return ResponseType.ForwardCalendarEventResponseMessage;
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00102358 File Offset: 0x00100558
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			ResponseCodeType result = ResponseCodeType.NoError;
			if (this.serviceResult.Code != ServiceResultCode.Success)
			{
				result = this.serviceResult.Error.MessageKey;
			}
			return result;
		}

		// Token: 0x040029DA RID: 10714
		private readonly ServiceResult<VoidResult> serviceResult;
	}
}
