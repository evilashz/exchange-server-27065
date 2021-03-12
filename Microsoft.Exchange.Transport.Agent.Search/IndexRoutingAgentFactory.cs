using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Transport.Agent.Search
{
	// Token: 0x02000004 RID: 4
	internal class IndexRoutingAgentFactory : RoutingAgentFactory
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000027D4 File Offset: 0x000009D4
		public IndexRoutingAgentFactory()
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("IndexRoutingAgentFactory", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.IndexRoutingAgentTracer, (long)this.GetHashCode());
			this.Config = new FlightingSearchConfig();
			this.enabled = this.Config.IndexAgentEnabled;
			this.diagnosticsSession.TraceDebug("Begin Factory Initialization.", new object[0]);
			int indexAgentListenPort = this.Config.IndexAgentListenPort;
			this.submissionPort = this.Config.ContentSubmissionPort;
			this.submissionHost = this.Config.HostName;
			if (this.Config.UseTransportAgentTestFlow)
			{
				this.flow = "Internal.Exchange.TransportAgentTestFlow";
			}
			else
			{
				this.flow = FlowDescriptor.GetTransportFlowDescriptor(this.Config).DisplayName;
			}
			this.documentFeeders = this.Config.IndexAgentFastFeeders;
			this.connectionTimeout = this.Config.TxDocumentFeederConnectionTimeout;
			this.transactionTimeout = this.Config.IndexAgentStreamTimeout;
			int indexAgentErrorsBeforeDelay = this.Config.IndexAgentErrorsBeforeDelay;
			TimeSpan indexAgentErrorDelayInterval = this.Config.IndexAgentErrorDelayInterval;
			if (this.enabled)
			{
				this.streamManager = StreamManager.CreateForListen(ComponentInstance.Globals.Search.ServiceName, new TcpListener.HandleFailure(this.OnTcpListenerFailure));
				this.streamManager.ConnectionTimeout = this.transactionTimeout;
				this.streamManager.ListenPort = indexAgentListenPort;
				this.streamManager.StartListening();
			}
			this.diagnosticsSession.TraceDebug("Factory Initialized.", new object[0]);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000294F File Offset: 0x00000B4F
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002957 File Offset: 0x00000B57
		internal ITransportFlowFeeder TransportFlowFeeder { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002968 File Offset: 0x00000B68
		internal SearchConfig Config { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002971 File Offset: 0x00000B71
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			this.IsReadyToProcessMessages();
			return new IndexRoutingAgent(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002980 File Offset: 0x00000B80
		public override void Close()
		{
			IndexRoutingAgentFactory.notActivelyInitializing.Set();
			if (this.streamManager != null)
			{
				this.streamManager.StopListening();
			}
			if (this.feeder != null)
			{
				this.feeder.Dispose();
			}
			this.diagnosticsSession.TraceDebug("Factory closed", new object[0]);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029D4 File Offset: 0x00000BD4
		internal void ReportConnectionStatus(bool messageProcessed)
		{
			lock (IndexRoutingAgentFactory.readyToProcessMessagesLock)
			{
				if (messageProcessed)
				{
					IndexRoutingAgentFactory.failedItemCounter = 0;
					return;
				}
				if (++IndexRoutingAgentFactory.failedItemCounter <= IndexRoutingAgentFactory.errorsBeforeDelay)
				{
					return;
				}
				this.SetFactoryToFailureState();
			}
			this.diagnosticsSession.TraceError<TimeSpan>("Too many errors. Delaying for {0}", IndexRoutingAgentFactory.errorDelayInterval);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A4C File Offset: 0x00000C4C
		internal bool IsReadyToProcessMessages()
		{
			this.diagnosticsSession.TraceDebug("Checking to see if the Feeder is ready to process messages.", new object[0]);
			if (!this.enabled)
			{
				return false;
			}
			if ((this.GetInstalledRoles() & (ServerRole.Mailbox | ServerRole.HubTransport)) == ServerRole.None)
			{
				return false;
			}
			int num = Interlocked.CompareExchange(ref IndexRoutingAgentFactory.submissionClientState, 1, 0);
			this.diagnosticsSession.TraceDebug<int>("The state of the RoutingAgent's Feeding client is currently: {0}.", num);
			switch (num)
			{
			case 0:
				break;
			case 1:
				return false;
			case 2:
				return false;
			case 3:
				return true;
			case 4:
				if (DateTime.UtcNow < IndexRoutingAgentFactory.nextFastRetryTime)
				{
					this.diagnosticsSession.TraceError("Not time to process messages.", new object[0]);
					return false;
				}
				num = Interlocked.CompareExchange(ref IndexRoutingAgentFactory.submissionClientState, 1, 4);
				if (num != 4)
				{
					return false;
				}
				break;
			default:
				throw new InvalidOperationException(string.Format("Unknown SubmissionClientState in the IndexRoutingAgentFactory. {0}", num));
			}
			IndexRoutingAgentFactory.notActivelyInitializing.Reset();
			Interlocked.Exchange(ref IndexRoutingAgentFactory.submissionClientState, 2);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.InitializeFastFeeder));
			return this.CheckForStateWhileInitializing();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B4C File Offset: 0x00000D4C
		private void InitializeFastFeeder(object state)
		{
			bool flag = false;
			this.diagnosticsSession.TraceDebug("Begin Initializing the Fast Feeder.", new object[0]);
			try
			{
				this.EnsureTransportCtsFlow();
				try
				{
					if (this.feeder != null)
					{
						this.feeder.Dispose();
					}
					using (DisposeGuard disposeGuard = default(DisposeGuard))
					{
						this.feeder = Factory.Current.CreateFastFeeder(this.submissionHost, this.submissionPort, this.documentFeeders, this.connectionTimeout, TimeSpan.Zero, this.transactionTimeout, this.flow);
						disposeGuard.Add<ISubmitDocument>(this.feeder);
						this.feeder.IndexSystemName = string.Empty;
						if (!this.Config.SkipMdmGeneration)
						{
							IIndexManager indexManager = Factory.Current.CreateIndexManager();
							string transportIndexSystem = indexManager.GetTransportIndexSystem();
							if (string.IsNullOrWhiteSpace(transportIndexSystem))
							{
								this.diagnosticsSession.TraceError("Need the transport flow Index system to generate an Mdm", new object[0]);
								this.SetFactoryToFailureState();
								return;
							}
							this.feeder.IndexSystemName = transportIndexSystem;
						}
						this.TransportFlowFeeder = new TransportFlowFeeder(this.streamManager, this.feeder);
						IndexRoutingAgentFactory.failedItemCounter = 0;
						flag = true;
						Interlocked.Exchange(ref IndexRoutingAgentFactory.submissionClientState, 3);
						disposeGuard.Success();
					}
				}
				catch (FastConnectionException arg)
				{
					this.diagnosticsSession.TraceError<FastConnectionException>("Exception creating FastFeeder: {0}", arg);
					this.SetFactoryToFailureState();
				}
			}
			finally
			{
				IndexRoutingAgentFactory.notActivelyInitializing.Set();
				this.diagnosticsSession.TraceDebug<string>("End Initializing the Fast Feeder. Result: {0}", flag ? "Success" : "Failure");
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D18 File Offset: 0x00000F18
		private bool CheckForStateWhileInitializing()
		{
			return IndexRoutingAgentFactory.notActivelyInitializing.WaitOne(this.transactionTimeout) && this.IsReadyToProcessMessages();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002D34 File Offset: 0x00000F34
		private void SetFactoryToFailureState()
		{
			IndexRoutingAgentFactory.nextFastRetryTime = DateTime.UtcNow + IndexRoutingAgentFactory.errorDelayInterval;
			Interlocked.Exchange(ref IndexRoutingAgentFactory.submissionClientState, 4);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D58 File Offset: 0x00000F58
		private bool EnsureTransportCtsFlow()
		{
			bool result;
			try
			{
				this.diagnosticsSession.TraceDebug("Attempting EnsureTransportFlow", new object[0]);
				FlowManager.Instance.EnsureTransportFlow();
				this.diagnosticsSession.TraceDebug("EnsureTransportFlow success", new object[0]);
				result = true;
			}
			catch (PerformingFastOperationException arg)
			{
				this.diagnosticsSession.TraceError<PerformingFastOperationException>("Exception calling EnsureTransportFlow: {0}", arg);
				IndexRoutingAgentFactory.nextFastRetryTime = DateTime.UtcNow + IndexRoutingAgentFactory.errorDelayInterval;
				result = false;
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E1C File Offset: 0x0000101C
		private ServerRole GetInstalledRoles()
		{
			if (this.installedRoles != ServerRole.None)
			{
				return this.installedRoles;
			}
			Exception ex = null;
			try
			{
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 441, "GetInstalledRoles", "f:\\15.00.1497\\sources\\dev\\mexagents\\src\\Search\\IndexRoutingAgentFactory.cs");
					Server server = topologyConfigurationSession.FindLocalServer();
					this.installedRoles = server.CurrentServerRole;
				});
			}
			catch (LocalServerNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (ADTransientException ex3)
			{
				ex = ex3;
			}
			catch (ADExternalException ex4)
			{
				ex = ex4;
			}
			catch (DataValidationException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				this.diagnosticsSession.TraceError<Exception>("Unable to determine installed server roles: {0}", ex);
			}
			return this.installedRoles;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EC0 File Offset: 0x000010C0
		private void OnTcpListenerFailure(bool addressAlreadyInUseFailure)
		{
			this.diagnosticsSession.TraceError("Stop Listening", new object[0]);
			this.enabled = false;
		}

		// Token: 0x0400000B RID: 11
		private const string ComponentName = "IndexRoutingAgentFactory";

		// Token: 0x0400000C RID: 12
		private const ServerRole RunOnRoles = ServerRole.Mailbox | ServerRole.HubTransport;

		// Token: 0x0400000D RID: 13
		private static readonly object readyToProcessMessagesLock = new object();

		// Token: 0x0400000E RID: 14
		private static DateTime nextFastRetryTime;

		// Token: 0x0400000F RID: 15
		private static int submissionClientState;

		// Token: 0x04000010 RID: 16
		private static ManualResetEvent notActivelyInitializing = new ManualResetEvent(true);

		// Token: 0x04000011 RID: 17
		private static int failedItemCounter;

		// Token: 0x04000012 RID: 18
		private static TimeSpan errorDelayInterval = TimeSpan.FromSeconds(60.0);

		// Token: 0x04000013 RID: 19
		private static int errorsBeforeDelay = 5;

		// Token: 0x04000014 RID: 20
		private readonly string submissionHost;

		// Token: 0x04000015 RID: 21
		private readonly int submissionPort;

		// Token: 0x04000016 RID: 22
		private readonly TimeSpan connectionTimeout;

		// Token: 0x04000017 RID: 23
		private readonly TimeSpan transactionTimeout;

		// Token: 0x04000018 RID: 24
		private readonly int documentFeeders;

		// Token: 0x04000019 RID: 25
		private readonly string flow;

		// Token: 0x0400001A RID: 26
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400001B RID: 27
		private readonly IStreamManager streamManager;

		// Token: 0x0400001C RID: 28
		private bool enabled;

		// Token: 0x0400001D RID: 29
		private ISubmitDocument feeder;

		// Token: 0x0400001E RID: 30
		private ServerRole installedRoles;

		// Token: 0x02000005 RID: 5
		private class SubmissionClientState
		{
			// Token: 0x04000021 RID: 33
			public const int Uninitialized = 0;

			// Token: 0x04000022 RID: 34
			public const int ReadyToInitialize = 1;

			// Token: 0x04000023 RID: 35
			public const int Initializing = 2;

			// Token: 0x04000024 RID: 36
			public const int Initialized = 3;

			// Token: 0x04000025 RID: 37
			public const int Failed = 4;
		}
	}
}
