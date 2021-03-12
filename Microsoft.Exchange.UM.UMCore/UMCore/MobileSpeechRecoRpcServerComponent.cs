using System;
using System.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200028C RID: 652
	internal sealed class MobileSpeechRecoRpcServerComponent : UMRPCComponentBase
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x000564F0 File Offset: 0x000546F0
		public static MobileSpeechRecoRpcServerComponent Instance
		{
			get
			{
				if (MobileSpeechRecoRpcServerComponent.instance == null)
				{
					lock (MobileSpeechRecoRpcServerComponent.staticLock)
					{
						if (MobileSpeechRecoRpcServerComponent.instance == null)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, null, "Creating singleton instance of MobileSpeechRecoRpcServerComponent", new object[0]);
							MobileSpeechRecoRpcServerComponent.instance = new MobileSpeechRecoRpcServerComponent();
						}
					}
				}
				return MobileSpeechRecoRpcServerComponent.instance;
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0005655C File Offset: 0x0005475C
		internal override void RegisterServer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "Entering MobileSpeechRecoRpcServerComponent.RegisterServer", new object[0]);
			ActiveDirectorySecurity sd = null;
			Util.GetServerSecurityDescriptor(ref sd);
			uint accessMask = 131220U;
			RpcServerBase.RegisterServer(typeof(MobileSpeechRecoRpcServerComponent.MobileSpeechRecoRpcServer), sd, accessMask);
		}

		// Token: 0x04000C6E RID: 3182
		private static object staticLock = new object();

		// Token: 0x04000C6F RID: 3183
		private static MobileSpeechRecoRpcServerComponent instance;

		// Token: 0x0200028D RID: 653
		internal sealed class MobileSpeechRecoRpcServer : MobileSpeechRecoRpcServerBase
		{
			// Token: 0x06001359 RID: 4953 RVA: 0x000565C0 File Offset: 0x000547C0
			public override void ExecuteStepAsync(byte[] inBlob, object token)
			{
				ValidateArgument.NotNull(inBlob, "inBlob");
				ValidateArgument.NotNull(token, "token");
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "Entering MobileSpeechRecoRpcServer.ExecuteStepAsync", new object[0]);
				try
				{
					IMobileSpeechRecoRequestStep mobileSpeechRecoRequestStep = MobileSpeechRecoRequestStep.Create(inBlob);
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcServer.ExecuteStepAsync Step Dump={0}", new object[]
					{
						mobileSpeechRecoRequestStep
					});
					mobileSpeechRecoRequestStep.ExecuteAsync(new MobileRecoRequestStepAsyncCompletedDelegate(this.OnRequestStepCompleted), token);
				}
				catch (Exception ex)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoRPCGeneralUnexpectedFailure, null, new object[]
					{
						CommonUtil.ToEventLogString(ex.ToString())
					});
					UMRPCComponentBase.HandleException(ex);
				}
			}

			// Token: 0x0600135A RID: 4954 RVA: 0x00056688 File Offset: 0x00054888
			public void OnRequestStepCompleted(int returnValue, byte[] outBlob, object token)
			{
				ValidateArgument.NotNull(outBlob, "outBlob");
				ValidateArgument.NotNull(token, "token");
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "Entering MobileSpeechRecoRpcServer.OnRequestStepCompleted - Return value='{0}'", new object[]
				{
					returnValue
				});
				base.CompleteAsync(returnValue, outBlob, token);
			}
		}
	}
}
