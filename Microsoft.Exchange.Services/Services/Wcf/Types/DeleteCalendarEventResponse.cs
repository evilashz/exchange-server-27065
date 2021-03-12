using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A1C RID: 2588
	[DataContract]
	public class DeleteCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x06004904 RID: 18692 RVA: 0x001021A3 File Offset: 0x001003A3
		internal DeleteCalendarEventResponse(ServiceResult<VoidResult> serviceResult)
		{
			this.serviceResult = serviceResult;
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x001021B2 File Offset: 0x001003B2
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return ResponseType.DeleteCalendarEventResponseMessage;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x001021BC File Offset: 0x001003BC
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			ResponseCodeType result = ResponseCodeType.NoError;
			if (this.serviceResult.Code != ServiceResultCode.Success)
			{
				result = this.serviceResult.Error.MessageKey;
			}
			return result;
		}

		// Token: 0x040029D2 RID: 10706
		private readonly ServiceResult<VoidResult> serviceResult;
	}
}
