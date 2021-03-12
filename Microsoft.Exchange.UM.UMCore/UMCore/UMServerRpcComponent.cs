using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000229 RID: 553
	internal sealed class UMServerRpcComponent : UMRPCComponentBase
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000479BC File Offset: 0x00045BBC
		internal static UMServerRpcComponent Instance
		{
			get
			{
				return UMServerRpcComponent.instance;
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x000479C4 File Offset: 0x00045BC4
		internal override void RegisterServer()
		{
			ActiveDirectorySecurity sd = null;
			Util.GetServerSecurityDescriptor(ref sd);
			RpcServerBase.RegisterServer(typeof(UMServerRpcComponent.UMServerRpc), sd, 32);
		}

		// Token: 0x04000B77 RID: 2935
		private static UMServerRpcComponent instance = new UMServerRpcComponent();

		// Token: 0x0200022A RID: 554
		internal sealed class UMServerRpc : UMRpcServer
		{
			// Token: 0x06001010 RID: 4112 RVA: 0x00047A0C File Offset: 0x00045C0C
			public override byte[] GetUmActiveCalls(bool isDialPlan, string dialPlan, bool isIpGateway, string ipGateway)
			{
				if (!UMServerPingRpcServerComponent.Instance.GuardBeforeExecution())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "GetUmActiveCalls: WP is shutting down.", new object[0]);
					return null;
				}
				byte[] array = null;
				try
				{
					this.isDialPlan = isDialPlan;
					this.dialPlan = dialPlan;
					this.isIpGateway = isIpGateway;
					this.ipGateway = ipGateway;
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "GetUmActiveCalls: Executing RPC request", new object[0]);
					ActiveCalls[] activeCalls = this.GetActiveCalls();
					array = Serialization.ObjectToBytes(activeCalls);
				}
				finally
				{
					UMServerPingRpcServerComponent.Instance.GuardAfterExecution();
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "GetUmActiveCalls: RPC request succeeded. Response {0}", new object[]
				{
					(array != null) ? "not null" : "null"
				});
				return array;
			}

			// Token: 0x06001011 RID: 4113 RVA: 0x00047AE8 File Offset: 0x00045CE8
			private ActiveCalls[] GetActiveCalls()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this.GetHashCode(), "In GetActiveCalls", new object[0]);
				BaseUMCallSession[] array = null;
				lock (UmServiceGlobals.VoipPlatform.CallSessionHashTable.SyncRoot)
				{
					int count = UmServiceGlobals.VoipPlatform.CallSessionHashTable.Count;
					if (count == 0)
					{
						return null;
					}
					array = new BaseUMCallSession[count];
					UmServiceGlobals.VoipPlatform.CallSessionHashTable.Values.CopyTo(array, 0);
				}
				List<ActiveCalls> list = new List<ActiveCalls>();
				foreach (BaseUMCallSession cs in array)
				{
					ActiveCalls item = null;
					if (this.ValidCallDataToAdd(cs, out item))
					{
						list.Add(item);
					}
				}
				return list.ToArray();
			}

			// Token: 0x06001012 RID: 4114 RVA: 0x00047BC8 File Offset: 0x00045DC8
			private bool ValidCallDataToAdd(BaseUMCallSession cs, out ActiveCalls call)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this.GetHashCode(), "In ValidCallDataToAdd", new object[0]);
				bool result;
				try
				{
					call = null;
					if (this.isDialPlan)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this.GetHashCode(), "In ValidCallDataToAdd, dialPlan = {0}", new object[]
						{
							this.dialPlan
						});
						if (string.Compare(cs.CurrentCallContext.DialPlan.Name, this.dialPlan, StringComparison.InvariantCultureIgnoreCase) != 0)
						{
							return false;
						}
					}
					else if (this.isIpGateway)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.DiagnosticTracer, this.GetHashCode(), "In ValidCallDataToAdd, IpGateway = {0}", new object[]
						{
							this.ipGateway
						});
						if (!cs.CurrentCallContext.GatewayDetails.Equals(new UMSmartHost(this.ipGateway)))
						{
							return false;
						}
					}
					call = new ActiveCalls();
					call.GatewayId = cs.CurrentCallContext.GatewayDetails.ToString();
					call.ServerId = Utils.GetLocalHostName();
					call.DialPlan = cs.CurrentCallContext.DialPlan.Name;
					call.DialedNumber = cs.CurrentCallContext.CalleeId.ToDial;
					call.CallType = cs.TaskCallType.ToString();
					call.CallingNumber = cs.CurrentCallContext.CallerId.ToDial;
					call.DiversionNumber = cs.CurrentCallContext.Extension;
					call.CallState = cs.State.ToString();
					call.AppState = cs.AppState.ToString();
					result = true;
				}
				catch (NullReferenceException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.DiagnosticTracer, this.GetHashCode(), "Exception in ValidCallDataToAdd , error ={0}", new object[]
					{
						ex.ToString()
					});
					call = null;
					result = false;
				}
				return result;
			}

			// Token: 0x04000B78 RID: 2936
			private bool isDialPlan;

			// Token: 0x04000B79 RID: 2937
			private string dialPlan;

			// Token: 0x04000B7A RID: 2938
			private bool isIpGateway;

			// Token: 0x04000B7B RID: 2939
			private string ipGateway;
		}
	}
}
