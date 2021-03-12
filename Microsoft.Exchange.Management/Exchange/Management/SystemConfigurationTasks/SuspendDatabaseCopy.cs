using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
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
	// Token: 0x020008C9 RID: 2249
	[Cmdlet("Suspend", "MailboxDatabaseCopy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.Low)]
	public sealed class SuspendDatabaseCopy : DatabaseCopyStateAction
	{
		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x0014E0E8 File Offset: 0x0014C2E8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.EnableReplayLag)
				{
					return Strings.ConfirmationMessageEnableReplayLag(base.DatabaseName, base.Server.Name);
				}
				if (!this.ActivationOnly)
				{
					return Strings.ConfirmationMessageSuspendDatabaseCopyIdentity(base.DatabaseName, base.Server.Name);
				}
				return Strings.ConfirmationMessageSuspendDatabaseCopyActivationIdentity(base.DatabaseName, base.Server.Name);
			}
		}

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x0014E153 File Offset: 0x0014C353
		// (set) Token: 0x06004FD0 RID: 20432 RVA: 0x0014E16A File Offset: 0x0014C36A
		[Parameter(Mandatory = true, ParameterSetName = "EnableReplayLag", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
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

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x0014E17D File Offset: 0x0014C37D
		// (set) Token: 0x06004FD2 RID: 20434 RVA: 0x0014E194 File Offset: 0x0014C394
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string SuspendComment
		{
			get
			{
				return (string)base.Fields["SuspendComment"];
			}
			set
			{
				base.Fields["SuspendComment"] = value;
			}
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x0014E1A7 File Offset: 0x0014C3A7
		// (set) Token: 0x06004FD4 RID: 20436 RVA: 0x0014E1CD File Offset: 0x0014C3CD
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter ActivationOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ActivationOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ActivationOnly"] = value;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x0014E1E5 File Offset: 0x0014C3E5
		// (set) Token: 0x06004FD6 RID: 20438 RVA: 0x0014E20B File Offset: 0x0014C40B
		[Parameter(Mandatory = true, ParameterSetName = "EnableReplayLag")]
		public SwitchParameter EnableReplayLag
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnableReplayLag"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EnableReplayLag"] = value;
			}
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0014E223 File Offset: 0x0014C423
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.IsSuspendOperation = true;
			base.IsReplayLagManagementOperation = this.EnableReplayLag;
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0014E244 File Offset: 0x0014C444
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
					if (base.IsOperationRunOnSource && !this.ActivationOnly && !this.EnableReplayLag)
					{
						base.WriteError(new InvalidOperationException(Strings.SuspendSgcOnHostServer(base.DatabaseName, base.Server.Name)), ErrorCategory.InvalidOperation, this.Identity);
					}
					base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, base.Server, true, new DataAccessTask<DatabaseCopy>.ADObjectOutOfScopeString(Strings.ErrorServerOutOfScope));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0014E2F8 File Offset: 0x0014C4F8
		internal override void ProcessRecordWorker(ReplayConfiguration replayConfiguration)
		{
			ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId, string>((long)this.GetHashCode(), "Suspend-DBC: ProcessRecordWorker: {0}, {1}", this.DataObject.Identity, this.SuspendComment);
			ExTraceGlobals.PFDTracer.TracePfd<int, ObjectId, string>((long)this.GetHashCode(), "PFD CRS {0} Suspend-DBC Initiated for : ProcessRecordWorker: {1}, {2}", 25051, this.DataObject.Identity, this.SuspendComment);
			if (this.EnableReplayLag)
			{
				Database database = this.DataObject.GetDatabase<Database>();
				ReplayRpcClientHelper.RpccEnableReplayLag(base.Server.Name, database.Guid, ActionInitiatorType.Administrator);
				return;
			}
			if (this.SuspendComment != null && this.SuspendComment.Length > 512)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "Suspend-DBC {0}: ProcessRecordWorker: SuspendComment length (length={1}, max length={2}) is too long: {3}", new object[]
				{
					this.DataObject.Identity,
					this.SuspendComment.Length,
					512,
					this.SuspendComment
				});
				base.WriteError(new SuspendCommentTooLongException(this.SuspendComment.Length, 512), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (!base.Stopping)
			{
				if (!base.UseRpc)
				{
					this.m_suspendThread = this.BeginSuspendUsingState(replayConfiguration);
				}
				else
				{
					this.m_suspendThread = base.BeginRpcOperation();
				}
				try
				{
					TimeSpan timeSpan = base.UseRpc ? DatabaseCopyStateAction.TimeoutRpc : SuspendDatabaseCopy.Timeout;
					if (!this.m_suspendThread.Join(timeSpan))
					{
						ExTraceGlobals.CmdletsTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "Suspend-DBC: suspend is being slow: timeout={0}", timeSpan);
						this.WriteWarning(Strings.SuspendSgcTimeout);
						if (!base.UseRpc)
						{
							this.m_suspendThread.Join();
						}
						else
						{
							this.m_event.WaitOne();
						}
					}
				}
				finally
				{
					if (this.m_Exception != null)
					{
						if (this.m_Terminating)
						{
							base.ThrowTerminatingError(this.m_Exception, ErrorCategory.NotSpecified, null);
						}
						else
						{
							ErrorCategory category;
							this.TranslateException(ref this.m_Exception, out category);
							if (this.m_Exception is ReplayServiceSuspendWantedSetException)
							{
								base.WriteWarning(this.m_Exception.Message);
								this.m_fSuccess = true;
							}
							else if (this.m_Exception is ReplayServiceSuspendRpcPartialSuccessCatalogFailedException)
							{
								base.WriteWarning(this.m_Exception.Message);
								this.m_fSuccess = true;
							}
							else if (!this.m_fFallbackToState)
							{
								this.WriteError(this.m_Exception, category, null, false);
							}
							else if (!base.Stopping)
							{
								ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "ProcessRecordWorker: There was an RPC connection error, so now falling back to Suspend through the State, for {0}.", this.DataObject.Identity);
								this.WriteWarning(Strings.SuspendSgcFallbackToState(this.DataObject.Identity.ToString(), this.m_Exception.Message));
							}
						}
					}
				}
			}
			if (!this.m_fSuccess && base.Stopping)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "Suspend was cancelled for {0}", this.DataObject.Identity);
				return;
			}
			if (!base.UseRpc && this.m_Exception == null && this.m_fSuccess)
			{
				replayConfiguration.ReplayState.SuspendMessage = this.SuspendComment;
			}
			else if (base.UseRpc && this.m_fFallbackToState)
			{
				ReplayConfiguration replayConfiguration2 = base.ConstructReplayConfiguration(this.DataObject.GetDatabase<Database>());
				replayConfiguration2.ReplayState.SuspendLockRemote.EnterSuspend();
				replayConfiguration2.ReplayState.SuspendMessage = this.SuspendComment;
				this.m_fSuccess = true;
			}
			if (this.m_fSuccess)
			{
				ExTraceGlobals.CmdletsTracer.TraceDebug<string, ObjectId>((long)this.GetHashCode(), "Suspended ({0}) for {1}", this.SuspendComment, this.DataObject.Identity);
				ExTraceGlobals.PFDTracer.TracePfd<int, string, ObjectId>((long)this.GetHashCode(), "PFD CRS {0} Sucessfully Suspended ({1}) for {2}", 20955, this.SuspendComment, this.DataObject.Identity);
				if (this.m_fFallbackToState)
				{
					ReplayEventLogConstants.Tuple_SuspendMarkedForDatabaseCopy.LogEvent(null, new object[]
					{
						this.DataObject.Identity
					});
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0014E6F0 File Offset: 0x0014C8F0
		private Thread BeginSuspendUsingState(ReplayConfiguration replayConfiguration)
		{
			ReplayState replayState = replayConfiguration.ReplayState;
			Thread thread = new Thread(new ParameterizedThreadStart(this.SuspendStateThreadProc));
			thread.IsBackground = true;
			thread.Start(replayState);
			return thread;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0014E728 File Offset: 0x0014C928
		private void SuspendStateThreadProc(object stateObject)
		{
			try
			{
				ReplayState replayState = (ReplayState)stateObject;
				replayState.SuspendLockRemote.EnterSuspend();
				this.m_fSuccess = true;
			}
			catch (Exception ex)
			{
				if (AmExceptionHelper.IsKnownClusterException(this, ex))
				{
					this.m_Exception = ex;
				}
				else
				{
					this.m_Exception = ex;
					this.m_Terminating = true;
				}
			}
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0014E784 File Offset: 0x0014C984
		protected override void RpcOperation()
		{
			Database database = this.DataObject.GetDatabase<Database>();
			if (!base.IsActivationRpcSupported)
			{
				ReplayRpcClientWrapper.RequestSuspend(base.GetServerFqdn(), database.Guid, this.SuspendComment);
				return;
			}
			DatabaseCopyActionFlags flags = DatabaseCopyActionFlags.Replication | DatabaseCopyActionFlags.Activation;
			if (this.ActivationOnly)
			{
				flags = DatabaseCopyActionFlags.Activation;
			}
			if (base.IsRequestSuspend3RpcSupported)
			{
				ReplayRpcClientHelper.RequestSuspend3(base.GetServerFqdn(), database.Guid, this.SuspendComment, (uint)flags, 2U);
				return;
			}
			ReplayRpcClientWrapper.RequestSuspend2(base.GetServerFqdn(), database.Guid, this.SuspendComment, (uint)flags);
		}

		// Token: 0x04002F2E RID: 12078
		private const string propnameSuspendComment = "SuspendComment";

		// Token: 0x04002F2F RID: 12079
		public const int SuspendCommentLengthLimit = 512;

		// Token: 0x04002F30 RID: 12080
		public static readonly TimeSpan Timeout = new TimeSpan(0, 0, 15);

		// Token: 0x04002F31 RID: 12081
		private Thread m_suspendThread;

		// Token: 0x04002F32 RID: 12082
		private bool m_Terminating;
	}
}
