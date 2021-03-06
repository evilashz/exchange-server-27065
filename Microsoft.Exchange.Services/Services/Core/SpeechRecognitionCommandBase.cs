using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002AC RID: 684
	internal abstract class SpeechRecognitionCommandBase<RequestType, ResponseType> : SingleStepServiceCommand<RequestType, ResponseType> where RequestType : BaseRequest
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x0005A344 File Offset: 0x00058544
		public SpeechRecognitionCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0005A350 File Offset: 0x00058550
		protected ServiceError MapRpcErrorToServiceError(int errorCode, string errorMessage)
		{
			ResponseCodeType messageKey = ResponseCodeType.ErrorInternalServerError;
			switch (errorCode)
			{
			case -2147466752:
				messageKey = ResponseCodeType.ErrorInternalServerError;
				goto IL_A6;
			case -2147466751:
			case -2147466749:
			case -2147466748:
				break;
			case -2147466750:
				messageKey = ResponseCodeType.ErrorInvalidRequest;
				goto IL_A6;
			case -2147466747:
				messageKey = ResponseCodeType.ErrorRecipientNotFound;
				goto IL_A6;
			case -2147466746:
				messageKey = ResponseCodeType.ErrorRecognizerNotInstalled;
				goto IL_A6;
			default:
				switch (errorCode)
				{
				case -2147466743:
					messageKey = ResponseCodeType.ErrorNoSpeechDetected;
					goto IL_A6;
				case -2147466742:
					break;
				case -2147466741:
					messageKey = ResponseCodeType.ErrorSpeechGrammarError;
					goto IL_A6;
				default:
					if (errorCode == 1722)
					{
						messageKey = ResponseCodeType.ErrorUMServerUnavailable;
						goto IL_A6;
					}
					break;
				}
				break;
			}
			ExAssert.RetailAssert(false, "Invalid error code {0}", new object[]
			{
				errorCode
			});
			IL_A6:
			return new ServiceError(errorMessage, messageKey, 0, ExchangeVersion.Exchange2012);
		}
	}
}
