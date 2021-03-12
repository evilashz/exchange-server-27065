using System;
using System.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000320 RID: 800
	internal sealed class GetPhoneCallInformation : SingleStepServiceCommand<GetPhoneCallInformationRequest, GetPhoneCallInformationResponseMessage>
	{
		// Token: 0x060016A2 RID: 5794 RVA: 0x00077008 File Offset: 0x00075208
		public GetPhoneCallInformation(CallContext callContext, GetPhoneCallInformationRequest request) : base(callContext, request)
		{
			this.callId = request.CallId;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0007701E File Offset: 0x0007521E
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetPhoneCallInformationResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00077048 File Offset: 0x00075248
		internal override ServiceResult<GetPhoneCallInformationResponseMessage> Execute()
		{
			UMCallInfoEx callInfo = null;
			using (UMClientCommon umclientCommon = new UMClientCommon(base.CallContext.AccessingPrincipal))
			{
				callInfo = umclientCommon.GetCallInfo(this.callId.Id);
			}
			return new ServiceResult<GetPhoneCallInformationResponseMessage>(new GetPhoneCallInformationResponseMessage
			{
				PhoneCallInformation = this.BuildCallInformationXml(callInfo)
			});
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000770B4 File Offset: 0x000752B4
		private XmlElement BuildCallInformationXml(UMCallInfoEx callInfo)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(base.XmlDocument, "PhoneCallInformation", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ServiceXml.CreateTextElement(xmlElement, "PhoneCallState", callInfo.CallState.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types");
			ServiceXml.CreateTextElement(xmlElement, "ConnectionFailureCause", callInfo.EventCause.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types");
			if (callInfo.EventCause != UMEventCause.None)
			{
				ServiceXml.CreateTextElement(xmlElement, "SIPResponseText", callInfo.ResponseText, "http://schemas.microsoft.com/exchange/services/2006/types");
				ServiceXml.CreateTextElement(xmlElement, "SIPResponseCode", callInfo.ResponseCode.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types");
			}
			return xmlElement;
		}

		// Token: 0x04000F39 RID: 3897
		private PhoneCallId callId;
	}
}
