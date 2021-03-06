using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Web.Administration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000D8 RID: 216
	public class F1TraceResponder : ResponderWorkItem
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00029598 File Offset: 0x00027798
		protected static string DefaultF1ProfilerDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(F1TraceResponder.defaultF1ProfilerDirectory))
				{
					if (File.Exists(Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\\F1 Profiler 12.0\\vsperf.exe")))
					{
						F1TraceResponder.defaultF1ProfilerDirectory = Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\\F1 Profiler 12.0");
					}
					else
					{
						F1TraceResponder.defaultF1ProfilerDirectory = Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\\F1 Profiler 11.0");
					}
				}
				return F1TraceResponder.defaultF1ProfilerDirectory;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000295E8 File Offset: 0x000277E8
		protected static string DefaultVSPerfReportDirectory
		{
			get
			{
				if (string.IsNullOrEmpty(F1TraceResponder.defaultVSPerfReportDirectory))
				{
					if (File.Exists(Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\\F1 Profiler 12.0\\vsperfreport.exe")))
					{
						F1TraceResponder.defaultVSPerfReportDirectory = Environment.ExpandEnvironmentVariables("%PROGRAMFILES%\\F1 Profiler 12.0");
					}
					else
					{
						F1TraceResponder.defaultVSPerfReportDirectory = Environment.ExpandEnvironmentVariables("%PROGRAMFILES(x86)%\\F1 Profiler 11.0");
					}
				}
				return F1TraceResponder.defaultVSPerfReportDirectory;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00029637 File Offset: 0x00027837
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0002963F File Offset: 0x0002783F
		protected int NumberOfProcesses { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00029648 File Offset: 0x00027848
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00029650 File Offset: 0x00027850
		protected TimeSpan Duration { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00029659 File Offset: 0x00027859
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00029661 File Offset: 0x00027861
		protected int ProcessId { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0002966A File Offset: 0x0002786A
		internal static bool IsWindows2012
		{
			get
			{
				return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00029698 File Offset: 0x00027898
		public static string CollectF1Trace(string f1ProfilerDirectory, List<int> processIds, string f1NameRoot, TracingContext traceContext, TimeSpan duration)
		{
			if (!F1TraceResponder.CheckStatusAndExpireIfNeeded(f1ProfilerDirectory, traceContext))
			{
				string safeF1Name = F1TraceResponder.GetSafeF1Name(f1NameRoot);
				F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.CollectF1Trace: Starting collection.");
				F1TraceResponder.TraceDebug(traceContext, string.Format("F1TraceResponder.CollectF1Trace: Max collection window is {0}. Traces are allowed to run for longer and will be cut short if another request is made after the window has expired", F1TraceResponder.maxTimeLimitForCollection));
				F1TraceResponder.StartF1Trace(f1ProfilerDirectory, processIds, safeF1Name, traceContext);
				Thread.Sleep(duration);
				F1TraceResponder.ShutDownF1(f1ProfilerDirectory, traceContext);
				F1TraceResponder.PackSummaryFile(null, safeF1Name, traceContext);
				return safeF1Name;
			}
			return null;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000296FC File Offset: 0x000278FC
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, int numberOfProcesses, TimeSpan duration, int processId, string logFileDirectory, string f1ProfilerDirectory)
		{
			return F1TraceResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, numberOfProcesses, duration, processId, logFileDirectory, f1ProfilerDirectory, string.Empty);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00029728 File Offset: 0x00027928
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, int numberOfProcesses, TimeSpan duration, int processId, string logFileDirectory, string f1ProfilerDirectory, string processName)
		{
			if (targetHealthState == ServiceHealthStatus.None)
			{
				throw new ArgumentException("The responder does not support ServiceHealthStatus.None as target health state.", "targetHealthState");
			}
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = F1TraceResponder.AssemblyPath;
			responderDefinition.TypeName = F1TraceResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.Attributes["NumberOfProcesses"] = numberOfProcesses.ToString();
			responderDefinition.Attributes["Duration"] = duration.ToString();
			responderDefinition.Attributes["ProcessId"] = processId.ToString();
			responderDefinition.Attributes["LogFileDirectory"] = logFileDirectory;
			responderDefinition.Attributes["F1ProfilerDirectory"] = f1ProfilerDirectory;
			responderDefinition.Attributes["ProcessName"] = processName;
			int num;
			if (duration < TimeSpan.FromMinutes(1.0))
			{
				num = 300;
			}
			else
			{
				num = (int)duration.TotalSeconds * 2;
			}
			responderDefinition.RecurrenceIntervalSeconds = num * 2;
			responderDefinition.WaitIntervalSeconds = num;
			responderDefinition.TimeoutSeconds = num;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.F1TraceResponder.Enabled;
			return responderDefinition;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00029878 File Offset: 0x00027A78
		public static void ShutDownF1(string f1ProfilerDirectory, TracingContext traceContext)
		{
			F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Shutting down / detach f1 profiler");
			if (!F1TraceResponder.IsWindows2012)
			{
				F1TraceResponder.ExecuteF1Process(f1ProfilerDirectory, traceContext, F1TraceResponder.vsPerfCmdProcess, "/detach /shutdown");
				return;
			}
			F1TraceResponder.ExecuteF1Process(f1ProfilerDirectory, traceContext, F1TraceResponder.vsPerfProcess, "/detach");
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000298B4 File Offset: 0x00027AB4
		public static void PackSummaryFile(string vsPerfReportDirectory, string f1NameRoot, TracingContext traceContext, Hashtable processNameIdMapping)
		{
			string safeF1Name = F1TraceResponder.GetSafeF1Name(f1NameRoot);
			if (!File.Exists(safeF1Name + ".vsps"))
			{
				F1TraceResponder.PackSummaryFile(vsPerfReportDirectory, safeF1Name, traceContext);
			}
			if (processNameIdMapping != null)
			{
				foreach (object obj in processNameIdMapping)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string f1Name = dictionaryEntry.Key.ToString();
					int[] array = (int[])dictionaryEntry.Value;
					foreach (int num in array)
					{
						string options = string.Format("SummaryFile /Process:{0}", num);
						string traceSuffix = string.Format("_{0}_{1}", F1TraceResponder.GetSafeF1Name(f1Name), num);
						F1TraceResponder.ExecuteVsPerfReport(vsPerfReportDirectory, safeF1Name, options, traceContext, traceSuffix);
					}
				}
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000299A4 File Offset: 0x00027BA4
		public static void CreateCallTreeSummaryFile(string vsPerfReportDirectory, string f1NameRoot, TracingContext traceContext)
		{
			F1TraceResponder.ExecuteVsPerfReport(vsPerfReportDirectory, f1NameRoot, "Summary:CallTree", traceContext, string.Empty);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000299B8 File Offset: 0x00027BB8
		public static bool IsF1Running(string f1ProfilerDirectory)
		{
			string input = null;
			if (!F1TraceResponder.IsWindows2012)
			{
				F1TraceResponder.ExecuteProcess(f1ProfilerDirectory, F1TraceResponder.vsPerfCmdProcess, "/status", 30000, ref input, null);
			}
			else
			{
				F1TraceResponder.ExecuteProcess(f1ProfilerDirectory, F1TraceResponder.vsPerfProcess, "/status", 30000, ref input, null);
			}
			return !Regex.Match(input, "is not active").Success && !Regex.Match(input, "does not appear to be running").Success;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00029A2C File Offset: 0x00027C2C
		internal static string GetOrCreateDefaultLogFileDirectory(TracingContext traceContext)
		{
			string text = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(F1TraceResponder.diagnosticsRegistryKey))
			{
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("LogFolderPath");
					if (!string.IsNullOrEmpty(text))
					{
						text = string.Format("{0}\\Dumps", text);
						F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Using defaultLogFileDirectory from EDS");
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Could not obtain defaultLogFileDirectory from EDS. Defaulting to temp directory.");
				text = F1TraceResponder.tempF1TracesDirectory;
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (IOException ex)
				{
					F1TraceResponder.TraceDebug(traceContext, string.Format("F1TraceResponder.GetOrCreateDefaultLogFileDirectory: Hit IO Exception while creating temp directory: {0}", ex.Message));
				}
			}
			return text;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00029AE8 File Offset: 0x00027CE8
		internal static string GetSafeF1Name(string f1Name)
		{
			string text = Path.GetFileName(f1Name);
			if (!string.IsNullOrEmpty(text))
			{
				text = Regex.Replace(text, "microsoft.exchange.", "m.e.", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "microsoft.office.", "m.o.", RegexOptions.IgnoreCase);
			}
			string directoryName = Path.GetDirectoryName(f1Name);
			return Path.Combine(F1TraceResponder.GetSafeTraceRoot(directoryName, F1TraceResponder.InvalidPathChars), F1TraceResponder.GetSafeTraceRoot(text, F1TraceResponder.InvalidFileNameChars));
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00029CE4 File Offset: 0x00027EE4
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			IDataAccessQuery<ResponderResult> lastSuccessfulResponderResult = base.Broker.GetLastSuccessfulResponderResult(base.Definition);
			Task<ResponderResult> task = lastSuccessfulResponderResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(ResponderResult lastResponderResult)
			{
				DateTime startTime = SqlDateTime.MinValue.Value;
				if (lastResponderResult != null)
				{
					startTime = lastResponderResult.ExecutionStartTime;
				}
				IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = this.Broker.GetLastSuccessfulMonitorResult(this.Definition.AlertMask, startTime, this.Result.ExecutionStartTime);
				Task<MonitorResult> task2 = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, this.TraceContext);
				task2.Continue(delegate(MonitorResult lastMonitorResult)
				{
					int numberOfProcesses = 0;
					int processId = 0;
					TimeSpan zero = TimeSpan.Zero;
					int.TryParse(this.Definition.Attributes["NumberOfProcesses"], out numberOfProcesses);
					int.TryParse(this.Definition.Attributes["ProcessId"], out processId);
					TimeSpan.TryParse(this.Definition.Attributes["Duration"], out zero);
					string logFileDirectory = this.Definition.Attributes["LogFileDirectory"];
					string f1ProfilerDirectory = this.Definition.Attributes["F1ProfilerDirectory"];
					string processName = this.Definition.Attributes["ProcessName"];
					string safeF1Name = F1TraceResponder.GetSafeF1Name(this.Definition.TargetResource);
					this.GetF1Trace(safeF1Name, numberOfProcesses, processId, processName, zero, logFileDirectory, f1ProfilerDirectory, lastMonitorResult);
				}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00029D44 File Offset: 0x00027F44
		private static void PackSummaryFile(string vsPerfReportDirectory, string f1NameRoot, TracingContext traceContext)
		{
			string safeF1Name = F1TraceResponder.GetSafeF1Name(f1NameRoot);
			F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Packing summary file '{0}'", new object[]
			{
				safeF1Name
			});
			F1TraceResponder.ExecuteVsPerfReport(vsPerfReportDirectory, safeF1Name, "SummaryFile", traceContext, string.Empty);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00029D84 File Offset: 0x00027F84
		internal static void StartF1Trace(string f1ProfilerDirectory, List<int> processIds, string f1NameRoot, TracingContext traceContext)
		{
			F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Starting collection.");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("/attach:");
			stringBuilder.Append(string.Format("\"{0}\"", string.Join<int>(",", processIds.ToArray())));
			string safeF1Name = F1TraceResponder.GetSafeF1Name(f1NameRoot);
			if (!F1TraceResponder.IsWindows2012)
			{
				stringBuilder.AppendFormat(" /start:sample /CS /user:Everyone /output:\"{0}\"", safeF1Name);
				F1TraceResponder.ExecuteF1Process(f1ProfilerDirectory, traceContext, F1TraceResponder.vsPerfCmdProcess, stringBuilder.ToString());
				return;
			}
			stringBuilder.AppendFormat(" /file:{0}", safeF1Name);
			F1TraceResponder.ExecuteF1Process(f1ProfilerDirectory, traceContext, F1TraceResponder.vsPerfProcess, stringBuilder.ToString());
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00029E20 File Offset: 0x00028020
		private static bool ExecuteF1Process(string f1ProfilerDirectory, TracingContext traceContext, string processName, string args)
		{
			string text = null;
			if (F1TraceResponder.ExecuteProcess(f1ProfilerDirectory, processName, args, 600000, ref text, null))
			{
				F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Successfully ran '{0} {1}'.", new object[]
				{
					processName,
					args
				});
				return true;
			}
			F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Failed to run '{0} {1}'. Output: '{2}'", new object[]
			{
				processName,
				args,
				text
			});
			return false;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00029E80 File Offset: 0x00028080
		private static void ExecuteVsPerfReport(string vsPerfReportDirectory, string f1NameRoot, string options, TracingContext traceContext, string traceSuffix)
		{
			if (string.IsNullOrEmpty(vsPerfReportDirectory))
			{
				vsPerfReportDirectory = F1TraceResponder.DefaultVSPerfReportDirectory;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (string.IsNullOrEmpty(traceSuffix))
			{
				stringBuilder.Append("/PackSymbols ");
			}
			if (!F1TraceResponder.IsWindows2012)
			{
				stringBuilder.AppendFormat("\"{0}.vsp\"", f1NameRoot);
			}
			else
			{
				stringBuilder.AppendFormat("\"{0}.vspx\"", f1NameRoot);
			}
			if (!string.IsNullOrEmpty(options))
			{
				stringBuilder.AppendFormat(" /{0}", options);
			}
			string arg = string.Format("{0}{1}", f1NameRoot, traceSuffix);
			stringBuilder.AppendFormat(" /output:{0} /symbolpath:srv*c:\\symbols", arg);
			string environmentVariable = Environment.GetEnvironmentVariable("_NT_SYMBOL_PATH");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				stringBuilder.AppendFormat(";{0}", environmentVariable);
			}
			string text2 = stringBuilder.ToString();
			string directoryName = Path.GetDirectoryName(f1NameRoot);
			F1TraceResponder.TraceDebug(traceContext, string.Format("F1TraceResponder.ExecuteVsPerfReport: Executing {0} from {1} with command options {2}", F1TraceResponder.vsPerfReportProcess, vsPerfReportDirectory, text2));
			if (F1TraceResponder.ExecuteProcess(vsPerfReportDirectory, F1TraceResponder.vsPerfReportProcess, text2, 600000, ref text, directoryName))
			{
				if (F1TraceResponder.IsWindows2012 && string.IsNullOrEmpty(traceSuffix))
				{
					F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Checking for missing symbols ngen binaries.");
					if (F1TraceResponder.CreateNgenSymbols(text, traceContext))
					{
						if (F1TraceResponder.ExecuteProcess(vsPerfReportDirectory, F1TraceResponder.vsPerfReportProcess, text2, 3000000, ref text, directoryName))
						{
							F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Successfully re-generated summary file after creating ngen'd symbols.");
							return;
						}
						F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Failed to generate summary file. Output: {0}", new object[]
						{
							text
						});
						return;
					}
				}
			}
			else
			{
				F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Failed to generate summary file. Output: {0}", new object[]
				{
					text
				});
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00029FEC File Offset: 0x000281EC
		private static bool CreateNgenSymbols(string output, TracingContext traceContext)
		{
			bool result = false;
			string[] array = output.Split(new string[]
			{
				"\n",
				"\r\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string pattern = "Failed to load symbols for (.+?\\.ni(\\.dll|\\.exe))";
			foreach (string input in array)
			{
				Match match = Regex.Match(input, pattern);
				if (match.Success)
				{
					string directory = string.Format("{0}\\Microsoft.NET\\Framework64\\v4.0.30319", Environment.GetEnvironmentVariable("windir"));
					string arguments = string.Format("createpdb {0} c:\\symbols", match.Groups[1]);
					if (F1TraceResponder.ExecuteProcess(directory, "ngen.exe", arguments, 3000000, ref output, null))
					{
						result = true;
						F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Successfully created ngen symbols for {0}.", new object[]
						{
							match.Groups[1]
						});
					}
					else
					{
						F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.DoResponderWork: Failed to create ngen symbols for {0}. Output: {1}", new object[]
						{
							match.Groups[1],
							output
						});
					}
				}
			}
			return result;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0002A0FC File Offset: 0x000282FC
		private static bool ExecuteProcess(string directory, string processName, string arguments, int processWaitTime, ref string output, string temp = null)
		{
			if (string.IsNullOrEmpty(directory))
			{
				directory = F1TraceResponder.DefaultF1ProfilerDirectory;
			}
			F1TraceResponder.processOutput = new StringBuilder();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.FileName = string.Format("{0}\\{1}", directory, processName);
			processStartInfo.Arguments = arguments;
			if (!string.IsNullOrEmpty(temp))
			{
				processStartInfo.EnvironmentVariables["TMP"] = temp;
				processStartInfo.EnvironmentVariables["TEMP"] = temp;
			}
			bool result;
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo = processStartInfo;
					process.OutputDataReceived += F1TraceResponder.ProcessOutputHandler;
					process.ErrorDataReceived += F1TraceResponder.ProcessOutputHandler;
					process.Start();
					process.BeginOutputReadLine();
					process.BeginErrorReadLine();
					process.WaitForExit(processWaitTime);
					output = F1TraceResponder.processOutput.ToString();
					if (process.HasExited)
					{
						result = (process.ExitCode == 0);
					}
					else
					{
						result = false;
					}
				}
			}
			catch (FileNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0002A228 File Offset: 0x00028428
		private static void ProcessOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			if (!string.IsNullOrEmpty(outLine.Data))
			{
				F1TraceResponder.processOutput.AppendLine(outLine.Data);
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0002A248 File Offset: 0x00028448
		private static void TraceDebug(TracingContext traceContext, string info)
		{
			F1TraceResponder.TraceDebug(traceContext, info, null);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0002A254 File Offset: 0x00028454
		private static void TraceDebug(TracingContext traceContext, string formatString, params object[] args)
		{
			string text = string.Empty;
			if (args != null)
			{
				text = string.Format(formatString, args);
			}
			else
			{
				text = formatString;
			}
			if (traceContext != null)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, traceContext, text, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\F1TraceResponder.cs", 850);
				return;
			}
			Console.WriteLine(text);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0002A29C File Offset: 0x0002849C
		private static bool CheckStatusAndExpireIfNeeded(string f1ProfilerDirectory, TracingContext traceContext)
		{
			if (!F1TraceResponder.IsF1Running(f1ProfilerDirectory))
			{
				return false;
			}
			F1TraceResponder.TraceDebug(traceContext, "F1TraceResponder.CheckStatusAndExpireIfNeeded: An instance of the collector is running.");
			string text = F1TraceResponder.IsWindows2012 ? Path.GetFileNameWithoutExtension(F1TraceResponder.vsPerfProcess) : Path.GetFileNameWithoutExtension("VsPerfSrv.exe");
			Process[] processesByName = Process.GetProcessesByName(text);
			DateTime utcNow = DateTime.UtcNow;
			foreach (Process process in processesByName)
			{
				try
				{
					TimeSpan timeSpan = utcNow - process.StartTime.ToUniversalTime();
					F1TraceResponder.TraceDebug(traceContext, string.Format("F1TraceResponder.CheckStatusAndExpireIfNeeded: Found collector process {0} with start time {1} which expires in {2}.", process.ProcessName, process.StartTime, F1TraceResponder.maxTimeLimitForCollection - timeSpan));
					if (timeSpan > F1TraceResponder.maxTimeLimitForCollection)
					{
						F1TraceResponder.TraceDebug(traceContext, string.Format("A long running collector was found. Detach will be called for {0}", text));
						F1TraceResponder.ShutDownF1(f1ProfilerDirectory, traceContext);
						break;
					}
				}
				catch (Exception arg)
				{
					F1TraceResponder.TraceDebug(traceContext, string.Format("F1TraceResponder.CheckStatusAndExpireIfNeeded: Failed to handle clearing of {0} instance due to exception {1}.", text, arg));
				}
				finally
				{
					process.Dispose();
				}
			}
			return F1TraceResponder.IsF1Running(f1ProfilerDirectory);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0002A3C0 File Offset: 0x000285C0
		private void TraceDebug(string info)
		{
			F1TraceResponder.TraceDebug(base.TraceContext, info);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0002A3CE File Offset: 0x000285CE
		private void TraceDebug(string formatString, params object[] args)
		{
			F1TraceResponder.TraceDebug(base.TraceContext, formatString, args);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0002A3E0 File Offset: 0x000285E0
		private Dictionary<int, TimeSpan> GetTotalProcessorTimeDelta(TimeSpan intervalMilliseconds, params Process[] processes)
		{
			Dictionary<int, TimeSpan> dictionary = new Dictionary<int, TimeSpan>();
			for (int i = 0; i < 2; i++)
			{
				foreach (Process process in processes)
				{
					try
					{
						if (process.Id != 0)
						{
							if (!dictionary.ContainsKey(process.Id))
							{
								dictionary.Add(process.Id, process.TotalProcessorTime);
							}
							else
							{
								dictionary[process.Id] = process.TotalProcessorTime - dictionary[process.Id];
							}
						}
					}
					catch (InvalidOperationException ex)
					{
						this.TraceDebug("F1TraceResponder.GetAverageProcessorTime: InvalidOperationException :{0}", new object[]
						{
							ex.Message
						});
					}
					catch (Win32Exception ex2)
					{
						this.TraceDebug("F1TraceResponder.GetAverageProcessorTime: Win32Exception :{0}", new object[]
						{
							ex2.Message
						});
					}
				}
				Thread.Sleep(intervalMilliseconds);
			}
			return dictionary;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0002A514 File Offset: 0x00028714
		private void GetF1Trace(string traceName, int numberOfProcesses, int processId, string processName, TimeSpan duration, string logFileDirectory, string f1ProfilerDirectory, MonitorResult monitorResult)
		{
			this.TraceDebug("F1TraceResponder.DoResponderWork: Started GetF1Trace execution.");
			if (!string.IsNullOrWhiteSpace(processName))
			{
				Process[] processesByName = Process.GetProcessesByName(processName);
				int num = 0;
				if (processesByName != null && processesByName.Length > 0)
				{
					Dictionary<int, TimeSpan> processTimes = this.GetTotalProcessorTimeDelta(TimeSpan.FromMilliseconds(30000.0), processesByName);
					IEnumerable<int> source = (from processIdKey in processTimes.Keys
					orderby processTimes[processIdKey] descending
					select processIdKey).Take(1);
					num = source.First<int>();
				}
				else if (processName.StartsWith("NodeRunner", StringComparison.OrdinalIgnoreCase))
				{
					num = this.GetProcessIdFromNodeRunnerProcessName(processName);
				}
				else
				{
					using (ServerManager serverManager = new ServerManager())
					{
						ApplicationPoolCollection applicationPools = serverManager.ApplicationPools;
						Dictionary<int, TimeSpan> processTimes;
						foreach (ApplicationPool applicationPool in applicationPools)
						{
							if (string.Compare(applicationPool.Name, processName, true) == 0)
							{
								WorkerProcessCollection workerProcesses = applicationPool.WorkerProcesses;
								List<Process> list = new List<Process>();
								foreach (WorkerProcess workerProcess in workerProcesses)
								{
									if (workerProcess.State == 1)
									{
										Process processById = Process.GetProcessById(workerProcess.ProcessId);
										if (processById != null)
										{
											list.Add(processById);
										}
									}
								}
								processTimes = this.GetTotalProcessorTimeDelta(TimeSpan.FromMilliseconds(30000.0), list.ToArray<Process>());
								IEnumerable<int> source2 = (from processIdKey in processTimes.Keys
								orderby processTimes[processIdKey] descending
								select processIdKey).Take(1);
								num = source2.First<int>();
							}
						}
					}
				}
				processId = num;
			}
			if (numberOfProcesses == 0 && processId == 0)
			{
				this.TraceDebug("F1TraceResponder.DoResponderWork: Both ProcessId and NumberOfProcesses parameters are 0. Skipping F1Trace.");
				return;
			}
			if (duration <= TimeSpan.Zero)
			{
				this.TraceDebug("F1TraceResponder.DoResponderWork: F1Trace duration cannot be zero Defaulting to 1 minute.");
				duration = TimeSpan.FromSeconds(60.0);
			}
			List<int> list2 = new List<int>();
			if (processId == 0)
			{
				this.TraceDebug("F1TraceResponder.DoResponderWork: No ProcessId provided. Finding top {0} processes", new object[]
				{
					numberOfProcesses
				});
				Process[] processes = Process.GetProcesses();
				Dictionary<int, TimeSpan> processTimes = this.GetTotalProcessorTimeDelta(TimeSpan.FromMilliseconds(30000.0), processes);
				IEnumerable<int> collection = (from processIdKey in processTimes.Keys
				orderby processTimes[processIdKey] descending
				select processIdKey).Take(numberOfProcesses);
				list2.AddRange(collection);
			}
			else
			{
				list2.Add(processId);
			}
			this.TraceDebug("F1TraceResponder.DoResponderWork: F1Trace Monitor is obtained.");
			if (string.IsNullOrEmpty(logFileDirectory))
			{
				logFileDirectory = F1TraceResponder.GetOrCreateDefaultLogFileDirectory(base.TraceContext);
			}
			this.TraceDebug("F1TraceResponder.DoResponderWork: LogFileDirectory is {0}.", new object[]
			{
				logFileDirectory
			});
			if (traceName.Contains("/"))
			{
				int num2 = traceName.LastIndexOf("/");
				traceName = traceName.Substring(num2 + 1);
			}
			string arg = string.Format("{0}-{1}-{2}", Environment.MachineName, traceName, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
			string safeF1Name = F1TraceResponder.GetSafeF1Name(string.Format("{0}\\{1}", logFileDirectory, arg));
			this.TraceDebug("F1TraceResponder.DoResponderWork: F1Trace file location: {0}.", new object[]
			{
				safeF1Name
			});
			F1TraceResponder.CollectF1Trace(f1ProfilerDirectory, list2, safeF1Name, base.TraceContext, duration);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		private int GetProcessIdFromNodeRunnerProcessName(string nodeRunnerProcessName)
		{
			int result = 0;
			string[] array = nodeRunnerProcessName.Split(new char[]
			{
				'#'
			});
			if (array.Length == 2)
			{
				string key = array[1];
				Dictionary<string, int> nodeProcessIds = SearchMonitoringHelper.GetNodeProcessIds();
				nodeProcessIds.TryGetValue(key, out result);
			}
			return result;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002A90C File Offset: 0x00028B0C
		private static string GetSafeTraceRoot(string traceRoot, char[] invalidChars)
		{
			if (traceRoot == null)
			{
				throw new ArgumentException("Trace root cannot be null");
			}
			foreach (char oldChar in invalidChars)
			{
				traceRoot = traceRoot.Replace(oldChar, '_');
			}
			return traceRoot;
		}

		// Token: 0x04000492 RID: 1170
		private const int ProcessorTimeDeltaIntervalMilliSeconds = 30000;

		// Token: 0x04000493 RID: 1171
		private const int DefaultF1TraceOperationWaitingTime = 30000;

		// Token: 0x04000494 RID: 1172
		private const string VsPerfSrvProcess = "VsPerfSrv.exe";

		// Token: 0x04000495 RID: 1173
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000496 RID: 1174
		private static readonly string TypeName = typeof(F1TraceResponder).FullName;

		// Token: 0x04000497 RID: 1175
		private static readonly char[] InvalidF1NameChars = new char[]
		{
			'.'
		};

		// Token: 0x04000498 RID: 1176
		private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars().Concat(F1TraceResponder.InvalidF1NameChars).ToArray<char>();

		// Token: 0x04000499 RID: 1177
		private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars().Concat(F1TraceResponder.InvalidF1NameChars).ToArray<char>();

		// Token: 0x0400049A RID: 1178
		private static readonly TimeSpan maxTimeLimitForCollection = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400049B RID: 1179
		private static string defaultF1ProfilerDirectory = string.Empty;

		// Token: 0x0400049C RID: 1180
		private static string defaultVSPerfReportDirectory = string.Empty;

		// Token: 0x0400049D RID: 1181
		private static string vsPerfCmdProcess = "VsPerfCmd.exe";

		// Token: 0x0400049E RID: 1182
		private static string vsPerfProcess = "VsPerf.exe";

		// Token: 0x0400049F RID: 1183
		private static string vsPerfReportProcess = "VSPerfReport.exe";

		// Token: 0x040004A0 RID: 1184
		private static string diagnosticsRegistryKey = string.Format("Software\\Microsoft\\ExchangeServer\\{0}\\Diagnostics", "v15");

		// Token: 0x040004A1 RID: 1185
		private static string tempF1TracesDirectory = Environment.ExpandEnvironmentVariables("%TEMP%\\F1Traces");

		// Token: 0x040004A2 RID: 1186
		private static StringBuilder processOutput;

		// Token: 0x020000D9 RID: 217
		internal static class AttributeNames
		{
			// Token: 0x040004A6 RID: 1190
			internal const string NumberOfProcesses = "NumberOfProcesses";

			// Token: 0x040004A7 RID: 1191
			internal const string Duration = "Duration";

			// Token: 0x040004A8 RID: 1192
			internal const string ProcessId = "ProcessId";

			// Token: 0x040004A9 RID: 1193
			internal const string LogFileDirectory = "LogFileDirectory";

			// Token: 0x040004AA RID: 1194
			internal const string F1ProfilerDirectory = "F1ProfilerDirectory";

			// Token: 0x040004AB RID: 1195
			internal const string ProcessName = "ProcessName";
		}
	}
}
