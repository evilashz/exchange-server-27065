using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008E4 RID: 2276
	internal static class Engine
	{
		// Token: 0x060050BE RID: 20670 RVA: 0x00151624 File Offset: 0x0014F824
		public static void Execute(ITaskContext taskContext, IWorkflow workflow)
		{
			foreach (ITask task in workflow.Tasks)
			{
				LocalizedString localizedString = new LocalizedString(task.Name);
				taskContext.UI.WriteVerbose(localizedString);
				taskContext.UI.WriteProgessIndicator(HybridStrings.HybridActivityConfigure, localizedString, workflow.PercentCompleted);
				Engine.Execute(taskContext, task);
				workflow.UpdateProgress(task);
			}
			taskContext.UI.WriteProgessIndicator(HybridStrings.HybridActivityConfigure, HybridStrings.HybridActivityCompleted, workflow.PercentCompleted);
			taskContext.Logger.LogInformation(HybridStrings.HybridInfoExecutionComplete);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0015174C File Offset: 0x0014F94C
		public static void Execute(ITaskContext taskContext, ITask task)
		{
			Engine.ExecuteSubStep("CheckPrereqs", taskContext, task, (ITaskContext tc, ITask t) => t.CheckPrereqs(tc), (ITaskContext tc, ITask t, Exception e) => new TaskCheckPrereqsException(t.Name, e, tc.Errors), true);
			if (Engine.ExecuteSubStep("NeedsConfiguration", taskContext, task, (ITaskContext tc, ITask t) => t.NeedsConfiguration(tc), (ITaskContext tc, ITask t, Exception e) => new TaskNeedsConfigurationException(t.Name, e, tc.Errors), false))
			{
				Engine.ExecuteSubStep("Configure", taskContext, task, (ITaskContext tc, ITask t) => t.Configure(tc), (ITaskContext tc, ITask t, Exception e) => new TaskConfigureException(t.Name, e, tc.Errors), true);
				Engine.ExecuteSubStep("ValidateConfiguration", taskContext, task, (ITaskContext tc, ITask t) => t.ValidateConfiguration(tc), (ITaskContext tc, ITask t, Exception e) => new TaskValidateConfigurationException(t.Name, e, tc.Errors), true);
			}
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00151880 File Offset: 0x0014FA80
		private static bool ExecuteSubStep(string subStepName, ITaskContext taskContext, ITask task, Func<ITaskContext, ITask, bool> substep, Func<ITaskContext, ITask, Exception, Exception> createException, bool throwOnFalse)
		{
			bool flag = false;
			Exception ex = null;
			ExDateTime now = ExDateTime.Now;
			try
			{
				taskContext.Logger.LogInformation(HybridStrings.HybridInfoTaskSubStepStart(task.Name, subStepName));
				flag = substep(taskContext, task);
				if (taskContext.Warnings.Count > 0)
				{
					foreach (LocalizedString localizedString in taskContext.Warnings)
					{
						taskContext.Logger.LogWarning(localizedString);
						taskContext.UI.WriteWarning(localizedString);
					}
					taskContext.Warnings.Clear();
				}
				if (throwOnFalse && !flag)
				{
					ex = createException(taskContext, task, null);
				}
			}
			catch (Exception arg)
			{
				ex = createException(taskContext, task, arg);
			}
			finally
			{
				if (ex != null)
				{
					taskContext.Logger.LogError(ex.ToString());
				}
				double totalMilliseconds = ExDateTime.Now.Subtract(now).TotalMilliseconds;
				taskContext.Logger.LogInformation(HybridStrings.HybridInfoTaskSubStepFinish(task.Name, subStepName, flag, totalMilliseconds));
				taskContext.Logger.LogInformation(new string('-', 128));
				if (ex != null)
				{
					throw ex;
				}
			}
			return flag;
		}

		// Token: 0x04002F7A RID: 12154
		internal const string SubStepCheckPrereqs = "CheckPrereqs";

		// Token: 0x04002F7B RID: 12155
		internal const string SubStepNeedsConfiguration = "NeedsConfiguration";

		// Token: 0x04002F7C RID: 12156
		internal const string SubStepConfigure = "Configure";

		// Token: 0x04002F7D RID: 12157
		internal const string SubStepValidateConfiguration = "ValidateConfiguration";
	}
}
