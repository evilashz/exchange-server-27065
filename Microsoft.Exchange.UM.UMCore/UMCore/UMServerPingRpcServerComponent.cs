using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000227 RID: 551
	internal sealed class UMServerPingRpcServerComponent : UMRPCComponentBase
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00047838 File Offset: 0x00045A38
		internal static UMServerPingRpcServerComponent Instance
		{
			get
			{
				return UMServerPingRpcServerComponent.instance;
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00047840 File Offset: 0x00045A40
		internal override void RegisterServer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcServerComponent::RegisterServer()", new object[0]);
			uint accessMask = 1U;
			FileSecurity allowExchangeServerSecurity = Util.GetAllowExchangeServerSecurity();
			RpcServerBase.RegisterServer(typeof(UMServerPingRpcServerComponent.UMServerPingRpcServer), allowExchangeServerSecurity, accessMask);
		}

		// Token: 0x04000B76 RID: 2934
		private static UMServerPingRpcServerComponent instance = new UMServerPingRpcServerComponent();

		// Token: 0x02000228 RID: 552
		internal sealed class UMServerPingRpcServer : UMServerPingRpcServerBase
		{
			// Token: 0x06001009 RID: 4105 RVA: 0x0004789C File Offset: 0x00045A9C
			public override int Ping(Guid dialPlanGuid, ref bool availableToTakeCalls)
			{
				int result = 0;
				availableToTakeCalls = false;
				if (!UMServerPingRpcServerComponent.Instance.GuardBeforeExecution())
				{
					return result;
				}
				try
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcServer::Ping() executing rpc request.", new object[0]);
					availableToTakeCalls = (Util.VerifyServerIsInDialPlan(Utils.GetLocalUMServer(), dialPlanGuid, false) && !Util.MaxCallLimitExceeded());
				}
				catch (ExchangeServerNotFoundException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcServer::Ping() Exception {0}", new object[]
					{
						ex
					});
				}
				catch (ADTransientException ex2)
				{
					CallIdTracer.TraceError(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcServer::Ping() Exception {0}", new object[]
					{
						ex2
					});
				}
				finally
				{
					UMServerPingRpcServerComponent.Instance.GuardAfterExecution();
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "UMServerPingRpcServer::Ping() AvailableToTakeCalls = {0}", new object[]
				{
					availableToTakeCalls
				});
				return result;
			}
		}
	}
}
