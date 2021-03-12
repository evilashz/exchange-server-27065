using System;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x02000100 RID: 256
	internal class ExRpcMwiTarget : MwiTargetBase
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00045551 File Offset: 0x00043751
		internal ExRpcMwiTarget(Server server) : base(server, ExRpcMwiTarget.instanceNameSuffix)
		{
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00045560 File Offset: 0x00043760
		public override void SendMessageAsync(MwiMessage message)
		{
			base.SendMessageAsync(message);
			ExRpcMwiTarget.MwiRpcAsyncState mwiRpcAsyncState = new ExRpcMwiTarget.MwiRpcAsyncState(message, new ExRpcMwiTarget.DoRpcDelegate(this.DoRpc));
			mwiRpcAsyncState.RpcDelegate.BeginInvoke(message, new AsyncCallback(this.RpcCompleted), mwiRpcAsyncState);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000455A4 File Offset: 0x000437A4
		private void DoRpc(MwiMessage message)
		{
			int num = 3;
			Server server = (Server)base.ConfigObject;
			while (num-- > 0)
			{
				string text = string.Empty;
				try
				{
					using (UMMwiDeliveryRpcClient ummwiDeliveryRpcClient = new UMMwiDeliveryRpcClient(server.Fqdn))
					{
						text = ummwiDeliveryRpcClient.OperationName;
						ummwiDeliveryRpcClient.SetTimeOut(10000);
						int assistantLatencyMsec = (int)(ExDateTime.UtcNow.Subtract(message.EventTimeUtc).TotalMilliseconds + (double)base.AverageProcessingTimeMsec);
						ExTraceGlobals.RpcTracer.TraceDebug((long)this.GetHashCode(), "ExRpcMwiTarget: Executing {0}. Mailbox:{1} {2} {3}", new object[]
						{
							text,
							message.MailboxGuid,
							message.UserName,
							message.TenantGuid
						});
						ummwiDeliveryRpcClient.SendMwiMessage(message.MailboxGuid, message.DialPlanGuid, message.UserExtension, message.UserName, message.UnreadVoicemailCount, message.TotalVoicemailCount, assistantLatencyMsec, message.TenantGuid);
						ExTraceGlobals.RpcTracer.TraceDebug((long)this.GetHashCode(), "ExRpcMwiTarget: {0} succeeded. Mailbox:{1} {2} {3}", new object[]
						{
							text,
							message.MailboxGuid,
							message.UserName,
							message.TenantGuid
						});
					}
					break;
				}
				catch (RpcException ex)
				{
					ExTraceGlobals.RpcTracer.TraceWarning((long)this.GetHashCode(), "ExRpcMwiTarget: {0} failed. Mailbox:{1} RetryCount:{2} Error:{3} {4}", new object[]
					{
						text,
						message.MailboxGuid,
						num,
						ex.ErrorCode,
						ex
					});
					if (num == 0 || !UMErrorCode.IsNetworkError(ex.ErrorCode))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00045794 File Offset: 0x00043994
		private void RpcCompleted(IAsyncResult result)
		{
			MwiDeliveryException error = null;
			ExRpcMwiTarget.MwiRpcAsyncState mwiRpcAsyncState = null;
			try
			{
				mwiRpcAsyncState = (ExRpcMwiTarget.MwiRpcAsyncState)result.AsyncState;
				mwiRpcAsyncState.RpcDelegate.EndInvoke(result);
			}
			catch (RpcException ex)
			{
				error = new MwiTargetException(base.Name, ex.ErrorCode, ex.Message, ex);
			}
			base.UpdatePerformanceCounters(mwiRpcAsyncState.Message, error);
			mwiRpcAsyncState.Message.CompletionCallback(mwiRpcAsyncState.Message, error);
		}

		// Token: 0x040006CD RID: 1741
		private const int RpcTimeoutMSEC = 10000;

		// Token: 0x040006CE RID: 1742
		private static string instanceNameSuffix = typeof(UMServer).Name;

		// Token: 0x02000101 RID: 257
		// (Invoke) Token: 0x06000A94 RID: 2708
		private delegate void DoRpcDelegate(MwiMessage message);

		// Token: 0x02000102 RID: 258
		private class MwiRpcAsyncState
		{
			// Token: 0x06000A97 RID: 2711 RVA: 0x00045826 File Offset: 0x00043A26
			internal MwiRpcAsyncState(MwiMessage message, ExRpcMwiTarget.DoRpcDelegate rpcDelegate)
			{
				this.message = message;
				this.rpcDelegate = rpcDelegate;
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0004583C File Offset: 0x00043A3C
			internal MwiMessage Message
			{
				get
				{
					return this.message;
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00045844 File Offset: 0x00043A44
			internal ExRpcMwiTarget.DoRpcDelegate RpcDelegate
			{
				get
				{
					return this.rpcDelegate;
				}
			}

			// Token: 0x040006CF RID: 1743
			private MwiMessage message;

			// Token: 0x040006D0 RID: 1744
			private ExRpcMwiTarget.DoRpcDelegate rpcDelegate;
		}
	}
}
