using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008E0 RID: 2272
	internal static class UpdateHybridConfigurationLogic
	{
		// Token: 0x0600509C RID: 20636 RVA: 0x00150F94 File Offset: 0x0014F194
		public static void Validate(HybridConfiguration dataObject, bool hasErrors, Task.TaskErrorLoggingDelegate writeErrorFunc)
		{
			if (hasErrors)
			{
				return;
			}
			if (dataObject.MaximumSupportedExchangeObjectVersion.ExchangeBuild < dataObject.ExchangeVersion.ExchangeBuild)
			{
				writeErrorFunc(new InvalidObjectOperationException(HybridStrings.ErrorHybridConfigurationTooNew(dataObject.ExchangeVersion.ToString(), dataObject.MaximumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x00150FEC File Offset: 0x0014F1EC
		public static void ProcessRecord(ILogger logger, IUserInterface ui, HybridConfiguration hybridConfigurationObject, Func<IOnPremisesSession> createOnPremisesSessionFunc, Func<ITenantSession> createTenantSessionFunc, Action<Exception, ErrorCategory, object> writeErrorFunc, bool forceUpgrade, bool suppressOAuthWarning)
		{
			try
			{
				using (StandardWorkflow standardWorkflow = new StandardWorkflow(logger, ui, hybridConfigurationObject, createOnPremisesSessionFunc, createTenantSessionFunc))
				{
					standardWorkflow.TaskContext.Parameters.Set<bool>("_forceUpgrade", forceUpgrade);
					standardWorkflow.TaskContext.Parameters.Set<bool>("_suppressOAuthWarning", suppressOAuthWarning);
					Engine.Execute(standardWorkflow.TaskContext, standardWorkflow);
				}
			}
			catch (Exception ex)
			{
				LocalizedString localizedString = HybridStrings.ExceptionUpdateHybridConfigurationFailedWithLog(ex.ToString(), Environment.MachineName, logger.ToString());
				writeErrorFunc(new LocalizedException(localizedString), ErrorCategory.WriteError, null);
			}
		}
	}
}
