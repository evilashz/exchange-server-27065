using System;
using System.Collections;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000017 RID: 23
	internal class RequestMonitor
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004B53 File Offset: 0x00002D53
		private RequestMonitor(string logFolderPath, int cacheLimitSize)
		{
			if (!RequestMonitor.Enabled.Value)
			{
				return;
			}
			this.cacheLimitSize = cacheLimitSize;
			this.logFolderPath = logFolderPath;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004B76 File Offset: 0x00002D76
		internal static RequestMonitor Instance
		{
			get
			{
				return RequestMonitor.instance;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004B7D File Offset: 0x00002D7D
		private bool IsThreadStopped
		{
			get
			{
				return this.workerThread == null || !this.workerThread.IsAlive;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004B97 File Offset: 0x00002D97
		private RequestMonitorLogger Logger
		{
			get
			{
				return this.logger;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004B9F File Offset: 0x00002D9F
		internal static void InitRequestMonitor(string logFolderPath, int cacheLimitSize = 300000)
		{
			RequestMonitor.instance = new RequestMonitor(logFolderPath, cacheLimitSize);
			if (!RequestMonitor.Enabled.Value)
			{
				return;
			}
			RequestMonitor.Instance.Initialize();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004BC4 File Offset: 0x00002DC4
		internal bool TryGetCurrentRequestMonitorContext(Guid requestId, out RequestMonitorContext context)
		{
			context = RequestMonitorContext.Current;
			return context != null || this.requestContextCache.TryGetValue(requestId, out context);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004BE0 File Offset: 0x00002DE0
		internal void Log(Guid requestId, RequestMonitorMetadata key, object value)
		{
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Log] Enter.");
			if (!RequestMonitor.Enabled.Value)
			{
				ExTraceGlobals.InstrumentationTracer.TraceDebug((long)this.GetHashCode(), "[RequestMonitor::Log] Exit. RequestMonitorEnabled=false");
				return;
			}
			RequestMonitorContext requestMonitorContext;
			if (this.TryGetCurrentRequestMonitorContext(requestId, out requestMonitorContext))
			{
				ExTraceGlobals.InstrumentationTracer.TraceDebug<RequestMonitorMetadata, object>((long)this.GetHashCode(), "[RequestMonitor::Log] Key={0}, Value={1}.", key, value);
				requestMonitorContext[key] = value;
			}
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Log] Exit.");
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C68 File Offset: 0x00002E68
		internal void RegisterRequest(Guid requestId)
		{
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::RegisterRequest] Enter.");
			if (!RequestMonitor.Enabled.Value || this.IsThreadStopped)
			{
				ExTraceGlobals.InstrumentationTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "[RequestMonitor::RegisterRequest] Exit. RequestMonitorEnabled={0}, ThreadStopped={1}", RequestMonitor.Enabled.Value, this.IsThreadStopped);
				return;
			}
			RequestMonitorContext value = new RequestMonitorContext(requestId);
			this.requestContextCache.TryAddAbsolute(requestId, value, RequestMonitor.MaxRequestLifeTime.Value);
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::RegisterRequest] Exit.");
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CFC File Offset: 0x00002EFC
		internal void UnRegisterRequest(Guid requestId)
		{
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::UnRegisterRequest] Enter.");
			if (!RequestMonitor.Enabled.Value)
			{
				return;
			}
			if (this.requestContextCache.Contains(requestId))
			{
				this.requestContextCache.Remove(requestId);
			}
			else
			{
				RequestMonitorContext requestMonitorContext = RequestMonitorContext.Current;
				if (requestMonitorContext != null)
				{
					requestMonitorContext[RequestMonitorMetadata.LoggingReason] = LoggingReason.End;
					this.Commit(RequestMonitorContext.Current);
				}
			}
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::UnRegisterRequest] Exit.");
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004D80 File Offset: 0x00002F80
		private void Initialize()
		{
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Initialize] Enter.");
			this.requestContextCache = new ExactTimeoutCache<Guid, RequestMonitorContext>(null, new ShouldRemoveDelegate<Guid, RequestMonitorContext>(this.OnBeforeExpire), null, this.cacheLimitSize, false);
			this.logItemQueue = Queue.Synchronized(new Queue());
			this.workerSignal = new AutoResetEvent(false);
			this.CreateWorkerThread();
			this.logger = new RequestMonitorLogger(this.logFolderPath);
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Initialize] Exit.");
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004E0C File Offset: 0x0000300C
		private void Commit(RequestMonitorContext context)
		{
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Commit] Enter.");
			if (this.IsThreadStopped)
			{
				ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Commit] Exit. Worker thread stopped.");
				return;
			}
			context[RequestMonitorMetadata.EndTime] = DateTime.UtcNow;
			this.logItemQueue.Enqueue(context);
			this.workerSignal.Set();
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::Commit] Exit.");
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004E90 File Offset: 0x00003090
		private bool OnBeforeExpire(Guid requestId, RequestMonitorContext context)
		{
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::OnBeforeExpire] Enter.");
			context[RequestMonitorMetadata.LoggingReason] = LoggingReason.Expired;
			ExTraceGlobals.InstrumentationTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[RequestMonitor::OnBeforeExpire] Request {0} was committed.", requestId);
			this.Commit(context);
			ExTraceGlobals.InstrumentationTracer.TraceFunction((long)this.GetHashCode(), "[RequestMonitor::OnBeforeExpire] Exit.");
			return true;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004EF5 File Offset: 0x000030F5
		private void CreateWorkerThread()
		{
			this.workerThread = new Thread(new ParameterizedThreadStart(this.WorkerProc));
			this.workerThread.IsBackground = true;
			this.workerThread.Start();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004F28 File Offset: 0x00003128
		private void WorkerProc(object state)
		{
			for (;;)
			{
				try
				{
					while (this.logItemQueue.Count > 0)
					{
						RequestMonitorContext context = this.logItemQueue.Dequeue() as RequestMonitorContext;
						this.Logger.Commit(context);
					}
				}
				catch (Exception exception)
				{
					Diagnostics.ReportException(exception, LoggerSettings.EventLog, LoggerSettings.EventTuple, null, null, ExTraceGlobals.InstrumentationTracer, "Exception from RequestMonitor : {0}");
				}
				this.workerSignal.WaitOne();
			}
		}

		// Token: 0x0400005B RID: 91
		private static readonly BoolAppSettingsEntry Enabled = new BoolAppSettingsEntry("RequestMonitor.Enabled", false, ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400005C RID: 92
		private static readonly TimeSpanAppSettingsEntry MaxRequestLifeTime = new TimeSpanAppSettingsEntry("RequestMonitor.DelayTimeToLogRequestInMinutes", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(1.0), ExTraceGlobals.InstrumentationTracer);

		// Token: 0x0400005D RID: 93
		private static RequestMonitor instance;

		// Token: 0x0400005E RID: 94
		private readonly int cacheLimitSize;

		// Token: 0x0400005F RID: 95
		private readonly string logFolderPath;

		// Token: 0x04000060 RID: 96
		private Queue logItemQueue;

		// Token: 0x04000061 RID: 97
		private AutoResetEvent workerSignal;

		// Token: 0x04000062 RID: 98
		private Thread workerThread;

		// Token: 0x04000063 RID: 99
		private RequestMonitorLogger logger;

		// Token: 0x04000064 RID: 100
		private ExactTimeoutCache<Guid, RequestMonitorContext> requestContextCache;
	}
}
