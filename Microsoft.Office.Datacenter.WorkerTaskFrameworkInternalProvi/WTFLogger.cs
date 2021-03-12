using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200002F RID: 47
	public sealed class WTFLogger : DisposeTrackableBase
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000B0C4 File Offset: 0x000092C4
		private WTFLogger()
		{
			this.logConfiguration = new WTFLogConfiguration();
			if (this.logConfiguration.IsLoggingEnabled)
			{
				this.logSchema = new WTFLogSchema(this.logConfiguration);
				this.log = new Log(this.logConfiguration.LogPrefix, new LogHeaderFormatter(this.logSchema), this.logConfiguration.LogComponent);
				this.log.Configure(this.logConfiguration.LogPath, this.logConfiguration.MaxLogAge, this.logConfiguration.MaxLogDirectorySizeInBytes, this.logConfiguration.MaxLogFileSizeInBytes, this.logConfiguration.LogBufferSizeInBytes, this.logConfiguration.FlushIntervalInMinutes);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000B17C File Offset: 0x0000937C
		public static WTFLogger Instance
		{
			get
			{
				if (WTFLogger.theInstance == null)
				{
					lock (WTFLogger.syncRoot)
					{
						if (WTFLogger.theInstance == null)
						{
							WTFLogger.theInstance = new WTFLogger();
						}
					}
				}
				return WTFLogger.theInstance;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B1DC File Offset: 0x000093DC
		public void Flush()
		{
			if (this.log != null)
			{
				this.log.Flush();
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B1F1 File Offset: 0x000093F1
		public void LogDebug(WTFLogComponent component, TracingContext context, string message, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			this.LogMessage(WTFLogger.LogLevel.Debug, component, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B203 File Offset: 0x00009403
		public void LogInformation(WTFLogComponent component, TracingContext context, string message, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			this.LogMessage(WTFLogger.LogLevel.Information, component, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B218 File Offset: 0x00009418
		public void LogException(WTFLogComponent component, TracingContext context, Exception exception, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(exception.Message);
			stringBuilder.Append("/ Stack: ");
			stringBuilder.Append(exception.StackTrace);
			this.LogMessage(WTFLogger.LogLevel.Exception, component, context, stringBuilder.ToString(), methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B266 File Offset: 0x00009466
		public void LogWarning(WTFLogComponent component, TracingContext context, string message, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			this.LogMessage(WTFLogger.LogLevel.Warning, component, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B278 File Offset: 0x00009478
		public void LogError(WTFLogComponent component, TracingContext context, string message, [CallerMemberName] string methodName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
		{
			this.LogMessage(WTFLogger.LogLevel.Error, component, context, message, methodName, sourceFilePath, sourceLineNumber);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B28C File Offset: 0x0000948C
		internal static WTFLogContext GetWorkItemContext(WTFLogger.LogLevel logLevel, WTFLogComponent component, TracingContext context, string message, string methodName, string sourceFilePath, int sourceLineNumber)
		{
			WTFLogContext result;
			result.WorkItemInstance = string.Empty;
			result.WorkItemType = string.Empty;
			result.WorkItemDefinition = string.Empty;
			result.WorkItemCreatedBy = string.Empty;
			result.WorkItemResult = string.Empty;
			result.ComponentName = string.Empty;
			result.ProcessAndThread = string.Empty;
			result.LogLevel = string.Empty;
			result.CallerMethod = string.Empty;
			result.CallerSourceLine = string.Empty;
			result.Message = message;
			if (context != null)
			{
				WorkItem workItem = context.WorkItem;
				if (workItem != null)
				{
					result.WorkItemInstance = workItem.InstanceId.ToString();
					result.WorkItemType = workItem.GetType().Name;
					WorkDefinition definition = workItem.Definition;
					WorkItemResult result2 = workItem.Result;
					if (definition != null)
					{
						result.WorkItemDefinition = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
						{
							definition.Name,
							definition.Id
						});
						result.WorkItemCreatedBy = definition.CreatedById.ToString();
					}
					if (result2 != null)
					{
						result.WorkItemResult = result2.ResultId.ToString();
					}
				}
			}
			if (component != null)
			{
				if (string.IsNullOrEmpty(component.Name))
				{
					result.ComponentName = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
					{
						component.Category,
						component.LogTag
					});
				}
				else
				{
					result.ComponentName = component.Name;
				}
			}
			result.ProcessAndThread = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				WTFLogger.CurrentProcess.Id,
				Thread.CurrentThread.ManagedThreadId
			});
			result.LogLevel = WTFLogger.LogLevelStrings[(int)logLevel];
			result.CallerMethod = methodName;
			result.CallerSourceLine = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				Path.GetFileName(sourceFilePath),
				sourceLineNumber
			});
			return result;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B4C0 File Offset: 0x000096C0
		internal void LogWithContext(WTFLogComponent component, WTFLogContext logContext)
		{
			if (this.logConfiguration.IsLoggingEnabled && component.IsTraceLoggingEnabled)
			{
				LogRowFormatter row = this.CreateRow(logContext);
				this.log.Append(row, 0);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B4F7 File Offset: 0x000096F7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WTFLogger>(this);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B4FF File Offset: 0x000096FF
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.log != null)
			{
				this.log.Flush();
				this.log.Close();
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B524 File Offset: 0x00009724
		private void LogMessage(WTFLogger.LogLevel logLevel, WTFLogComponent component, TracingContext context, string message, string methodName, string sourceFilePath, int sourceLineNumber)
		{
			WTFLogContext workItemContext = WTFLogger.GetWorkItemContext(logLevel, component, context, message, methodName, sourceFilePath, sourceLineNumber);
			ExTraceGlobals.CoreTracer.TraceInformation(context.LId, (long)context.Id, workItemContext.ToString());
			if (component.IsTraceLoggingEnabled && this.logConfiguration.IsLoggingEnabled)
			{
				LogRowFormatter row = this.CreateRow(workItemContext);
				this.log.Append(row, 0);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B590 File Offset: 0x00009790
		private LogRowFormatter CreateRow(WTFLogContext logContext)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema, true);
			logRowFormatter[1] = logContext.WorkItemInstance;
			logRowFormatter[2] = logContext.WorkItemType;
			logRowFormatter[3] = logContext.WorkItemDefinition;
			logRowFormatter[4] = logContext.WorkItemCreatedBy;
			logRowFormatter[5] = logContext.WorkItemResult;
			logRowFormatter[6] = logContext.ComponentName;
			logRowFormatter[7] = logContext.ProcessAndThread;
			logRowFormatter[8] = logContext.LogLevel;
			logRowFormatter[9] = logContext.CallerMethod;
			logRowFormatter[10] = logContext.CallerSourceLine;
			logRowFormatter[11] = logContext.Message;
			return logRowFormatter;
		}

		// Token: 0x04000124 RID: 292
		private static readonly string[] LogLevelStrings = new string[]
		{
			"Debug",
			"Information",
			"Exception",
			"Warning",
			"Error"
		};

		// Token: 0x04000125 RID: 293
		private static readonly Process CurrentProcess = Process.GetCurrentProcess();

		// Token: 0x04000126 RID: 294
		private static volatile WTFLogger theInstance;

		// Token: 0x04000127 RID: 295
		private static object syncRoot = new object();

		// Token: 0x04000128 RID: 296
		private readonly WTFLogConfiguration logConfiguration;

		// Token: 0x04000129 RID: 297
		private readonly WTFLogSchema logSchema;

		// Token: 0x0400012A RID: 298
		private readonly Log log;

		// Token: 0x02000030 RID: 48
		public enum LogLevel
		{
			// Token: 0x0400012C RID: 300
			Debug,
			// Token: 0x0400012D RID: 301
			Information,
			// Token: 0x0400012E RID: 302
			Exception,
			// Token: 0x0400012F RID: 303
			Warning,
			// Token: 0x04000130 RID: 304
			Error
		}
	}
}
