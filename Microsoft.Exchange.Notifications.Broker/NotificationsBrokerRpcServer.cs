using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.NotificationsBroker;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001B RID: 27
	internal sealed class NotificationsBrokerRpcServer : NotificationsBrokerAsyncRpcServer
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00007474 File Offset: 0x00005674
		public static void Start()
		{
			if (NotificationsBrokerRpcServer.instance == null)
			{
				bool flag = false;
				try
				{
					NotificationsBrokerRpcServer.asyncDispatch = new NotificationsBrokerAsyncDispatch();
					FileSecurity fileSecurity = new FileSecurity();
					SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
					FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
					fileSecurity.SetOwner(securityIdentifier);
					fileSecurity.SetAccessRule(accessRule);
					SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
					FileSystemAccessRule rule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
					fileSecurity.AddAccessRule(rule);
					NotificationsBrokerRpcServer.instance = (NotificationsBrokerRpcServer)RpcServerBase.RegisterServer(typeof(NotificationsBrokerRpcServer), fileSecurity, 1, true);
					flag = true;
				}
				catch (RpcException ex)
				{
					ExTraceGlobals.ServiceTracer.TraceError<string>(0L, "Error registering the Notifications Broker RPC interface: {0}", ex.Message);
					NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_RpcRegisterInterfaceFailure, string.Empty, new object[]
					{
						string.Format("0x{1:X8}", ex.ErrorCode)
					});
				}
				finally
				{
					if (!flag)
					{
						NotificationsBrokerRpcServer.asyncDispatch = null;
						NotificationsBrokerRpcServer.Stop();
						NotificationsBrokerRpcServer.instance = null;
					}
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000757C File Offset: 0x0000577C
		public static void Stop()
		{
			if (NotificationsBrokerRpcServer.instance != null)
			{
				NotificationsBrokerRpcServer.asyncDispatch.StopGetNextNotificationCalls();
				RpcServerBase.UnregisterInterface(NotificationsBrokerAsyncRpcServer.RpcIntfHandle, false);
				NotificationsBrokerRpcServer.instance = null;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000075A0 File Offset: 0x000057A0
		public override INotificationsBrokerAsyncDispatch GetAsyncDispatch()
		{
			return NotificationsBrokerRpcServer.asyncDispatch;
		}

		// Token: 0x0400007E RID: 126
		private static NotificationsBrokerRpcServer instance;

		// Token: 0x0400007F RID: 127
		private static NotificationsBrokerAsyncDispatch asyncDispatch;
	}
}
