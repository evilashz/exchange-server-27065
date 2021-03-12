using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008C0 RID: 2240
	[Cmdlet("Resume", "MailboxDatabaseCopy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ResumeDatabaseCopy : DatabaseCopyStateAction
	{
		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x0014B5BB File Offset: 0x001497BB
		// (set) Token: 0x06004F6F RID: 20335 RVA: 0x0014B5D2 File Offset: 0x001497D2
		[Parameter(Mandatory = true, ParameterSetName = "DisableReplayLag", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override DatabaseCopyIdParameter Identity
		{
			get
			{
				return (DatabaseCopyIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x0014B5E5 File Offset: 0x001497E5
		// (set) Token: 0x06004F71 RID: 20337 RVA: 0x0014B60B File Offset: 0x0014980B
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter ReplicationOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ReplicationOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ReplicationOnly"] = value;
			}
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x0014B623 File Offset: 0x00149823
		// (set) Token: 0x06004F73 RID: 20339 RVA: 0x0014B649 File Offset: 0x00149849
		[Parameter(Mandatory = true, ParameterSetName = "DisableReplayLag")]
		public SwitchParameter DisableReplayLag
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableReplayLag"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DisableReplayLag"] = value;
			}
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x0014B661 File Offset: 0x00149861
		// (set) Token: 0x06004F75 RID: 20341 RVA: 0x0014B678 File Offset: 0x00149878
		[Parameter(Mandatory = false, ParameterSetName = "DisableReplayLag")]
		public string DisableReplayLagReason
		{
			get
			{
				return (string)base.Fields["DisableReplayLagReason"];
			}
			set
			{
				base.Fields["DisableReplayLagReason"] = value;
			}
		}

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x0014B68C File Offset: 0x0014988C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.ReplicationOnly)
				{
					return Strings.ConfirmationMessageResumeDatabaseCopyReplicationIdentity(base.DatabaseName, base.Server.Name);
				}
				if (this.DisableReplayLag)
				{
					return Strings.ConfirmationMessageDisableReplayLag(base.DatabaseName, base.Server.Name);
				}
				return Strings.ConfirmationMessageResumeDatabaseCopyIdentity(base.DatabaseName, base.Server.Name);
			}
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0014B6F7 File Offset: 0x001498F7
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.IsSuspendOperation = false;
			base.IsReplayLagManagementOperation = this.DisableReplayLag;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0014B718 File Offset: 0x00149918
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (base.HasErrors)
				{
					TaskLogger.LogExit();
				}
				else
				{
					if (base.IsOperationRunOnSource && !this.DisableReplayLag)
					{
						this.WriteWarning(Strings.ResumeSgcOnHostServer(base.DatabaseName, base.Server.Name));
					}
					base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, base.Server, true, new DataAccessTask<DatabaseCopy>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0014B7AC File Offset: 0x001499AC
		internal override void ProcessRecordWorker(ReplayConfiguration replayConfiguration)
		{
			ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "Resume-DBC: InternalProcessRecord: {0}", this.DataObject.Identity);
			ExTraceGlobals.PFDTracer.TracePfd<int, ObjectId>((long)this.GetHashCode(), "PFD CRS {0} Resume-DBC initiated for : InternalProcessRecord: {1}", 28635, this.DataObject.Identity);
			if (this.DisableReplayLag)
			{
				Database database = this.DataObject.GetDatabase<Database>();
				ReplayRpcClientHelper.RpccDisableReplayLag(base.Server.Name, database.Guid, this.DisableReplayLagReason, ActionInitiatorType.Administrator);
				return;
			}
			if (!base.UseRpc)
			{
				this.ResumeUsingState(replayConfiguration);
			}
			else if (!base.Stopping)
			{
				Thread thread = base.BeginRpcOperation();
				try
				{
					TimeSpan timeoutRpc = DatabaseCopyStateAction.TimeoutRpc;
					if (!thread.Join(timeoutRpc))
					{
						ExTraceGlobals.CmdletsTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "Resume-DBC: resume is being slow: timeout={0}", timeoutRpc);
						this.WriteWarning(Strings.ResumeSgcTimeout);
						this.m_event.WaitOne();
					}
				}
				finally
				{
					if (this.m_Exception != null)
					{
						ErrorCategory category;
						this.TranslateException(ref this.m_Exception, out category);
						if (this.m_Exception is ReplayServiceSuspendWantedClearedException)
						{
							base.WriteWarning(this.m_Exception.Message);
							this.m_fSuccess = true;
						}
						else if (this.m_Exception is ReplayServiceResumeRpcPartialSuccessCatalogFailedException)
						{
							base.WriteWarning(this.m_Exception.Message);
							this.m_fSuccess = true;
						}
						else if (this.m_Exception is ReplayServiceResumeRpcFailedSeedingException)
						{
							base.WriteWarning(this.m_Exception.Message);
						}
						else if (!this.m_fFallbackToState)
						{
							this.WriteError(this.m_Exception, category, null, false);
						}
						else if (!base.Stopping)
						{
							ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "ProcessRecordWorker: There was an RPC connection error, so now falling back to Resume through the State, for {0}.", this.DataObject.Identity);
							this.WriteWarning(Strings.ResumeSgcFallbackToState(this.DataObject.Identity.ToString(), this.m_Exception.Message));
						}
					}
				}
				if (!this.m_fSuccess && base.Stopping)
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "Resume was cancelled for {0}", this.DataObject.Identity);
					return;
				}
				if (!this.m_fSuccess && this.m_fFallbackToState)
				{
					ReplayConfiguration replayConfiguration2 = base.ConstructReplayConfiguration(this.DataObject.GetDatabase<Database>());
					this.ResumeUsingState(replayConfiguration2);
				}
			}
			ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "ResumeReplay() for {0}", this.DataObject.Identity);
			if (this.m_fSuccess)
			{
				ExTraceGlobals.PFDTracer.TracePfd<int, ObjectId>((long)this.GetHashCode(), "PFD CRS {0} Sucessfully ResumeReplay() for {1}", 24539, this.DataObject.Identity);
				if (this.m_fFallbackToState)
				{
					ReplayEventLogConstants.Tuple_ResumeMarkedForDatabaseCopy.LogEvent(null, new object[]
					{
						this.DataObject.Identity
					});
				}
			}
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x0014BA80 File Offset: 0x00149C80
		private void ResumeUsingState(ReplayConfiguration replayConfiguration)
		{
			ReplayState replayState = replayConfiguration.ReplayState;
			replayState.SuspendLockRemote.TryLeaveSuspend();
			replayState.SuspendMessage = null;
			this.m_fSuccess = true;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x0014BAB0 File Offset: 0x00149CB0
		protected override void RpcOperation()
		{
			Database database = this.DataObject.GetDatabase<Database>();
			if (base.IsActivationRpcSupported)
			{
				DatabaseCopyActionFlags flags = DatabaseCopyActionFlags.Replication | DatabaseCopyActionFlags.Activation;
				if (this.ReplicationOnly)
				{
					flags = DatabaseCopyActionFlags.Replication;
				}
				ReplayRpcClientHelper.RequestResume2(base.GetServerFqdn(), database.Guid, (uint)flags);
				return;
			}
			ReplayRpcClientWrapper.RequestResume(base.GetServerFqdn(), database.Guid);
		}
	}
}
