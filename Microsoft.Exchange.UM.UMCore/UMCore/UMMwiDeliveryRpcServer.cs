using System;
using System.Security.AccessControl;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000218 RID: 536
	internal sealed class UMMwiDeliveryRpcServer : UMMwiDeliveryRpcServerBase, IUMAsyncComponent
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x000463ED File Offset: 0x000445ED
		public AutoResetEvent StoppedEvent
		{
			get
			{
				return UMMwiDeliveryRpcServer.loadBalancer.ShutDownEvent;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000463F9 File Offset: 0x000445F9
		public bool IsInitialized
		{
			get
			{
				return UMMwiDeliveryRpcServer.initialized;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00046400 File Offset: 0x00044600
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0004640D File Offset: 0x0004460D
		internal static UMMwiDeliveryRpcServer Instance
		{
			get
			{
				return UMMwiDeliveryRpcServer.instance;
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00046414 File Offset: 0x00044614
		public void StartNow(StartupStage stage)
		{
			if (stage == StartupStage.WPActivation)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, 0, "{0} starting in stage {1}", new object[]
				{
					this.Name,
					stage
				});
				UMMwiDeliveryRpcServer.loadBalancer = new MwiLoadBalancer(ExTraceGlobals.MWITracer, UMIPGatewayMwiTargetPicker.Instance, new UMIPGatewayMwiErrorStrategy());
				uint accessMask = 1U;
				FileSecurity allowExchangeServerSecurity = Util.GetAllowExchangeServerSecurity();
				RpcServerBase.RegisterServer(typeof(UMMwiDeliveryRpcServer), allowExchangeServerSecurity, accessMask);
				UMMwiDeliveryRpcServer.initialized = true;
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0004648D File Offset: 0x0004468D
		public void StopAsync()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, 0, "UMMwiDeliveryRpcServer.Stop(): Stopping RPC server", new object[0]);
			if (UMMwiDeliveryRpcServer.loadBalancer != null)
			{
				RpcServerBase.StopServer(UMMwiDeliveryRpcServerBase.RpcIntfHandle);
				UMMwiDeliveryRpcServer.loadBalancer.ShutdownAsync();
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000464C5 File Offset: 0x000446C5
		public void CleanupAfterStopped()
		{
			if (UMMwiDeliveryRpcServer.loadBalancer != null)
			{
				UMMwiDeliveryRpcServer.loadBalancer.CleanupAfterAsyncShutdown();
				UMMwiDeliveryRpcServer.loadBalancer = null;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000465AC File Offset: 0x000447AC
		public override int SendMwiMessage(Guid mailboxGuid, Guid dialPlanGuid, string userExtension, string userName, int unreadVoicemailCount, int totalVoicemailCount, int assistantLatencyMsec, Guid tenantGuid)
		{
			int result = 0;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					ExDateTime eventTimeUtc = ExDateTime.UtcNow.Subtract(TimeSpan.FromMilliseconds((double)assistantLatencyMsec));
					MwiMessage mwiMessage = new MwiMessage(mailboxGuid, dialPlanGuid, userName, userExtension, unreadVoicemailCount, totalVoicemailCount, UMMwiDeliveryRpcServer.MessageExpirationTime, eventTimeUtc, tenantGuid);
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMMwiDeliveryRpcServer.SendMwiMessage(message={0})", new object[]
					{
						mwiMessage
					});
					UMMwiDeliveryRpcServer.loadBalancer.SendMessage(mwiMessage);
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMMwiDeliveryRpcServer.SendMwiMessage(message={0}) loadBalancer.SendMessage has finished.", new object[]
					{
						mwiMessage
					});
				});
			}
			catch (GrayException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMMwiDeliveryRpcServer.SendMwiMessage failed: {0} ", new object[]
				{
					ex
				});
				result = -2147466752;
			}
			return result;
		}

		// Token: 0x04000B4D RID: 2893
		private static readonly TimeSpan MessageExpirationTime = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000B4E RID: 2894
		private static MwiLoadBalancer loadBalancer;

		// Token: 0x04000B4F RID: 2895
		private static bool initialized;

		// Token: 0x04000B50 RID: 2896
		private static UMMwiDeliveryRpcServer instance = new UMMwiDeliveryRpcServer();
	}
}
