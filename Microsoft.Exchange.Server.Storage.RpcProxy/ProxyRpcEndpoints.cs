using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.Rpc.ExchangeServer;
using Microsoft.Exchange.Rpc.PoolRpc;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.WorkerManager;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000020 RID: 32
	public sealed class ProxyRpcEndpoints
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		private ProxyRpcEndpoints(RpcInstanceManager rpcManager, ProxySessionManager sessionManager, ProxyAdminRpcServer adminServer, ProxyPoolRpcServer poolRpcServer, ProxyMapiRpcServer mapiRpcServer)
		{
			this.rpcManager = rpcManager;
			this.sessionManager = sessionManager;
			this.adminRpcServer = adminServer;
			this.poolRpcServer = poolRpcServer;
			this.mapiRpcServer = mapiRpcServer;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000C12A File Offset: 0x0000A32A
		private static ProxyRpcEndpoints Instance
		{
			get
			{
				return ProxyRpcEndpoints.instance;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000C134 File Offset: 0x0000A334
		public static bool Initialize(int nonRecoveryDatabasesMax, int recoveryDatabasesMax, int activeDatabasesMax)
		{
			bool flag = false;
			if (ProxyRpcEndpoints.instance == null)
			{
				try
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Initializing RPC global server.");
					string[] array = new string[2];
					string[] array2 = new string[2];
					array[0] = "ncalrpc";
					array2[0] = null;
					array[1] = "ncacn_ip_tcp";
					array2[1] = null;
					RpcServerBase.StartGlobalServer(array, array2, ProxyRpcEndpoints.GetRpcThreadCount());
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Initializing RPC instance manager.");
					RpcInstanceManager manager = new RpcInstanceManager(WorkerManager.Instance, nonRecoveryDatabasesMax, recoveryDatabasesMax, activeDatabasesMax);
					ProxySessionManager manager2 = new ProxySessionManager(manager);
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Initializing RPC endpoints.");
					ProxyRpcEndpoints.instance = new ProxyRpcEndpoints(manager, manager2, new ProxyAdminRpcServer(manager), new ProxyPoolRpcServer(manager2), new ProxyMapiRpcServer(manager2));
					flag = ProxyRpcEndpoints.instance.StartInterfaces();
				}
				catch (DuplicateRpcEndpointException ex)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
					if (Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder(200);
						stringBuilder.Append("Duplicate RPC endpoint detected. [Exception=");
						stringBuilder.Append(ex.ToString());
						stringBuilder.Append("]");
						Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, stringBuilder.ToString());
					}
				}
				finally
				{
					if (!flag)
					{
						ProxyRpcEndpoints.Terminate();
					}
				}
			}
			return flag;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000C29C File Offset: 0x0000A49C
		public static void Terminate()
		{
			if (ProxyRpcEndpoints.instance != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping pool RPC server.");
				ProxyRpcEndpoints.instance.poolRpcServer.StopAcceptingClientRequests();
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping RPC session manager.");
				ProxyRpcEndpoints.instance.sessionManager.StopAcceptingClientRequests();
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping RPC manager.");
				ProxyRpcEndpoints.instance.rpcManager.StopAcceptingCalls();
				ProxyRpcEndpoints.instance.StopInterfaces();
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping RPC global server.");
				RpcServerBase.StopGlobalServer();
				ProxyRpcEndpoints.instance = null;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000C33C File Offset: 0x0000A53C
		private static int GetAdminInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
		{
			instance = null;
			ProxyRpcEndpoints proxyRpcEndpoints = ProxyRpcEndpoints.Instance;
			if (proxyRpcEndpoints == null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy.ExTraceGlobals.ProxyAdminTracer.TraceError(0L, "Proxy RPC endpoint is not initialized. RPC call rejected.");
				return -2147221227;
			}
			instance = proxyRpcEndpoints.adminRpcServer;
			return (int)ErrorCode.NoError;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000C380 File Offset: 0x0000A580
		private static int GetPoolInterfaceInstance(Guid instanceGuid, out IPoolRpcServer instance)
		{
			instance = null;
			ProxyRpcEndpoints proxyRpcEndpoints = ProxyRpcEndpoints.Instance;
			if (proxyRpcEndpoints == null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy.ExTraceGlobals.ProxyMapiTracer.TraceError(0L, "Proxy RPC endpoint is not initialized. RPC call rejected.");
				return -2147221227;
			}
			instance = proxyRpcEndpoints.poolRpcServer;
			return (int)ErrorCode.NoError;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private static void PoolConnectionDropped(IntPtr contextHandle)
		{
			ProxyRpcEndpoints proxyRpcEndpoints = ProxyRpcEndpoints.Instance;
			if (proxyRpcEndpoints != null)
			{
				proxyRpcEndpoints.poolRpcServer.EcPoolDisconnect(contextHandle);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		private static IProxyServer GetMTInterfaceInstace()
		{
			ProxyRpcEndpoints proxyRpcEndpoints = ProxyRpcEndpoints.Instance;
			if (proxyRpcEndpoints == null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy.ExTraceGlobals.ProxyMapiTracer.TraceError(0L, "Proxy RPC endpoint is not initialized. RPC call rejected.");
				throw new FailRpcException("EmsmdbMT interface not started", -2147221227);
			}
			return proxyRpcEndpoints.mapiRpcServer;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000C428 File Offset: 0x0000A628
		private static uint GetRpcThreadCount()
		{
			int? maximumRpcThreadCount = ((IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory).GetMaximumRpcThreadCount(NullExecutionContext.Instance);
			if (maximumRpcThreadCount == null || maximumRpcThreadCount.Value <= 0)
			{
				return 500U;
			}
			return (uint)maximumRpcThreadCount.Value;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000C46C File Offset: 0x0000A66C
		private bool StartInterfaces()
		{
			bool flag = false;
			if (this.admin20server == null)
			{
				try
				{
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting admin20 RPC interface.");
					this.admin20server = (ProxyRpcEndpoints.Admin20RpcServerEndpoint)RpcServerBase.RegisterInterface(typeof(ProxyRpcEndpoints.Admin20RpcServerEndpoint), true, true, "Exchange Server STORE Admin20 Proxy Interface");
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting admin40 RPC interface.");
					this.admin40server = (ProxyRpcEndpoints.Admin40RpcServerEndpoint)RpcServerBase.RegisterInterface(typeof(ProxyRpcEndpoints.Admin40RpcServerEndpoint), true, true, "Exchange Server STORE Admin40 Proxy Interface");
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting admin50 RPC interface.");
					this.admin50server = (ProxyRpcEndpoints.Admin50RpcServerEndpoint)RpcServerBase.RegisterInterface(typeof(ProxyRpcEndpoints.Admin50RpcServerEndpoint), true, true, "Exchange Server STORE Admin50 Proxy Interface");
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting emsmdbPool RPC interface.");
					this.poolServer = (ProxyRpcEndpoints.PoolRpcServerEndpoint)RpcServerBase.RegisterInterface(typeof(ProxyRpcEndpoints.PoolRpcServerEndpoint), true, true, "Exchange Server STORE EmsmdbPool Proxy Interface");
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting emsmdbPoolNotify RPC interface.");
					this.poolNotifyServer = (ProxyRpcEndpoints.PoolNotifyRpcServerEndpoint)RpcServerBase.RegisterAutoListenInterface(typeof(ProxyRpcEndpoints.PoolNotifyRpcServerEndpoint), 65536, false, true, "Exchange Server STORE EmsmdbPoolNotify Proxy Interface");
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting emsmdbMT RPC interface.");
					this.mapiServer = (ProxyRpcEndpoints.MapiMTRpcServerEndpoint)RpcServerBase.RegisterInterface(typeof(ProxyRpcEndpoints.MapiMTRpcServerEndpoint), true, "Exchange Server STORE EmsmdbMT Proxy Interface");
					Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Starting emsmdbMTAsync RPC interface.");
					this.asyncMapiServer = (ProxyRpcEndpoints.AsyncMapiMTRpcServerEndpoint)RpcServerBase.RegisterAutoListenInterface(typeof(ProxyRpcEndpoints.AsyncMapiMTRpcServerEndpoint), 65536);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.StopInterfaces();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000C614 File Offset: 0x0000A814
		private void StopInterfaces()
		{
			if (this.admin20server != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping admin20 RPC interface.");
				RpcServerBase.UnregisterInterface(Admin20RpcServer.RpcIntfHandle, true);
				this.admin20server = null;
			}
			if (this.admin40server != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping admin40 RPC interface.");
				RpcServerBase.UnregisterInterface(Admin40RpcServer.RpcIntfHandle, true);
				this.admin40server = null;
			}
			if (this.admin50server != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping admin50 RPC interface.");
				RpcServerBase.UnregisterInterface(Admin50RpcServer.RpcIntfHandle, true);
				this.admin50server = null;
			}
			if (this.poolServer != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping emsmdbPool RPC interface.");
				RpcServerBase.UnregisterInterface(PoolRpcServerBase.RpcIntfHandle, true);
				this.poolServer = null;
			}
			if (this.poolNotifyServer != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping emsmdbPoolNotify RPC interface.");
				RpcServerBase.UnregisterInterface(PoolNotifyRpcServerBase.RpcIntfHandle, true);
				this.poolNotifyServer = null;
			}
			if (this.mapiServer != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping emsmdbMT RPC interface.");
				RpcServerBase.UnregisterInterface(ExchangeRpcServerMT.RpcIntfHandle, true);
				this.mapiServer = null;
			}
			if (this.asyncMapiServer != null)
			{
				Microsoft.Exchange.Diagnostics.Components.ManagedStore.Service.ExTraceGlobals.StartupShutdownTracer.TraceDebug(0L, "Stopping asyncemsmdbMT RPC interface.");
				RpcServerBase.UnregisterInterface(ExchangeRpcServerMTAsync.RpcIntfHandle, true);
				this.asyncMapiServer = null;
			}
		}

		// Token: 0x04000086 RID: 134
		private const int MaximumMTConnections = 65536;

		// Token: 0x04000087 RID: 135
		private const int MaximumNotificationCalls = 65536;

		// Token: 0x04000088 RID: 136
		private const uint DefaultMaximumRpcThreads = 500U;

		// Token: 0x04000089 RID: 137
		private const string ProtocolLRPC = "ncalrpc";

		// Token: 0x0400008A RID: 138
		private const string ProtocolTCP = "ncacn_ip_tcp";

		// Token: 0x0400008B RID: 139
		private static ProxyRpcEndpoints instance;

		// Token: 0x0400008C RID: 140
		private RpcInstanceManager rpcManager;

		// Token: 0x0400008D RID: 141
		private ProxySessionManager sessionManager;

		// Token: 0x0400008E RID: 142
		private ProxyRpcEndpoints.Admin20RpcServerEndpoint admin20server;

		// Token: 0x0400008F RID: 143
		private ProxyRpcEndpoints.Admin40RpcServerEndpoint admin40server;

		// Token: 0x04000090 RID: 144
		private ProxyRpcEndpoints.Admin50RpcServerEndpoint admin50server;

		// Token: 0x04000091 RID: 145
		private ProxyRpcEndpoints.PoolRpcServerEndpoint poolServer;

		// Token: 0x04000092 RID: 146
		private ProxyRpcEndpoints.PoolNotifyRpcServerEndpoint poolNotifyServer;

		// Token: 0x04000093 RID: 147
		private ProxyRpcEndpoints.MapiMTRpcServerEndpoint mapiServer;

		// Token: 0x04000094 RID: 148
		private ProxyRpcEndpoints.AsyncMapiMTRpcServerEndpoint asyncMapiServer;

		// Token: 0x04000095 RID: 149
		private ProxyAdminRpcServer adminRpcServer;

		// Token: 0x04000096 RID: 150
		private ProxyPoolRpcServer poolRpcServer;

		// Token: 0x04000097 RID: 151
		private ProxyMapiRpcServer mapiRpcServer;

		// Token: 0x02000021 RID: 33
		private sealed class Admin20RpcServerEndpoint : Admin20RpcServer
		{
			// Token: 0x06000103 RID: 259 RVA: 0x0000C750 File Offset: 0x0000A950
			public override int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
			{
				return ProxyRpcEndpoints.GetAdminInterfaceInstance(instanceGuid, out instance);
			}
		}

		// Token: 0x02000022 RID: 34
		private sealed class Admin40RpcServerEndpoint : Admin40RpcServer
		{
			// Token: 0x06000105 RID: 261 RVA: 0x0000C761 File Offset: 0x0000A961
			public override int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
			{
				return ProxyRpcEndpoints.GetAdminInterfaceInstance(instanceGuid, out instance);
			}
		}

		// Token: 0x02000023 RID: 35
		private sealed class Admin50RpcServerEndpoint : Admin50RpcServer
		{
			// Token: 0x06000107 RID: 263 RVA: 0x0000C772 File Offset: 0x0000A972
			public override int GetInterfaceInstance(Guid instanceGuid, out IAdminRpcServer instance)
			{
				return ProxyRpcEndpoints.GetAdminInterfaceInstance(instanceGuid, out instance);
			}
		}

		// Token: 0x02000024 RID: 36
		private sealed class PoolRpcServerEndpoint : PoolRpcServerBase
		{
			// Token: 0x06000109 RID: 265 RVA: 0x0000C783 File Offset: 0x0000A983
			public override int GetInterfaceInstance(Guid instanceGuid, out IPoolRpcServer instance)
			{
				return ProxyRpcEndpoints.GetPoolInterfaceInstance(instanceGuid, out instance);
			}

			// Token: 0x0600010A RID: 266 RVA: 0x0000C78C File Offset: 0x0000A98C
			public override void ConnectionDropped(IntPtr contextHandle)
			{
				ProxyRpcEndpoints.PoolConnectionDropped(contextHandle);
			}
		}

		// Token: 0x02000025 RID: 37
		private sealed class PoolNotifyRpcServerEndpoint : PoolNotifyRpcServerBase
		{
			// Token: 0x0600010C RID: 268 RVA: 0x0000C79C File Offset: 0x0000A99C
			public override int GetInterfaceInstance(Guid instanceGuid, out IPoolRpcServer instance)
			{
				return ProxyRpcEndpoints.GetPoolInterfaceInstance(instanceGuid, out instance);
			}

			// Token: 0x0600010D RID: 269 RVA: 0x0000C7A5 File Offset: 0x0000A9A5
			public override void ConnectionDropped(IntPtr contextHandle)
			{
				ProxyRpcEndpoints.PoolConnectionDropped(contextHandle);
			}
		}

		// Token: 0x02000026 RID: 38
		private sealed class MapiMTRpcServerEndpoint : ExchangeRpcServerMT
		{
			// Token: 0x0600010F RID: 271 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
			public override IProxyServer GetProxyServer()
			{
				return ProxyRpcEndpoints.GetMTInterfaceInstace();
			}
		}

		// Token: 0x02000027 RID: 39
		private sealed class AsyncMapiMTRpcServerEndpoint : ExchangeRpcServerMTAsync
		{
			// Token: 0x06000111 RID: 273 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
			public override IProxyServer GetProxyServer()
			{
				return ProxyRpcEndpoints.GetMTInterfaceInstace();
			}
		}
	}
}
