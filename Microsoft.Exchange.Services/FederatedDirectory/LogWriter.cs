using System;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200007B RID: 123
	internal sealed class LogWriter : ILogWriter
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000F680 File Offset: 0x0000D880
		private LogWriter()
		{
			LogManager.Initialize(this);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000F690 File Offset: 0x0000D890
		public static void SimpleLog(object toString)
		{
			LogManager.SendTraceTag(0U, 1, 4, "{0}", new object[]
			{
				toString
			});
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000F6B6 File Offset: 0x0000D8B6
		public static void TraceAndLog(LogWriter.TraceMethod traceMethod, LogTraceLevel level, int hashcode, string formatString, params object[] data)
		{
			traceMethod((long)hashcode, formatString, data);
			LogManager.SendTraceTag(0U, 1, level, formatString, data);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000F6CE File Offset: 0x0000D8CE
		internal static void Initialize()
		{
			ExAssert.RetailAssert(LogWriter.singleton != null, "singleton should not be null");
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000F6E5 File Offset: 0x0000D8E5
		public void Initialize(NameValueCollection parameters)
		{
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F6E7 File Offset: 0x0000D8E7
		public bool ShouldTrace(LogCategory category, LogTraceLevel level)
		{
			return LogWriter.Tracer.IsTraceEnabled(TraceType.ErrorTrace) || LogWriter.Tracer.IsTraceEnabled(TraceType.WarningTrace) || LogWriter.Tracer.IsTraceEnabled(TraceType.DebugTrace);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000F710 File Offset: 0x0000D910
		public void CorrelationCloseExistingAndStartNew(Guid correlationId)
		{
			this.CorrelationEnd();
			this.CorrelationStart(correlationId);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000F720 File Offset: 0x0000D920
		public void CorrelationStart(Guid correlationId)
		{
			if (LogWriter.currentCorrelationId != Guid.Empty)
			{
				LogWriter.Tracer.TraceWarning<Guid, Guid>((long)this.GetHashCode(), "Unexpected CorrelationStart() call when already started. CorrelationId={0}, CurrentCorrelationId={1}", correlationId, LogWriter.currentCorrelationId);
			}
			else
			{
				LogWriter.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CorrelationStart: CorrelationId={0}", correlationId);
			}
			LogWriter.currentCorrelationId = correlationId;
			LogWriter.clearThreadScope = false;
			if (ActivityContext.GetCurrentActivityScope() == null)
			{
				IActivityScope activityScope = CorrelationContext.GetActivityScope(correlationId);
				if (activityScope != null)
				{
					ActivityContext.SetThreadScope(activityScope);
					LogWriter.clearThreadScope = true;
				}
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F79C File Offset: 0x0000D99C
		public void CorrelationEnd()
		{
			if (LogWriter.currentCorrelationId == Guid.Empty)
			{
				LogWriter.Tracer.TraceWarning((long)this.GetHashCode(), "Unexpected CorrelationEnd() call when no active correlation.");
			}
			else
			{
				LogWriter.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CorrelationEnd: CorrelationId={0}", LogWriter.currentCorrelationId);
			}
			if (LogWriter.clearThreadScope)
			{
				ActivityContext.ClearThreadScope();
				LogWriter.clearThreadScope = false;
			}
			LogWriter.currentCorrelationId = Guid.Empty;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F809 File Offset: 0x0000DA09
		public Guid CorrelationGet()
		{
			return LogWriter.currentCorrelationId;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000F810 File Offset: 0x0000DA10
		public void RecordRequest(RequestMonitor requestMonitor)
		{
			LogWriter.Tracer.TraceDebug<Guid, int, RequestType>((long)this.GetHashCode(), "RecordRequest: CorrelationId={0}, CurrentStage={1}, RequestType={2}", requestMonitor.CorrelationId, requestMonitor.CurrentStage, requestMonitor.RequestType);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F83C File Offset: 0x0000DA3C
		public void SendTraceTag(uint tagID, LogCategory category, LogTraceLevel level, string output, params object[] data)
		{
			string text = string.Format(CultureInfo.InvariantCulture, output, data);
			LogWriter.Tracer.TraceDebug((long)this.GetHashCode(), "SendTraceTag: currentCorrelationId={0}, tagId={1}, category={2}, level={3}. {4}", new object[]
			{
				LogWriter.currentCorrelationId,
				tagID,
				category,
				level,
				text
			});
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
			{
				{
					FederatedDirectoryLogSchema.TraceTag.ActivityId,
					LogWriter.currentCorrelationId
				},
				{
					FederatedDirectoryLogSchema.TraceTag.Message,
					text
				}
			});
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		public void SendExceptionTag(uint tagID, LogCategory category, Exception ex, string output, params object[] data)
		{
			string text = string.Format(CultureInfo.InvariantCulture, output, data);
			LogWriter.Tracer.TraceError((long)this.GetHashCode(), "SendExceptionTag: currentCorrelationId={0}, tagId={1}, category={2}, exception={3}. {4}", new object[]
			{
				LogWriter.currentCorrelationId,
				tagID,
				category,
				ex,
				text
			});
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.ExceptionTag>
			{
				{
					FederatedDirectoryLogSchema.ExceptionTag.ActivityId,
					LogWriter.currentCorrelationId
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.ExceptionType,
					ex.GetType()
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.ExceptionDetail,
					ex
				},
				{
					FederatedDirectoryLogSchema.ExceptionTag.Message,
					text
				}
			});
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F964 File Offset: 0x0000DB64
		public void AssertTag(uint tagID, LogCategory category, bool condition, string output, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, output, data);
				LogWriter.Tracer.TraceError((long)this.GetHashCode(), "AssertTag: currentCorrelationId={0}, tagId={1}, category={2}. {3}", new object[]
				{
					LogWriter.currentCorrelationId,
					tagID,
					category,
					text
				});
				FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.AssertTag>
				{
					{
						FederatedDirectoryLogSchema.AssertTag.ActivityId,
						LogWriter.currentCorrelationId
					},
					{
						FederatedDirectoryLogSchema.AssertTag.Message,
						text
					}
				});
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		public void ShipAssertTag(uint tagID, LogCategory category, bool condition, string output, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, output, data);
				LogWriter.Tracer.TraceError((long)this.GetHashCode(), "ShipAssertTag: activityId={0}, tagId={1}, category={2}. {3}", new object[]
				{
					LogWriter.currentCorrelationId,
					tagID,
					category,
					text
				});
				FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.ShipAssertTag>
				{
					{
						FederatedDirectoryLogSchema.ShipAssertTag.ActivityId,
						LogWriter.currentCorrelationId
					},
					{
						FederatedDirectoryLogSchema.ShipAssertTag.Message,
						text
					}
				});
			}
		}

		// Token: 0x040005C3 RID: 1475
		private static readonly Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;

		// Token: 0x040005C4 RID: 1476
		private static readonly LogWriter singleton = new LogWriter();

		// Token: 0x040005C5 RID: 1477
		[ThreadStatic]
		private static Guid currentCorrelationId;

		// Token: 0x040005C6 RID: 1478
		[ThreadStatic]
		private static bool clearThreadScope;

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x06000323 RID: 803
		public delegate void TraceMethod(long id, string formatString, params object[] args);
	}
}
