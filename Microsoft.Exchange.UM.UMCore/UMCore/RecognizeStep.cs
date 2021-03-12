using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200028F RID: 655
	internal class RecognizeStep : MobileSpeechRecoRequestStep
	{
		// Token: 0x06001362 RID: 4962 RVA: 0x0005686C File Offset: 0x00054A6C
		public RecognizeStep(Guid requestId, MdbefPropertyCollection args) : base(requestId, args, "Recognize")
		{
			MobileSpeechRecoTracer.TraceDebug(this, requestId, "Entering RecognizeStep constructor", new object[0]);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00056890 File Offset: 0x00054A90
		protected override void InternalExecuteAsync()
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.RequestId, "Entering RecognizeStep.InternalExecuteAsync", new object[0]);
			base.CollectStartStatisticsLog();
			byte[] array;
			this.ExtractArgs(base.Args, out array);
			MobileSpeechRecoTracer.TraceDebug(this, base.RequestId, "RecognizeStep.ExecuteAsync audioBytes length ='{0}' bytes", new object[]
			{
				array.Length
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoRecognizeRPCParams, null, new object[]
			{
				base.RequestId,
				array.Length
			});
			MobileSpeechRecoDispatcher.Instance.RecognizeAsync(base.RequestId, array, new MobileRecoAsyncCompletedDelegate(base.OnStepCompleted));
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0005693C File Offset: 0x00054B3C
		private void ExtractArgs(MdbefPropertyCollection inArgs, out byte[] audioBytes)
		{
			MobileSpeechRecoTracer.TraceDebug(this, base.RequestId, "Entering RecognizeStep.ExtractArgs", new object[0]);
			object obj = MobileSpeechRecoRequestStep.ExtractArg(inArgs, 2416247042U, "audioBytes");
			audioBytes = (byte[])obj;
			if (audioBytes == null)
			{
				MobileSpeechRecoTracer.TraceError(this, base.RequestId, "AddRecoRequestStep.ExtractArgs -  Audio bytes is null", new object[0]);
				throw new ArgumentNullException("audioBytes");
			}
		}

		// Token: 0x04000C70 RID: 3184
		private const string DescriptionStr = "Recognize";
	}
}
