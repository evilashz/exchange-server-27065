using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000A1 RID: 161
	public class LocalPowerShellProvider : IDisposable
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00021C64 File Offset: 0x0001FE64
		public Runspace psRunspace
		{
			get
			{
				if (this.IsValidRunSpace())
				{
					return this._runSpace;
				}
				this.InitializePSRunspace();
				return this._runSpace;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00021C81 File Offset: 0x0001FE81
		public LocalPowerShellProvider()
		{
			if (!this.IsValidRunSpace())
			{
				this.InitializePSRunspace();
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00021C98 File Offset: 0x0001FE98
		public Collection<PSObject> RunExchangeCmdlet<T>(string cmdletName, Dictionary<string, T> parameters, TracingContext traceContext, bool throwPipelineError = false)
		{
			if (string.IsNullOrEmpty(cmdletName))
			{
				throw new ArgumentNullException("cmdletName", "Cmdlet name cannot be null or empty.");
			}
			Collection<PSObject> result;
			try
			{
				Pipeline pipeline = this.psRunspace.CreatePipeline();
				Command command = new Command(cmdletName);
				if (parameters != null)
				{
					foreach (KeyValuePair<string, T> keyValuePair in parameters)
					{
						T value = keyValuePair.Value;
						if (value.ToString().Contains("SwitchValue"))
						{
							command.Parameters.Add(keyValuePair.Key);
						}
						else
						{
							command.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				pipeline.Commands.Add(command);
				result = pipeline.Invoke();
				if (pipeline.Error.Count > 0)
				{
					Collection<object> collection = pipeline.Error.ReadToEnd();
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in collection)
					{
						stringBuilder.Append(obj.ToString());
					}
					string message = string.Format("Error occurred when invoking ExchangeCmdlet [{0}]: {1}", cmdletName, stringBuilder.ToString());
					PublicFolderMonitoringHelper.LogMessage(message, new object[0]);
					WTFDiagnostics.TraceDebug(ExTraceGlobals.PublicFoldersTracer, traceContext, message, null, "RunExchangeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\LocalPowerShell\\LocalPowerShellProvider.cs", 117);
					if (throwPipelineError)
					{
						throw new ActiveMonitoringPowerShellException(message);
					}
				}
			}
			catch (Exception ex)
			{
				string message = string.Format("Failed to run the cmdlet {0}. Exception: {1}", cmdletName, ex.Message);
				PublicFolderMonitoringHelper.LogMessage(message, new object[0]);
				WTFDiagnostics.TraceDebug(ExTraceGlobals.PublicFoldersTracer, traceContext, message, null, "RunExchangeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\LocalPowerShell\\LocalPowerShellProvider.cs", 129);
				throw new Exception(message, ex);
			}
			return result;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00021EA4 File Offset: 0x000200A4
		public Collection<PSObject> RunPowershellScript(string scriptFile, Dictionary<string, string> scriptParameters, TracingContext traceContext, bool redirectScriptOutputToFile = false, string logFile = null)
		{
			RunspaceInvoke runspaceInvoke = new RunspaceInvoke(this.psRunspace);
			Pipeline pipeline = this.psRunspace.CreatePipeline();
			Command command = new Command(scriptFile);
			Collection<PSObject> result = null;
			if (scriptParameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in scriptParameters)
				{
					CommandParameter item;
					if (keyValuePair.Value.Contains("SwitchValue"))
					{
						item = new CommandParameter(keyValuePair.Key, new SwitchParameter(true));
					}
					else
					{
						item = new CommandParameter(keyValuePair.Key, keyValuePair.Value);
					}
					command.Parameters.Add(item);
				}
			}
			pipeline.Commands.Add(command);
			if (redirectScriptOutputToFile)
			{
				Command command2 = new Command("Out-File");
				CommandParameter item2 = new CommandParameter("FilePath", logFile);
				command2.Parameters.Add(item2);
				pipeline.Commands.Add(command2);
			}
			try
			{
				result = pipeline.Invoke();
			}
			catch (Exception ex)
			{
				string message = string.Format("Exception Invoking the powershell script [{0}] : {1}", scriptFile, ex.Message);
				PublicFolderMonitoringHelper.LogMessage(message, new object[0]);
				WTFDiagnostics.TraceDebug(ExTraceGlobals.PublicFoldersTracer, traceContext, message, null, "RunPowershellScript", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\LocalPowerShell\\LocalPowerShellProvider.cs", 194);
				runspaceInvoke.Dispose();
				throw new Exception(message, ex);
			}
			runspaceInvoke.Dispose();
			return result;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002201C File Offset: 0x0002021C
		internal bool IsValidRunSpace()
		{
			return this._runSpace != null;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0002202C File Offset: 0x0002022C
		internal void InitializePSRunspace()
		{
			RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
			PSSnapInException ex = null;
			runspaceConfiguration.AddPSSnapIn("Microsoft.Exchange.Management.PowerShell.E2010", out ex);
			if (ex != null)
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.PublicFoldersTracer, TracingContext.Default, "Non-fatal error occurred while adding the powerShell snap-in. Warning: {0}", ex.Message, null, "InitializePSRunspace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\LocalPowerShell\\LocalPowerShellProvider.cs", 226);
			}
			this._runSpace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
			this._runSpace.Open();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00022093 File Offset: 0x00020293
		public void Dispose()
		{
			if (this.IsValidRunSpace())
			{
				this._runSpace.Dispose();
				this._runSpace = null;
			}
		}

		// Token: 0x04000395 RID: 917
		private Runspace _runSpace;
	}
}
