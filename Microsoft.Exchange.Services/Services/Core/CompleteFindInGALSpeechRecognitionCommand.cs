using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002AD RID: 685
	internal sealed class CompleteFindInGALSpeechRecognitionCommand : SpeechRecognitionCommandBase<CompleteFindInGALSpeechRecognitionRequest, RecognitionResult>
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0005A412 File Offset: 0x00058612
		public CompleteFindInGALSpeechRecognitionCommand(CallContext callContext, CompleteFindInGALSpeechRecognitionRequest request) : base(callContext, request)
		{
			this.recognitionId = request.RecognitionId;
			this.audioData = request.AudioData;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0005A434 File Offset: 0x00058634
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new CompleteFindInGALSpeechRecognitionResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0005A45C File Offset: 0x0005865C
		internal override ServiceResult<RecognitionResult> Execute()
		{
			ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "CompleteFindInGALSpeechRecognitionCommand.Execute called");
			if (this.recognitionId == null)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "CompleteFindInGALSpeechRecognitionCommand.Execute - recognitionId is null");
				throw new InvalidRequestException();
			}
			if (this.audioData == null)
			{
				ExTraceGlobals.GetEventsCallTracer.TraceDebug((long)this.GetHashCode(), "CompleteFindInGALSpeechRecognitionCommand.Execute - audioData is null");
				throw new InvalidRequestException();
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CompleteFindInGALSpeechRecoRequestParams, null, new object[]
			{
				this.recognitionId.RequestId,
				this.audioData.Length
			});
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			Server localServer = adtopologyLookup.GetLocalServer();
			MobileSpeechRecoRpcClient mobileSpeechRecoRpcClient = new MobileSpeechRecoRpcClient(this.recognitionId.RequestId, localServer.Fqdn, null);
			IAsyncResult asyncResult = mobileSpeechRecoRpcClient.BeginRecognize(this.audioData, null, null);
			MobileRecoRPCAsyncCompletedArgs mobileRecoRPCAsyncCompletedArgs = mobileSpeechRecoRpcClient.EndRecognize(asyncResult);
			ServiceResult<RecognitionResult> result;
			if (mobileRecoRPCAsyncCompletedArgs.ErrorCode == 0)
			{
				RecognitionResult recognitionResult = new RecognitionResult(mobileRecoRPCAsyncCompletedArgs.Result);
				result = new ServiceResult<RecognitionResult>(recognitionResult);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CompleteFindInGALSpeechRecoRequestSuccess, null, new object[]
				{
					this.recognitionId.RequestId,
					CommonUtil.ToEventLogString(recognitionResult.Result)
				});
			}
			else
			{
				ServiceError serviceError = base.MapRpcErrorToServiceError(mobileRecoRPCAsyncCompletedArgs.ErrorCode, mobileRecoRPCAsyncCompletedArgs.ErrorMessage);
				result = new ServiceResult<RecognitionResult>(serviceError);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CompleteFindInGALSpeechRecoRequestFailed, null, new object[]
				{
					this.recognitionId.RequestId,
					CommonUtil.ToEventLogString(serviceError.MessageKey),
					CommonUtil.ToEventLogString(serviceError.MessageText)
				});
			}
			return result;
		}

		// Token: 0x04000D07 RID: 3335
		private RecognitionId recognitionId;

		// Token: 0x04000D08 RID: 3336
		private byte[] audioData;
	}
}
