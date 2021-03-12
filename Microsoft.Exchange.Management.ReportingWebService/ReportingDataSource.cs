using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ReportingTask;
using Microsoft.Exchange.Management.ReportingWebService.PowerShell;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200002D RID: 45
	internal class ReportingDataSource : IReportingDataSource
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00005212 File Offset: 0x00003412
		public ReportingDataSource(IPrincipal principal)
		{
			this.principal = principal;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005221 File Offset: 0x00003421
		public IList<T> GetData<T>(IEntity entity, Expression expression)
		{
			if (!typeof(T).IsAssignableFrom(entity.ClrType))
			{
				throw new ArgumentException("The underline clr type of the resouce entity doesn't match the generic type T");
			}
			return (IList<T>)this.GetData(entity, expression);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000052D0 File Offset: 0x000034D0
		public IList GetData(IEntity entity, Expression expression)
		{
			IPSCommandWrapper ipscommandWrapper = DependencyFactory.CreatePSCommandWrapper();
			ipscommandWrapper.AddCommand(entity.TaskInvocationInfo.CmdletName);
			if (entity.TaskInvocationInfo.Parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in entity.TaskInvocationInfo.Parameters)
				{
					ipscommandWrapper.AddParameter(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (!this.principal.IsInRole(string.Format("{0}\\{1}?{2}", entity.TaskInvocationInfo.SnapinName, entity.TaskInvocationInfo.CmdletName, "Expression")))
			{
				throw new InvalidOperationException(string.Format("Expression parameter is not avaible for the cmdlet {0}\\{1}. Add an entry in Microsoft.Exchange.Configuration.Authorization.ClientRoleEntries.TenantReportingRequiredParameters for your cmdlet.", entity.TaskInvocationInfo.SnapinName, entity.TaskInvocationInfo.CmdletName));
			}
			ipscommandWrapper.AddParameter("Expression", expression);
			IList data = null;
			using (new AverageTimePerfCounter(RwsPerfCounters.AverageReportCmdletResponseTime, RwsPerfCounters.AverageReportCmdletResponseTimeBase, true))
			{
				PowerShellResults result = ipscommandWrapper.Invoke(ReportingDataSource.RunspaceMediator);
				if (result.Succeeded)
				{
					ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.CreateGenericTypeListForResults, delegate
					{
						data = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
						{
							entity.ClrType
						}), new object[]
						{
							result.Output.Count
						});
					});
					foreach (PSObject psobject in result.Output)
					{
						data.Add(psobject.BaseObject);
					}
					ReportingDataSource.reportRowCounter.AddSample((long)data.Count);
				}
				else
				{
					ReportingWebServiceEventLogConstants.Tuple_InvokeCmdletFailed.LogEvent(new object[]
					{
						EventLogExtension.GetUserNameToLog(),
						ipscommandWrapper.Commands[0].CommandText,
						result.Errors[0].Exception
					});
					RwsPerfCounters.ReportCmdletErrors.Increment();
					this.ThrowError(result.Errors[0]);
				}
			}
			return data;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005580 File Offset: 0x00003780
		private void ThrowError(ErrorRecord errorRecord)
		{
			Exception exception = errorRecord.Exception;
			if (exception is OverBudgetException)
			{
				ServiceDiagnostics.ThrowError(ReportingErrorCode.ErrorOverBudget, Strings.ErrorOverBudget, exception);
				return;
			}
			if (exception is ConnectionFailedException)
			{
				ServiceDiagnostics.ThrowError(ReportingErrorCode.ConnectionFailedException, Strings.ConnectionFailedException, exception);
				return;
			}
			if (exception is ADTransientException)
			{
				ServiceDiagnostics.ThrowError(ReportingErrorCode.ADTransientError, Strings.ADTransientError, exception);
				return;
			}
			if (exception is InvalidExpressionException)
			{
				ServiceDiagnostics.ThrowError(ReportingErrorCode.InvalidQueryException, Strings.InvalidQueryException, exception);
				return;
			}
			if (exception is DataMartTimeoutException)
			{
				ServiceDiagnostics.ThrowError(ReportingErrorCode.DataMartTimeoutException, Strings.DataMartTimeoutException, exception);
				return;
			}
			ServiceDiagnostics.ThrowError(ReportingErrorCode.UnknownError, Strings.UnknownError, exception);
		}

		// Token: 0x04000066 RID: 102
		private static readonly RunspaceMediator RunspaceMediator = new RunspaceMediator(new ReportingWebServiceRunspaceFactory(), new ReportingWebServiceRunspaceCache());

		// Token: 0x04000067 RID: 103
		private static AveragePerfCounter reportRowCounter = new AveragePerfCounter(RwsPerfCounters.AverageReportRow, RwsPerfCounters.AverageReportRowBase);

		// Token: 0x04000068 RID: 104
		private readonly IPrincipal principal;
	}
}
