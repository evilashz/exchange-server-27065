using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Management.Automation;
using System.Net.Sockets;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000031 RID: 49
	public abstract class Task : PSCmdlet, IDisposable, ICommandShell, ITaskIOPipeline
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008412 File Offset: 0x00006612
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000841A File Offset: 0x0000661A
		internal TaskContext CurrentTaskContext { get; private set; }

		// Token: 0x06000202 RID: 514 RVA: 0x00008440 File Offset: 0x00006640
		static Task()
		{
			if (ExchangeSetupContext.IsUnpacked)
			{
				Task.fileSearchAssemblyResolver.FileNameFilter = ((string fileName) => fileName.StartsWith("Microsoft.Exchange."));
				Task.fileSearchAssemblyResolver.Recursive = false;
				Task.fileSearchAssemblyResolver.SearchPaths = new string[]
				{
					ExchangeSetupContext.BinPath,
					Path.Combine(ExchangeSetupContext.BinPath, "FIP-FS\\Bin"),
					Path.Combine(ExchangeSetupContext.BinPath, "CmdletExtensionAgents"),
					Path.Combine(ExchangeSetupContext.BinPath, "res")
				};
				Task.fileSearchAssemblyResolver.ErrorTracer = delegate(string error)
				{
					TaskLogger.Trace(error, new object[0]);
				};
				Task.fileSearchAssemblyResolver.Install();
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008514 File Offset: 0x00006714
		internal Task()
		{
			this.CurrentTaskContext = new TaskContext(this);
			TaskLogger.LogEnter(new object[]
			{
				base.GetType().FullName
			});
			this.Fields = new PropertyBag();
			this.taskIOPipeline = new TaskIOPipeline(this.CurrentTaskContext);
			this.taskIOPipeline.PrependTaskIOPipelineHandler(this);
			ITaskModuleFactory taskModuleFactory = this.CreateTaskModuleFactory();
			this.taskModules.AddRange(taskModuleFactory.Create(this.CurrentTaskContext));
			this.taskEvents = new TaskEvent(this.CurrentTaskContext);
			TaskLogger.LogExit();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000085C0 File Offset: 0x000067C0
		~Task()
		{
			this.Dispose(false);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000085F0 File Offset: 0x000067F0
		public void SetFileSearchPath(string[] searchPath)
		{
			Task.fileSearchAssemblyResolver.SearchPaths = searchPath;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000085FD File Offset: 0x000067FD
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00008605 File Offset: 0x00006805
		internal ConfigurationContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000860E File Offset: 0x0000680E
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00008616 File Offset: 0x00006816
		protected internal PropertyBag Fields { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000861F File Offset: 0x0000681F
		internal int CurrentObjectIndex
		{
			get
			{
				return this.CurrentTaskContext.CurrentObjectIndex;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000862C File Offset: 0x0000682C
		internal TaskStage Stage
		{
			get
			{
				return this.CurrentTaskContext.Stage;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000863C File Offset: 0x0000683C
		internal ADServerSettings ServerSettings
		{
			get
			{
				ADDriverContext threadADContext = ADSessionSettings.GetThreadADContext();
				if (threadADContext == null)
				{
					return null;
				}
				return threadADContext.ServerSettings;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000865C File Offset: 0x0000685C
		private TaskInvocationInfo MyInvocationInfo
		{
			get
			{
				if (this.invocationInfo == null)
				{
					this.invocationInfo = this.CreateFromCmdletInvocationInfo();
					ActionPreference preference;
					if (this.TryGetVariableValue<ActionPreference>("VerbosePreference", out preference))
					{
						this.invocationInfo.IsVerboseOn = Task.IsSwitchOn(base.MyInvocation.BoundParameters, "Verbose", preference);
					}
					if (this.TryGetVariableValue<ActionPreference>("DebugPreference", out preference))
					{
						this.invocationInfo.IsDebugOn = Task.IsSwitchOn(base.MyInvocation.BoundParameters, "Debug", preference);
					}
				}
				return this.invocationInfo;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000086E3 File Offset: 0x000068E3
		public new ISessionState SessionState
		{
			get
			{
				return this.CurrentTaskContext.SessionState;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000086F0 File Offset: 0x000068F0
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000086FD File Offset: 0x000068FD
		public new string ParameterSetName
		{
			get
			{
				return this.MyInvocationInfo.ParameterSetName;
			}
			set
			{
				this.MyInvocationInfo.ParameterSetName = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000870B File Offset: 0x0000690B
		internal ProvisioningCache ProvisioningCache
		{
			get
			{
				return ProvisioningCache.Instance;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008712 File Offset: 0x00006912
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00008738 File Offset: 0x00006938
		protected SwitchParameter InternalForce
		{
			get
			{
				return (SwitchParameter)(this.Fields["InternalForce"] ?? false);
			}
			set
			{
				this.Fields["InternalForce"] = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00008750 File Offset: 0x00006950
		public bool NeedSuppressingPiiData
		{
			get
			{
				return this.ExchangeRunspaceConfig != null && this.ExchangeRunspaceConfig.NeedSuppressingPiiData;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008767 File Offset: 0x00006967
		protected internal ProvisioningHandler[] ProvisioningHandlers
		{
			get
			{
				return this.provisioningHandlers;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000876F File Offset: 0x0000696F
		protected internal bool IsProvisioningLayerAvailable
		{
			get
			{
				return this.provisioningHandlers != null && this.provisioningHandlers.Length > 0;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008786 File Offset: 0x00006986
		internal string ExecutingUserIdentityName
		{
			get
			{
				if (this.CurrentTaskContext.UserInfo != null)
				{
					return this.CurrentTaskContext.UserInfo.ExecutingUserIdentityName;
				}
				return null;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000087A7 File Offset: 0x000069A7
		internal bool TryGetExecutingUserId(out ADObjectId executingUserId)
		{
			executingUserId = ((this.CurrentTaskContext.UserInfo == null) ? null : this.CurrentTaskContext.UserInfo.ExecutingUserId);
			return executingUserId != null;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000087D3 File Offset: 0x000069D3
		internal ExchangeRunspaceConfiguration ExchangeRunspaceConfig
		{
			get
			{
				return this.CurrentTaskContext.ExchangeRunspaceConfig;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000087E0 File Offset: 0x000069E0
		internal OrganizationId ExecutingUserOrganizationId
		{
			get
			{
				if (this.CurrentTaskContext.UserInfo != null)
				{
					return this.CurrentTaskContext.UserInfo.ExecutingUserOrganizationId;
				}
				return null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008801 File Offset: 0x00006A01
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00008822 File Offset: 0x00006A22
		protected internal OrganizationId CurrentOrganizationId
		{
			get
			{
				if (this.CurrentTaskContext.UserInfo != null)
				{
					return this.CurrentTaskContext.UserInfo.CurrentOrganizationId;
				}
				return null;
			}
			set
			{
				this.CurrentTaskContext.UserInfo.UpdateCurrentOrganizationId(value);
				ProvisioningLayer.SetUserScope(this);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000883B File Offset: 0x00006A3B
		internal ScopeSet ScopeSet
		{
			get
			{
				return this.CurrentTaskContext.ScopeSet;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008848 File Offset: 0x00006A48
		protected PropertyBag UserSpecifiedParameters
		{
			get
			{
				return this.MyInvocationInfo.UserSpecifiedParameters;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008855 File Offset: 0x00006A55
		protected virtual LocalizedString ConfirmationMessage
		{
			get
			{
				return LocalizedString.Empty;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000885C File Offset: 0x00006A5C
		protected virtual ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new TaskModuleFactory();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008863 File Offset: 0x00006A63
		protected bool HasErrors
		{
			get
			{
				return this.CurrentTaskContext.ErrorInfo.HasErrors;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008875 File Offset: 0x00006A75
		protected int ProcessId
		{
			get
			{
				return Constants.ProcessId;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000887C File Offset: 0x00006A7C
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00008898 File Offset: 0x00006A98
		protected string AdditionalLogData
		{
			get
			{
				return (string)this.CurrentTaskContext.Items["Log_AdditionalLogData"];
			}
			set
			{
				this.CurrentTaskContext.Items["Log_AdditionalLogData"] = value;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000088B0 File Offset: 0x00006AB0
		public void Dispose()
		{
			TaskLogger.LogEnter();
			this.Dispose(true);
			GC.SuppressFinalize(this);
			TaskLogger.LogExit();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000088C9 File Offset: 0x00006AC9
		internal void Execute()
		{
			this.BeginProcessing();
			this.ProcessRecord();
			this.EndProcessing();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000088DD File Offset: 0x00006ADD
		internal virtual void PreInternalProcessRecord()
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000088E0 File Offset: 0x00006AE0
		internal void CheckExclusiveParameters(params object[] parameterKeys)
		{
			object obj = null;
			string arg = null;
			foreach (object obj2 in parameterKeys)
			{
				if (this.Fields.IsModified(obj2))
				{
					if (obj == null)
					{
						obj = obj2;
						arg = ((obj2 is ADPropertyDefinition) ? ((ADPropertyDefinition)obj2).Name : obj2.ToString());
					}
					else
					{
						string arg2 = (obj2 is ADPropertyDefinition) ? ((ADPropertyDefinition)obj2).Name : obj2.ToString();
						this.ThrowTerminatingError(new ArgumentException(Strings.MutuallyExclusiveArguments(arg, arg2)), ErrorCategory.InvalidArgument, null);
					}
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00008973 File Offset: 0x00006B73
		protected virtual bool IsKnownException(Exception e)
		{
			return TaskHelper.IsTaskKnownException(e);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000897C File Offset: 0x00006B7C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.context = null;
				this.Fields = null;
				if (this.taskModules != null)
				{
					foreach (ITaskModule taskModule in this.taskModules)
					{
						taskModule.Dispose();
					}
					this.taskModules = null;
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008B16 File Offset: 0x00006D16
		protected sealed override void BeginProcessing()
		{
			this.ProcessTaskStage(TaskStage.BeginProcessing, delegate
			{
				using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.BeginProcessing/Task.InitTaskContext", LoggerHelper.CmdletPerfMonitors))
				{
					this.InitTaskContext();
				}
				using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.BeginProcessing/Task.InitTaskModule", LoggerHelper.CmdletPerfMonitors))
				{
					this.InitTaskModule();
				}
				this.taskEvents.OnPreInit(null);
				this.WriteVerbose(Strings.VerboseTaskBeginProcessing(this.MyInvocationInfo.InvocationName));
				if (!ProvisioningLayer.Disabled)
				{
					this.provisioningHandlers = ProvisioningLayer.GetProvisioningHandlers(this);
					ProvisioningLayer.SetLogMessageDelegate(this);
				}
			}, delegate
			{
				using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.BeginProcessing/InternalBeginProcessing", LoggerHelper.CmdletPerfMonitors))
				{
					this.InternalBeginProcessing();
				}
			}, delegate
			{
				this.taskEvents.OnInitCompleted(null);
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008B44 File Offset: 0x00006D44
		private void InitTaskModule()
		{
			TaskLogger.LogEnter();
			foreach (ITaskModule taskModule in this.taskModules)
			{
				taskModule.Init(this.taskEvents);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008BA8 File Offset: 0x00006DA8
		private void InitTaskContext()
		{
			if (this.CurrentTaskContext.SessionState == null)
			{
				this.CurrentTaskContext.SessionState = ((base.SessionState != null) ? new PSSessionState(base.SessionState) : null);
			}
			if (this.CurrentTaskContext.InvocationInfo != null)
			{
				this.invocationInfo = this.CurrentTaskContext.InvocationInfo;
				foreach (object key in this.Fields.Keys)
				{
					object value = this.Fields[key];
					this.CurrentTaskContext.InvocationInfo.Fields[key] = value;
					this.CurrentTaskContext.InvocationInfo.AddToUserSpecifiedParameter(key, value);
				}
				this.Fields = this.CurrentTaskContext.InvocationInfo.Fields;
				return;
			}
			this.CurrentTaskContext.InvocationInfo = this.MyInvocationInfo;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008CA8 File Offset: 0x00006EA8
		protected virtual void InternalBeginProcessing()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008D36 File Offset: 0x00006F36
		protected sealed override void EndProcessing()
		{
			this.ProcessTaskStage(TaskStage.EndProcessing, delegate
			{
				this.WriteVerbose(Strings.VerboseTaskEndProcessing(this.MyInvocationInfo.InvocationName));
				this.taskEvents.OnPreRelease(null);
			}, delegate
			{
				using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.EndProcessing/InternalEndProcessing", LoggerHelper.CmdletPerfMonitors))
				{
					this.InternalEndProcessing();
				}
				ProvisioningLayer.EndProcessing(this);
			}, delegate
			{
				this.taskEvents.OnRelease(null);
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008D63 File Offset: 0x00006F63
		protected virtual void InternalEndProcessing()
		{
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008D68 File Offset: 0x00006F68
		protected sealed override void StopProcessing()
		{
			using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.StopProcessing", LoggerHelper.CmdletPerfMonitors))
			{
				TaskLogger.LogEnter();
				try
				{
					this.taskEvents.OnPreStop(null);
					this.CurrentTaskContext.WasStopped = true;
					CmdletLogger.SafeSetLogger(this.CurrentTaskContext.UniqueId, RpsCmdletMetadata.ExecutionResult, "Stopped");
					using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.StopProcessing/InternalStopProcessing", LoggerHelper.CmdletPerfMonitors))
					{
						this.InternalStopProcessing();
					}
				}
				catch (Exception ex)
				{
					TaskErrorEventArg taskErrorEventArg = new TaskErrorEventArg(ex, null);
					this.taskEvents.OnError(new GenericEventArg<TaskErrorEventArg>(taskErrorEventArg));
					TaskLogger.LogError(ex);
					if (!taskErrorEventArg.ExceptionHandled)
					{
						throw;
					}
				}
				finally
				{
					this.taskEvents.OnStop(null);
					TaskLogger.LogExit();
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008E8C File Offset: 0x0000708C
		protected virtual void InternalStopProcessing()
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000920E File Offset: 0x0000740E
		protected sealed override void ProcessRecord()
		{
			this.ProcessTaskStage(TaskStage.ProcessRecord, delegate
			{
				this.CurrentTaskContext.CurrentObjectIndex++;
				if (this.CurrentObjectIndex == 0)
				{
					this.userCurrentOrganizationId = this.CurrentOrganizationId;
				}
				else
				{
					this.CurrentOrganizationId = this.userCurrentOrganizationId;
				}
				if (!this.MyInvocationInfo.IsCmdletInvokedWithoutPSFramework)
				{
					this.MyInvocationInfo.UpdateSpecifiedParameters(base.MyInvocation.BoundParameters);
				}
				this.taskEvents.OnPreIterate(null);
				if (this.Context == null)
				{
					try
					{
						this.Context = new ConfigurationContext();
					}
					catch (SocketException exception)
					{
						this.WriteError(exception, (ErrorCategory)1001, this.CurrentObjectIndex);
					}
				}
				if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					string arg = TaskVerboseStringHelper.FormatUserSpecifiedParameters(this.UserSpecifiedParameters);
					ExTraceGlobals.LogTracer.Information<string, string>(0L, "Processing {0} {1}", this.MyInvocationInfo.CommandName, arg);
				}
				ProvisioningLayer.SetUserSpecifiedParameters(this, this.UserSpecifiedParameters);
				ProvisioningLayer.SetProvisioningCache(this, this.ProvisioningCache);
				ProvisioningLayer.SetUserScope(this);
			}, delegate
			{
				using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task.ProcessRecord/InnerProcessRecord", LoggerHelper.CmdletPerfMonitors))
				{
					using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "InnerProcessRecord/InternalStateReset", LoggerHelper.CmdletPerfMonitors))
					{
						this.InternalStateReset();
					}
					if (!this.HasErrors)
					{
						using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "InnerProcessRecord/InternalValidate", LoggerHelper.CmdletPerfMonitors))
						{
							this.InternalValidate();
						}
						this.PostInternalValidate();
					}
					if (!this.HasErrors)
					{
						ProvisioningValidationError[] array = ProvisioningLayer.ValidateUserScope(this);
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								ProvisioningValidationException exception = new ProvisioningValidationException(array[i].Description, array[i].AgentName, array[i].Exception);
								this.WriteError(exception, (ErrorCategory)array[i].ErrorCategory, null, array.Length - 1 == i);
							}
						}
						this.InternalProvisioningValidation();
					}
					if (!this.HasErrors)
					{
						if (this.ConfirmationMessage == LocalizedString.Empty || this.ShouldProcess(this.ConfirmationMessage))
						{
							string orgId = (this.CurrentOrganizationId != null) ? this.CurrentOrganizationId.ToString() : string.Empty;
							if (this.IsVerboseOn && !TaskLogger.IsSetupLogging)
							{
								this.WriteVerbose(Strings.VerboseResolvedOrganization(orgId));
							}
							this.PreInternalProcessRecord();
							using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "InnerProcessRecord/InternalProcessRecord", LoggerHelper.CmdletPerfMonitors))
							{
								this.InternalProcessRecord();
								goto IL_1CA;
							}
						}
						this.CurrentTaskContext.WasCancelled = true;
						CmdletLogger.SafeSetLogger(this.CurrentTaskContext.UniqueId, RpsCmdletMetadata.ExecutionResult, "Cancelled");
					}
					IL_1CA:
					ProvisioningLayer.OnComplete(this, !this.CurrentTaskContext.WasCancelled && !this.HasErrors, null);
				}
			}, delegate
			{
				this.taskEvents.OnIterateCompleted(null);
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000923B File Offset: 0x0000743B
		protected virtual void InternalStateReset()
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000923D File Offset: 0x0000743D
		protected virtual void InternalValidate()
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000923F File Offset: 0x0000743F
		protected virtual void PostInternalValidate()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009241 File Offset: 0x00007441
		protected virtual void InternalProcessRecord()
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009243 File Offset: 0x00007443
		protected virtual void InternalProvisioningValidation()
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009248 File Offset: 0x00007448
		private bool ProcessError(Exception exception, bool terminating)
		{
			bool flag;
			return this.ProcessError(exception, terminating, false, false, out flag);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009264 File Offset: 0x00007464
		private bool ProcessError(Exception exception, bool terminating, bool shouldRetryForRetryableError, bool shouldLogIfRetryNotHappens, out bool retryHappens)
		{
			TaskLogger.LogEnter();
			TaskErrorInfo errorInfo = this.CurrentTaskContext.ErrorInfo;
			bool? isUnknownException = null;
			if (errorInfo.Exception == null)
			{
				ErrorCategory errorCategory = ErrorCategory.NotSpecified;
				bool flag = this.IsKnownException(exception);
				isUnknownException = new bool?(!flag);
				if (flag)
				{
					this.TranslateException(ref exception, out errorCategory);
				}
				errorInfo.SetErrorInfo(exception, (ExchangeErrorCategory)errorCategory, flag ? null : this.CurrentObjectIndex, null, terminating, flag);
			}
			else
			{
				isUnknownException = new bool?(!errorInfo.IsKnownError);
				errorInfo.TerminatePipeline = terminating;
			}
			TaskErrorEventArg taskErrorEventArg = new TaskErrorEventArg(exception, isUnknownException);
			this.taskEvents.OnError(new GenericEventArg<TaskErrorEventArg>(taskErrorEventArg));
			if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.LogTracer.TraceError<int, string, Exception>(0L, "Cmdlet iteration index: '{0}', Stage: '{1}', Error: '{2}'", this.CurrentObjectIndex, this.Stage.ToString(), exception);
			}
			ProvisioningLayer.OnComplete(this, false, exception);
			retryHappens = false;
			bool result;
			try
			{
				if (taskErrorEventArg.ExceptionHandled)
				{
					result = true;
				}
				else
				{
					if (exception is TransientException)
					{
						if (shouldRetryForRetryableError)
						{
							retryHappens = true;
							return true;
						}
						if (shouldLogIfRetryNotHappens)
						{
							CmdletLogger.SafeAppendGenericInfo("RetryableError-Not-Retried", "true");
						}
					}
					if (this.Stage != TaskStage.ProcessRecord)
					{
						this.CurrentTaskContext.ShouldTerminateCmdletExecution = true;
					}
					CmdletLogger.SafeSetLogger(this.CurrentTaskContext.UniqueId, RpsCmdletMetadata.ExecutionResult, "Error");
					if (!errorInfo.IsKnownError)
					{
						TaskLogger.LogEvent(TaskEventLogConstants.Tuple_TaskThrowingUnhandledException, this.MyInvocationInfo, exception.Message, new object[]
						{
							this.MyInvocationInfo.DisplayName,
							exception
						});
						CmdletLogHelper.SetCmdletErrorType(this.CurrentTaskContext.UniqueId, "UnHandled");
						result = false;
					}
					else if (this.CurrentTaskContext.ServerSettingsAfterFailOver != null && (exception is ADTransientException || exception is LdapException || exception is DirectoryOperationException))
					{
						ADServerSettingsChangedException ex = new ADServerSettingsChangedException(DirectoryStrings.RunspaceServerSettingsChanged, this.CurrentTaskContext.ServerSettingsAfterFailOver);
						CmdletLogger.SafeAppendGenericError(this.CurrentTaskContext.UniqueId, "ADServerSettingsChangedException", ex, (Exception ex1) => false);
						this.WriteError(ex, ExchangeErrorCategory.ServerTransient, this.CurrentTaskContext.CurrentObjectIndex, terminating);
						result = true;
					}
					else
					{
						if (errorInfo.TerminatePipeline)
						{
							CmdletLogHelper.SetCmdletErrorType(this.CurrentTaskContext.UniqueId, "TerminatePipeline");
							this.CurrentTaskContext.ShouldTerminateCmdletExecution = true;
							this.ThrowTerminatePipelineError(Task.CreateErrorRecord(errorInfo));
						}
						this.WriteError(errorInfo);
						result = true;
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009508 File Offset: 0x00007708
		protected virtual void TranslateException(ref Exception e, out ErrorCategory category)
		{
			category = ErrorCategory.NotSpecified;
			if (typeof(TranslatableProvisioningException).IsInstanceOfType(e) && e.InnerException != null)
			{
				e = e.InnerException;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009534 File Offset: 0x00007734
		internal static ErrorRecord CreateErrorRecord(TaskErrorInfo errorInfo)
		{
			int num = LocalizedException.GenerateErrorCode(errorInfo.Exception);
			string str = string.Format("[Server={0},RequestId={1},TimeStamp={2}] ", Environment.MachineName, (ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value : Guid.Empty, DateTime.UtcNow);
			str += string.Format("[FailureCategory={0}] ", FailureCategory.Cmdlet.ToString() + "-" + errorInfo.Exception.GetType().Name);
			ErrorRecord errorRecord = new ErrorRecord(errorInfo.Exception, str + num.ToString("X"), (ErrorCategory)errorInfo.ExchangeErrorCategory.Value, errorInfo.Target);
			if (errorInfo.HelpUrl != null)
			{
				errorRecord.ErrorDetails = new ErrorDetails(errorInfo.Exception.Message);
				errorRecord.ErrorDetails.RecommendedAction = errorInfo.HelpUrl;
			}
			return errorRecord;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000962C File Offset: 0x0000782C
		private TaskInvocationInfo CreateFromCmdletInvocationInfo()
		{
			InvocationInfo invocationInfo = this.SessionState.Variables["MyInvocation"] as InvocationInfo;
			string rootScriptName = (invocationInfo != null) ? invocationInfo.ScriptName : null;
			return new TaskInvocationInfo((base.MyInvocation.MyCommand != null) ? base.MyInvocation.MyCommand.Name : this.GenerateCmdletName(), base.MyInvocation.InvocationName, base.MyInvocation.ScriptName, rootScriptName, base.MyInvocation.CommandOrigin == CommandOrigin.Internal, base.MyInvocation.BoundParameters, this.Fields, (base.Host == null) ? "HostWithoutPSRunspace" : base.Host.Name)
			{
				ParameterSetName = base.ParameterSetName
			};
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000096EC File Offset: 0x000078EC
		private string GenerateCmdletName()
		{
			CmdletAttribute cmdletAttribute = (CmdletAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(CmdletAttribute), false);
			if (cmdletAttribute != null)
			{
				return string.Format("{0}-{1}", cmdletAttribute.VerbName, cmdletAttribute.NounName);
			}
			return base.GetType().Name;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000973C File Offset: 0x0000793C
		private static string CurrentTimeString
		{
			get
			{
				return ExDateTime.UtcNow.ToString("[HH:mm:ss.fff G\\MT] ");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000975B File Offset: 0x0000795B
		private string VerboseTaskName
		{
			get
			{
				if (this.verboseTaskName == null)
				{
					this.verboseTaskName = this.MyInvocationInfo.CommandName + " : ";
				}
				return this.verboseTaskName;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009786 File Offset: 0x00007986
		internal bool IsDebugOn
		{
			get
			{
				return this.MyInvocationInfo.IsDebugOn;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009793 File Offset: 0x00007993
		internal bool IsVerboseOn
		{
			get
			{
				return this.MyInvocationInfo.IsVerboseOn;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000097A0 File Offset: 0x000079A0
		public void WriteVerbose(LocalizedString text)
		{
			TaskLogger.LogEnter();
			try
			{
				TaskLogger.Log(text);
				LocalizedString localizedString;
				this.taskIOPipeline.WriteVerbose(text, out localizedString);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000097E0 File Offset: 0x000079E0
		public void WriteDebug(LocalizedString text)
		{
			TaskLogger.LogEnter();
			try
			{
				TaskLogger.Log(text);
				LocalizedString localizedString;
				this.taskIOPipeline.WriteDebug(text, out localizedString);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009820 File Offset: 0x00007A20
		public void WriteError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteError(exception, category, target, true, null);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000982D File Offset: 0x00007A2D
		public void WriteError(LocalizedException exception, ExchangeErrorCategory category, object target)
		{
			this.WriteError(exception, (ErrorCategory)category, target, true);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009839 File Offset: 0x00007A39
		public virtual void WriteError(Exception exception, ErrorCategory category, object target, bool reThrow)
		{
			this.WriteError(exception, category, target, reThrow, null);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009847 File Offset: 0x00007A47
		public virtual void WriteError(LocalizedException exception, ExchangeErrorCategory category, object target, bool reThrow)
		{
			this.WriteError(exception, (ErrorCategory)category, target, reThrow);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009854 File Offset: 0x00007A54
		public void WriteError(Exception exception, ErrorCategory category, object target, bool reThrow, string helpUrl)
		{
			if (reThrow)
			{
				this.ThrowError(exception, category, target, helpUrl);
				return;
			}
			this.WriteErrorNoThrow(exception, category, target, helpUrl);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009874 File Offset: 0x00007A74
		public void WriteWarning(LocalizedString text, string helpUrl)
		{
			TaskLogger.LogEnter();
			try
			{
				TaskLogger.LogWarning(text);
				LocalizedString localizedString;
				this.taskIOPipeline.WriteWarning(text, helpUrl, out localizedString);
				if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.LogTracer.Information<int, TaskStage, LocalizedString>(0L, "Cmdlet iteration index: '{0}', Stage: '{1}', Warning: '{2}'", this.CurrentObjectIndex, this.Stage, text);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000098E0 File Offset: 0x00007AE0
		public virtual void WriteWarning(LocalizedString text)
		{
			this.WriteWarning(text, null);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000098EC File Offset: 0x00007AEC
		public void WriteProgress(LocalizedString activity, LocalizedString statusDescription, int percentCompleted)
		{
			TaskLogger.LogEnter();
			this.WriteProgress(new ExProgressRecord(this.CurrentObjectIndex, activity, statusDescription)
			{
				PercentComplete = percentCompleted
			});
			TaskLogger.LogExit();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009920 File Offset: 0x00007B20
		public void WriteProgress(ExProgressRecord record)
		{
			TaskLogger.LogEnter();
			ExProgressRecord exProgressRecord;
			this.taskIOPipeline.WriteProgress(record, out exProgressRecord);
			TaskLogger.LogExit();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009948 File Offset: 0x00007B48
		public bool ShouldProcess(LocalizedString message)
		{
			TaskLogger.LogEnter(new object[]
			{
				message
			});
			bool result;
			try
			{
				result = this.ShouldProcess(message.ToString(), null, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000999C File Offset: 0x00007B9C
		public bool ShouldContinue(LocalizedString message)
		{
			ConfirmationChoice confirmationChoice = ConfirmationChoice.No;
			if (this.confirmationPreferences.ContainsKey(message.BaseId))
			{
				confirmationChoice = this.confirmationPreferences[message.BaseId];
			}
			bool flag = confirmationChoice == ConfirmationChoice.YesToAll;
			bool flag2 = confirmationChoice == ConfirmationChoice.NoToAll;
			bool? flag3;
			this.taskIOPipeline.ShouldContinue(message, string.Empty, ref flag, ref flag2, out flag3);
			if (flag || flag2)
			{
				this.confirmationPreferences[message.BaseId] = (flag ? ConfirmationChoice.YesToAll : ConfirmationChoice.NoToAll);
			}
			return flag3.Value;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009A28 File Offset: 0x00007C28
		public new void WriteObject(object sendToPipeline)
		{
			this.CurrentTaskContext.ObjectWrittenToPipeline = true;
			object obj;
			this.taskIOPipeline.WriteObject(sendToPipeline, out obj);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009A50 File Offset: 0x00007C50
		public void ThrowTerminatingError(LocalizedException exception, ExchangeErrorCategory category, object target)
		{
			this.ThrowTerminatingError(exception, (ErrorCategory)category, target);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009A5B File Offset: 0x00007C5B
		public void SetShouldExit(int exitCode)
		{
			base.Host.SetShouldExit(exitCode);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009A69 File Offset: 0x00007C69
		public void PrependTaskIOPipelineHandler(ITaskIOPipeline pipeline)
		{
			this.taskIOPipeline.PrependTaskIOPipelineHandler(pipeline);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009A78 File Offset: 0x00007C78
		protected void ThrowTerminatingError(Exception exception, ErrorCategory category, object target)
		{
			TaskLogger.LogEnter(new object[]
			{
				(exception != null) ? exception.Message : string.Empty,
				category,
				target
			});
			TaskLogger.LogError(exception);
			this.CurrentTaskContext.ErrorInfo.SetErrorInfo(exception, (ExchangeErrorCategory)category, target, null, true, true);
			throw exception;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009ACF File Offset: 0x00007CCF
		protected void ThrowNonLocalizedTerminatingError(Exception exception, ExchangeErrorCategory category, object target)
		{
			this.ThrowTerminatingError(exception, (ErrorCategory)category, target);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009ADA File Offset: 0x00007CDA
		[Obsolete("Use WriteProgress(LocalizedString activity, LocalizedString statusDescription, int percentCompleted) instead.")]
		protected new void WriteProgress(ProgressRecord record)
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009ADC File Offset: 0x00007CDC
		[Obsolete("Use void WriteVerbose(LocalizedString text) instead.")]
		protected new void WriteVerbose(string text)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009ADE File Offset: 0x00007CDE
		[Obsolete("Use ShouldContinue(LocalizedString message) instead.")]
		protected new bool ShouldProcess(string target)
		{
			return base.ShouldProcess(target);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009AE8 File Offset: 0x00007CE8
		protected new bool ShouldProcess(string target, string action, string caption)
		{
			bool? flag;
			this.taskIOPipeline.ShouldProcess(target, action, caption, out flag);
			return flag.Value;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009B0D File Offset: 0x00007D0D
		[Obsolete("Use ThrowTerminatingError(LocalizedException e, ErrorCategory category, object target) instead.")]
		protected new void ThrowTerminatingError(ErrorRecord errorRecord)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009B0F File Offset: 0x00007D0F
		[Obsolete("Use WriteError(Exception e, ErrorCategory category, object target) instead.")]
		protected new void WriteError(ErrorRecord errorRecord)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009B14 File Offset: 0x00007D14
		public bool TryGetVariableValue<T>(string name, out T value)
		{
			object obj = null;
			if (this.SessionState != null && this.SessionState.Variables != null)
			{
				this.SessionState.Variables.TryGetValue(name, out obj);
			}
			if (obj == null)
			{
				value = default(T);
				return null == value;
			}
			if (obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			PSObject psobject = obj as PSObject;
			if (psobject != null && psobject.BaseObject is T)
			{
				value = (T)((object)psobject.BaseObject);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009BB0 File Offset: 0x00007DB0
		private void WriteErrorNoThrow(Exception exception, ErrorCategory errorCategory, object target, string helpUrl)
		{
			TaskLogger.LogEnter();
			TaskLogger.LogError(exception);
			try
			{
				if (!string.IsNullOrEmpty(helpUrl))
				{
					TaskLogger.Log(Strings.LogHelpUrl(helpUrl));
				}
				this.CurrentTaskContext.ErrorInfo.SetErrorInfo(exception, (ExchangeErrorCategory)errorCategory, target, helpUrl, false, true);
				this.WriteError(this.CurrentTaskContext.ErrorInfo);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009C20 File Offset: 0x00007E20
		private void WriteError(TaskErrorInfo errorInfo)
		{
			TaskErrorInfo taskErrorInfo;
			this.taskIOPipeline.WriteError(errorInfo, out taskErrorInfo);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009C3C File Offset: 0x00007E3C
		private void ThrowError(Exception exception, ErrorCategory errorCategory, object target, string helpUrl)
		{
			TaskLogger.LogEnter();
			TaskLogger.LogError(exception);
			if (!string.IsNullOrEmpty(helpUrl))
			{
				TaskLogger.Log(Strings.LogHelpUrl(helpUrl));
			}
			this.CurrentTaskContext.ErrorInfo.SetErrorInfo(exception, (ExchangeErrorCategory)errorCategory, target, helpUrl, false, true);
			throw exception;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009C76 File Offset: 0x00007E76
		private void ThrowTerminatePipelineError(ErrorRecord errorRecord)
		{
			throw new ThrowTerminatingErrorException(errorRecord);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009C80 File Offset: 0x00007E80
		private static bool IsSwitchOn(Dictionary<string, object> boundParameters, string parameterName, ActionPreference preference)
		{
			object obj;
			return preference != ActionPreference.SilentlyContinue || !boundParameters.TryGetValue(parameterName, out obj) || !(obj is SwitchParameter) || !((SwitchParameter)obj).IsPresent || true;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009CB6 File Offset: 0x00007EB6
		[Conditional("DEBUG")]
		private static void AssertObjectIsSerializable(object obj)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009CF0 File Offset: 0x00007EF0
		public bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			output = input;
			this.StartAndEndTaskIOPipelineFunctionInternalTracking(this.CurrentTaskContext.UniqueId, "PowerShellLatency", "WriteVerbose", delegate()
			{
				this.WriteVerbose(Task.CurrentTimeString + this.VerboseTaskName + input);
			});
			return false;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009D70 File Offset: 0x00007F70
		public bool WriteDebug(LocalizedString input, out LocalizedString output)
		{
			output = input;
			this.StartAndEndTaskIOPipelineFunctionInternalTracking(this.CurrentTaskContext.UniqueId, "PowerShellLatency", "WriteDebug", delegate()
			{
				this.WriteDebug(Task.CurrentTimeString + input);
			});
			return false;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009E24 File Offset: 0x00008024
		public bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output)
		{
			output = input;
			this.StartAndEndTaskIOPipelineFunctionInternalTracking(this.CurrentTaskContext.UniqueId, "PowerShellLatency", "WriteWarning", delegate()
			{
				if (string.IsNullOrEmpty(helperUrl))
				{
					this.WriteWarning(input);
					return;
				}
				this.WriteWarning(WarningReportEventArgs.CombineWarningMessageHelpUrl(input, helperUrl));
			});
			return false;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009EA0 File Offset: 0x000080A0
		public bool WriteError(TaskErrorInfo input, out TaskErrorInfo output)
		{
			output = input;
			if (input != null)
			{
				this.StartAndEndTaskIOPipelineFunctionInternalTracking(this.CurrentTaskContext.UniqueId, "PowerShellLatency", "WriteError", delegate()
				{
					this.WriteError(Task.CreateErrorRecord(input));
				});
			}
			return false;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009F1C File Offset: 0x0000811C
		public bool WriteObject(object input, out object output)
		{
			output = input;
			this.StartAndEndTaskIOPipelineFunctionInternalTracking(this.CurrentTaskContext.UniqueId, "PowerShellLatency", "WriteObject", delegate()
			{
				this.WriteObject(input);
			});
			return false;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009F88 File Offset: 0x00008188
		public bool WriteProgress(ExProgressRecord input, out ExProgressRecord output)
		{
			output = input;
			if (input != null)
			{
				this.StartAndEndTaskIOPipelineFunctionInternalTracking(this.CurrentTaskContext.UniqueId, "PowerShellLatency", "WriteProgress", delegate()
				{
					this.WriteProgress(input);
				});
			}
			return false;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009FE8 File Offset: 0x000081E8
		public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output)
		{
			output = new bool?(false);
			if (this.CurrentTaskContext.WasCancelled || this.CurrentTaskContext.WasStopped)
			{
				return false;
			}
			using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "UserInteractionLatency", "ShouldContinue", LoggerHelper.CmdletPerfMonitors))
			{
				output = new bool?(base.ShouldContinue(query, caption, ref yesToAll, ref noToAll));
			}
			return false;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A09C File Offset: 0x0000829C
		public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out bool? output)
		{
			output = new bool?(this.StartAndEndTaskIOPipelineFunctionInternalTracking<bool>(this.CurrentTaskContext.UniqueId, "UserInteractionLatency", "ShouldProcess", () => this.ShouldProcess(verboseDescription, verboseWarning, caption)));
			return false;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A100 File Offset: 0x00008300
		private void StartAndEndTaskIOPipelineFunctionInternalTracking(Guid cmdletUniqueId, string groupName, string funcName, Action func)
		{
			if (this.CurrentTaskContext.WasCancelled || this.CurrentTaskContext.WasStopped)
			{
				return;
			}
			using (new CmdletMonitoredScope(cmdletUniqueId, groupName, funcName, LoggerHelper.CmdletPerfMonitors))
			{
				func();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A15C File Offset: 0x0000835C
		private T StartAndEndTaskIOPipelineFunctionInternalTracking<T>(Guid cmdletUniqueId, string groupName, string funcName, Func<T> func)
		{
			if (this.CurrentTaskContext.WasCancelled || this.CurrentTaskContext.WasStopped)
			{
				return default(T);
			}
			T result;
			using (new CmdletMonitoredScope(cmdletUniqueId, groupName, funcName, LoggerHelper.CmdletPerfMonitors))
			{
				result = func();
			}
			return result;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A1C0 File Offset: 0x000083C0
		protected virtual bool IsRetryableTask
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A1C4 File Offset: 0x000083C4
		private void ProcessTaskStage(TaskStage taskStage, Action initFunc, Action mainFunc, Action completeFunc)
		{
			TaskLogger.LogEnter(new object[]
			{
				taskStage
			});
			if (this.CurrentTaskContext.ShouldTerminateCmdletExecution && taskStage == TaskStage.ProcessRecord)
			{
				TaskLogger.LogExit();
				return;
			}
			using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task." + taskStage, LoggerHelper.CmdletPerfMonitors))
			{
				bool terminatePipelineIfFailed = taskStage != TaskStage.ProcessRecord;
				try
				{
					this.CurrentTaskContext.Stage = taskStage;
					CmdletLogHelper.ClearCmdletErrorType(this.CurrentTaskContext.UniqueId);
					if (this.CurrentTaskContext != null)
					{
						this.CurrentTaskContext.Reset();
					}
					using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task." + taskStage + ".Init", LoggerHelper.CmdletPerfMonitors))
					{
						if (!this.InvokeNonRetryableFunc(initFunc, terminatePipelineIfFailed))
						{
							return;
						}
					}
					using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task." + taskStage + ".Retry", LoggerHelper.CmdletPerfMonitors))
					{
						this.InvokeRetryableFunc("Task." + taskStage + ".Main", mainFunc, terminatePipelineIfFailed);
					}
				}
				finally
				{
					using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", "Task." + taskStage + ".Complete", LoggerHelper.CmdletPerfMonitors))
					{
						this.InvokeNonRetryableFunc(completeFunc, terminatePipelineIfFailed);
					}
					TaskLogger.LogExit();
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A3E0 File Offset: 0x000085E0
		private bool InvokeNonRetryableFunc(Action func, bool terminatePipelineIfFailed)
		{
			bool result;
			try
			{
				func();
				result = true;
			}
			catch (Exception exception)
			{
				if (!this.ProcessError(exception, terminatePipelineIfFailed))
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A41C File Offset: 0x0000861C
		private void InvokeRetryableFunc(string funcName, Action func, bool terminatePipelineIfFailed)
		{
			int num = this.IsRetryableTask ? (AppSettings.Current.MaxCmdletRetryCnt + 1) : 1;
			for (int i = 0; i < num; i++)
			{
				if (this.CurrentTaskContext != null)
				{
					this.CurrentTaskContext.Reset();
				}
				bool flag = false;
				Exception ex = null;
				using (new CmdletMonitoredScope(this.CurrentTaskContext.UniqueId, "BizLogic", funcName + "." + (i + 1), LoggerHelper.CmdletPerfMonitors))
				{
					try
					{
						func();
					}
					catch (Exception ex2)
					{
						ex = ex2;
						bool shouldRetryForRetryableError = i < num - 1 && !this.CurrentTaskContext.ObjectWrittenToPipeline;
						bool shouldLogIfRetryNotHappens = i < num - 1 && this.CurrentTaskContext.ObjectWrittenToPipeline;
						if (!this.ProcessError(ex2, terminatePipelineIfFailed, shouldRetryForRetryableError, shouldLogIfRetryNotHappens, out flag))
						{
							throw;
						}
					}
				}
				if (!flag)
				{
					break;
				}
				this.CurrentTaskContext.CommandShell.WriteVerbose(Strings.WarningTaskRetried(ex.Message));
				CmdletLogger.SafeAppendGenericInfo(this.CurrentTaskContext.UniqueId, funcName + "#Retry#" + (i + 1), (this.CurrentTaskContext.ErrorInfo.Exception != null) ? this.CurrentTaskContext.ErrorInfo.Exception.GetType().Name : "NULL");
			}
		}

		// Token: 0x0400009A RID: 154
		private ConfigurationContext context;

		// Token: 0x0400009B RID: 155
		private ProvisioningHandler[] provisioningHandlers;

		// Token: 0x0400009C RID: 156
		private OrganizationId userCurrentOrganizationId;

		// Token: 0x0400009D RID: 157
		private TaskInvocationInfo invocationInfo;

		// Token: 0x0400009E RID: 158
		private List<ITaskModule> taskModules = new List<ITaskModule>();

		// Token: 0x0400009F RID: 159
		private TaskEvent taskEvents;

		// Token: 0x040000A0 RID: 160
		private static FileSearchAssemblyResolver fileSearchAssemblyResolver = new FileSearchAssemblyResolver();

		// Token: 0x040000A1 RID: 161
		private string verboseTaskName;

		// Token: 0x040000A2 RID: 162
		private Dictionary<int, ConfirmationChoice> confirmationPreferences = new Dictionary<int, ConfirmationChoice>();

		// Token: 0x040000A3 RID: 163
		private TaskIOPipeline taskIOPipeline;

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x0600027E RID: 638
		internal delegate void ErrorLoggerDelegate(LocalizedException exception, ExchangeErrorCategory category, object target);

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x06000282 RID: 642
		internal delegate void ErrorLoggerReThrowDelegate(LocalizedException exception, ExchangeErrorCategory category, object target, bool reThrow);

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x06000286 RID: 646
		internal delegate void TaskVerboseLoggingDelegate(LocalizedString message);

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x0600028A RID: 650
		internal delegate void TaskWarningLoggingDelegate(LocalizedString message);

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x0600028E RID: 654
		internal delegate void TaskProgressLoggingDelegate(LocalizedString activity, LocalizedString statusDescription, int percent);

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x06000292 RID: 658
		internal delegate bool TaskShouldContinueDelegate(LocalizedString promptMessage);

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x06000296 RID: 662
		internal delegate void TaskErrorLoggingDelegate(Exception exception, ErrorCategory category, object target);

		// Token: 0x02000039 RID: 57
		// (Invoke) Token: 0x0600029A RID: 666
		internal delegate void TaskErrorLoggingReThrowDelegate(Exception exception, ErrorCategory category, object target, bool reThrow);
	}
}
