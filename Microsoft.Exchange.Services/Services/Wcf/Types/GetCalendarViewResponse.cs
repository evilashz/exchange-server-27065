using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A2D RID: 2605
	[DataContract]
	public class GetCalendarViewResponse : IExchangeWebMethodResponse
	{
		// Token: 0x06004981 RID: 18817 RVA: 0x0010293D File Offset: 0x00100B3D
		internal GetCalendarViewResponse(ServiceResult<Event[]> results)
		{
			this.Events = results.Value;
			this.responseCode = ((results.Code == ServiceResultCode.Success) ? ResponseCodeType.NoError : results.Error.MessageKey);
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x0010296E File Offset: 0x00100B6E
		// (set) Token: 0x06004983 RID: 18819 RVA: 0x00102976 File Offset: 0x00100B76
		public Event[] Events { get; private set; }

		// Token: 0x06004984 RID: 18820 RVA: 0x0010297F File Offset: 0x00100B7F
		Microsoft.Exchange.Services.Core.Types.ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return Microsoft.Exchange.Services.Core.Types.ResponseType.GetCalendarViewResponseMessage;
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x00102986 File Offset: 0x00100B86
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			return this.responseCode;
		}

		// Token: 0x04002A01 RID: 10753
		private readonly ResponseCodeType responseCode;
	}
}
