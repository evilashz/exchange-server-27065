using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x02000010 RID: 16
	internal static class PSCommandExtension
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002E00 File Offset: 0x00001000
		public static PowerShellResults Invoke(this PSCommand psCommand, RunspaceMediator runspaceMediator)
		{
			PowerShellResults results = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.InvokeCmdletLatency, delegate
			{
				using (RunspaceProxy runspace = new RunspaceProxy(runspaceMediator))
				{
					try
					{
						ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.InvokeCmdletExcludeRunspaceCreationLatency, delegate
						{
							using (PowerShell powerShell = runspace.CreatePowerShell(psCommand))
							{
								try
								{
									Collection<PSObject> output = null;
									ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.InvokeCmdletExclusiveLatency, delegate
									{
										output = powerShell.Invoke();
									});
									ErrorRecord[] array = new ErrorRecord[powerShell.Streams.Error.Count];
									powerShell.Streams.Error.CopyTo(array, 0);
									results = new PowerShellResults(output, new Collection<ErrorRecord>(array));
								}
								catch (RuntimeException ex)
								{
									ErrorRecord errorRecord3;
									if (ex.ErrorRecord != null && !(ex is ParameterBindingException))
									{
										errorRecord3 = ex.ErrorRecord;
									}
									else
									{
										errorRecord3 = new ErrorRecord(ex, string.Empty, ErrorCategory.NotSpecified, null);
									}
									results = new PowerShellResults(new Collection<ErrorRecord>(new ErrorRecord[]
									{
										errorRecord3
									}));
								}
							}
						});
					}
					catch (OverBudgetException exception)
					{
						ErrorRecord errorRecord = new ErrorRecord(exception, string.Empty, ErrorCategory.NotSpecified, null);
						results = new PowerShellResults(new Collection<ErrorRecord>(new ErrorRecord[]
						{
							errorRecord
						}));
					}
					catch (ADTransientException exception2)
					{
						ErrorRecord errorRecord2 = new ErrorRecord(exception2, string.Empty, ErrorCategory.NotSpecified, null);
						results = new PowerShellResults(new Collection<ErrorRecord>(new ErrorRecord[]
						{
							errorRecord2
						}));
					}
				}
			});
			return results;
		}
	}
}
