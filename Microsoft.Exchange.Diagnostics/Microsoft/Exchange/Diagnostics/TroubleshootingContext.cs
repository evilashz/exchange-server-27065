using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B7 RID: 183
	public class TroubleshootingContext
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00012805 File Offset: 0x00010A05
		private static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (TroubleshootingContext.faultInjectionTracer == null)
				{
					TroubleshootingContext.faultInjectionTracer = new FaultInjectionTrace(TroubleshootingContext.diagnosticsComponentGuid, 1);
				}
				return TroubleshootingContext.faultInjectionTracer;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00012823 File Offset: 0x00010A23
		public MemoryTraceBuilder MemoryTraceBuilder
		{
			get
			{
				return this.memoryTraceBuilder;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001282B File Offset: 0x00010A2B
		public TroubleshootingContext(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			this.location = location;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00012848 File Offset: 0x00010A48
		public void TraceOperationCompletedAndUpdateContext()
		{
			this.TraceOperationCompletedAndUpdateContext((long)this.GetHashCode());
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00012858 File Offset: 0x00010A58
		public void TraceOperationCompletedAndUpdateContext(long id)
		{
			if (!ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				return;
			}
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			this.AddOperationCompletedMarker(memoryTraceBuilder, id);
			int startIndex = memoryTraceBuilder.FindLastEntryIndex(2, TroubleshootingContext.markerEntry.TraceType, TroubleshootingContext.markerEntry.TraceTag, TroubleshootingContext.markerEntry.ComponentGuid, TroubleshootingContext.markerEntry.FormatString);
			lock (this)
			{
				if (this.memoryTraceBuilder == null)
				{
					this.memoryTraceBuilder = new MemoryTraceBuilder(1000, 128000);
				}
				memoryTraceBuilder.CopyTo(this.memoryTraceBuilder, startIndex);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00012914 File Offset: 0x00010B14
		public IEnumerable<TraceEntry> GetTraces()
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			if (memoryTraceBuilder == null)
			{
				return new TraceEntry[0];
			}
			return memoryTraceBuilder.GetTraces();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00012937 File Offset: 0x00010B37
		public void SendExceptionReportWithTraces(Exception exception, bool terminating)
		{
			this.SendExceptionReportWithTraces(exception, terminating, false);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00012942 File Offset: 0x00010B42
		public void SendExceptionReportWithTraces(Exception exception, bool terminating, bool verbose)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				this.ReportProblem(this.memoryTraceBuilder, exception, null, true, terminating, verbose);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00012970 File Offset: 0x00010B70
		public void SendTroubleshootingReportWithTraces(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			string functionNameFromException = TroubleshootingContext.GetFunctionNameFromException(exception);
			this.SendTroubleshootingReportWithTraces(exception, functionNameFromException);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001299A File Offset: 0x00010B9A
		public void SendTroubleshootingReportWithTraces(Exception exception, string functionName)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				this.ReportProblem(this.memoryTraceBuilder, exception, functionName, false, false);
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000129C6 File Offset: 0x00010BC6
		private static void DumpExceptionInfo(Exception e, StringBuilder output)
		{
			while (e != null)
			{
				output.AppendFormat("{0}\n{1}\n\n", e.Message, e.StackTrace);
				e = e.InnerException;
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000129F0 File Offset: 0x00010BF0
		private void AddOperationCompletedMarker(MemoryTraceBuilder memoryTraceBuilder, long id)
		{
			memoryTraceBuilder.BeginEntry(TroubleshootingContext.markerEntry.TraceType, TroubleshootingContext.markerEntry.ComponentGuid, TroubleshootingContext.markerEntry.TraceTag, id, TroubleshootingContext.markerEntry.FormatString);
			memoryTraceBuilder.EndEntry();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00012A3E File Offset: 0x00010C3E
		private void ReportProblem(MemoryTraceBuilder contextTraceBuilder, Exception exception, string functionName, bool isExceptionReport, bool isExceptionReportTerminating)
		{
			this.ReportProblem(contextTraceBuilder, exception, functionName, isExceptionReport, isExceptionReportTerminating, false);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00012A50 File Offset: 0x00010C50
		private static string GetFunctionNameFromException(Exception exception)
		{
			string result;
			if (exception.TargetSite != null && exception.TargetSite.ReflectedType != null && exception.TargetSite.ReflectedType.FullName != null && exception.TargetSite.Name != null)
			{
				result = exception.TargetSite.ReflectedType.FullName + "." + exception.TargetSite.Name;
			}
			else
			{
				result = "unknown";
			}
			return result;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00012ACE File Offset: 0x00010CCE
		private static bool IsTestTopology()
		{
			return string.Equals("EXTST-", ExWatson.LabName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00012AE0 File Offset: 0x00010CE0
		private void ReportProblem(MemoryTraceBuilder contextTraceBuilder, Exception exception, string functionName, bool isExceptionReport, bool isExceptionReportTerminating, bool verbose)
		{
			using (TempFileStream tempFileStream = TempFileStream.CreateInstance("Traces_", false))
			{
				using (StreamWriter streamWriter = new StreamWriter(tempFileStream))
				{
					bool addHeader = true;
					if (contextTraceBuilder != null)
					{
						lock (this)
						{
							contextTraceBuilder.Dump(streamWriter, addHeader, verbose);
						}
						addHeader = false;
					}
					MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
					if (memoryTraceBuilder != null)
					{
						memoryTraceBuilder.Dump(streamWriter, addHeader, verbose);
					}
					streamWriter.Flush();
				}
				StringBuilder stringBuilder = new StringBuilder(1024);
				TroubleshootingContext.DumpExceptionInfo(exception, stringBuilder);
				if (TroubleshootingContext.IsTestTopology())
				{
					string path = ExWatson.AppName + "_" + DateTime.UtcNow.ToString("yyyyMMdd_hhmmss") + ".trace";
					try
					{
						File.Copy(tempFileStream.FilePath, Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("SystemDrive"), "\\dumps"), path));
					}
					catch
					{
					}
				}
				if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
				{
					if (isExceptionReport)
					{
						WatsonExtraFileReportAction watsonExtraFileReportAction = null;
						try
						{
							watsonExtraFileReportAction = new WatsonExtraFileReportAction(tempFileStream.FilePath);
							ExWatson.RegisterReportAction(watsonExtraFileReportAction, WatsonActionScope.Thread);
							ExWatson.SendReport(exception, isExceptionReportTerminating ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None, null);
							goto IL_152;
						}
						finally
						{
							if (watsonExtraFileReportAction != null)
							{
								ExWatson.UnregisterReportAction(watsonExtraFileReportAction, WatsonActionScope.Thread);
							}
						}
					}
					ExWatson.SendTroubleshootingWatsonReport("15.00.1497.010", this.location, "UnexpectedCondition:" + exception.GetType().Name, exception.StackTrace, functionName, stringBuilder.ToString(), tempFileStream.FilePath);
					IL_152:
					File.Delete(tempFileStream.FilePath);
				}
			}
		}

		// Token: 0x0400036F RID: 879
		private const string AssemblyVersion = "15.00.1497.010";

		// Token: 0x04000370 RID: 880
		private const int MaxTraceBufferEntryCount = 1000;

		// Token: 0x04000371 RID: 881
		private const int MaxTraceBufferSize = 128000;

		// Token: 0x04000372 RID: 882
		private const int EstimatedReportLength = 1024;

		// Token: 0x04000373 RID: 883
		private const int TagFaultInjection = 1;

		// Token: 0x04000374 RID: 884
		internal static readonly Exception FaultInjectionInvalidOperationException = new InvalidOperationException("Fault injection created exception: EEB7F5D9-C4EA-41a7-81DD-C8F7B98216B5");

		// Token: 0x04000375 RID: 885
		private static readonly Guid diagnosticsComponentGuid = new Guid("20e99398-d277-4ead-acde-0dbe119f7ce6");

		// Token: 0x04000376 RID: 886
		private static readonly TraceEntry markerEntry = new TraceEntry(TraceType.InfoTrace, ExTraceGlobals.CommonTracer.Category, 0, 0L, "<Operation completed>", 0, 0);

		// Token: 0x04000377 RID: 887
		private static FaultInjectionTrace faultInjectionTracer;

		// Token: 0x04000378 RID: 888
		private MemoryTraceBuilder memoryTraceBuilder;

		// Token: 0x04000379 RID: 889
		private string location;
	}
}
