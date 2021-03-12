using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration;
using Microsoft.Exchange.RpcClientAccess.Messages;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200003F RID: 63
	internal sealed class RpcHttpConnectionRegistrationAsyncServer : RpcHttpConnectionRegistrationAsyncRpcServer
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000C9B6 File Offset: 0x0000ABB6
		internal static void Initialize(IRpcHttpConnectionRegistrationAsyncDispatch rpcHttpConnectionRegistrationAsyncDispatch, int maximumConcurrentCalls, ExEventLog eventLog)
		{
			Util.ThrowOnNullArgument(rpcHttpConnectionRegistrationAsyncDispatch, "rpcHttpConnectionRegistrationAsyncDispatch");
			Util.ThrowOnNullArgument(eventLog, "eventLog");
			RpcHttpConnectionRegistrationAsyncServer.rpcHttpConnectionRegistrationAsyncDispatch = rpcHttpConnectionRegistrationAsyncDispatch;
			RpcHttpConnectionRegistrationAsyncServer.eventLog = eventLog;
			RpcHttpConnectionRegistrationAsyncServer.maximumConcurrentCalls = maximumConcurrentCalls;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		internal static void Start()
		{
			bool flag = false;
			if (RpcHttpConnectionRegistrationAsyncServer.server == null)
			{
				try
				{
					RpcHttpConnectionRegistrationAsyncServer.server = (RpcHttpConnectionRegistrationAsyncServer)RpcServerBase.RegisterAutoListenInterface(typeof(RpcHttpConnectionRegistrationAsyncServer), RpcHttpConnectionRegistrationAsyncServer.CreateSecurityDescriptor(), RpcHttpConnectionRegistrationAsyncServer.maximumConcurrentCalls, true, true, null, true, false, false);
					flag = true;
				}
				catch (DuplicateRpcEndpointException ex)
				{
					RpcHttpConnectionRegistrationAsyncServer.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_DuplicateRpcEndpoint, string.Empty, new object[]
					{
						ex.Message
					});
					throw new RpcServiceAbortException("RpcHttpConnectionRegistrationAsyncServer is being aborted the service due to DuplicateRpcEndpointException", ex);
				}
				finally
				{
					if (!flag)
					{
						RpcHttpConnectionRegistrationAsyncServer.Stop();
						RpcHttpConnectionRegistrationAsyncServer.rpcHttpConnectionRegistrationAsyncDispatch = null;
					}
				}
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CA84 File Offset: 0x0000AC84
		internal static void Stop()
		{
			if (RpcHttpConnectionRegistrationAsyncServer.server != null)
			{
				RpcServerBase.UnregisterInterface(RpcHttpConnectionRegistrationAsyncRpcServer.RpcIntfHandle, true);
				RpcHttpConnectionRegistrationAsyncServer.server = null;
				RpcHttpConnectionRegistration.Instance.Clear();
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		private static FileSecurity CreateSecurityDescriptor()
		{
			FileSecurity fileSecurity = new FileSecurity();
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			FileSystemAccessRule rule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			FileSystemAccessRule rule2 = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.AddAccessRule(rule);
			fileSecurity.AddAccessRule(rule2);
			return fileSecurity;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000CAF7 File Offset: 0x0000ACF7
		public override IRpcHttpConnectionRegistrationAsyncDispatch GetRpcHttpConnectionRegistrationAsyncDispatch()
		{
			return RpcHttpConnectionRegistrationAsyncServer.rpcHttpConnectionRegistrationAsyncDispatch;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000CAFE File Offset: 0x0000ACFE
		public override bool IsShuttingDown()
		{
			return RpcClientAccessService.IsShuttingDown;
		}

		// Token: 0x0400011D RID: 285
		private static RpcHttpConnectionRegistrationAsyncServer server = null;

		// Token: 0x0400011E RID: 286
		private static IRpcHttpConnectionRegistrationAsyncDispatch rpcHttpConnectionRegistrationAsyncDispatch = null;

		// Token: 0x0400011F RID: 287
		private static int maximumConcurrentCalls;

		// Token: 0x04000120 RID: 288
		private static ExEventLog eventLog;
	}
}
