using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000FB RID: 251
	public class TraceLogResponder : ResponderWorkItem
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x0002DC4C File Offset: 0x0002BE4C
		public static ResponderDefinition CreateDefinition(TraceLogResponder.TraceAttributes traceAttributes)
		{
			string text = string.Empty;
			string value = string.Empty;
			if ((!string.IsNullOrEmpty(traceAttributes.ProviderGuid) || !string.IsNullOrEmpty(traceAttributes.ProviderGuidFile)) && !string.IsNullOrEmpty(traceAttributes.KernelLoggerFlags))
			{
				throw new ArgumentException("Cannot specify both guids and kernel logger flags at the same time.");
			}
			if (!string.IsNullOrEmpty(traceAttributes.ProviderGuidFile) && !File.Exists(traceAttributes.ProviderGuidFile))
			{
				throw new ArgumentException(string.Format("The guid file specified does not exist : {0}", traceAttributes.ProviderGuid));
			}
			if (!string.IsNullOrEmpty(traceAttributes.ProviderGuid))
			{
				value = traceAttributes.ProviderGuid.Replace(",", Environment.NewLine);
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(TraceLogResponder.DiagnosticsRegistryKey))
			{
				if (registryKey != null)
				{
					if (string.IsNullOrEmpty(traceAttributes.LogFileDirectory) && string.IsNullOrEmpty(TraceLogResponder.edsDumpDirectory))
					{
						text = (string)registryKey.GetValue("LogFolderPath");
						if (!string.IsNullOrEmpty(text))
						{
							TraceLogResponder.edsDumpDirectory = string.Format("{0}\\Dumps", text);
						}
						else
						{
							TraceLogResponder.edsDumpDirectory = Environment.ExpandEnvironmentVariables("%systemdrive%\\Dumps");
						}
					}
					text = TraceLogResponder.edsDumpDirectory;
					string text2 = (string)registryKey.GetValue("MsiInstallPath");
					if (!string.IsNullOrEmpty(text2))
					{
						TraceLogResponder.traceLogFilePathName = string.Format("{0}\\tracelog.exe", text2);
						TraceLogResponder.etwFilePathName = string.Format("{0}\\etw.exe", text2);
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("Cannot find EDS dump directory, logFileDirectory needs to be specified.");
			}
			if (traceAttributes.DurationInSeconds <= 0)
			{
				throw new ArgumentException("Trace log duration must be bigger than zero.");
			}
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = TraceLogResponder.AssemblyPath;
			responderDefinition.TypeName = TraceLogResponder.TypeName;
			responderDefinition.Name = traceAttributes.Name;
			responderDefinition.ServiceName = traceAttributes.ServiceName;
			responderDefinition.AlertTypeId = traceAttributes.AlertTypeId;
			responderDefinition.AlertMask = traceAttributes.AlertMask;
			responderDefinition.TargetResource = traceAttributes.TargetResource;
			responderDefinition.TargetHealthState = traceAttributes.TargetHealthState;
			responderDefinition.Enabled = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.TraceLogResponder.Enabled;
			responderDefinition.Attributes["ProviderGuid"] = value;
			responderDefinition.Attributes["ProviderGuidFile"] = traceAttributes.ProviderGuidFile;
			responderDefinition.Attributes["LogFileDirectory"] = text;
			responderDefinition.Attributes["DurationInSeconds"] = traceAttributes.DurationInSeconds.ToString();
			responderDefinition.Attributes["KernelLoggerFlags"] = traceAttributes.KernelLoggerFlags;
			responderDefinition.Attributes["SampleMask"] = traceAttributes.SampleMask;
			responderDefinition.Attributes["false"] = traceAttributes.ShouldAppendAdditionalMessage.ToString();
			int num;
			if (traceAttributes.DurationInSeconds < 60)
			{
				num = 300;
			}
			else
			{
				num = traceAttributes.DurationInSeconds * 2;
			}
			responderDefinition.RecurrenceIntervalSeconds = num * 2;
			responderDefinition.WaitIntervalSeconds = num;
			responderDefinition.TimeoutSeconds = num;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = true;
			return responderDefinition;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0002E030 File Offset: 0x0002C230
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			IDataAccessQuery<ResponderResult> lastSuccessfulResponderResult = base.Broker.GetLastSuccessfulResponderResult(base.Definition);
			Task<ResponderResult> task = lastSuccessfulResponderResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(ResponderResult lastResponderResult)
			{
				DateTime lastExecutionTime = SqlDateTime.MinValue.Value;
				if (lastResponderResult != null)
				{
					lastExecutionTime = lastResponderResult.ExecutionStartTime;
				}
				IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = this.Broker.GetLastSuccessfulMonitorResult(this.Definition.AlertMask, lastExecutionTime, this.Result.ExecutionStartTime);
				Task<MonitorResult> task2 = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, this.TraceContext);
				task2.Continue(delegate(MonitorResult lastMonitorResult)
				{
					this.CollectTrace(lastExecutionTime, lastMonitorResult);
				}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002E090 File Offset: 0x0002C290
		private static string CreateGuidFile(string providerGuid, string outputFilePath, string guidFileName)
		{
			TraceLogResponder traceLogResponder = new TraceLogResponder();
			string text = Path.Combine(outputFilePath, guidFileName);
			traceLogResponder.OutputToFile(providerGuid, text);
			if (File.Exists(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002E0C0 File Offset: 0x0002C2C0
		private string GetAdEmailData(string data)
		{
			string[] array = Regex.Split(data, "\r\n");
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i <= array.Length - 1; i++)
			{
				if (array[i].Equals("Filter statistics", StringComparison.OrdinalIgnoreCase))
				{
					if (num == 0)
					{
						stringBuilder.AppendLine("Filter Statistics");
						this.ParseEmailDataAndZipAdDriverClientLogs(array, "statistics", 10, i, stringBuilder);
					}
					num++;
				}
				else if (array[i].Equals("MergeCLIENT statistics", StringComparison.OrdinalIgnoreCase))
				{
					if (num2 == 0)
					{
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("Merged Client Statistics");
						this.ParseEmailDataAndZipAdDriverClientLogs(array, "statistics", 10, i, stringBuilder);
					}
					num2++;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002E174 File Offset: 0x0002C374
		private void ParseEmailDataAndZipAdDriverClientLogs(string[] data, string terminatingString, int maxEntries, int currentEntry, StringBuilder output)
		{
			int num = 0;
			IPAddress ipaddress = null;
			for (int i = currentEntry + 1; i < data.Length; i++)
			{
				string text = data[i];
				if (text.Contains(terminatingString))
				{
					return;
				}
				if (text.StartsWith("COUNT", StringComparison.OrdinalIgnoreCase))
				{
					string ipString = data[i].Split(new char[]
					{
						' '
					})[1];
					if (num == 1)
					{
						string value = string.Format("Top {0} statistics", maxEntries);
						output.AppendLine(value);
					}
					if (ipaddress == null && IPAddress.TryParse(ipString, out ipaddress))
					{
						string text2 = this.ResolveIpAddressToHostname(ipaddress);
						if (!string.IsNullOrEmpty(text2))
						{
							this.FindAndZipFiles(text2);
						}
					}
					num++;
				}
				if (num == maxEntries + 2)
				{
					return;
				}
				output.AppendLine(text);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002E268 File Offset: 0x0002C468
		private void FindAndZipFiles(string clientName)
		{
			DateTime fromTime = DateTime.UtcNow.AddHours(-1.0);
			string path = string.Format("{0}-{1}.zip", clientName, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
			string archiveFileName = Path.Combine(TraceLogResponder.edsDumpDirectory, path);
			string path2 = string.Format("\\\\{0}\\Exchange\\Logging\\ADDriver", clientName);
			if (Directory.Exists(path2))
			{
				try
				{
					IEnumerable<string> enumerable = from f in Directory.GetFiles(path2, "*.LOG")
					orderby File.GetLastWriteTimeUtc(f) descending
					where File.GetLastWriteTimeUtc(f) >= fromTime && File.GetLastWriteTimeUtc(f) <= DateTime.UtcNow
					select f;
					foreach (string text in enumerable)
					{
						using (ZipArchive zipArchive = ZipFile.Open(archiveFileName, ZipArchiveMode.Update))
						{
							string fileName = Path.GetFileName(text);
							try
							{
								zipArchive.CreateEntryFromFile(text, fileName);
							}
							catch (Exception ex)
							{
								WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format(ex.Message, new object[0]), null, "FindAndZipFiles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 424);
							}
						}
					}
				}
				catch (Exception ex2)
				{
					WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format(ex2.Message, new object[0]), null, "FindAndZipFiles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 434);
				}
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002E424 File Offset: 0x0002C624
		private string ResolveIpAddressToHostname(IPAddress ipAddress)
		{
			string result = string.Empty;
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
				result = hostEntry.HostName.ToString();
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format(ex.Message, new object[0]), null, "ResolveIpAddressToHostname", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 458);
			}
			return result;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002E494 File Offset: 0x0002C694
		private void CollectTrace(DateTime lastExecutionTime, MonitorResult monitorResult)
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			int num = attributeHelper.GetInt("DurationInSeconds", true, 0, null, null);
			string @string = attributeHelper.GetString("LogFileDirectory", true, null);
			string string2 = attributeHelper.GetString("KernelLoggerFlags", false, null);
			string string3 = attributeHelper.GetString("ProviderGuid", false, null);
			string text = attributeHelper.GetString("ProviderGuidFile", false, string.Empty);
			string name = base.Definition.Name;
			string string4 = attributeHelper.GetString("SampleMask", false, string.Empty);
			IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(string4, lastExecutionTime, base.Result.ExecutionStartTime);
			ProbeResult probeResult = probeResults.FirstOrDefault<ProbeResult>();
			string string5 = attributeHelper.GetString("false", false, string.Empty);
			bool shouldAppendAdditionalMessage;
			bool.TryParse(string5, out shouldAppendAdditionalMessage);
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "TraceLogResponder.DoResponderWork: Started CollectTrace.", null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 493);
			string path = string.Format("{0}-{1}-{2}.etl", Environment.MachineName, name, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
			string text2 = Path.Combine(@string, path);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "TraceLogResponder.DoResponderWork: logFileNamePath is {0}.", text2, null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 501);
			if (num > 900)
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("TraceLogResponder.DoResponderWork: Maximum time limit is {0} secs.", 900.ToString()), null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 510);
				num = 900;
			}
			this.RunTraceLog(string.Format("-stop \"{0}\"", name), 30000);
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			if (string.IsNullOrEmpty(text))
			{
				string guidFileName = string.Format("{0}-{1}.guid", name, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
				text = TraceLogResponder.CreateGuidFile(string3, @string, guidFileName);
			}
			string arguments = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				arguments = string.Format("-start \"{0}\" -guid \"{1}\" -f \"{2}\" -seq {3} -min {4} -max {5}", new object[]
				{
					name,
					text,
					text2,
					500.ToString(),
					2.ToString(),
					200.ToString()
				});
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("-start \"{0}\" -f \"{1}\" -seq {2} -min {3} -max {4}", new object[]
				{
					name,
					text2,
					500.ToString(),
					2.ToString(),
					200.ToString()
				}));
				if (!string.IsNullOrEmpty(string2))
				{
					stringBuilder.Append(string2);
				}
				arguments = stringBuilder.ToString();
			}
			if (!this.RunTraceLog(arguments, 30000))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("TraceLogResponder.DoResponderWork: Failed to start trace {0}, skip collecting traces.", name), null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 556);
				this.RunTraceLog(string.Format("-stop \"{0}\"", name), 30000);
				return;
			}
			Thread.Sleep(num * 1000);
			if (!this.RunTraceLog(string.Format("-stop \"{0}\"", name), 30000))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("TraceLogResponder.DoResponderWork: Cannot stop trace {0}, skip collecting traces.", name), null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 572);
				this.RunTraceLog(string.Format("-stop \"{0}\"", name), 30000);
				return;
			}
			try
			{
				string text3 = File.ReadAllText(text);
				if (text3.Contains("1C83B2FC-C04F-11D1-8AFC-00C04FC21914") && !this.AnalyzeAdTraceLog(name, text2, @string, 60000, shouldAppendAdditionalMessage, probeResult.Error))
				{
					WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("TraceLogResponder.DoResponderWork: Cannot analyze trace {0}", name), null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 590);
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, string.Format("TraceLogResponder.DoResponderWork: Cannot analyze trace {0}", name, ex.Message), null, "CollectTrace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 600);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
		private bool RunTraceLog(string arguments, int timeoutInMilliSeconds)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.FileName = TraceLogResponder.traceLogFilePathName;
			processStartInfo.Arguments = arguments;
			bool result;
			try
			{
				using (Process process = Process.Start(processStartInfo))
				{
					process.WaitForExit(timeoutInMilliSeconds);
					result = (process.ExitCode == 0);
				}
			}
			catch (FileNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0002E964 File Offset: 0x0002CB64
		private bool AnalyzeAdTraceLog(string traceName, string traceFileNamePath, string outputFileLocation, int timeoutInMilliSeconds, bool shouldAppendAdditionalMessage, string probeResult)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.CreateNoWindow = false;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.FileName = TraceLogResponder.etwFilePathName;
			string path = string.Format("{0}-{1}-{2}-ADDiagParsed.txt", Environment.MachineName, traceName, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
			string text = Path.Combine(outputFileLocation, path);
			processStartInfo.Arguments = traceFileNamePath;
			if (this.ShouldRunEtw(traceFileNamePath))
			{
				try
				{
					using (Process process = Process.Start(processStartInfo))
					{
						string message = string.Format("TraceLogResponder.DoResponderWork: Parsing trace file:{0}, and sending output to:{1}", traceFileNamePath, text);
						WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, message, null, "AnalyzeAdTraceLog", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 675);
						string data = process.StandardOutput.ReadToEnd();
						this.OutputToFile(data, text);
						if (shouldAppendAdditionalMessage)
						{
							string adEmailData = this.GetAdEmailData(data);
							string text2 = probeResult + Environment.NewLine + Environment.NewLine + adEmailData;
							string path2 = string.Format("{0}-{1}-{2}-EmailMessage.txt", DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"), Environment.MachineName, traceName);
							string outputFileNamePath = Path.Combine(outputFileLocation, path2);
							this.OutputToFile(text2, outputFileNamePath);
							lock (EscalateResponderHelper.CustomMessageDictionaryLock)
							{
								EscalateResponderHelper.AdditionalMessageContainer value = new EscalateResponderHelper.AdditionalMessageContainer(DateTime.UtcNow, text2);
								EscalateResponderHelper.AlertMaskToCustomMessageMap[base.Definition.AlertMask] = value;
							}
						}
						process.WaitForExit(timeoutInMilliSeconds);
						return process.ExitCode == 0;
					}
				}
				catch (FileNotFoundException)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002EB40 File Offset: 0x0002CD40
		private void OutputToFile(string data, string outputFileNamePath)
		{
			try
			{
				File.WriteAllText(outputFileNamePath, data);
			}
			catch (Exception ex)
			{
				string message = string.Format("TraceLogResponder.DoResponderWork: Writing trace parse output failed:{0}, due to {1}", outputFileNamePath, ex.Message);
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, message, null, "OutputToFile", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 736);
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002EB9C File Offset: 0x0002CD9C
		private bool ShouldRunEtw(string traceFileNamePath)
		{
			for (int i = 0; i < 5; i++)
			{
				try
				{
					using (FileStream fileStream = new FileStream(traceFileNamePath, FileMode.Open, FileAccess.Read))
					{
						fileStream.ReadByte();
						return true;
					}
				}
				catch (Exception ex)
				{
					string message = string.Format("TraceLogResponder.DoResponderWork: Cannot open trace file:{0}, due to {1}, retrying...", traceFileNamePath, ex.Message);
					WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, message, null, "ShouldRunEtw", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\TraceLogResponder.cs", 769);
				}
				Thread.Sleep(2000);
			}
			return false;
		}

		// Token: 0x0400052F RID: 1327
		private const int MaximumFileSize = 500;

		// Token: 0x04000530 RID: 1328
		private const int MaximumBufferCount = 200;

		// Token: 0x04000531 RID: 1329
		private const int MinimumBufferCount = 2;

		// Token: 0x04000532 RID: 1330
		private const int MaximumDurationInSeconds = 900;

		// Token: 0x04000533 RID: 1331
		private const string ADDiagTraceGuid = "1C83B2FC-C04F-11D1-8AFC-00C04FC21914";

		// Token: 0x04000534 RID: 1332
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000535 RID: 1333
		private static readonly string DiagnosticsRegistryKey = string.Format("Software\\Microsoft\\ExchangeServer\\{0}\\Diagnostics", "v15");

		// Token: 0x04000536 RID: 1334
		private static readonly string TypeName = typeof(TraceLogResponder).FullName;

		// Token: 0x04000537 RID: 1335
		private static string edsDumpDirectory;

		// Token: 0x04000538 RID: 1336
		private static string traceLogFilePathName;

		// Token: 0x04000539 RID: 1337
		private static string etwFilePathName;

		// Token: 0x020000FC RID: 252
		public class TraceAttributes
		{
			// Token: 0x060007C0 RID: 1984 RVA: 0x0002EC78 File Offset: 0x0002CE78
			public TraceAttributes(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string providerGuid, string providerGuidFile, string logFileDirectory, int durationInSeconds, string kernelLoggerFlags, string sampleMask, bool shouldAppendAdditionalMessage)
			{
				this.Name = name;
				this.ServiceName = serviceName;
				this.AlertTypeId = alertTypeId;
				this.AlertMask = alertMask;
				this.TargetHealthState = targetHealthState;
				this.ProviderGuid = providerGuid;
				this.ProviderGuidFile = providerGuidFile;
				this.LogFileDirectory = logFileDirectory;
				this.DurationInSeconds = durationInSeconds;
				this.KernelLoggerFlags = kernelLoggerFlags;
				this.SampleMask = sampleMask;
				this.ShouldAppendAdditionalMessage = shouldAppendAdditionalMessage;
			}

			// Token: 0x0400053B RID: 1339
			public readonly string Name;

			// Token: 0x0400053C RID: 1340
			public readonly string ServiceName;

			// Token: 0x0400053D RID: 1341
			public readonly string AlertTypeId;

			// Token: 0x0400053E RID: 1342
			public readonly string AlertMask;

			// Token: 0x0400053F RID: 1343
			public readonly string TargetResource;

			// Token: 0x04000540 RID: 1344
			public readonly ServiceHealthStatus TargetHealthState;

			// Token: 0x04000541 RID: 1345
			public readonly string ProviderGuid;

			// Token: 0x04000542 RID: 1346
			public readonly string ProviderGuidFile;

			// Token: 0x04000543 RID: 1347
			public readonly string LogFileDirectory;

			// Token: 0x04000544 RID: 1348
			public readonly int DurationInSeconds;

			// Token: 0x04000545 RID: 1349
			public readonly string KernelLoggerFlags;

			// Token: 0x04000546 RID: 1350
			public readonly string SampleMask;

			// Token: 0x04000547 RID: 1351
			public readonly bool ShouldAppendAdditionalMessage;
		}

		// Token: 0x020000FD RID: 253
		internal static class AttributeNames
		{
			// Token: 0x04000548 RID: 1352
			internal const string ProviderGuidFile = "ProviderGuidFile";

			// Token: 0x04000549 RID: 1353
			internal const string ProviderGuid = "ProviderGuid";

			// Token: 0x0400054A RID: 1354
			internal const string KernelLoggerFlags = "KernelLoggerFlags";

			// Token: 0x0400054B RID: 1355
			internal const string LogFileDirectory = "LogFileDirectory";

			// Token: 0x0400054C RID: 1356
			internal const string DurationInSeconds = "DurationInSeconds";

			// Token: 0x0400054D RID: 1357
			internal const string SampleMask = "SampleMask";

			// Token: 0x0400054E RID: 1358
			internal const string ShouldAppendAdditionalMessage = "false";
		}
	}
}
