using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.Core.EventLog;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000277 RID: 631
	internal static class CriticalFeatureHelper
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x000514B8 File Offset: 0x0004F6B8
		internal static void Execute(this ICriticalFeature feature, Action action, TaskContext taskContext, string methodNameInLog)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				Guid uniqueId = taskContext.UniqueId;
				bool flag = false;
				Exception ex2;
				bool needReportException = !TaskHelper.IsTaskKnownException(ex2) && TaskHelper.ShouldReportException(ex2, out flag);
				if (!needReportException)
				{
					CmdletLogger.SafeAppendGenericError(uniqueId, methodNameInLog, ex2.ToString(), false);
				}
				else
				{
					CmdletLogger.SafeAppendGenericError(uniqueId, methodNameInLog, ex2.ToString(), true);
					CmdletLogger.SafeSetLogger(uniqueId, RpsCmdletMetadata.ErrorType, "UnHandled");
				}
				if (feature == null || feature.IsCriticalException(ex2))
				{
					throw;
				}
				if (!flag)
				{
					taskContext.CommandShell.WriteWarning(Strings.WarningTaskModuleSkipped(methodNameInLog, ex2.Message));
				}
				Diagnostics.ReportException(ex2, Constants.CoreEventLogger, TaskEventLogConstants.Tuple_UnhandledException, (object ex) => needReportException, null, ExTraceGlobals.InstrumentationTracer, "Exception from  " + methodNameInLog + ": {0}");
			}
		}
	}
}
