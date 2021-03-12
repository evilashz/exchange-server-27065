using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A32 RID: 2610
	[DataContract]
	public class RespondToCalendarEventResponse : IExchangeWebMethodResponse
	{
		// Token: 0x060049A4 RID: 18852 RVA: 0x00102ABF File Offset: 0x00100CBF
		internal RespondToCalendarEventResponse(ServiceResult<VoidResult> serviceResult)
		{
			this.serviceResult = serviceResult;
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00102ACE File Offset: 0x00100CCE
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return ResponseType.RespondToCalendarEventResponseMessage;
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00102AD8 File Offset: 0x00100CD8
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			ResponseCodeType result = ResponseCodeType.NoError;
			if (this.serviceResult.Code != ServiceResultCode.Success)
			{
				result = this.serviceResult.Error.MessageKey;
			}
			return result;
		}

		// Token: 0x04002A0F RID: 10767
		private readonly ServiceResult<VoidResult> serviceResult;
	}
}
