using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000E0 RID: 224
	internal static class ThreadContext
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x000193AC File Offset: 0x000175AC
		public static void SetWithExceptionHandling(string label, ThreadCounter threadCounter, ClientContext clientContext, RequestLogger requestLogger, ThreadContext.ExecuteDelegate executeDelegate)
		{
			ThreadContext.Set(label, threadCounter, clientContext, requestLogger, delegate()
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						executeDelegate();
					});
				}
				catch (GrayException arg)
				{
					string arg2 = (clientContext != null) ? clientContext.IdentityForFilteredTracing : "none";
					ThreadContext.Tracer.TraceError<string, GrayException>(0L, "{0}: failed with exception: {1}", arg2, arg);
				}
			});
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019404 File Offset: 0x00017604
		public static T Set<T>(string label, ThreadCounter threadCounter, ClientContext clientContext, RequestLogger requestLogger, ThreadContext.ExecuteDelegate<T> executeDelegate)
		{
			T result = default(T);
			ThreadContext.Set(label, threadCounter, clientContext, requestLogger, delegate()
			{
				result = executeDelegate();
			});
			return result;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019448 File Offset: 0x00017648
		private static void Set(string label, ThreadCounter threadCounter, ClientContext clientContext, RequestLogger requestLogger, ThreadContext.ExecuteDelegate executeDelegate)
		{
			string text = (clientContext != null) ? clientContext.IdentityForFilteredTracing : "none";
			RequestStatisticsForThread requestStatisticsForThread = RequestStatisticsForThread.Begin();
			threadCounter.Increment();
			ThreadContext.Tracer.TraceDebug<string, string, string>(0L, "{0}: Thread entered {1}. MessageId={2}", text, label, (clientContext != null) ? (clientContext.MessageId ?? "<null>") : "none");
			try
			{
				using (new ASTraceFilter(null, text))
				{
					TraceContext.Set(text);
					try
					{
						executeDelegate();
					}
					finally
					{
						TraceContext.Reset();
					}
				}
			}
			finally
			{
				threadCounter.Decrement();
				RequestStatistics requestStatistics = requestStatisticsForThread.End(RequestStatisticsType.ThreadCPULongPole, label);
				if (requestStatistics != null && requestLogger != null)
				{
					requestLogger.Add(requestStatistics);
				}
				ThreadContext.Tracer.TraceDebug<string, string>(0L, "{0}: Thread exited {1}", text, label);
			}
		}

		// Token: 0x04000364 RID: 868
		private static readonly Trace Tracer = ExTraceGlobals.RequestRoutingTracer;

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x060005D4 RID: 1492
		public delegate void ExecuteDelegate();

		// Token: 0x020000E2 RID: 226
		// (Invoke) Token: 0x060005D8 RID: 1496
		public delegate T ExecuteDelegate<T>();
	}
}
