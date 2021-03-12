using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000005 RID: 5
	public sealed class AdminRpcEndpoint : IAdminRpcEndpoint
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002283 File Offset: 0x00000483
		private AdminRpcEndpoint()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000228B File Offset: 0x0000048B
		public static IAdminRpcEndpoint Instance
		{
			get
			{
				return AdminRpcEndpoint.hookableInstance.Value;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002297 File Offset: 0x00000497
		public static int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
		{
			instance = Globals.AdminRpcServer;
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022AC File Offset: 0x000004AC
		public bool StartInterface(Guid? instanceGuid, bool isLocalOnly)
		{
			bool flag = false;
			if (this.admin20server == null)
			{
				try
				{
					this.admin20server = (AdminRpcEndpoint.Admin20RpcServerEndpoint)AdminRpcServerBase.RegisterServerInstance(typeof(AdminRpcEndpoint.Admin20RpcServerEndpoint), instanceGuid, isLocalOnly, "Exchange Server STORE Admin20 Interface");
					this.admin40server = (AdminRpcEndpoint.Admin40RpcServerEndpoint)AdminRpcServerBase.RegisterServerInstance(typeof(AdminRpcEndpoint.Admin40RpcServerEndpoint), instanceGuid, isLocalOnly, "Exchange Server STORE Admin40 Interface");
					this.admin50server = (AdminRpcEndpoint.Admin50RpcServerEndpoint)AdminRpcServerBase.RegisterServerInstance(typeof(AdminRpcEndpoint.Admin50RpcServerEndpoint), instanceGuid, isLocalOnly, "Exchange Server STORE Admin50 Interface");
					flag = true;
				}
				catch (DuplicateRpcEndpointException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
					Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DuplicateAdminRpcEndpoint, new object[]
					{
						(instanceGuid != null) ? instanceGuid.Value : Guid.Empty,
						DiagnosticsNativeMethods.GetCurrentProcessId().ToString()
					});
				}
				finally
				{
					if (!flag)
					{
						this.StopInterface();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023A8 File Offset: 0x000005A8
		public void StopInterface()
		{
			if (this.admin20server != null)
			{
				RpcServerBase.UnregisterInterface(Admin20RpcServer.RpcIntfHandle, true);
				this.admin20server = null;
			}
			if (this.admin40server != null)
			{
				RpcServerBase.UnregisterInterface(Admin40RpcServer.RpcIntfHandle, true);
				this.admin40server = null;
			}
			if (this.admin50server != null)
			{
				RpcServerBase.UnregisterInterface(Admin50RpcServer.RpcIntfHandle, true);
				this.admin50server = null;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002403 File Offset: 0x00000603
		internal static IDisposable SetTestHook(IAdminRpcEndpoint testHook)
		{
			return AdminRpcEndpoint.SetTestHook(testHook);
		}

		// Token: 0x0400003B RID: 59
		private static Hookable<IAdminRpcEndpoint> hookableInstance = Hookable<IAdminRpcEndpoint>.Create(false, new AdminRpcEndpoint());

		// Token: 0x0400003C RID: 60
		private AdminRpcEndpoint.Admin20RpcServerEndpoint admin20server;

		// Token: 0x0400003D RID: 61
		private AdminRpcEndpoint.Admin40RpcServerEndpoint admin40server;

		// Token: 0x0400003E RID: 62
		private AdminRpcEndpoint.Admin50RpcServerEndpoint admin50server;

		// Token: 0x02000006 RID: 6
		private sealed class Admin20RpcServerEndpoint : Admin20RpcServer
		{
			// Token: 0x06000017 RID: 23 RVA: 0x0000241D File Offset: 0x0000061D
			public override int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
			{
				return AdminRpcEndpoint.GetInterfaceInstance(instanceGuid, out instance);
			}
		}

		// Token: 0x02000007 RID: 7
		private sealed class Admin40RpcServerEndpoint : Admin40RpcServer
		{
			// Token: 0x06000019 RID: 25 RVA: 0x0000242E File Offset: 0x0000062E
			public override int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
			{
				return AdminRpcEndpoint.GetInterfaceInstance(instanceGuid, out instance);
			}
		}

		// Token: 0x02000008 RID: 8
		private sealed class Admin50RpcServerEndpoint : Admin50RpcServer
		{
			// Token: 0x0600001B RID: 27 RVA: 0x0000243F File Offset: 0x0000063F
			public override int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
			{
				return AdminRpcEndpoint.GetInterfaceInstance(instanceGuid, out instance);
			}
		}
	}
}
