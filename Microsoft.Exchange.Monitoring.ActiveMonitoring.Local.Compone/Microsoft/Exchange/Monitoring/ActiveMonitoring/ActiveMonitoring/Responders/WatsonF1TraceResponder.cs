using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000EB RID: 235
	public class WatsonF1TraceResponder : WatsonResponder
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x0002C204 File Offset: 0x0002A404
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string exceptionType, ReportOptions reportOptions, string logFileDirectory, string f1ProfilerDirectory, TimeSpan duration)
		{
			ResponderDefinition responderDefinition = WatsonResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, exceptionType, "E12IIS", reportOptions);
			responderDefinition.TypeName = WatsonF1TraceResponder.TypeName;
			responderDefinition.AssemblyPath = WatsonF1TraceResponder.AssemblyPath;
			responderDefinition.Attributes["LogFileDirectory"] = logFileDirectory;
			responderDefinition.Attributes["F1ProfilerDirectory"] = f1ProfilerDirectory;
			responderDefinition.Attributes["Duration"] = duration.ToString();
			responderDefinition.Attributes["AllowDefaultCallStack"] = false.ToString();
			responderDefinition.RecurrenceIntervalSeconds = (int)duration.TotalSeconds * 2;
			return responderDefinition;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		internal static string GetHotPath(string callstackPath, TracingContext traceContext)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = -1;
			try
			{
				foreach (string text in File.ReadLines(callstackPath).Skip(1))
				{
					string text2 = WatsonF1TraceResponder.ExtractFunctionName(text);
					if (string.IsNullOrEmpty(text))
					{
						break;
					}
					int num2 = WatsonF1TraceResponder.ExtractLevel(text.Replace(text2, string.Empty));
					if (num2 <= num)
					{
						break;
					}
					stringBuilder.Append("    at " + text2 + Environment.NewLine);
					num = num2;
				}
			}
			catch (IOException ex)
			{
				WatsonResponder.TraceDebug(traceContext, string.Format("WatsonF1TraceResponder.GetHotPath: Failed - hit IO Exception {0}", ex.Message));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0002C378 File Offset: 0x0002A578
		internal static string ExtractFunctionName(string line)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(line))
			{
				string[] array = line.Split(new char[]
				{
					'"'
				});
				if (array.Length > 2 && string.IsNullOrEmpty(array[0]))
				{
					result = array[1];
				}
			}
			return result;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002C3BC File Offset: 0x0002A5BC
		internal static int ExtractLevel(string lineNoFunction)
		{
			int result = -1;
			string[] array = lineNoFunction.Split(new char[]
			{
				','
			});
			if (array.Length == 14)
			{
				int.TryParse(array[10], out result);
			}
			return result;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
		internal override string CollectCallstack(Process process)
		{
			string text = WatsonResponder.DefaultCallStack;
			if (process == null)
			{
				throw new ArgumentException("Parameter may not be null.", "process");
			}
			string text2 = base.Definition.Attributes["LogFileDirectory"];
			if (string.IsNullOrEmpty(text2))
			{
				text2 = F1TraceResponder.GetOrCreateDefaultLogFileDirectory(base.TraceContext);
			}
			string f1ProfilerDirectory = base.Definition.Attributes["F1ProfilerDirectory"];
			string safeF1Name = F1TraceResponder.GetSafeF1Name(base.Definition.TargetResource);
			string path = string.Format("{0}-{1}-{2}", Environment.MachineName, safeF1Name, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
			string text3 = Path.Combine(text2, path);
			TimeSpan timeSpan = TimeSpan.Zero;
			if (!TimeSpan.TryParse(base.Definition.Attributes["Duration"], out timeSpan))
			{
				string arg = base.Definition.Attributes["Duration"];
				WatsonResponder.TraceDebug(base.TraceContext, string.Format("WatsonF1TraceResponder.CollectCallstack: Invalid duration {0}, reverting to default min.", arg));
				timeSpan = WatsonF1TraceResponder.MinF1TraceDuration;
			}
			if (timeSpan < WatsonF1TraceResponder.MinF1TraceDuration)
			{
				WatsonResponder.TraceDebug(base.TraceContext, string.Format("WatsonF1TraceResponder.CollectCallstack: Duration of {0} too small, resetting to min.", timeSpan.TotalSeconds));
				timeSpan = WatsonF1TraceResponder.MinF1TraceDuration;
			}
			else if (timeSpan > WatsonF1TraceResponder.MaxF1TraceDuration)
			{
				WatsonResponder.TraceDebug(base.TraceContext, string.Format("WatsonF1TraceResponder.CollectCallstack: Duration of {0} too large, resetting to max.", timeSpan.TotalSeconds));
				timeSpan = WatsonF1TraceResponder.MaxF1TraceDuration;
			}
			text3 = F1TraceResponder.CollectF1Trace(f1ProfilerDirectory, new List<int>
			{
				process.Id
			}, text3, base.TraceContext, timeSpan);
			if (string.IsNullOrEmpty(text3))
			{
				return text;
			}
			F1TraceResponder.CreateCallTreeSummaryFile(null, text3, base.TraceContext);
			WatsonResponder.TraceDebug(base.TraceContext, "WatsonF1TraceResponder.CollectCallstack: F1 Trace Completed.");
			string text4 = text3 + WatsonF1TraceResponder.F1FileExtension;
			if (File.Exists(text4))
			{
				base.Definition.Attributes["FilesToUpload"] = text4;
			}
			else
			{
				WatsonResponder.TraceDebug(base.TraceContext, string.Format("WatsonF1TraceResponder.CollectCallstack: Failed to create f1, path '{0}' DNE.", text4));
			}
			string text5 = text3 + "_CallTreeSummary.csv";
			string empty = string.Empty;
			if (File.Exists(text5))
			{
				text = WatsonF1TraceResponder.GetHotPath(text5, base.TraceContext);
			}
			else
			{
				WatsonResponder.TraceDebug(base.TraceContext, string.Format("WatsonF1TraceResponder.CollectCallstack: Callstack summary file failed, path '{0}' DNE.", text5));
			}
			if (string.IsNullOrEmpty(text))
			{
				return base.CollectCallstack(process);
			}
			return text;
		}

		// Token: 0x040004DF RID: 1247
		private const string F1CallstackFilenameEnd = "_CallTreeSummary.csv";

		// Token: 0x040004E0 RID: 1248
		private static readonly string TypeName = typeof(WatsonF1TraceResponder).FullName;

		// Token: 0x040004E1 RID: 1249
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040004E2 RID: 1250
		private static readonly TimeSpan MinF1TraceDuration = TimeSpan.FromSeconds(30.0);

		// Token: 0x040004E3 RID: 1251
		private static readonly TimeSpan MaxF1TraceDuration = TimeSpan.FromMinutes(15.0);

		// Token: 0x040004E4 RID: 1252
		private static readonly string F1FileExtension = F1TraceResponder.IsWindows2012 ? ".vspx" : ".vsp";

		// Token: 0x020000EC RID: 236
		internal static class F1AttributeNames
		{
			// Token: 0x040004E5 RID: 1253
			internal const string LogFileDirectory = "LogFileDirectory";

			// Token: 0x040004E6 RID: 1254
			internal const string F1ProfilerDirectory = "F1ProfilerDirectory";

			// Token: 0x040004E7 RID: 1255
			internal const string Duration = "Duration";
		}
	}
}
