using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.FfoCentralAdmin
{
	// Token: 0x0200008E RID: 142
	public class FfoPowerShellScriptProbe : ProbeWorkItem
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001863F File Offset: 0x0001683F
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00018647 File Offset: 0x00016847
		private string ScriptCommand { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00018650 File Offset: 0x00016850
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00018658 File Offset: 0x00016858
		private string WarningPreference { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00018661 File Offset: 0x00016861
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00018669 File Offset: 0x00016869
		private string VerbosePreference { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00018672 File Offset: 0x00016872
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0001867A File Offset: 0x0001687A
		private string ErrorPreference { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00018683 File Offset: 0x00016883
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0001868B File Offset: 0x0001688B
		private string DebugPreference { get; set; }

		// Token: 0x060003F3 RID: 1011 RVA: 0x00018694 File Offset: 0x00016894
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			string @string = attributeHelper.GetString("ScriptPath", true, "Datacenter");
			string string2 = attributeHelper.GetString("Parameters", false, null);
			this.VerbosePreference = attributeHelper.GetString("VerbosePreference", false, "SilentlyContinue");
			this.DebugPreference = attributeHelper.GetString("DebugPreference", false, "SilentlyContinue");
			this.ErrorPreference = attributeHelper.GetString("ErrorPreference", false, null);
			this.WarningPreference = attributeHelper.GetString("WarningPreference", false, null);
			this.ScriptCommand = this.CreateScriptCommand(@string, string2);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00018734 File Offset: 0x00016934
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.ExecutionContext = string.Format("FfoPowerShellScriptProbe started at {0}.{1}", DateTime.UtcNow, Environment.NewLine);
			this.InitializeAttributes(null);
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, "In FfoPowerShellScriptProbe", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 109);
			InitialSessionState initialSessionState = InitialSessionState.CreateDefault();
			initialSessionState.LanguageMode = PSLanguageMode.FullLanguage;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, "Opening New Runspace", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 116);
			using (Runspace runspace = RunspaceFactory.CreateRunspace(initialSessionState))
			{
				runspace.Open();
				PowerShell powerShell2;
				PowerShell powerShell = powerShell2 = PowerShell.Create();
				try
				{
					powerShell.Commands.AddScript(string.Format("$DebugPreference='{0}'", this.DebugPreference));
					powerShell.Commands.AddScript(string.Format("$VerbosePreference='{0}'", this.VerbosePreference));
					if (!string.IsNullOrEmpty(this.WarningPreference))
					{
						powerShell.Commands.AddScript(string.Format("$WarningPreference='{0}'", this.WarningPreference));
					}
					if (!string.IsNullOrEmpty(this.ErrorPreference))
					{
						powerShell.Commands.AddScript(string.Format("$ErrorPreference='{0}'", this.ErrorPreference));
					}
					powerShell.Commands.AddScript(this.ScriptCommand);
					powerShell.Runspace = runspace;
					string text = string.Format("Executing Powershell Command: {0}. {1}", this.ScriptCommand, Environment.NewLine);
					ProbeResult result = base.Result;
					result.ExecutionContext += text;
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 142);
					text = string.Format("$DebugPreference='{0}', $VerbosePreference='{1}', $WarningPreference='{2}', $ErrorPreference='{3}'. {4}", new object[]
					{
						this.DebugPreference,
						this.VerbosePreference,
						this.WarningPreference,
						this.ErrorPreference,
						Environment.NewLine
					});
					ProbeResult result2 = base.Result;
					result2.ExecutionContext += text;
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 147);
					try
					{
						Collection<PSObject> collection = powerShell.Invoke();
						WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, "Processing result...", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 152);
						string value = "A command that prompts the user failed because the host program or the command type does not support user interaction";
						foreach (PSObject arg in collection)
						{
							stringBuilder2.AppendFormat("INFO: {0} {1} ", arg, Environment.NewLine);
						}
						foreach (DebugRecord arg2 in powerShell.Streams.Debug)
						{
							stringBuilder2.AppendFormat("DEBUG: {0} {1} ", arg2, Environment.NewLine);
						}
						foreach (WarningRecord arg3 in powerShell.Streams.Warning)
						{
							stringBuilder2.AppendFormat("WARNING: {0} {1} ", arg3, Environment.NewLine);
						}
						foreach (VerboseRecord arg4 in powerShell.Streams.Verbose)
						{
							stringBuilder2.AppendFormat("VERBOSE: {0} {1} ", arg4, Environment.NewLine);
						}
						foreach (ErrorRecord errorRecord in powerShell.Streams.Error)
						{
							if (errorRecord.ToString().IndexOf(value) == -1)
							{
								stringBuilder.AppendFormat("ERROR: {0} {1} ", errorRecord, Environment.NewLine);
							}
						}
					}
					catch (Exception ex)
					{
						ProbeResult result3 = base.Result;
						result3.ExecutionContext += "Probe execution failed";
						throw new Exception(string.Format("Executing Powershell Probe Command: '{0}' failed with error: {1} {2}", this.ScriptCommand, Environment.NewLine, ex.Message));
					}
				}
				finally
				{
					if (powerShell2 != null)
					{
						((IDisposable)powerShell2).Dispose();
					}
				}
				runspace.Close();
				ProbeResult result4 = base.Result;
				result4.ExecutionContext += stringBuilder2.ToString();
				ProbeResult result5 = base.Result;
				result5.ExecutionContext += string.Format("FfoPowerShellScriptProbe completed at {0}.{1}", DateTime.UtcNow, Environment.NewLine);
				string text2 = stringBuilder.ToString();
				if (text2.Length > 0)
				{
					WTFDiagnostics.TraceFunction<string, string>(ExTraceGlobals.CentralAdminTracer, base.TraceContext, "Probe execution reported errors {0} on machine {1}", text2, Environment.MachineName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 201);
					base.Result.FailureContext = text2;
					base.Result.Error = text2;
					throw new Exception(text2);
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00018CD0 File Offset: 0x00016ED0
		private string CreateScriptCommand(string scriptPath, string parameters)
		{
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
			if (string.IsNullOrEmpty(text))
			{
				throw new Exception(string.Format("Could not retrieve Exchange Install Path on Machine {0}", Environment.MachineName));
			}
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CentralAdminTracer, base.TraceContext, string.Format("Creating Powershell Command:  ScriptPath: {0}, Parameters: {1}", scriptPath, parameters), null, "CreateScriptCommand", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\FfoCentralAdmin\\Probes\\FfoPowerShellScriptProbe.cs", 221);
			string text2 = Path.Combine(text, scriptPath);
			StringBuilder stringBuilder = new StringBuilder();
			if (File.Exists(text2))
			{
				if (!string.IsNullOrEmpty(parameters))
				{
					string[] array = parameters.Split(new char[]
					{
						';'
					});
					foreach (string text3 in array)
					{
						if (text3.IndexOf("=") == -1)
						{
							stringBuilder.AppendFormat(" -{0}", text3);
						}
						else
						{
							string[] array3 = text3.Split(new char[]
							{
								'='
							});
							stringBuilder.AppendFormat(" -{0} {1}", array3[0], array3[1]);
						}
					}
				}
				return string.Format("&(gi '{0}') {1}", text2, stringBuilder);
			}
			throw new Exception(string.Format("Probe Script file {0} could not be found on Machine {1}. Exchange Install Path: {2}, ScriptPath: {3}", new object[]
			{
				text2,
				Environment.MachineName,
				text,
				scriptPath
			}));
		}

		// Token: 0x0200008F RID: 143
		internal static class AttributeNames
		{
			// Token: 0x0400024E RID: 590
			internal const string ScriptPath = "ScriptPath";

			// Token: 0x0400024F RID: 591
			internal const string Parameters = "Parameters";

			// Token: 0x04000250 RID: 592
			internal const string DebugPreference = "DebugPreference";

			// Token: 0x04000251 RID: 593
			internal const string VerbosePreference = "VerbosePreference";

			// Token: 0x04000252 RID: 594
			internal const string ErrorPreference = "ErrorPreference";

			// Token: 0x04000253 RID: 595
			internal const string WarningPreference = "WarningPreference";
		}

		// Token: 0x02000090 RID: 144
		internal static class DefaultValues
		{
			// Token: 0x04000254 RID: 596
			internal const string ScriptPath = "Datacenter";

			// Token: 0x04000255 RID: 597
			internal const string DebugPreference = "SilentlyContinue";

			// Token: 0x04000256 RID: 598
			internal const string VerbosePreference = "SilentlyContinue";
		}
	}
}
