using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps.Probes
{
	// Token: 0x02000427 RID: 1063
	public class RecipientSignOffScenarioProbe : RPSLogonProbe
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x000984F0 File Offset: 0x000966F0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			Task<Runspace> task = Task.Factory.StartNew<Runspace>(new Func<Runspace>(base.OpenRPSConnection), cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
			task = task.ContinueWith<Runspace>(new Func<Task<Runspace>, Runspace>(this.InvokeNewMailContactCmdlet), cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
			task = task.ContinueWith<Runspace>(new Func<Task<Runspace>, Runspace>(this.InvokeSetMailContactCmdlet), cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
			task = task.ContinueWith<Runspace>(new Func<Task<Runspace>, Runspace>(this.InvokeGetMailContactCmdlet), cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
			task = task.ContinueWith<Runspace>(new Func<Task<Runspace>, Runspace>(this.InvokeRemoveMailContactCmdlet), cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
			task.ContinueWith(new Action<Task<Runspace>>(this.CloseRPSConnection), cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000985B4 File Offset: 0x000967B4
		protected void CloseRPSConnection(Task<Runspace> executingTask)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.RPSTracer, base.TraceContext, "Entering CloseRPSConnection()", null, "CloseRPSConnection", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\RecipientSignOffScenarioProbe.cs", 81);
			DateTime utcNow = DateTime.UtcNow;
			try
			{
				if (base.Runspace != null)
				{
					base.Runspace.Close();
					base.Runspace.Dispose();
					base.Runspace = null;
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RPSTracer, base.TraceContext, "Close RPS connection successfully", null, "CloseRPSConnection", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\RecipientSignOffScenarioProbe.cs", 90);
				}
			}
			catch (Exception innerException)
			{
				base.ThrowFailureException("Close RPS connection failed.", DateTime.UtcNow - utcNow, innerException);
			}
			WTFDiagnostics.TraceFunction(ExTraceGlobals.RPSTracer, base.TraceContext, "Leaving CloseRPSConnection()", null, "CloseRPSConnection", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\RecipientSignOffScenarioProbe.cs", 98);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00098680 File Offset: 0x00096880
		private Runspace InvokeNewMailContactCmdlet(Task<Runspace> previousTask)
		{
			this.identity = "RSOSP_" + Guid.NewGuid().ToString();
			return this.InvokeCmdlet(previousTask, new Command("New-MailContact")
			{
				Parameters = 
				{
					{
						"Name",
						this.identity
					},
					{
						"ExternalEmailAddress",
						this.identity + "@outside.datacenter"
					}
				}
			}, null);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000986FC File Offset: 0x000968FC
		private Runspace InvokeSetMailContactCmdlet(Task<Runspace> previousTask)
		{
			return this.InvokeCmdlet(previousTask, new Command("Set-MailContact")
			{
				Parameters = 
				{
					{
						"Identity",
						this.identity
					},
					{
						"DisplayName",
						this.identity + "displayName"
					}
				}
			}, null);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0009886C File Offset: 0x00096A6C
		private Runspace InvokeGetMailContactCmdlet(Task<Runspace> previousTask)
		{
			return this.InvokeCmdlet(previousTask, new Command("Get-MailContact")
			{
				Parameters = 
				{
					{
						"Identity",
						this.identity
					}
				}
			}, delegate(Collection<PSObject> result)
			{
				if (result == null || result.Count != 1)
				{
					throw new ApplicationException("Cannot find created mail contact object");
				}
				PSObject psobject = result[0];
				string text = psobject.Properties["Name"].Value as string;
				string text2 = psobject.Properties["ExternalEmailAddress"].Value as string;
				string text3 = psobject.Properties["DisplayName"].Value as string;
				if (string.Compare(text, this.identity) != 0 || string.Compare(text3, this.identity + "displayName") != 0 || !text2.Contains(this.identity + "@outside.datacenter"))
				{
					throw new ApplicationException(string.Format("Mailcontact content doesn't match, Name={0}, DisplayName={1}, ExternalEmailAddress={2}, expected Name={3}, DisplayName={4}, ExternalEmailAddress={5}", new object[]
					{
						text,
						text3,
						text2,
						this.identity,
						this.identity + "displayName",
						this.identity + "@outside.datacenter"
					}));
				}
			});
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000988B0 File Offset: 0x00096AB0
		private Runspace InvokeRemoveMailContactCmdlet(Task<Runspace> previousTask)
		{
			return this.InvokeCmdlet(previousTask, new Command("Remove-MailContact")
			{
				Parameters = 
				{
					{
						"Identity",
						this.identity
					},
					{
						"Confirm",
						false
					}
				}
			}, null);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00098900 File Offset: 0x00096B00
		private Runspace InvokeCmdlet(Task<Runspace> openTask, Command command, Action<Collection<PSObject>> checkResultDelegate)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.RPSTracer, base.TraceContext, "Entering InvokeCmdlet()", null, "InvokeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\RecipientSignOffScenarioProbe.cs", 194);
			DateTime utcNow = DateTime.UtcNow;
			if (base.Runspace == null)
			{
				return null;
			}
			try
			{
				using (PowerShell powerShell = PowerShell.Create())
				{
					powerShell.Commands.AddCommand(command);
					powerShell.Runspace = base.Runspace;
					Collection<PSObject> obj = powerShell.Invoke();
					if (powerShell.Streams.Error != null && powerShell.Streams.Error.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine("Cmdlet return the following error :");
						foreach (ErrorRecord errorRecord in powerShell.Streams.Error)
						{
							stringBuilder.AppendLine(errorRecord.Exception.ToString());
						}
						throw new ApplicationException(stringBuilder.ToString());
					}
					if (checkResultDelegate != null)
					{
						checkResultDelegate(obj);
					}
				}
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RPSTracer, base.TraceContext, "Execute cmdlet successfully", null, "InvokeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\RecipientSignOffScenarioProbe.cs", 227);
			}
			catch (Exception innerException)
			{
				base.ThrowFailureException("Execute cmdlet failed.", DateTime.UtcNow - utcNow, innerException);
			}
			WTFDiagnostics.TraceFunction(ExTraceGlobals.RPSTracer, base.TraceContext, "Leaving InvokeCmdlet()", null, "InvokeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\RecipientSignOffScenarioProbe.cs", 234);
			return base.Runspace;
		}

		// Token: 0x040012B9 RID: 4793
		private string identity;
	}
}
