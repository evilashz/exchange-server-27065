using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000FE RID: 254
	internal abstract class ConfigurationCoreLogger<T> : RequestDetailsLoggerBase<T> where T : ConfigurationCoreLogger<T>, new()
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		protected static string DefaultLogFolderPath
		{
			get
			{
				string result;
				try
				{
					string text;
					if ((text = ConfigurationCoreLogger<T>.defaultLogFolderPath) == null)
					{
						text = (ConfigurationCoreLogger<T>.defaultLogFolderPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging", "CmdletInfra"));
					}
					result = text;
				}
				catch (SetupVersionInformationCorruptException)
				{
					result = "C:\\Program Files\\Microsoft\\Exchange Server\\V15\\CmdletInfra";
				}
				return result;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001DB44 File Offset: 0x0001BD44
		internal static void FlushQueuedFileWrites()
		{
			ConfigurationCoreLogger<T>.workerSignal.Set();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001DB54 File Offset: 0x0001BD54
		internal void AsyncCommit(bool forceSync)
		{
			if (!base.IsDisposed)
			{
				ExTraceGlobals.InstrumentationTracer.TraceDebug<ConfigurationCoreLogger<T>, bool>((long)this.GetHashCode(), "Dispose {0} logger. forceSync = {1}.", this, forceSync);
				ServiceCommonMetadataPublisher.PublishMetadata();
				if (!forceSync)
				{
					ConfigurationCoreLogger<T>.fileIoQueue.Enqueue(this);
					ConfigurationCoreLogger<T>.workerSignal.Set();
				}
				else
				{
					this.Dispose();
				}
				RequestDetailsLoggerBase<T>.SetCurrent(HttpContext.Current, default(T));
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001DBBA File Offset: 0x0001BDBA
		protected override void SetPerLogLineSizeLimit()
		{
			ActivityScopeImpl.MaxAppendableColumnLength = LoggerSettings.MaxAppendableColumnLength;
			RequestDetailsLoggerBase<T>.ErrorMessageLengthThreshold = LoggerSettings.ErrorMessageLengthThreshold;
			RequestDetailsLoggerBase<T>.ProcessExceptionMessage = LoggerSettings.ProcessExceptionMessage;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001DBDA File Offset: 0x0001BDDA
		protected override bool LogFullException(Exception ex)
		{
			return true;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		protected override void InitializeLogger()
		{
			ActivityContext.RegisterMetadata(typeof(ConfigurationCoreMetadata));
			ActivityContext.RegisterMetadata(typeof(ServiceCommonMetadata));
			ActivityContext.RegisterMetadata(typeof(ServiceLatencyMetadata));
			ThreadPool.QueueUserWorkItem(new WaitCallback(ConfigurationCoreLogger<T>.CommitLogLines));
			base.InitializeLogger();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001DC34 File Offset: 0x0001BE34
		private static void CommitLogLines(object state)
		{
			for (;;)
			{
				try
				{
					while (ConfigurationCoreLogger<T>.fileIoQueue.Count > 0)
					{
						ConfigurationCoreLogger<T> configurationCoreLogger = ConfigurationCoreLogger<T>.fileIoQueue.Dequeue() as ConfigurationCoreLogger<T>;
						if (configurationCoreLogger != null && !configurationCoreLogger.IsDisposed)
						{
							configurationCoreLogger.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					Diagnostics.ReportException(exception, LoggerSettings.EventLog, LoggerSettings.EventTuple, null, null, ExTraceGlobals.InstrumentationTracer, "Exception from ConfigurationCoreLogger<T>.CommitLogLines : {0}");
				}
				ConfigurationCoreLogger<T>.workerSignal.WaitOne();
			}
		}

		// Token: 0x040004AB RID: 1195
		private static Queue fileIoQueue = Queue.Synchronized(new Queue());

		// Token: 0x040004AC RID: 1196
		private static AutoResetEvent workerSignal = new AutoResetEvent(false);

		// Token: 0x040004AD RID: 1197
		private static string defaultLogFolderPath;
	}
}
