using System;
using System.Globalization;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.Core.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000280 RID: 640
	internal class LoggingModule : TaskIOPipelineBase, ITaskModule, ICriticalFeature
	{
		// Token: 0x060015FC RID: 5628 RVA: 0x0005209C File Offset: 0x0005029C
		public LoggingModule(TaskContext context)
		{
			this.context = context;
			if (!ActivityContext.IsStarted)
			{
				ActivityContext.ClearThreadScope();
				this.activityScope = ActivityContext.Start(this);
				if (HttpContext.Current != null)
				{
					this.activityScope.UpdateFromMessage(HttpContext.Current.Request);
					this.activityScope.SerializeTo(HttpContext.Current.Response);
				}
			}
			InitializeLoggerSettingsHelper.InitLoggerSettings();
			this.StartLogging();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00052111 File Offset: 0x00050311
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00052114 File Offset: 0x00050314
		void ITaskModule.Init(ITaskEvent task)
		{
			task.InitCompleted += this.OnInitCompleted;
			task.PreIterate += this.OnPreIterate;
			task.IterateCompleted += this.OnIterateCompleted;
			task.Release += this.OnRelease;
			task.Stop += this.OnStop;
			task.Error += this.OnError;
			if (this.context != null && this.context.CommandShell != null)
			{
				this.context.CommandShell.PrependTaskIOPipelineHandler(this);
			}
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000521B3 File Offset: 0x000503B3
		void ITaskModule.Dispose()
		{
			this.CommitLog("Dispose");
			if (this.activityScope != null)
			{
				this.activityScope.Dispose();
				this.activityScope = null;
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000521DA File Offset: 0x000503DA
		public override bool WriteError(TaskErrorInfo input, out TaskErrorInfo output)
		{
			if (input != null && input.Exception != null)
			{
				CmdletStaticDataWithUniqueId<Exception>.Set(this.context.UniqueId, input.Exception);
			}
			return base.WriteError(input, out output);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00052205 File Offset: 0x00050405
		private void OnInitCompleted(object sender, EventArgs eventArgs)
		{
			if (this.context.ErrorInfo != null && this.context.ShouldTerminateCmdletExecution)
			{
				this.CommitLog("InitOnError");
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0005222C File Offset: 0x0005042C
		private void OnPreIterate(object sender, EventArgs eventArgs)
		{
			if (!this.isFirstIteration)
			{
				this.CommitLog("OnPreIterate");
				this.StartLogging();
			}
			this.isFirstIteration = false;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00052250 File Offset: 0x00050450
		private void OnIterateCompleted(object sender, EventArgs eventArgs)
		{
			if (this.context.InvocationInfo != null)
			{
				Guid uniqueId = this.context.UniqueId;
				this.parametersSetInLog = true;
				CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.Cmdlet, this.context.InvocationInfo.CommandName);
				CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.Parameters, TaskVerboseStringHelper.FormatUserSpecifiedParameters(this.context.InvocationInfo.UserSpecifiedParameters ?? new PropertyBag()));
			}
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000522C5 File Offset: 0x000504C5
		private void OnStop(object sender, EventArgs eventArgs)
		{
			this.CommitLog("OnStop");
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000522D2 File Offset: 0x000504D2
		private void OnRelease(object sender, EventArgs eventArgs)
		{
			this.CommitLog(null);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000522DC File Offset: 0x000504DC
		private void OnError(object sender, GenericEventArg<TaskErrorEventArg> genericEventArg)
		{
			if (genericEventArg.Data.ExceptionHandled)
			{
				return;
			}
			Exception exception = genericEventArg.Data.Exception;
			Guid uniqueId = this.context.UniqueId;
			Exception ex = CmdletStaticDataWithUniqueId<Exception>.Get(uniqueId);
			if (ex != null && ex != exception)
			{
				CmdletLogger.SafeAppendGenericError(uniqueId, this.context.Stage.ToString() + ".FromWriteError", ex.ToString(), false);
			}
			bool isUnhandledException = (genericEventArg.Data.IsUnknownException != null) ? genericEventArg.Data.IsUnknownException.Value : TaskHelper.IsTaskUnhandledException(exception);
			CmdletLogger.SafeAppendGenericError(uniqueId, this.context.Stage.ToString(), exception.ToString(), isUnhandledException);
			if (exception is LocalizedException)
			{
				CmdletLogger.SafeAppendGenericError(uniqueId, "ExceptionStringId", ((LocalizedException)exception).LocalizedString.StringId, false);
				if (CmdletLogHelper.NeedConvertLogMessageToUS)
				{
					LocalizedException ex2 = (LocalizedException)exception;
					IFormatProvider formatProvider = ex2.FormatProvider;
					try
					{
						ex2.FormatProvider = CmdletLogHelper.DefaultLoggingCulture;
						CmdletLogger.SafeAppendGenericError(uniqueId, this.context.Stage.ToString() + "(en-us)", ex2.ToString(), false);
					}
					finally
					{
						ex2.FormatProvider = formatProvider;
					}
				}
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00052434 File Offset: 0x00050634
		private void StartLogging()
		{
			this.logPendingCommit = true;
			this.parametersSetInLog = false;
			Guid uniqueId = this.context.UniqueId;
			CmdletThreadStaticData.RegisterCmdletUniqueId(uniqueId);
			CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.StartTime, DateTime.UtcNow);
			CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.ExecutionResult, "Success");
			CmdletLatencyTracker.StartLatencyTracker(uniqueId);
			CmdletLatencyTracker.StartInternalTracking(uniqueId, "Cmd", true);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000524C4 File Offset: 0x000506C4
		private void CommitLog(string loggingStep)
		{
			if (!this.logPendingCommit)
			{
				return;
			}
			this.logPendingCommit = false;
			Guid cmdletUniqueId = this.context.UniqueId;
			try
			{
				CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.CmdletUniqueId, cmdletUniqueId);
				if (loggingStep != null)
				{
					CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "Logging", loggingStep);
				}
				CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.ProcessId, Constants.ProcessId);
				CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.ProcessName, Constants.ProcessName);
				CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.ThreadId, Environment.CurrentManagedThreadId);
				CmdletLatencyTracker.EndInternalTracking(cmdletUniqueId, "Cmd");
				CmdletLatencyTracker.PushLatencyDetailsToLog(cmdletUniqueId, CmdletLogHelper.FuncNameToLogMetaDic, delegate(Enum metadata, double latency)
				{
					CmdletLogger.UpdateLatency(cmdletUniqueId, metadata, latency);
				}, delegate(string funcName, string totalLatency)
				{
					CmdletLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, funcName, totalLatency);
				});
				long num = CmdletLatencyTracker.StopLatencyTracker(cmdletUniqueId);
				CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.TotalTime, num);
				CmdletLatencyTracker.DisposeLatencyTracker(cmdletUniqueId);
				if (!this.parametersSetInLog)
				{
					if (this.context.InvocationInfo != null)
					{
						CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.Cmdlet, this.context.InvocationInfo.CommandName);
						CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.Parameters, TaskVerboseStringHelper.FormatUserSpecifiedParameters(this.context.InvocationInfo.UserSpecifiedParameters ?? new PropertyBag()));
					}
					else
					{
						CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "InvocationInfo", "null");
					}
				}
				TaskUserInfo userInfo = this.context.UserInfo;
				if (userInfo != null)
				{
					CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.TenantId, userInfo.ExecutingUserOrganizationId.GetFriendlyName());
					CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.AuthenticatedUser, (userInfo.ExecutingWindowsLiveId != SmtpAddress.Empty) ? userInfo.ExecutingWindowsLiveId.ToString() : ((userInfo.ExecutingUserId != null && !string.IsNullOrWhiteSpace(userInfo.ExecutingUserId.Name)) ? userInfo.ExecutingUserId.Name : userInfo.ExecutingUserIdentityName));
					CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.EffectiveOrganization, userInfo.CurrentOrganizationId.GetFriendlyName());
				}
				else
				{
					CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "UserInfo", "null");
				}
				if (this.context.ExchangeRunspaceConfig != null)
				{
					CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.UserServicePlan, this.context.ExchangeRunspaceConfig.ServicePlanForLogging);
					CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.IsAdmin, this.context.ExchangeRunspaceConfig.HasAdminRoles);
					if (this.context.ExchangeRunspaceConfig.ConfigurationSettings != null && this.context.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown)
					{
						CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.ClientApplication, this.context.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication);
					}
				}
				else
				{
					CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "ExchangeRunspaceConfig", "null");
				}
				if (!CmdletLogHelper.DefaultLoggingCulture.Equals(CultureInfo.CurrentUICulture) || !CmdletLogHelper.DefaultLoggingCulture.Equals(CultureInfo.CurrentCulture))
				{
					CmdletLogger.SafeSetLogger(cmdletUniqueId, RpsCmdletMetadata.CultureInfo, CultureInfo.CurrentUICulture + "," + CultureInfo.CurrentCulture);
				}
			}
			catch (Exception ex)
			{
				Diagnostics.LogExceptionWithTrace(Constants.CoreEventLogger, TaskEventLogConstants.Tuple_NonCrashingException, null, ExTraceGlobals.InstrumentationTracer, null, "Exception from CmdletLogger.AsyncCommit : {0}", ex);
				CmdletLogger.SafeAppendGenericError(cmdletUniqueId, "CommitLog", ex, new Func<Exception, bool>(TaskHelper.IsTaskUnhandledException));
			}
			finally
			{
				try
				{
					CmdletLogger.SafeSetLogger(this.context.UniqueId, RpsCmdletMetadata.EndTime, DateTime.UtcNow);
					CmdletLogger.AsyncCommit(cmdletUniqueId, true);
					CmdletThreadStaticData.UnRegisterCmdletUniqueId(cmdletUniqueId);
				}
				catch (Exception exception)
				{
					this.logPendingCommit = true;
					Diagnostics.LogExceptionWithTrace(Constants.CoreEventLogger, TaskEventLogConstants.Tuple_NonCrashingException, null, ExTraceGlobals.InstrumentationTracer, null, "Exception from CmdletLogger.AsyncCommit : {0}", exception);
				}
			}
		}

		// Token: 0x040006B3 RID: 1715
		private bool isFirstIteration = true;

		// Token: 0x040006B4 RID: 1716
		private bool logPendingCommit;

		// Token: 0x040006B5 RID: 1717
		private bool parametersSetInLog;

		// Token: 0x040006B6 RID: 1718
		private readonly TaskContext context;

		// Token: 0x040006B7 RID: 1719
		private ActivityScope activityScope;
	}
}
