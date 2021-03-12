using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.SQM;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000290 RID: 656
	public class TaskModuleFactory : ITaskModuleFactory
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x000555EB File Offset: 0x000537EB
		public TaskModuleFactory()
		{
			this.RegisterModules();
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00055609 File Offset: 0x00053809
		public static void DisableModule(TaskModuleKey key)
		{
			TaskModuleFactory.disabledTaskModules[(int)key] = true;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00055613 File Offset: 0x00053813
		public static void EnableModule(TaskModuleKey key)
		{
			TaskModuleFactory.disabledTaskModules[(int)key] = false;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00055774 File Offset: 0x00053974
		public IEnumerable<ITaskModule> Create(TaskContext context)
		{
			for (int i = 0; i < this.taskModules.Length; i++)
			{
				if (!TaskModuleFactory.disabledTaskModules[i] && this.taskModules[i] != null)
				{
					yield return (ITaskModule)Activator.CreateInstance(this.taskModules[i], new object[]
					{
						context
					});
				}
			}
			yield break;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00055798 File Offset: 0x00053998
		private void RegisterModules()
		{
			this.RegisterModule(TaskModuleKey.Logging, typeof(LoggingModule));
			this.RegisterModule(TaskModuleKey.LatencyTracker, typeof(LatencyTrackingModule));
			this.RegisterModule(TaskModuleKey.Rbac, typeof(RbacModule));
			this.RegisterModule(TaskModuleKey.RunspaceServerSettingsInit, typeof(RunspaceServerSettingsInitModule));
			this.RegisterModule(TaskModuleKey.ReportException, typeof(ReportExceptionModule));
			this.RegisterModule(TaskModuleKey.CmdletHealthCounters, typeof(CmdletHealthCountersModule));
			this.RegisterModule(TaskModuleKey.SetErrorExecutionContext, typeof(SetErrorExecutionContextModule));
			this.RegisterModule(TaskModuleKey.Throttling, typeof(ThrottlingModule<ResourceThrottlingCallback>));
			this.RegisterModule(TaskModuleKey.TaskFaultInjection, typeof(TaskFaultInjectionModule));
			if (CmdletSqmSession.Instance.Enabled)
			{
				this.RegisterModule(TaskModuleKey.Sqm, typeof(SqmModule));
			}
			this.RegisterModule(TaskModuleKey.PiiRedaction, typeof(PiiRedactionModuleBase));
			if (Constants.IsPowerShellWebService)
			{
				this.RegisterModule(TaskModuleKey.PswsPropertyConverter, typeof(PswsPropertyConverterModule));
				this.RegisterModule(TaskModuleKey.PswsErrorHandling, typeof(PswsErrorHandling));
			}
			else
			{
				this.RegisterModule(TaskModuleKey.AutoReportProgress, typeof(AutoReportProgressModule));
			}
			if (TaskLogger.IsSetupLogging)
			{
				this.RegisterModule(TaskModuleKey.SetupLogging, typeof(SetupLoggingModule));
			}
			this.RegisterModule(TaskModuleKey.RunspaceServerSettingsFinalize, typeof(RunspaceServerSettingsFinalizeModule));
			this.RegisterModule(TaskModuleKey.CmdletIterationEvent, typeof(CmdletIterationEventModule));
			this.RegisterModule(TaskModuleKey.CmdletProxy, typeof(ProxyModule));
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000558FC File Offset: 0x00053AFC
		protected void RegisterModule(TaskModuleKey key, Type module)
		{
			this.taskModules[(int)key] = module;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00055907 File Offset: 0x00053B07
		protected void UnregisterModule(TaskModuleKey key)
		{
			this.taskModules[(int)key] = null;
		}

		// Token: 0x040006F8 RID: 1784
		private static readonly int moduleCount = Enum.GetValues(typeof(TaskModuleKey)).Length;

		// Token: 0x040006F9 RID: 1785
		private static readonly bool[] disabledTaskModules = new bool[TaskModuleFactory.moduleCount];

		// Token: 0x040006FA RID: 1786
		private readonly Type[] taskModules = new Type[TaskModuleFactory.moduleCount];
	}
}
