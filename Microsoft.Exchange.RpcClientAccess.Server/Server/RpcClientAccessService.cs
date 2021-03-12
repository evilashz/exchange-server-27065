using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Messages;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000038 RID: 56
	internal sealed class RpcClientAccessService : BaseObject, IRpcService, IDisposable
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00007FDC File Offset: 0x000061DC
		public RpcClientAccessService(IRpcServiceManager serviceManager)
		{
			this.eventLog = RpcClientAccessService.CreateEventLog();
			this.serviceManager = serviceManager;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_StartingRpcClientService, string.Empty, new object[]
				{
					currentProcess.Id,
					"Microsoft Exchange",
					"15.00.1497.010"
				});
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008060 File Offset: 0x00006260
		public string Name
		{
			get
			{
				base.CheckDisposed();
				return "MSExchangeRPC";
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000806D File Offset: 0x0000626D
		internal static bool IsShuttingDown
		{
			get
			{
				return RpcClientAccessService.isShuttingDown;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008074 File Offset: 0x00006274
		public bool IsEnabled()
		{
			base.CheckDisposed();
			return Configuration.ServiceConfiguration.IsServiceEnabled && !Configuration.ServiceConfiguration.IsDisabledOnMailboxRole;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008098 File Offset: 0x00006298
		public void OnStartBegin()
		{
			base.CheckDisposed();
			if (this.isRpcServiceEndpointStarted)
			{
				throw new InvalidOperationException("RpcClientAccessService.OnStartBegin(): This RPC service endpoint is already active! Please check if the RPC Client Access Service is being initialized twice.");
			}
			try
			{
				this.LogInitializationCheckPoint("RegisterServiceClass");
				ServiceHelper.RegisterSPN("exchangeMDB", this.eventLog, RpcClientAccessServiceEventLogConstants.Tuple_SpnRegisterFailure);
				RpcClientAccessPerformanceCountersWrapper.Initialize(new RcaPerformanceCounters(), new RpcHttpConnectionRegistrationPerformanceCounters(), new XtcPerformanceCounters());
				this.LogInitializationCheckPoint("ConfigurationManager");
				Configuration.EventLogger = new ConfigurationSchema.EventLogger(this.LogConfigurationEvent);
				this.configurationManager = new ConfigurationManager();
				this.configurationManager.AsyncUnhandledException += this.OnAsyncUnhandledException;
				this.configurationManager.ConfigurationLoadFailed += this.OnConfigurationLoadFailed;
				this.configurationManager.LoadAndRegisterForNotifications();
				ProtocolLog.Initialize();
				this.configurationManager.ConfigurationLoadFailed += this.OnConfigurationUpdateFailed;
				this.configurationManager.ConfigurationLoadFailed -= this.OnConfigurationLoadFailed;
				Configuration.ConfigurationChanged += this.OnConfigurationChanged;
				this.LogInitializationCheckPoint("CanStartServiceOnLocalServer");
				if (this.CanStartServiceOnLocalServer())
				{
					this.LogInitializationCheckPoint("RpcDispatch");
					RopLogon.AuthenticationContextCompression = new AuthContextDecompressor();
					this.rpcInterfaces = new Dictionary<string, RpcClientAccessService.RPCInterfaceContainer>(4);
					this.rpcDispatch = RpcClientAccessService.CreateRpcDispatch();
					this.rpcDispatchPool = RpcClientAccessService.CreateRpcDispatchPool();
					IExchangeDispatch exchangeDispatch = RpcClientAccessService.CreateExchangeDispatch(this.rpcDispatch);
					RpcClientAccessService.RPCInterfaceContainer value = new RpcClientAccessService.RPCInterfaceContainer(exchangeDispatch, RpcClientAccessService.CreateExchangeAsyncDispatch(exchangeDispatch, this.rpcDispatchPool), null);
					this.rpcInterfaces.Add("exchangeDispatch", value);
					RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch = RpcClientAccessService.CreateRpcHttpConnectionRegistrationDispatch();
					DispatchPool dispatchPool = RpcClientAccessService.CreateRpcHttpConnectionRegistrationRpcDispatchPool();
					RpcClientAccessService.RPCInterfaceContainer value2 = new RpcClientAccessService.RPCInterfaceContainer(rpcHttpConnectionRegistrationDispatch, RpcClientAccessService.CreateRpcHttpConnectionRegistrationAsyncDispatch(rpcHttpConnectionRegistrationDispatch, dispatchPool), dispatchPool);
					this.rpcInterfaces.Add("rpcHttpConnectionRegistrationDispatch", value2);
					this.serviceManager.AddHttpPort(6001.ToString());
					this.serviceManager.EnableLrpc();
					RpcServer.Initialize((IExchangeAsyncDispatch)this.rpcInterfaces["exchangeDispatch"].AsyncDispatchInterface, this.rpcDispatch.MaximumConnections, this.eventLog);
					RpcAsynchronousServer.Initialize((IExchangeAsyncDispatch)this.rpcInterfaces["exchangeDispatch"].AsyncDispatchInterface, this.rpcDispatch.MaximumConnections, this.eventLog);
					RpcHttpConnectionRegistrationAsyncServer.Initialize((IRpcHttpConnectionRegistrationAsyncDispatch)this.rpcInterfaces["rpcHttpConnectionRegistrationDispatch"].AsyncDispatchInterface, this.rpcDispatch.MaximumConnections, this.eventLog);
					this.serviceManager.AddServer(new Action(RpcAsynchronousServer.Start), new Action(RpcAsynchronousServer.Stop));
					this.serviceManager.AddServer(new Action(RpcServer.Start), new Action(RpcServer.Stop));
					this.serviceManager.AddServer(new Action(RpcHttpConnectionRegistrationAsyncServer.Start), new Action(RpcHttpConnectionRegistrationAsyncServer.Stop));
				}
				this.isRpcServiceEndpointStarted = true;
			}
			finally
			{
				if (!this.isRpcServiceEndpointStarted)
				{
					this.CleanUpInternalRpcEndpointState();
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008394 File Offset: 0x00006594
		public void OnStartEnd()
		{
			base.CheckDisposed();
			if (this.isWebServiceEndpointStarted)
			{
				throw new InvalidOperationException("RpcClientAccessService.OnStartEnd(): This web service endpoint is already active! Please check if the RPC Client Access Service is being initialized twice.");
			}
			if (Configuration.ServiceConfiguration.EnableWebServicesEndpoint)
			{
				try
				{
					this.LogInitializationCheckPoint("WebServiceEndPoint.Start");
					DispatchPool dispatchPool = RpcClientAccessService.CreateWebServiceDispatchPool();
					IExchangeAsyncDispatch exchangeAsyncDispatch = RpcClientAccessService.CreateExchangeAsyncDispatch((IExchangeDispatch)this.rpcInterfaces["exchangeDispatch"].DispatchInterface, dispatchPool);
					RpcClientAccessService.RPCInterfaceContainer value = new RpcClientAccessService.RPCInterfaceContainer(null, exchangeAsyncDispatch, dispatchPool);
					this.rpcInterfaces.Add("webserviceendpoint", value);
					VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
					string arg = snapshot.RpcClientAccess.XtcEndpoint.Enabled ? "444" : "443";
					string endpoint = string.Format("https://localhost:{0}/xrop", arg);
					WebServiceEndPoint.Start(exchangeAsyncDispatch, endpoint, this.eventLog);
					this.isWebServiceEndpointStarted = true;
				}
				finally
				{
					if (!this.isWebServiceEndpointStarted)
					{
						this.CleanUpInternalWebEndpointState();
					}
				}
			}
			if (Configuration.ServiceConfiguration.CanServicePrivateLogons)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientAccessServiceStartPrivateSuccess, string.Empty, new object[0]);
			}
			if (Configuration.ServiceConfiguration.CanServicePublicLogons)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientAccessServiceStartPublicSuccess, string.Empty, new object[0]);
			}
			this.checkExchangeRpcServiceResponsive = new CheckExchangeRpcServiceResponsive(this.eventLog);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000084EC File Offset: 0x000066EC
		public void OnStopBegin()
		{
			base.CheckDisposed();
			this.CleanUpInternalRpcEndpointState();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000084FC File Offset: 0x000066FC
		public void OnStopEnd()
		{
			base.CheckDisposed();
			this.CleanUpInternalWebEndpointState();
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientAccessServiceStopSuccess, string.Empty, new object[]
				{
					currentProcess.Id,
					"Microsoft Exchange",
					"15.00.1497.010"
				});
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008574 File Offset: 0x00006774
		public void HandleUnexpectedExceptionOnStart(Exception ex)
		{
			base.CheckDisposed();
			if (ex is DuplicateRpcEndpointException)
			{
				this.LogExceptionEvent(RpcClientAccessServiceEventLogConstants.Tuple_DuplicateRpcEndpoint, ex);
				return;
			}
			this.LogExceptionEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientServiceUnexpectedExceptionOnStart, ex);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000859D File Offset: 0x0000679D
		public void HandleUnexpectedExceptionOnStop(Exception ex)
		{
			base.CheckDisposed();
			this.LogExceptionEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientServiceUnexpectedExceptionOnStop, ex);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000085B1 File Offset: 0x000067B1
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RpcClientAccessService>(this);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000085BC File Offset: 0x000067BC
		protected override void InternalDispose()
		{
			if (this.rpcInterfaces != null)
			{
				foreach (RpcClientAccessService.RPCInterfaceContainer rpcinterfaceContainer in this.rpcInterfaces.Values)
				{
					rpcinterfaceContainer.Dispose();
				}
				this.rpcInterfaces = null;
			}
			Util.DisposeIfPresent(this.rpcDispatch);
			Util.DisposeIfPresent(this.configurationManager);
			Util.DisposeIfPresent(this.checkExchangeRpcServiceResponsive);
			ProtocolLog.Shutdown();
			base.InternalDispose();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008650 File Offset: 0x00006850
		private static ExEventLog CreateEventLog()
		{
			return new ExEventLog(RpcClientAccessService.ComponentGuid, "MSExchangeRPC");
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008664 File Offset: 0x00006864
		private static IRpcDispatch CreateRpcDispatch()
		{
			IRpcDispatch result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				RpcDispatch rpcDispatch = new RpcDispatch(ConnectionHandler.Factory, new DriverFactory());
				disposeGuard.Add<RpcDispatch>(rpcDispatch);
				IRpcDispatch rpcDispatch2 = new WatsonOnUnhandledExceptionDispatch(rpcDispatch);
				disposeGuard.Success();
				result = rpcDispatch2;
			}
			return result;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000086C4 File Offset: 0x000068C4
		private static IExchangeDispatch CreateExchangeDispatch(IRpcDispatch rpcDispatch)
		{
			return new ExchangeDispatch(rpcDispatch);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000086CC File Offset: 0x000068CC
		private static IExchangeAsyncDispatch CreateExchangeAsyncDispatch(IExchangeDispatch exchangeDispatch, DispatchPool dispatchPool)
		{
			return new ExchangeAsyncDispatch(exchangeDispatch, dispatchPool);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000086D5 File Offset: 0x000068D5
		private static RpcHttpConnectionRegistrationDispatch CreateRpcHttpConnectionRegistrationDispatch()
		{
			return new RpcHttpConnectionRegistrationDispatch(RpcHttpConnectionRegistration.Instance);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000086E1 File Offset: 0x000068E1
		private static IRpcHttpConnectionRegistrationAsyncDispatch CreateRpcHttpConnectionRegistrationAsyncDispatch(RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch, DispatchPool dispatchPool)
		{
			return new RpcHttpConnectionRegistrationAsyncDispatch(rpcHttpConnectionRegistrationDispatch, dispatchPool);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000086EC File Offset: 0x000068EC
		private static DispatchPool CreateRpcDispatchPool()
		{
			return new DispatchPool("RpcDispatchPoolThread", Configuration.ServiceConfiguration.MaximumRpcTasks, Configuration.ServiceConfiguration.MaximumRpcThreads, Configuration.ServiceConfiguration.MinimumRpcThreads, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskQueueLength, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskThreads, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskActiveThreads, RpcClientAccessPerformanceCountersWrapper.RcaPerformanceCounters.DispatchTaskOperationsRate);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000874C File Offset: 0x0000694C
		private static DispatchPool CreateRpcHttpConnectionRegistrationRpcDispatchPool()
		{
			return new DispatchPool("RpcHttpConnectionRegistrationDispatchPoolThread", Configuration.ServiceConfiguration.MaximumRpcHttpConnectionRegistrationTasks, Configuration.ServiceConfiguration.MaximumRpcHttpConnectionRegistrationThreads, Configuration.ServiceConfiguration.MinimumRpcHttpConnectionRegistrationThreads, RpcClientAccessPerformanceCountersWrapper.RpcHttpConnectionRegistrationPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskQueueLength, RpcClientAccessPerformanceCountersWrapper.RpcHttpConnectionRegistrationPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskThreads, RpcClientAccessPerformanceCountersWrapper.RpcHttpConnectionRegistrationPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskActiveThreads, RpcClientAccessPerformanceCountersWrapper.RpcHttpConnectionRegistrationPerformanceCounters.RpcHttpConnectionRegistrationDispatchTaskOperationsRate);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000087AC File Offset: 0x000069AC
		private static DispatchPool CreateWebServiceDispatchPool()
		{
			return new DispatchPool("XtcDispatchPoolThread", Configuration.ServiceConfiguration.MaximumWebServiceTasks, Configuration.ServiceConfiguration.MaximumWebServiceThreads, Configuration.ServiceConfiguration.MinimumWebServiceThreads, RpcClientAccessPerformanceCountersWrapper.XtcPerformanceCounters.XTCDispatchTaskQueueLength, RpcClientAccessPerformanceCountersWrapper.XtcPerformanceCounters.XTCDispatchTaskThreads, RpcClientAccessPerformanceCountersWrapper.XtcPerformanceCounters.XTCDispatchTaskActiveThreads, RpcClientAccessPerformanceCountersWrapper.XtcPerformanceCounters.XTCDispatchTaskOperationsRate);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000880C File Offset: 0x00006A0C
		private void CleanUpInternalRpcEndpointState()
		{
			RpcClientAccessService.isShuttingDown = true;
			WebServiceEndPoint.IsShuttingDown = true;
			Util.DisposeIfPresent(this.checkExchangeRpcServiceResponsive);
			if (this.rpcInterfaces != null)
			{
				foreach (RpcClientAccessService.RPCInterfaceContainer rpcinterfaceContainer in this.rpcInterfaces.Values)
				{
					if (rpcinterfaceContainer.DispatchPool != null)
					{
						ExDateTime now = ExDateTime.Now;
						while (ExDateTime.Now - now < RpcClientAccessService.waitDrainOnShutdown)
						{
							Thread.Sleep(500);
							if (rpcinterfaceContainer.DispatchPool.ActiveThreads == 0)
							{
								break;
							}
						}
					}
				}
			}
			Util.DisposeIfPresent(this.rpcDispatchPool);
			this.rpcDispatchPool = null;
			if (this.rpcInterfaces != null)
			{
				foreach (RpcClientAccessService.RPCInterfaceContainer rpcinterfaceContainer2 in this.rpcInterfaces.Values)
				{
					rpcinterfaceContainer2.Dispose();
				}
			}
			Util.DisposeIfPresent(this.rpcDispatch);
			this.rpcDispatch = null;
			this.rpcInterfaces = null;
			this.isRpcServiceEndpointStarted = false;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000893C File Offset: 0x00006B3C
		private void CleanUpInternalWebEndpointState()
		{
			if (this.rpcInterfaces != null)
			{
				this.rpcInterfaces.Remove("webserviceendpoint");
			}
			WebServiceEndPoint.Stop();
			this.isWebServiceEndpointStarted = false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008963 File Offset: 0x00006B63
		private void LogInitializationCheckPoint(string phase)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008978 File Offset: 0x00006B78
		private void LogConfigurationEvent(ExEventLog.EventTuple tuple, params object[] args)
		{
			string periodicKey = null;
			if (tuple.Period == ExEventLog.EventPeriod.LogPeriodic)
			{
				periodicKey = args.Aggregate(tuple.EventId.GetHashCode(), (int hashCode, object arg) => hashCode ^= ((arg != null) ? arg.GetHashCode() : 0)).ToString();
			}
			this.eventLog.LogEvent(tuple, periodicKey, args);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000089DC File Offset: 0x00006BDC
		private bool CanStartServiceOnLocalServer()
		{
			ServiceConfiguration serviceConfiguration = Configuration.ServiceConfiguration;
			if (!serviceConfiguration.IsServiceEnabled)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_ServiceProtocolNotEnabled, string.Empty, new object[0]);
				return false;
			}
			if (serviceConfiguration.IsDisabledOnMailboxRole)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_CannotStartServiceOnMailboxRole, string.Empty, new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008A3C File Offset: 0x00006C3C
		private void OnAsyncUnhandledException(Exception ex)
		{
			this.LogExceptionEvent(RpcClientAccessServiceEventLogConstants.Tuple_UnexpectedExceptionOnConfigurationUpdate, ex);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008A4C File Offset: 0x00006C4C
		private void OnConfigurationChanged(object newConfiguration)
		{
			ProtocolLog.Referesh();
			if (this.configurationUpdateError && !this.configurationManager.HasConfigurationsThatFailToUpdate)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_ConfigurationUpdateAfterError, string.Empty, new object[0]);
			}
			if (Configuration.ServiceConfiguration.LogEveryConfigurationUpdate)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_ConfigurationUpdate, string.Empty, new object[0]);
			}
			this.configurationUpdateError = false;
			if (!this.CanStartServiceOnLocalServer())
			{
				this.serviceManager.StopService();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008AD1 File Offset: 0x00006CD1
		private void OnConfigurationLoadFailed(Exception ex)
		{
			this.LogExceptionEvent(RpcClientAccessServiceEventLogConstants.Tuple_ConfigurationLoadFailed, ex);
			throw new RpcServiceAbortException(string.Format("RpcClientAccessService is being aborted due to ConfigurationLoadFailed {0}", ex.Message), ex);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008AF5 File Offset: 0x00006CF5
		private void OnConfigurationUpdateFailed(Exception ex)
		{
			this.LogExceptionEvent(RpcClientAccessServiceEventLogConstants.Tuple_ConfigurationUpdateFailed, ex);
			this.configurationUpdateError = true;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008B0C File Offset: 0x00006D0C
		private void LogExceptionEvent(ExEventLog.EventTuple tuple, Exception ex)
		{
			this.eventLog.LogEvent(tuple, string.Empty, new object[]
			{
				ex
			});
		}

		// Token: 0x040000D3 RID: 211
		internal const string RpcClientAccessServiceName = "MSExchangeRPC";

		// Token: 0x040000D4 RID: 212
		private const string RpcClientAccessServicePrincipalClass = "exchangeMDB";

		// Token: 0x040000D5 RID: 213
		private static readonly Guid ComponentGuid = ServiceConfiguration.ComponentGuid;

		// Token: 0x040000D6 RID: 214
		private static readonly TimeSpan waitDrainOnShutdown = TimeSpan.FromSeconds(15.0);

		// Token: 0x040000D7 RID: 215
		private static bool isShuttingDown = false;

		// Token: 0x040000D8 RID: 216
		private readonly ExEventLog eventLog;

		// Token: 0x040000D9 RID: 217
		private readonly IRpcServiceManager serviceManager;

		// Token: 0x040000DA RID: 218
		private ConfigurationManager configurationManager;

		// Token: 0x040000DB RID: 219
		private bool isRpcServiceEndpointStarted;

		// Token: 0x040000DC RID: 220
		private bool isWebServiceEndpointStarted;

		// Token: 0x040000DD RID: 221
		private IRpcDispatch rpcDispatch;

		// Token: 0x040000DE RID: 222
		private DispatchPool rpcDispatchPool;

		// Token: 0x040000DF RID: 223
		private Dictionary<string, RpcClientAccessService.RPCInterfaceContainer> rpcInterfaces;

		// Token: 0x040000E0 RID: 224
		private bool configurationUpdateError;

		// Token: 0x040000E1 RID: 225
		private CheckExchangeRpcServiceResponsive checkExchangeRpcServiceResponsive;

		// Token: 0x02000039 RID: 57
		private class RPCInterfaceContainer
		{
			// Token: 0x060001C1 RID: 449 RVA: 0x00008B5C File Offset: 0x00006D5C
			public RPCInterfaceContainer(object dispatchInterface, object asyncDispatchInterface, DispatchPool dispatchPool)
			{
				this.dispatchInterface = dispatchInterface;
				this.asyncDispatchInterface = asyncDispatchInterface;
				this.dispatchPool = dispatchPool;
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008B79 File Offset: 0x00006D79
			public object DispatchInterface
			{
				get
				{
					return this.dispatchInterface;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008B81 File Offset: 0x00006D81
			public object AsyncDispatchInterface
			{
				get
				{
					return this.asyncDispatchInterface;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008B89 File Offset: 0x00006D89
			public DispatchPool DispatchPool
			{
				get
				{
					return this.dispatchPool;
				}
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00008B91 File Offset: 0x00006D91
			public void Dispose()
			{
				Util.DisposeIfPresent(this.DispatchPool);
				this.dispatchPool = null;
				this.asyncDispatchInterface = null;
				this.dispatchInterface = null;
			}

			// Token: 0x040000E3 RID: 227
			private object dispatchInterface;

			// Token: 0x040000E4 RID: 228
			private object asyncDispatchInterface;

			// Token: 0x040000E5 RID: 229
			private DispatchPool dispatchPool;
		}
	}
}
