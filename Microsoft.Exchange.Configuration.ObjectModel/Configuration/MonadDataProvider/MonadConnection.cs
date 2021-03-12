using System;
using System.Data;
using System.Data.Common;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics.Components.Monad;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001C8 RID: 456
	internal class MonadConnection : DbConnection
	{
		// Token: 0x06001007 RID: 4103 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		public MonadConnection() : this("timeout=30")
		{
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00030C05 File Offset: 0x0002EE05
		public MonadConnection(string connectionString) : this(connectionString, MonadConnection.defaultInteractionHandler)
		{
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00030C13 File Offset: 0x0002EE13
		public MonadConnection(string connectionString, CommandInteractionHandler uiHandler) : this(connectionString, uiHandler, null)
		{
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00030C1E File Offset: 0x0002EE1E
		public MonadConnection(string connectionString, CommandInteractionHandler uiHandler, RunspaceServerSettingsPresentationObject serverSettings) : this(connectionString, uiHandler, serverSettings, null)
		{
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00030C2C File Offset: 0x0002EE2C
		public MonadConnection(string connectionString, CommandInteractionHandler uiHandler, RunspaceServerSettingsPresentationObject serverSettings, MonadConnectionInfo connectionInfo)
		{
			this.SyncRoot = new object();
			base..ctor();
			ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "new MonadConnection({0})", connectionString);
			if (uiHandler == null)
			{
				throw new ArgumentNullException("uiHandler");
			}
			if (string.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentException("Argument 'connectionString' was null or emtpy.");
			}
			this.pooled = true;
			this.timeout = 0;
			this.ConnectionString = connectionString;
			this.InteractionHandler = uiHandler;
			this.state = ConnectionState.Closed;
			if (connectionInfo != null)
			{
				this.remote = true;
				if (this.pooled)
				{
					this.mediator = MonadConnection.mediatorPool.GetRunspacePooledMediatorInstance(new MonadMediatorPoolKey(connectionInfo, serverSettings));
				}
				else
				{
					this.mediator = new RunspaceMediator(new MonadRemoteRunspaceFactory(connectionInfo, serverSettings), new EmptyRunspaceCache());
				}
			}
			else if (this.pooled)
			{
				this.mediator = MonadConnection.mediatorPool.GetRunspacePooledMediatorInstance();
			}
			else
			{
				this.mediator = MonadConnection.mediatorPool.GetRunspaceNonPooledMediatorInstance();
			}
			this.serverSettings = serverSettings;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00030D1D File Offset: 0x0002EF1D
		internal MonadConnection(CommandInteractionHandler uiHandler, RunspaceMediator mediator) : this(uiHandler, mediator, null)
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00030D28 File Offset: 0x0002EF28
		internal MonadConnection(CommandInteractionHandler uiHandler, RunspaceMediator mediator, RunspaceServerSettingsPresentationObject serverSettings)
		{
			this.SyncRoot = new object();
			base..ctor();
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "new MonadConnection(RunspaceMediator)");
			if (uiHandler == null)
			{
				throw new ArgumentNullException("uiHandler");
			}
			if (mediator == null)
			{
				throw new ArgumentNullException("mediator");
			}
			this.pooled = true;
			this.timeout = 0;
			this.InteractionHandler = uiHandler;
			this.mediator = mediator;
			this.state = ConnectionState.Closed;
			this.serverSettings = serverSettings;
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600100E RID: 4110 RVA: 0x00030DA4 File Offset: 0x0002EFA4
		// (remove) Token: 0x0600100F RID: 4111 RVA: 0x00030DDC File Offset: 0x0002EFDC
		public override event StateChangeEventHandler StateChange;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001010 RID: 4112 RVA: 0x00030E14 File Offset: 0x0002F014
		// (remove) Token: 0x06001011 RID: 4113 RVA: 0x00030E48 File Offset: 0x0002F048
		internal static event EventHandler Test_StateChanged;

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00030E7B File Offset: 0x0002F07B
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x00030E83 File Offset: 0x0002F083
		public CommandInteractionHandler InteractionHandler
		{
			get
			{
				return this.commandInteractionHandler;
			}
			set
			{
				if (value == null)
				{
					value = MonadConnection.defaultInteractionHandler;
				}
				this.commandInteractionHandler = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00030E96 File Offset: 0x0002F096
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00030EA0 File Offset: 0x0002F0A0
		public override string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				ExTraceGlobals.IntegrationTracer.Information<string>((long)this.GetHashCode(), "-->MonadConnection.ConnectionString={0}", value);
				string[] array = value.Split(new char[]
				{
					'='
				});
				if (array.Length != 2)
				{
					throw new ArgumentException(Strings.ExceptionMDAInvalidConnectionString(value), value);
				}
				if (array[0].Trim().ToLowerInvariant() == "timeout")
				{
					int num = int.Parse(array[1].Trim());
					if (num < 0)
					{
						throw new ArgumentOutOfRangeException("timeout", num, Strings.InvalidNegativeValue("timeout"));
					}
					this.timeout = num;
				}
				else
				{
					if (!(array[0].Trim().ToLowerInvariant() == "pooled"))
					{
						throw new ArgumentException(Strings.ExceptionMDAInvalidConnectionString(value), value);
					}
					if (bool.Parse(array[1].Trim()))
					{
						throw new ArgumentException(Strings.ExceptionMDAInvalidConnectionString(value), value);
					}
					this.pooled = false;
				}
				this.connectionString = value;
				ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadConnection.ConnectionString");
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00030FB6 File Offset: 0x0002F1B6
		public override int ConnectionTimeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00030FBE File Offset: 0x0002F1BE
		public bool IsPooled
		{
			get
			{
				return this.pooled;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00030FC6 File Offset: 0x0002F1C6
		public override string ServerVersion
		{
			get
			{
				return MonadHost.ServerVersion;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		public override ConnectionState State
		{
			get
			{
				if (this.state == ConnectionState.Open)
				{
					if (this.RunspaceProxy.State == RunspaceState.Broken)
					{
						this.state = ConnectionState.Broken;
					}
					else if (this.RunspaceProxy.State == RunspaceState.Closed || this.RunspaceProxy.State == RunspaceState.Closing)
					{
						this.state = ConnectionState.Closed;
					}
				}
				return this.state;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00031027 File Offset: 0x0002F227
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003102E File Offset: 0x0002F22E
		public override string DataSource
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00031035 File Offset: 0x0002F235
		internal bool IsRemote
		{
			get
			{
				return this.remote;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0003103D File Offset: 0x0002F23D
		internal RunspaceProxy RunspaceProxy
		{
			get
			{
				return this.proxy;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00031045 File Offset: 0x0002F245
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003104D File Offset: 0x0002F24D
		internal MonadCommand CurrentCommand
		{
			get
			{
				return this.currentCommand;
			}
			set
			{
				this.currentCommand = value;
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00031058 File Offset: 0x0002F258
		public override void Open()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadConnection.Open");
			lock (this.SyncRoot)
			{
				if (this.state != ConnectionState.Closed)
				{
					throw new InvalidOperationException(Strings.ExceptionMDAConnectionAlreadyOpened);
				}
				this.proxy = new RunspaceProxy(this.mediator);
				if (this.serverSettings != null && !this.IsRemote)
				{
					this.InitializeLocalServerSettings(this.serverSettings);
				}
				this.SetState(ConnectionState.Open);
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadConnection.Open");
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003110C File Offset: 0x0002F30C
		public override void Close()
		{
			this.Close(false);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00031115 File Offset: 0x0002F315
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003111C File Offset: 0x0002F31C
		internal void NotifyExecutionStarting()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadConnection.NotifyExecutionStarting");
			lock (this.SyncRoot)
			{
				if (this.state == ConnectionState.Closed)
				{
					throw new InvalidOperationException(Strings.ExceptionMDAConnectionMustBeOpened);
				}
				if (ConnectionState.Open != this.state)
				{
					throw new InvalidOperationException(Strings.ExceptionMDACommandAlreadyExecuting);
				}
				this.SetState(ConnectionState.Open | ConnectionState.Connecting);
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadConnection.NotifyExecutionStarting");
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000311BC File Offset: 0x0002F3BC
		internal void NotifyExecutionStarted()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadConnection.NotifyExecutionStarted");
			lock (this.SyncRoot)
			{
				if (this.state == ConnectionState.Closed)
				{
					throw new InvalidOperationException(Strings.ExceptionMDAConnectionMustBeOpened);
				}
				if ((ConnectionState.Executing & this.state) != ConnectionState.Closed)
				{
					throw new InvalidOperationException(Strings.ExceptionMDACommandAlreadyExecuting);
				}
				this.SetState(ConnectionState.Open | ConnectionState.Executing);
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadConnection.NotifyExecutionStarted");
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003125C File Offset: 0x0002F45C
		internal void NotifyExecutionFinished()
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadConnection.NotifyExecutionFinished");
			lock (this.SyncRoot)
			{
				if (this.state == ConnectionState.Closed)
				{
					throw new InvalidOperationException(Strings.ExceptionMDAConnectionMustBeOpened);
				}
				if ((ConnectionState.Executing & this.state) == ConnectionState.Closed)
				{
					throw new InvalidOperationException(Strings.ExceptionMDACommandNotExecuting);
				}
				this.SetState(ConnectionState.Open);
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadConnection.NotifyExecutionFinished");
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000312FC File Offset: 0x0002F4FC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(true);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003130F File Offset: 0x0002F50F
		protected override DbCommand CreateDbCommand()
		{
			return new MonadCommand();
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00031316 File Offset: 0x0002F516
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003131D File Offset: 0x0002F51D
		private bool ShouldSerializeInteractionHandler()
		{
			return this.InteractionHandler != MonadConnection.defaultInteractionHandler;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003132F File Offset: 0x0002F52F
		private void ResetInteractionHandler()
		{
			this.InteractionHandler = MonadConnection.defaultInteractionHandler;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003133C File Offset: 0x0002F53C
		private void Close(bool force)
		{
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "-->MonadConnection.Close");
			lock (this.SyncRoot)
			{
				if (this.state != ConnectionState.Closed)
				{
					if (!force && (ConnectionState.Executing & this.state) != ConnectionState.Closed)
					{
						throw new InvalidOperationException(Strings.ExceptionMDACommandStillExecuting);
					}
					if (this.serverSettings != null && !this.IsRemote)
					{
						this.CleanUpLocalServerSettings();
					}
					this.proxy.Dispose();
					this.proxy = null;
					this.SetState(ConnectionState.Closed);
				}
			}
			ExTraceGlobals.IntegrationTracer.Information((long)this.GetHashCode(), "<--MonadConnection.Close");
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000313F8 File Offset: 0x0002F5F8
		private void SetState(ConnectionState state)
		{
			ExTraceGlobals.VerboseTracer.Information<ConnectionState>((long)this.GetHashCode(), "-->MonadConnection.SetState({0})", state);
			ConnectionState originalState = this.state;
			this.state = state;
			if (this.StateChange != null)
			{
				ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "\tInvoking event subscribers.");
				this.StateChange(this, new StateChangeEventArgs(originalState, state));
			}
			ExTraceGlobals.VerboseTracer.Information((long)this.GetHashCode(), "<--MonadConnection.SetState()");
			if (MonadConnection.Test_StateChanged != null)
			{
				MonadConnection.Test_StateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00031488 File Offset: 0x0002F688
		private void InitializeLocalServerSettings(RunspaceServerSettingsPresentationObject serverSettings)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Set-ADServerSettings");
			pscommand.AddParameter("RunspaceServerSettings", serverSettings);
			using (PowerShell powerShell = this.proxy.CreatePowerShell(pscommand))
			{
				try
				{
					powerShell.Invoke();
				}
				catch (CmdletInvocationException ex)
				{
					if (ex.InnerException != null)
					{
						throw ex.InnerException;
					}
					throw;
				}
				if (powerShell.Streams.Error.Count > 0)
				{
					ErrorRecord errorRecord = powerShell.Streams.Error[0];
					throw new CmdletInvocationException(errorRecord.Exception.Message, errorRecord.Exception);
				}
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00031540 File Offset: 0x0002F740
		private void CleanUpLocalServerSettings()
		{
			EngineIntrinsics engineIntrinsics = this.RunspaceProxy.GetVariable("ExecutionContext") as EngineIntrinsics;
			if (engineIntrinsics != null)
			{
				ISessionState sessionState = new PSSessionState(engineIntrinsics.SessionState);
				if (ExchangePropertyContainer.IsContainerInitialized(sessionState))
				{
					ExchangePropertyContainer.SetServerSettings(sessionState, null);
				}
			}
			this.RunspaceProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, null);
		}

		// Token: 0x04000364 RID: 868
		public const string DefaultConnectionString = "timeout=30";

		// Token: 0x04000365 RID: 869
		public const string NonpooledConnectionString = "pooled=false";

		// Token: 0x04000366 RID: 870
		private const string TimeoutKey = "timeout";

		// Token: 0x04000367 RID: 871
		private const string PooledKey = "pooled";

		// Token: 0x04000368 RID: 872
		private static readonly CommandInteractionHandler defaultInteractionHandler = new CommandInteractionHandler();

		// Token: 0x04000369 RID: 873
		private static MonadMediatorPool mediatorPool = new MonadMediatorPool(3, TimeSpan.FromMinutes(5.0));

		// Token: 0x0400036A RID: 874
		private ConnectionState state;

		// Token: 0x0400036B RID: 875
		private RunspaceProxy proxy;

		// Token: 0x0400036C RID: 876
		private object SyncRoot;

		// Token: 0x0400036D RID: 877
		private string connectionString;

		// Token: 0x0400036E RID: 878
		private int timeout;

		// Token: 0x0400036F RID: 879
		private bool pooled;

		// Token: 0x04000370 RID: 880
		private bool remote;

		// Token: 0x04000371 RID: 881
		private CommandInteractionHandler commandInteractionHandler;

		// Token: 0x04000372 RID: 882
		private RunspaceMediator mediator;

		// Token: 0x04000373 RID: 883
		private RunspaceServerSettingsPresentationObject serverSettings;

		// Token: 0x04000374 RID: 884
		private MonadCommand currentCommand;
	}
}
