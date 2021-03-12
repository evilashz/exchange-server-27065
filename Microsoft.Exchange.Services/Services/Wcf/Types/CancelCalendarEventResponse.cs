using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A18 RID: 2584
	[DataContract]
	public class CancelCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x060048EE RID: 18670 RVA: 0x00101FD4 File Offset: 0x001001D4
		internal CancelCalendarEventResponse(ServiceResult<VoidResult> serviceResult)
		{
			this.serviceResult = serviceResult;
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x00101FE3 File Offset: 0x001001E3
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return ResponseType.CancelCalendarEventResponseMessage;
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00101FEC File Offset: 0x001001EC
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			ResponseCodeType result = ResponseCodeType.NoError;
			if (this.serviceResult.Code != ServiceResultCode.Success)
			{
				result = this.serviceResult.Error.MessageKey;
			}
			return result;
		}

		// Token: 0x040029CB RID: 10699
		private readonly ServiceResult<VoidResult> serviceResult;
	}
}
