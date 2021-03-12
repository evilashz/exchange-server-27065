using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.FailFast;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000271 RID: 625
	internal class ThrottlingModule<T> : TaskIOPipelineBase, ITaskModule, IThrottlingModuleInfo, ICriticalFeature where T : IThrottlingCallback, new()
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0004F614 File Offset: 0x0004D814
		public IPowerShellBudget PSBudget
		{
			get
			{
				IPowerShellBudget result = null;
				if (this.context.SessionState != null)
				{
					result = ExchangePropertyContainer.GetPowerShellBudget(this.context.SessionState);
				}
				return result;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0004F642 File Offset: 0x0004D842
		public ResourceKey[] ResourceKeys
		{
			get
			{
				return this.resourceKeys;
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0004F64C File Offset: 0x0004D84C
		public static LocalizedString GetDelayedInfo(TaskContext context)
		{
			LocalizedString empty = LocalizedString.Empty;
			context.TryGetItem<LocalizedString>("DelayedInfo", ref empty);
			return empty;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0004F670 File Offset: 0x0004D870
		public static string GetThrottlingInfo(TaskContext context)
		{
			string result = null;
			context.TryGetItem<string>("ThrottlingInfo", ref result);
			return result;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004F690 File Offset: 0x0004D890
		static ThrottlingModule()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			t.Initialize();
			ThrottlingModule<T>.computerName = Environment.GetEnvironmentVariable("COMPUTERNAME");
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004F704 File Offset: 0x0004D904
		public ThrottlingModule(TaskContext context, bool disableCostHandle)
		{
			this.context = context;
			this.casCostHandleDisabled = disableCostHandle;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0004F746 File Offset: 0x0004D946
		public ThrottlingModule(TaskContext context) : this(context, false)
		{
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0004F750 File Offset: 0x0004D950
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return ex is OverBudgetException || ex is ResourceUnhealthyException;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0004F768 File Offset: 0x0004D968
		public void Init(ITaskEvent task)
		{
			task.PreIterate += this.StartBudgetProcess;
			task.PreIterate += this.CheckOverBudget;
			task.PreIterate += this.CheckResourceHealth;
			task.IterateCompleted += this.EndBudgetProcess;
			task.Stop += this.EndBudgetProcess;
			if (this.context.CommandShell != null)
			{
				this.context.CommandShell.PrependTaskIOPipelineHandler(this);
			}
			this.budgets.Add(new BudgetInformation
			{
				Budget = this.PSBudget,
				ThrottledEventInfo = TaskEventLogConstants.Tuple_TaskThrottled
			});
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0004F816 File Offset: 0x0004DA16
		public void Dispose()
		{
			this.DisposeCostHandle();
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0004F904 File Offset: 0x0004DB04
		private void StartBudgetProcess(object sender, EventArgs e)
		{
			string cmdLetName = null;
			if (this.context.InvocationInfo != null)
			{
				cmdLetName = this.context.InvocationInfo.CommandName;
			}
			this.DisposeCostHandle();
			this.SafeBudgetAction(delegate(BudgetInformation budgetInfo)
			{
				CmdletLogger.SafeSetLogger(this.context.UniqueId, RpsCmdletMetadata.UserBudgetOnStart, budgetInfo.Budget.GetCmdletBudgetUsage());
			});
			this.SafeBudgetAction(delegate(BudgetInformation budgetInfo)
			{
				budgetInfo.Budget.EndLocal();
			});
			this.SafeBudgetAction(delegate(BudgetInformation budgetInfo)
			{
				budgetInfo.Handle = budgetInfo.Budget.StartCmdlet(cmdLetName);
			});
			if (!this.casCostHandleDisabled)
			{
				this.SafeBudgetAction(delegate(BudgetInformation budgetInfo)
				{
					try
					{
						budgetInfo.Budget.StartLocal(string.Format("Task.StartBudgetProcess.{0}", cmdLetName), default(TimeSpan));
						budgetInfo.Budget.LocalCostHandle.MaxLiveTime = TimeSpan.FromHours(1.0);
					}
					catch (InvalidOperationException)
					{
						try
						{
							CmdletLogger.SafeAppendGenericError("StartLocalBudgetFailed", cmdLetName, false);
							budgetInfo.Budget.EndLocal();
						}
						catch (Exception)
						{
						}
					}
				});
				return;
			}
			ThrottlingModule<T>.LogInformationTrace("CasCostHandle is not enabled in cmdlet '{0}'", new object[]
			{
				cmdLetName
			});
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
		private void CheckOverBudget(object sender, EventArgs e)
		{
			this.SafeBudgetAction(delegate(BudgetInformation budgetInfo)
			{
				budgetInfo.Budget.CheckOverBudget(CostType.CMDLET);
			});
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0004FA08 File Offset: 0x0004DC08
		private void CheckResourceHealth(object sender, EventArgs e)
		{
			try
			{
				if (this.PSBudget != null)
				{
					T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
					t.CheckResourceHealth(this);
				}
			}
			catch (ResourceUnhealthyException ex)
			{
				OrganizationId organizationId = null;
				if (this.context.UserInfo != null)
				{
					organizationId = this.context.UserInfo.ExecutingUserOrganizationId;
				}
				this.LogPeriodicEvent(TaskEventLogConstants.Tuple_ResourceHealthCutOff, new object[]
				{
					organizationId,
					ex.ResourceKey.ToString()
				});
				this.TriggerFailFast(null);
				if (this.context.CommandShell != null)
				{
					this.context.CommandShell.ThrowTerminatingError(ex, ExchangeErrorCategory.Context, null);
				}
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0004FAE5 File Offset: 0x0004DCE5
		private void EndBudgetProcess(object sender, EventArgs e)
		{
			if (!this.casCostHandleDisabled)
			{
				this.EnforceDelay();
			}
			this.SafeBudgetAction(delegate(BudgetInformation budgetInfo)
			{
				budgetInfo.Budget.EndLocal();
			});
			this.DisposeCostHandle();
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0004FB1E File Offset: 0x0004DD1E
		public override bool WriteObject(object input, out object output)
		{
			this.writeCount++;
			if (this.writeCount % 50 == 0)
			{
				this.EnforceDelay();
			}
			output = input;
			return true;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0004FB54 File Offset: 0x0004DD54
		private void SafeBudgetAction(Action<BudgetInformation> action)
		{
			foreach (BudgetInformation budgetInformation in from x in this.budgets
			where x.Budget != null
			select x)
			{
				try
				{
					action(budgetInformation);
				}
				catch (OverBudgetException ex)
				{
					if (ex.PolicyPart == "LocalTime" || ex.PolicyPart == "PowerShellMaxCmdlets")
					{
						OrganizationId organizationId = null;
						if (this.context.UserInfo != null)
						{
							organizationId = this.context.UserInfo.ExecutingUserOrganizationId;
						}
						this.LogPeriodicEvent(budgetInformation.ThrottledEventInfo, new object[]
						{
							organizationId,
							budgetInformation.Budget.ToString(),
							ex
						});
					}
					if (ex.PolicyPart == "MaxDestructiveCmdletsTimePeriod")
					{
						OrganizationId organizationId2 = null;
						if (this.context.UserInfo != null)
						{
							organizationId2 = this.context.UserInfo.ExecutingUserOrganizationId;
						}
						if (OrganizationId.ForestWideOrgId.Equals(organizationId2))
						{
							this.LogPeriodicEvent(TaskEventLogConstants.Tuple_DestructiveTaskThrottledForFirstOrg, new object[]
							{
								organizationId2,
								budgetInformation.Budget.ToString(),
								ex
							});
						}
						else
						{
							this.LogPeriodicEvent(TaskEventLogConstants.Tuple_DestructiveTaskThrottledForTenant, new object[]
							{
								organizationId2,
								budgetInformation.Budget.ToString(),
								ex
							});
						}
					}
					if (ex.PolicyPart == "PowerShellMaxCmdlets" || ex.PolicyPart == "MaxDestructiveCmdletsTimePeriod" || ex.PolicyPart == "LocalTime")
					{
						this.TriggerFailFast(ex);
						if (this.context.CommandShell == null)
						{
							throw;
						}
						this.context.CommandShell.ThrowTerminatingError(ThrottlingModule<T>.WrapOverBudgetException(ex, budgetInformation.Budget), ExchangeErrorCategory.Context, null);
					}
				}
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0004FD80 File Offset: 0x0004DF80
		internal void TriggerFailFast(OverBudgetException cmdletOverBudget)
		{
			if (this.context.UserInfo.ExecutingWindowsLiveId.IsValidAddress)
			{
				FailFastUserCache.Instance.AddUserToCache(this.context.UserInfo.ExecutingWindowsLiveId.ToString(), BlockedType.NewRequest, (cmdletOverBudget == null) ? TimeSpan.Zero : new TimeSpan(0, 0, 0, 0, cmdletOverBudget.BackoffTime));
				CmdletLogger.SafeAppendColumn(this.context.UniqueId, RpsCmdletMetadata.ContributeToFailFast, "Cmdlet", LoggerHelper.GetContributeToFailFastValue("User", this.context.UserInfo.ExecutingUserId.ToString(), "NewRequest", (double)((cmdletOverBudget == null) ? -1 : cmdletOverBudget.BackoffTime)));
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0004FE3C File Offset: 0x0004E03C
		public bool OnBeforeDelay(DelayInfo delayInfo)
		{
			LocalizedString warningMessage = LocalizedString.Empty;
			if (delayInfo is UserQuotaDelayInfo)
			{
				UserQuotaDelayInfo userQuotaDelayInfo = (UserQuotaDelayInfo)delayInfo;
				if (userQuotaDelayInfo.OverBudgetException != null)
				{
					warningMessage = Strings.WarningCmdletTarpittingByUserQuota(userQuotaDelayInfo.OverBudgetException.PolicyPart, delayInfo.Delay.TotalSeconds.ToString(), ThrottlingModule<T>.computerName);
				}
				else
				{
					warningMessage = Strings.WarningCmdletMicroDelay(delayInfo.Delay.TotalMilliseconds.ToString());
				}
			}
			else if (delayInfo is ResourceLoadDelayInfo && ((ResourceLoadDelayInfo)delayInfo).ResourceKey != null)
			{
				warningMessage = Strings.WarningCmdletTarpittingByResourceLoad(((ResourceLoadDelayInfo)delayInfo).ResourceKey.ToString(), delayInfo.Delay.TotalSeconds.ToString());
			}
			this.WriteCmdletMicroDelayMessage(warningMessage, delayInfo.Delay.TotalSeconds);
			return true;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0004FF10 File Offset: 0x0004E110
		protected void EnforceDelay()
		{
			if (this.context.ErrorInfo.TerminatePipeline)
			{
				return;
			}
			IPowerShellBudget psbudget = this.PSBudget;
			if (psbudget != null)
			{
				T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				DelayEnforcementResults delayEnforcementResults = t.EnforceDelay(this, ThrottlingModule<T>.costTypesInEnforceDelay, ThrottlingModule<T>.CmdletMaxPreferredDelay);
				LocalizedString localizedString = this.GenerateDelayInfoLogString(this.PSBudget, delayEnforcementResults);
				if (!string.IsNullOrEmpty(localizedString))
				{
					string text = string.Format("; PolicyDN: {0}; Snapshot: {1}", psbudget.ThrottlingPolicy.GetIdentityString(), psbudget);
					LocalizedString warningMessage = new LocalizedString(localizedString + text);
					this.context.Items["ThrottlingInfo"] = text;
					this.context.Items["DelayedInfo"] = localizedString;
					Guid uniqueId = this.context.UniqueId;
					CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.ThrottlingInfo, text);
					CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.DelayInfo, CmdletLogHelper.NeedConvertLogMessageToUS ? localizedString.ToString(CmdletLogHelper.DefaultLoggingCulture) : localizedString);
					this.WriteCmdletMicroDelayMessage(warningMessage, delayEnforcementResults.DelayInfo.Delay.TotalSeconds);
				}
				PowerShellBudgetWrapper powerShellBudgetWrapper = psbudget as PowerShellBudgetWrapper;
				WorkloadManagementLogger.SetBudgetBalance(powerShellBudgetWrapper.GetInnerBudget().CasTokenBucket.GetBalance().ToString(), null);
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00050080 File Offset: 0x0004E280
		private void WriteCmdletMicroDelayMessage(LocalizedString warningMessage, double delaySeconds)
		{
			if (warningMessage == LocalizedString.Empty)
			{
				return;
			}
			if (delaySeconds >= 10.0 || (this.context.ExchangeRunspaceConfig != null && this.context.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.ForwardSync))
			{
				this.context.CommandShell.WriteWarning(warningMessage);
				return;
			}
			if (this.context.InvocationInfo.IsVerboseOn)
			{
				this.context.CommandShell.WriteVerbose(warningMessage);
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00050104 File Offset: 0x0004E304
		public WorkloadSettings GetWorkloadSettings()
		{
			ExchangeRunspaceConfigurationSettings.ExchangeApplication exchangeApplication = ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown;
			if (this.context.SessionState != null)
			{
				exchangeApplication = ExchangePropertyContainer.GetPropagatedClientAppId(this.context.SessionState);
			}
			if (exchangeApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown && this.context.ExchangeRunspaceConfig != null)
			{
				exchangeApplication = this.context.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication;
			}
			return this.GetWorkloadSettings(exchangeApplication);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00050160 File Offset: 0x0004E360
		private WorkloadSettings GetWorkloadSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication application)
		{
			switch (application)
			{
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.PowerShell:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.EWS:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.ManagementShell:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.DebugUser:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.ReportingWebService:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.PswsClient:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.Office365Partner:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.Intune:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.CRM:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.ActiveMonitor:
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP:
				return new WorkloadSettings(WorkloadType.PowerShell);
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.GalSync:
				return new WorkloadSettings(WorkloadType.PowerShellGalSync, true);
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.ForwardSync:
				return new WorkloadSettings(WorkloadType.PowerShellForwardSync, true);
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.BackSync:
				return new WorkloadSettings(WorkloadType.PowerShellBackSync, true);
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.NonInteractivePowershell:
				return new WorkloadSettings(WorkloadType.PowerShell, true);
			case ExchangeRunspaceConfigurationSettings.ExchangeApplication.DiscretionaryScripts:
				return new WorkloadSettings(WorkloadType.PowerShellDiscretionaryWorkFlow, true);
			}
			return new WorkloadSettings(WorkloadType.PowerShellLowPriorityWorkFlow, true);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00050220 File Offset: 0x0004E420
		private LocalizedString GenerateDelayInfoLogString(IBudget budget, DelayEnforcementResults info)
		{
			LocalizedString result = LocalizedString.Empty;
			if (!string.Equals(info.NotEnforcedReason, "No Delay Necessary", StringComparison.OrdinalIgnoreCase))
			{
				CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "DelayInfo.Type", info.DelayInfo.GetType().Name);
				if (info.NotEnforcedReason != null)
				{
					CmdletLogger.SafeAppendGenericInfo(this.context.UniqueId, "NotEnforcedReason", info.NotEnforcedReason);
				}
				if (string.Equals(info.NotEnforcedReason, "Max Delayed Threads Exceeded", StringComparison.OrdinalIgnoreCase))
				{
					BudgetTypeSetting budgetTypeSetting = BudgetTypeSettings.Get(budget.Owner.BudgetType);
					if (info.DelayInfo is UserQuotaDelayInfo)
					{
						UserQuotaDelayInfo userQuotaDelayInfo = info.DelayInfo as UserQuotaDelayInfo;
						result = Strings.UserQuotaDelayNotEnforcedMaxThreadsExceeded((int)info.DelayInfo.Delay.TotalMilliseconds, info.DelayInfo.Required, userQuotaDelayInfo.OverBudgetException.PolicyPart, budgetTypeSetting.MaxDelayedThreads);
					}
					else if (info.DelayInfo is ResourceLoadDelayInfo)
					{
						ResourceLoadDelayInfo resourceLoadDelayInfo = info.DelayInfo as ResourceLoadDelayInfo;
						result = Strings.ResourceLoadDelayNotEnforcedMaxThreadsExceeded((int)info.DelayInfo.Delay.TotalMilliseconds, info.DelayInfo.Required, resourceLoadDelayInfo.ResourceKey.ToString(), resourceLoadDelayInfo.ResourceLoad.ToString(), budgetTypeSetting.MaxDelayedThreads);
					}
					else
					{
						result = Strings.MicroDelayNotEnforcedMaxThreadsExceeded((int)info.DelayInfo.Delay.TotalMilliseconds, info.DelayInfo.Required, budgetTypeSetting.MaxDelayedThreads);
					}
				}
				else
				{
					CmdletLogger.SafeSetLogger(this.context.UniqueId, RpsCmdletMetadata.ThrottlingDelay, info.DelayedAmount.TotalMilliseconds);
					if (info.DelayInfo is UserQuotaDelayInfo)
					{
						UserQuotaDelayInfo userQuotaDelayInfo2 = info.DelayInfo as UserQuotaDelayInfo;
						result = Strings.UserQuotaDelayInfo((int)info.DelayedAmount.TotalMilliseconds, info.Enforced, (int)info.DelayInfo.Delay.TotalMilliseconds, info.DelayInfo.Required, userQuotaDelayInfo2.OverBudgetException.PolicyPart, info.NotEnforcedReason);
					}
					else if (info.DelayInfo is ResourceLoadDelayInfo)
					{
						ResourceLoadDelayInfo resourceLoadDelayInfo2 = info.DelayInfo as ResourceLoadDelayInfo;
						result = Strings.ResourceLoadDelayInfo((int)info.DelayedAmount.TotalMilliseconds, info.Enforced, (int)info.DelayInfo.Delay.TotalMilliseconds, info.DelayInfo.Required, resourceLoadDelayInfo2.ResourceKey.ToString(), resourceLoadDelayInfo2.ResourceLoad.ToString(), info.NotEnforcedReason);
					}
					else
					{
						result = Strings.MicroDelayInfo((int)info.DelayedAmount.TotalMilliseconds, info.Enforced, (int)info.DelayInfo.Delay.TotalMilliseconds, info.DelayInfo.Required, info.NotEnforcedReason);
					}
				}
			}
			return result;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00050514 File Offset: 0x0004E714
		private void DisposeCostHandle()
		{
			foreach (BudgetInformation budgetInformation in from x in this.budgets
			where x.Handle != null
			select x)
			{
				budgetInformation.Handle.Dispose();
				budgetInformation.Handle = null;
				if (budgetInformation.Budget != null && budgetInformation.Budget.LocalCostHandle != null)
				{
					budgetInformation.Budget.EndLocal();
				}
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000505B0 File Offset: 0x0004E7B0
		private static LocalizedException WrapOverBudgetException(OverBudgetException originalException, IBudget budget)
		{
			string value = string.Format("{0}{4}(PolicyDN: {1};{4}Snapshot: {2};{4}Computer: {3})", new object[]
			{
				originalException.Message,
				originalException.ThrottlingPolicyDN,
				originalException.Snapshot,
				ThrottlingModule<T>.computerName,
				Environment.NewLine
			});
			return new OverBudgetException(new LocalizedString(value), budget, originalException.PolicyPart, originalException.PolicyValue, originalException.BackoffTime);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0005061C File Offset: 0x0004E81C
		protected void LogPeriodicEvent(ExEventLog.EventTuple eventInfo, params object[] messageArguments)
		{
			string periodicKey = this.context.InvocationInfo.ShellHostName;
			if (this.context.UserInfo.CurrentOrganizationId != null && !OrganizationId.ForestWideOrgId.Equals(this.context.UserInfo.CurrentOrganizationId))
			{
				periodicKey = this.context.UserInfo.CurrentOrganizationId.ToString();
			}
			TaskLogger.LogEvent(eventInfo, this.context.InvocationInfo, periodicKey, messageArguments);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00050697 File Offset: 0x0004E897
		private static void LogInformationTrace(string format, params object[] args)
		{
			if (ExTraceGlobals.LogTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.LogTracer.Information(0L, format, args);
			}
		}

		// Token: 0x0400068C RID: 1676
		private const string ThrottlingInfoKey = "ThrottlingInfo";

		// Token: 0x0400068D RID: 1677
		private const string DelayedInfoKey = "DelayedInfo";

		// Token: 0x0400068E RID: 1678
		private const int ThresholdToHideThrottlingWarning = 10;

		// Token: 0x0400068F RID: 1679
		private const int MicroDelayWriteCount = 50;

		// Token: 0x04000690 RID: 1680
		private TaskContext context;

		// Token: 0x04000691 RID: 1681
		private readonly bool casCostHandleDisabled;

		// Token: 0x04000692 RID: 1682
		private static string computerName;

		// Token: 0x04000693 RID: 1683
		protected ResourceKey[] resourceKeys = new ResourceKey[]
		{
			ProcessorResourceKey.Local
		};

		// Token: 0x04000694 RID: 1684
		internal List<BudgetInformation> budgets = new List<BudgetInformation>();

		// Token: 0x04000695 RID: 1685
		private static readonly TimeSpan CmdletMaxPreferredDelay = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000696 RID: 1686
		private static readonly CostType[] costTypesInEnforceDelay = new CostType[]
		{
			CostType.CAS,
			CostType.CMDLET
		};

		// Token: 0x04000697 RID: 1687
		private int writeCount;
	}
}
