using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000890 RID: 2192
	public abstract class DatabaseCopyStateAction : DatabaseCopyActionTask
	{
		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x0013F1A9 File Offset: 0x0013D3A9
		protected bool UseRpc
		{
			get
			{
				return this.m_UseRpc;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x0013F1B1 File Offset: 0x0013D3B1
		// (set) Token: 0x06004C7B RID: 19579 RVA: 0x0013F1B9 File Offset: 0x0013D3B9
		protected bool IsSuspendOperation
		{
			get
			{
				return this.m_IsSuspend;
			}
			set
			{
				this.m_IsSuspend = value;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x0013F1C2 File Offset: 0x0013D3C2
		// (set) Token: 0x06004C7D RID: 19581 RVA: 0x0013F1CA File Offset: 0x0013D3CA
		protected bool IsActivationRpcSupported
		{
			get
			{
				return this.m_isActivationRpcSupported;
			}
			set
			{
				this.m_isActivationRpcSupported = value;
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06004C7E RID: 19582 RVA: 0x0013F1D3 File Offset: 0x0013D3D3
		// (set) Token: 0x06004C7F RID: 19583 RVA: 0x0013F1DB File Offset: 0x0013D3DB
		protected bool IsRequestSuspend3RpcSupported
		{
			get
			{
				return this.m_isRequestSuspend3RpcSupported;
			}
			set
			{
				this.m_isRequestSuspend3RpcSupported = value;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x0013F1E4 File Offset: 0x0013D3E4
		// (set) Token: 0x06004C81 RID: 19585 RVA: 0x0013F1EC File Offset: 0x0013D3EC
		protected bool IsOperationRunOnSource
		{
			get
			{
				return this.m_isOperationRunOnSource;
			}
			set
			{
				this.m_isOperationRunOnSource = value;
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x0013F1F5 File Offset: 0x0013D3F5
		// (set) Token: 0x06004C83 RID: 19587 RVA: 0x0013F1FD File Offset: 0x0013D3FD
		protected bool IsReplayLagManagementOperation { get; set; }

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x0013F206 File Offset: 0x0013D406
		protected Server Server
		{
			get
			{
				return this.m_server;
			}
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0013F20E File Offset: 0x0013D40E
		protected override bool IsKnownException(Exception e)
		{
			return e is TaskServerException || AmExceptionHelper.IsKnownClusterException(this, e) || base.IsKnownException(e);
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0013F22A File Offset: 0x0013D42A
		protected override void InternalStopProcessing()
		{
			TaskLogger.LogEnter();
			if (!this.m_event.Set())
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "InternalStopProcessing: failed to set signal.");
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x0013F25C File Offset: 0x0013D45C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, DatabaseCopyIdParameter>((long)this.GetHashCode(), "DatabaseCopyStateAction: enter InternalValidate(DB,ident): {0}, {1}", base.DatabaseName, this.Identity);
				base.InternalValidate();
				if (base.HasErrors)
				{
					TaskLogger.LogExit();
				}
				else
				{
					Database database = this.DataObject.GetDatabase<Database>();
					DatabaseAvailabilityGroup dagForDatabase = DagTaskHelper.GetDagForDatabase(database, base.DataSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
					DagTaskHelper.PreventTaskWhenTPREnabled(dagForDatabase, this);
					this.m_UseRpc = ReplayRpcVersionControl.IsSuspendRpcSupported(this.Server.AdminDisplayVersion);
					ServerVersion serverVersion = this.IsReplayLagManagementOperation ? ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion : ReplayRpcVersionControl.SuspendRpcSupportVersion;
					if (this.m_UseRpc)
					{
						if (this.IsSuspendOperation)
						{
							base.WriteVerbose(Strings.SuspendSgcUseRpc(this.Server.AdminDisplayVersion.ToString(), serverVersion.ToString()));
						}
						else
						{
							base.WriteVerbose(Strings.ResumeSgcUseRpc(this.Server.AdminDisplayVersion.ToString(), serverVersion.ToString()));
						}
					}
					else if (this.IsReplayLagManagementOperation)
					{
						base.WriteError(new ReplayLagRpcUnsupportedException(this.Server.Name, this.Server.AdminDisplayVersion.ToString(), ReplayRpcVersionControl.GetCopyStatusEx4RpcSupportVersion.ToString()), ExchangeErrorCategory.ServerOperation, this.Server);
					}
					else if (this.IsSuspendOperation)
					{
						base.WriteVerbose(Strings.SuspendSgcUseState(this.Server.Name, this.Server.AdminDisplayVersion.ToString(), ReplayRpcVersionControl.SuspendRpcSupportVersion.ToString()));
					}
					else
					{
						base.WriteVerbose(Strings.ResumeSgcUseState(this.Server.Name, this.Server.AdminDisplayVersion.ToString(), ReplayRpcVersionControl.SuspendRpcSupportVersion.ToString()));
					}
					this.IsActivationRpcSupported = ReplayRpcVersionControl.IsActivationRpcSupported(this.Server.AdminDisplayVersion);
					this.IsRequestSuspend3RpcSupported = ReplayRpcVersionControl.IsRequestSuspend3RpcSupported(this.Server.AdminDisplayVersion);
					this.IsOperationRunOnSource = false;
					DatabaseLocationInfo databaseLocationInfo;
					if (database.ReplicationType == ReplicationType.Remote && RemoteReplayConfiguration.IsServerRcrSource(ADObjectWrapperFactory.CreateWrapper(database), ADObjectWrapperFactory.CreateWrapper(this.Server), out databaseLocationInfo))
					{
						this.IsOperationRunOnSource = true;
					}
					ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "DatabaseCopyStateAction: leave InternalValidate: {0}", base.DatabaseName);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004C88 RID: 19592
		internal abstract void ProcessRecordWorker(ReplayConfiguration replayConfiguration);

		// Token: 0x06004C89 RID: 19593
		protected abstract void RpcOperation();

		// Token: 0x06004C8A RID: 19594 RVA: 0x0013F4A8 File Offset: 0x0013D6A8
		protected void RpcThreadProc()
		{
			try
			{
				this.RpcOperation();
				this.m_fSuccess = true;
			}
			catch (TaskServerTransientException ex)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "RpcThreadProc: Caught exception in RPC: {0}", ex.ToString());
				if (!base.Stopping)
				{
					this.m_Exception = ex;
				}
				else
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "RpcThreadProc: Cancel was requested, so leaving RPC Thread.");
				}
			}
			catch (TaskServerException ex2)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string>((long)this.GetHashCode(), "RpcThreadProc: Caught exception in RPC: {0}", ex2.ToString());
				if (!base.Stopping)
				{
					this.m_Exception = ex2;
					if (ex2 is ReplayServiceDownException)
					{
						this.m_fFallbackToState = true;
					}
				}
				else
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "RpcThreadProc: Cancel was requested, so leaving RPC Thread.");
				}
			}
			finally
			{
				if (!this.m_event.Set())
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "RpcThreadProc: failed to set signal.");
				}
			}
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x0013F5B0 File Offset: 0x0013D7B0
		protected Thread BeginRpcOperation()
		{
			if (this.IsSuspendOperation)
			{
				base.WriteVerbose(Strings.SuspendSgcRpcRequest(this.DataObject.Identity.ToString()));
			}
			else
			{
				base.WriteVerbose(Strings.ResumeSgcRpcRequest(this.DataObject.Identity.ToString()));
			}
			Thread thread = new Thread(new ThreadStart(this.RpcThreadProc));
			thread.IsBackground = true;
			thread.Start();
			return thread;
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0013F620 File Offset: 0x0013D820
		protected sealed override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.Validate(this.DataObject);
				if (!base.HasErrors)
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "DatabaseCopyStateAction: enter InternalProcessRecord: {0}", this.DataObject.Identity);
					Database database = this.DataObject.GetDatabase<Database>();
					if (!this.m_UseRpc)
					{
						ReplayConfiguration replayConfiguration = base.ConstructReplayConfiguration(database);
						this.ProcessRecordWorker(replayConfiguration);
					}
					else
					{
						this.ProcessRecordWorker(null);
					}
					ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "DatabaseCopyStateAction: leave InternalProcessRecord: {0}", this.DataObject.Identity);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0013F6E0 File Offset: 0x0013D8E0
		protected string GetServerFqdn()
		{
			return this.Server.Fqdn;
		}

		// Token: 0x04002D90 RID: 11664
		public static readonly TimeSpan TimeoutRpc = new TimeSpan(0, 0, 20);

		// Token: 0x04002D91 RID: 11665
		protected ManualResetEvent m_event = new ManualResetEvent(false);

		// Token: 0x04002D92 RID: 11666
		protected bool m_fFallbackToState;

		// Token: 0x04002D93 RID: 11667
		protected bool m_fSuccess;

		// Token: 0x04002D94 RID: 11668
		protected Exception m_Exception;

		// Token: 0x04002D95 RID: 11669
		private bool m_UseRpc;

		// Token: 0x04002D96 RID: 11670
		private bool m_IsSuspend;

		// Token: 0x04002D97 RID: 11671
		private bool m_isActivationRpcSupported;

		// Token: 0x04002D98 RID: 11672
		private bool m_isRequestSuspend3RpcSupported;

		// Token: 0x04002D99 RID: 11673
		private bool m_isOperationRunOnSource;
	}
}
