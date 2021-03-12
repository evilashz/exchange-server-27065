using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Responders
{
	// Token: 0x02000234 RID: 564
	public class NetworkAdapterRecoveryResponder : ResponderWorkItem
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x000693A4 File Offset: 0x000675A4
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, ServiceHealthStatus targetHealthState, bool enabled = true)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition
			{
				AssemblyPath = NetworkAdapterRecoveryResponder.AssemblyPath,
				TypeName = NetworkAdapterRecoveryResponder.TypeName,
				Name = name,
				ServiceName = serviceName,
				AlertTypeId = alertTypeId,
				AlertMask = alertMask,
				TargetHealthState = targetHealthState,
				RecurrenceIntervalSeconds = 300,
				TimeoutSeconds = 300,
				MaxRetryAttempts = 3,
				Enabled = enabled
			};
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.SetNetworkAdapter, Environment.MachineName, null);
			return responderDefinition;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x00069484 File Offset: 0x00067684
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			Task<ProbeResult> result = base.GetLastSuccessfulMonitorResult(cancellationToken).ContinueWith<Task<ProbeResult>>((Task<MonitorResult> monitorResult) => this.GetProbeResultAsync(monitorResult.Result.LastFailedProbeId, monitorResult.Result.LastFailedProbeResultId, cancellationToken)).Result;
			if (result.Result.StateAttribute1.Contains("No Recovery"))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, base.TraceContext, "NetworkAdapterRecoveryResponder:: DoResponderWork(): We are in test environment, no recovery action will be taken.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 95);
				return;
			}
			NetworkAdapterProbe.MissingEntriesInNetworkAdapter missingEntries = (NetworkAdapterProbe.MissingEntriesInNetworkAdapter)result.Result.StateAttribute6;
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.SetNetworkAdapter, Environment.MachineName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate()
			{
				NetworkAdapterRecoveryResponder.FixAdapterSettings(missingEntries, this.TraceContext, this.Result);
			});
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0006953C File Offset: 0x0006773C
		internal static bool FixAdapterSettings(NetworkAdapterProbe.MissingEntriesInNetworkAdapter missingEntries, TracingContext traceContext)
		{
			return NetworkAdapterRecoveryResponder.FixAdapterSettings(missingEntries, traceContext, null);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00069560 File Offset: 0x00067760
		internal static bool FixAdapterSettings(NetworkAdapterProbe.MissingEntriesInNetworkAdapter missingEntries, TracingContext traceContext, ResponderResult responderResult)
		{
			FileInfo fileInfo = new FileInfo("D:\\NetworkMonitoring\\ServerNetworkAdapterConfiguration.txt");
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			if (!fileInfo.Exists)
			{
				if (traceContext != null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, "NetworkAdapterRecoveryResponder:: DoResponderWork(): File does not exist, recovery action FAILED.", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 191);
				}
				return false;
			}
			using (StreamReader streamReader = new StreamReader("D:\\NetworkMonitoring\\ServerNetworkAdapterConfiguration.txt"))
			{
				while (streamReader.Peek() >= 0)
				{
					string text5 = streamReader.ReadLine();
					string[] array = text5.Split(new char[]
					{
						':'
					}, 2);
					string a;
					if (array.Length == 2 && (a = array[0]) != null)
					{
						if (!(a == "IpAddress"))
						{
							if (!(a == "SubnetMask"))
							{
								if (!(a == "DefaultGateway"))
								{
									if (a == "DnsAddresses")
									{
										text4 = array[1];
									}
								}
								else
								{
									text3 = array[1];
								}
							}
							else
							{
								text = array[1];
							}
						}
						else
						{
							text2 = array[1];
						}
					}
				}
			}
			if (traceContext != null)
			{
				WTFDiagnostics.TraceInformation<string, string, string, string>(ExTraceGlobals.NetworkTracer, traceContext, "IPAddress: {0}, SubnetMask: {1}, DefaultGateway: {2}, DnsAddresses: {3}", text2, text, text3, text4, null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 174);
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.NetworkTracer, traceContext, "NetworkAdapterRecoveryResponder:: DoResponderWork(): File contents have been read from file {0}.", "D:\\NetworkMonitoring\\ServerNetworkAdapterConfiguration.txt", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 184);
			}
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			if (responderResult != null)
			{
				NetworkUtils.LogWorkItemMessage(traceContext, responderResult, string.Format("{0} network interfaces were detected.", allNetworkInterfaces.Length), new object[0]);
			}
			NetworkInterface[] array2 = (from nic in allNetworkInterfaces
			where nic.Name.ToUpperInvariant().Equals("MAPI")
			select nic).ToArray<NetworkInterface>();
			if (array2.Length == 0)
			{
				if (traceContext != null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, "No MAPI network interface is found. No recovery will be applied. Returning...", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 216);
				}
				return false;
			}
			if (array2.Length > 1)
			{
				if (traceContext != null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, "More than one MAPI network interfaces are found. No recovery will be applied. Returning...", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 229);
				}
				return false;
			}
			NetworkInterface networkInterface = array2[0];
			bool? flag = null;
			bool? flag2 = null;
			bool? flag3 = null;
			if (missingEntries.HasFlag(NetworkAdapterProbe.MissingEntriesInNetworkAdapter.IpAddress))
			{
				if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text))
				{
					if (traceContext != null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, "Configuration is missing IpAddress or SubnetMask but recovery file does not have a value to restore. No recovery will be applied. Returning...", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 248);
					}
					return false;
				}
				string[] array3 = text2.Split(new char[]
				{
					' '
				});
				string[] array4 = text.Split(new char[]
				{
					' '
				});
				string processStartInfoArgument = string.Format("interface ip add address \"{0}\" address={1} mask={2}", networkInterface.Name, array3[0], array4[0]);
				string message = string.Format("NetworkAdapterRecoveryResponder:: DoResponderWork(): IpAddress changed to value {0} in adapter {1} and subnet is {2}.", array3[0], networkInterface.Name, text);
				flag = new bool?(NetworkAdapterRecoveryResponder.SetNetworkAdapter(processStartInfoArgument, message, traceContext));
			}
			if (missingEntries.HasFlag(NetworkAdapterProbe.MissingEntriesInNetworkAdapter.DefaultGateway))
			{
				if (string.IsNullOrEmpty(text3))
				{
					if (traceContext != null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, "Configuration is missing DefaultGateway but recovery file does not have a value to restore. No recovery will be applied. Returning...", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 278);
					}
					return false;
				}
				string[] array5 = text3.Split(new char[]
				{
					' '
				});
				if (array5.Length > 0)
				{
					string processStartInfoArgument2 = string.Format("interface ip add address \"{0}\" gateway={1} gwmetric=0", networkInterface.Name, array5[0]);
					string message2 = string.Format("NetworkAdapterRecoveryResponder:: DoResponderWork(): defaultGateway changed to value {0} in adapter {1}.", array5[0], networkInterface.Name);
					flag2 = new bool?(NetworkAdapterRecoveryResponder.SetNetworkAdapter(processStartInfoArgument2, message2, traceContext));
				}
			}
			if (missingEntries.HasFlag(NetworkAdapterProbe.MissingEntriesInNetworkAdapter.DnsAddresses))
			{
				if (string.IsNullOrEmpty(text4))
				{
					if (traceContext != null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, "Configuration is missing DnsAddresses but recovery file does not have a value to restore. No recovery will be applied. Returning...", null, "FixAdapterSettings", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 308);
					}
					return false;
				}
				string[] array6 = text4.Split(new char[]
				{
					' '
				});
				foreach (string text6 in array6)
				{
					string processStartInfoArgument3 = string.Format("interface ip add dns \"{0}\" {1}", networkInterface.Name, text6);
					string message3 = string.Format("NetworkAdapterRecoveryResponder:: DoResponderWork(): dnsServer changed to value {0} in adapter {1}.", text6, networkInterface.Name);
					bool flag4 = NetworkAdapterRecoveryResponder.SetNetworkAdapter(processStartInfoArgument3, message3, traceContext);
					flag3 = new bool?((flag3 ?? true) && flag4);
				}
			}
			bool result = false;
			if (flag != null || flag2 != null || flag3 != null)
			{
				result = ((flag ?? true) && (flag2 ?? true) && (flag3 ?? true));
			}
			return result;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00069A44 File Offset: 0x00067C44
		private static bool SetNetworkAdapter(string processStartInfoArgument, string message, TracingContext traceContext)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo("netsh", processStartInfoArgument)
			{
				UseShellExecute = false
			};
			bool result;
			using (Process process = new Process())
			{
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.RedirectStandardError = true;
				process.StartInfo = processStartInfo;
				process.Start();
				process.WaitForExit();
				bool flag = process.ExitCode == 0;
				if (traceContext != null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.NetworkTracer, traceContext, flag ? message : string.Format("{0}\n{1}", process.StandardOutput.ReadToEnd(), process.StandardError.ReadToEnd()), null, "SetNetworkAdapter", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Network\\Responders\\NetworkAdapterRecoveryResponder.cs", 373);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x04000BDD RID: 3037
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000BDE RID: 3038
		private static readonly string TypeName = typeof(NetworkAdapterRecoveryResponder).FullName;
	}
}
