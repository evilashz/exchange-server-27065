using System;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000333 RID: 819
	internal sealed class GetTimeZoneOffsets : SingleStepServiceCommand<GetTimeZoneOffsetsRequest, GetTimeZoneOffsetsResponseMessage>
	{
		// Token: 0x06001703 RID: 5891 RVA: 0x0007A66E File Offset: 0x0007886E
		public GetTimeZoneOffsets(CallContext callContext, GetTimeZoneOffsetsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0007A683 File Offset: 0x00078883
		internal override IExchangeWebMethodResponse GetResponse()
		{
			this.responseMessage.Initialize(base.Result.Code, base.Result.Error);
			return this.responseMessage;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0007A6AC File Offset: 0x000788AC
		internal override ServiceResult<GetTimeZoneOffsetsResponseMessage> Execute()
		{
			ExTraceGlobals.GetTimeZoneOffsetsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "GetTimeZoneOffsets.Execute: User '{0}'", base.CallContext.EffectiveCaller.PrimarySmtpAddress);
			ExDateTime startTime = ExDateTime.ParseISO(base.Request.StartTime);
			ExDateTime endTime = ExDateTime.ParseISO(base.Request.EndTime);
			this.responseMessage.TimeZones = GetTimeZoneOffsetsCore.GetTheTimeZoneOffsets(startTime, endTime, base.Request.TimeZoneId);
			return new ServiceResult<GetTimeZoneOffsetsResponseMessage>(this.responseMessage);
		}

		// Token: 0x04000F9F RID: 3999
		private GetTimeZoneOffsetsResponseMessage responseMessage = new GetTimeZoneOffsetsResponseMessage();
	}
}
