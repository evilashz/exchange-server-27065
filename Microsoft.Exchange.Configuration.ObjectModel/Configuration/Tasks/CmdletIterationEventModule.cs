using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000275 RID: 629
	public class CmdletIterationEventModule : TaskIOPipelineBase, ITaskModule, ICriticalFeature
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00050A98 File Offset: 0x0004EC98
		private ExEventLog.EventTuple CmdletSuccessEventTuple
		{
			get
			{
				if (this.context.InvocationInfo != null && this.context.InvocationInfo.CommandName.Equals("Get-ManagementEndpoint", StringComparison.OrdinalIgnoreCase))
				{
					return TaskEventLogConstants.Tuple_LogCmdletSuccess;
				}
				if (this.context.ExchangeRunspaceConfig == null)
				{
					return TaskEventLogConstants.Tuple_LogMediumLevelCmdletSuccess;
				}
				if (this.context.InvocationInfo != null && this.context.InvocationInfo.CommandName.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
				{
					return TaskEventLogConstants.Tuple_LogLowLevelCmdletSuccess;
				}
				return TaskEventLogConstants.Tuple_LogCmdletSuccess;
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00050B3D File Offset: 0x0004ED3D
		public CmdletIterationEventModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00050B57 File Offset: 0x0004ED57
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00050B5C File Offset: 0x0004ED5C
		public void Init(ITaskEvent task)
		{
			task.Error += new EventHandler<GenericEventArg<TaskErrorEventArg>>(this.LogCmdletIterationEvent);
			task.IterateCompleted += this.LogCmdletIterationEvent;
			task.PreStop += this.OnPreStop;
			task.Stop += this.LogCmdletStopEvent;
			if (this.context.CommandShell != null)
			{
				this.context.CommandShell.PrependTaskIOPipelineHandler(this);
			}
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00050BCF File Offset: 0x0004EDCF
		public override bool WriteObject(object input, out object output)
		{
			output = input;
			this.outputObjectCount++;
			CmdletLogger.SafeSetLogger(this.context.UniqueId, RpsCmdletMetadata.OutputObjectCount, this.outputObjectCount);
			return true;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00050C05 File Offset: 0x0004EE05
		public void Dispose()
		{
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00050C07 File Offset: 0x0004EE07
		private void OnPreStop(object sender, EventArgs e)
		{
			this.wasStopped = true;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00050C10 File Offset: 0x0004EE10
		private void LogCmdletStopEvent(object sender, EventArgs e)
		{
			this.LogCmdletIterationEvent();
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00050C18 File Offset: 0x0004EE18
		private void LogCmdletIterationEvent(object sender, EventArgs e)
		{
			if (!this.wasStopped)
			{
				this.LogCmdletIterationEvent();
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00050C28 File Offset: 0x0004EE28
		private void LogCmdletIterationEvent()
		{
			bool flag = this.context.ExchangeRunspaceConfig == null;
			bool flag2 = VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.WriteEventLogInEnglish.Enabled && (CultureInfo.CurrentUICulture != CmdletLogHelper.DefaultLoggingCulture || CultureInfo.CurrentCulture != CmdletLogHelper.DefaultLoggingCulture);
			object[] array = new object[27];
			array[0] = ((this.context.InvocationInfo != null) ? this.context.InvocationInfo.DisplayName : string.Empty);
			array[1] = ((this.context.InvocationInfo == null) ? null : TaskVerboseStringHelper.FormatUserSpecifiedParameters(this.context.InvocationInfo.UserSpecifiedParameters ?? new PropertyBag()));
			array[2] = (flag ? ((this.context.UserInfo != null && this.context.UserInfo.ExecutingUserId != null) ? this.context.UserInfo.ExecutingUserId.ToString() : string.Empty) : this.context.ExchangeRunspaceConfig.IdentityName);
			array[3] = (flag ? null : this.context.ExchangeRunspaceConfig.LogonUserSid);
			array[4] = null;
			if (!flag)
			{
				SecurityIdentifier securityIdentifier = null;
				this.context.ExchangeRunspaceConfig.TryGetExecutingUserSid(out securityIdentifier);
				array[4] = securityIdentifier;
			}
			array[5] = this.GenerateApplicationString();
			array[6] = CmdletIterationEventModule.processIdAndName;
			array[7] = (flag ? ((this.context.UserInfo != null) ? this.context.UserInfo.CurrentOrganizationId : null) : this.context.ExchangeRunspaceConfig.OrganizationId);
			array[8] = Environment.CurrentManagedThreadId;
			DateTime utcNow = DateTime.UtcNow;
			array[9] = utcNow.Subtract(this.lastDateTimeValue);
			this.lastDateTimeValue = utcNow;
			ADDriverContext threadADContext = ADSessionSettings.GetThreadADContext();
			if (threadADContext == null)
			{
				array[10] = null;
			}
			else
			{
				array[10] = (flag2 ? TaskVerboseStringHelper.GetADServerSettings(null, threadADContext.ServerSettings, CmdletLogHelper.DefaultLoggingCulture) : TaskVerboseStringHelper.GetADServerSettings(null, threadADContext.ServerSettings, null));
			}
			if (this.context.ErrorInfo.HasErrors)
			{
				if (this.context.ErrorInfo.Exception != null)
				{
					Exception exception = this.context.ErrorInfo.Exception;
					array[11] = exception;
					array[12] = this.context.ErrorInfo.ExchangeErrorCategory.Value;
					if (exception != null && exception.InnerException != null)
					{
						array[13] = exception.InnerException;
					}
					if (exception is LocalizedException)
					{
						array[14] = ((LocalizedException)exception).LocalizedString.StringId;
						if (!flag2)
						{
							goto IL_2DE;
						}
						LocalizedException ex = (LocalizedException)exception;
						IFormatProvider formatProvider = ex.FormatProvider;
						try
						{
							ex.FormatProvider = CmdletLogHelper.DefaultLoggingCulture;
							array[11] = ex.ToString();
							goto IL_2DE;
						}
						finally
						{
							ex.FormatProvider = formatProvider;
						}
					}
					array[14] = "NonLocalizedException";
				}
				else
				{
					array[11] = "null";
				}
			}
			IL_2DE:
			object obj;
			this.context.Items.TryGetValue("Log_AdditionalLogData", out obj);
			array[15] = obj;
			LocalizedString delayedInfo = ThrottlingModule<ResourceThrottlingCallback>.GetDelayedInfo(this.context);
			if (!string.IsNullOrEmpty(delayedInfo))
			{
				array[16] = (flag2 ? delayedInfo.ToString(CmdletLogHelper.DefaultLoggingCulture) : delayedInfo) + ThrottlingModule<ResourceThrottlingCallback>.GetThrottlingInfo(this.context);
			}
			array[17] = SuppressingPiiContext.NeedPiiSuppression;
			this.context.Items.TryGetValue("Log_CmdletProxyInfo", out obj);
			array[18] = obj;
			if (this.context.Items.TryGetValue("Log_ProxiedObjectCount", out obj))
			{
				obj = string.Format("{0} objects execution has been proxied to remote server.", obj);
			}
			array[19] = obj;
			if (this.context.Items.TryGetValue("Log_RequestQueryFilterInGetTasks", out obj))
			{
				array[20] = string.Format("Request Filter used is: {0}", obj);
			}
			if (this.context.Items.TryGetValue("Log_InternalQueryFilterInGetTasks", out obj))
			{
				array[21] = string.Format("Cmdlet Filter used is: {0}", obj);
			}
			array[22] = this.outputObjectCount;
			array[23] = "ActivityId: " + ((ActivityContext.ActivityId != null) ? ActivityContext.ActivityId.Value.ToString() : string.Empty);
			if (!flag && this.context.ExchangeRunspaceConfig != null)
			{
				array[24] = this.context.ExchangeRunspaceConfig.GetRBACInformationSummary();
			}
			if (Constants.IsPowerShellWebService && HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Headers != null)
			{
				array[25] = HttpContext.Current.Request.Headers["client-request-id"];
			}
			array[26] = CultureInfo.CurrentUICulture.Name;
			ExEventLog.EventTuple eventInfo;
			if (this.context.ErrorInfo.HasErrors)
			{
				eventInfo = TaskEventLogConstants.Tuple_LogCmdletError;
			}
			else if (this.context.WasCancelled)
			{
				eventInfo = TaskEventLogConstants.Tuple_LogCmdletCancelled;
			}
			else if (this.wasStopped)
			{
				eventInfo = TaskEventLogConstants.Tuple_LogCmdletStopped;
			}
			else
			{
				eventInfo = this.CmdletSuccessEventTuple;
			}
			try
			{
				TaskLogger.LogEvent("All", eventInfo, array);
			}
			catch (ArgumentException ex2)
			{
				if (this.context.CommandShell != null)
				{
					this.context.CommandShell.WriteWarning(Strings.WarningCannotWriteToEventLog(ex2.ToString()));
				}
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0005118C File Offset: 0x0004F38C
		private string GenerateApplicationString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = this.context.InvocationInfo != null && string.Compare(this.context.InvocationInfo.ShellHostName, "ServerRemoteHost", true) == 0;
			string value;
			if (Constants.IsPowerShellWebService)
			{
				value = "Psws";
			}
			else if (flag)
			{
				value = "Remote";
			}
			else
			{
				value = "Local";
			}
			string value2;
			if (this.context.ExchangeRunspaceConfig != null && (flag || this.context.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication != ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown))
			{
				value2 = this.context.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication.ToString();
			}
			else
			{
				value2 = ((this.context.InvocationInfo != null) ? this.context.InvocationInfo.ShellHostName : string.Empty);
			}
			stringBuilder.Append(value);
			stringBuilder.Append("-");
			stringBuilder.Append(value2);
			string value3 = ExchangeRunspaceConfigurationSettings.ExchangeUserType.Unknown.ToString();
			if (this.context.ExchangeRunspaceConfig != null)
			{
				value3 = this.context.ExchangeRunspaceConfig.ConfigurationSettings.UserType.ToString();
			}
			stringBuilder.Append("-");
			stringBuilder.Append(value3);
			if (this.context.ExchangeRunspaceConfig != null && this.context.ExchangeRunspaceConfig.ConfigurationSettings.IsProxy)
			{
				stringBuilder.Append("-Proxy");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040006A2 RID: 1698
		public const string ProxiedObjectRecordCountKey = "Log_ProxiedObjectCount";

		// Token: 0x040006A3 RID: 1699
		public const string RequestQueryFilterInGetTasksKey = "Log_RequestQueryFilterInGetTasks";

		// Token: 0x040006A4 RID: 1700
		public const string InternalQueryFilterInGetTasksKey = "Log_InternalQueryFilterInGetTasks";

		// Token: 0x040006A5 RID: 1701
		public const string AdditionalLogDataKey = "Log_AdditionalLogData";

		// Token: 0x040006A6 RID: 1702
		public const string CmdletProxyInfoKey = "Log_CmdletProxyInfo";

		// Token: 0x040006A7 RID: 1703
		private static readonly string processIdAndName = string.Format("{0} {1}", Constants.ProcessId, Constants.ProcessName);

		// Token: 0x040006A8 RID: 1704
		private readonly TaskContext context;

		// Token: 0x040006A9 RID: 1705
		private bool wasStopped;

		// Token: 0x040006AA RID: 1706
		private int outputObjectCount;

		// Token: 0x040006AB RID: 1707
		private DateTime lastDateTimeValue = DateTime.UtcNow;
	}
}
