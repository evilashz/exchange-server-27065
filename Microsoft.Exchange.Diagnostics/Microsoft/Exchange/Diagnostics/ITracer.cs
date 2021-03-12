using System;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000074 RID: 116
	public interface ITracer
	{
		// Token: 0x06000258 RID: 600
		void TraceDebug<T0>(long id, string formatString, T0 arg0);

		// Token: 0x06000259 RID: 601
		void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x0600025A RID: 602
		void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x0600025B RID: 603
		void TraceDebug(long id, string formatString, params object[] args);

		// Token: 0x0600025C RID: 604
		void TraceDebug(long id, string message);

		// Token: 0x0600025D RID: 605
		void TraceWarning<T0>(long id, string formatString, T0 arg0);

		// Token: 0x0600025E RID: 606
		void TraceWarning(long id, string message);

		// Token: 0x0600025F RID: 607
		void TraceWarning(long id, string formatString, params object[] args);

		// Token: 0x06000260 RID: 608
		void TraceError(long id, string message);

		// Token: 0x06000261 RID: 609
		void TraceError<T0>(long id, string formatString, T0 arg0);

		// Token: 0x06000262 RID: 610
		void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000263 RID: 611
		void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000264 RID: 612
		void TraceError(long id, string formatString, params object[] args);

		// Token: 0x06000265 RID: 613
		void TracePerformance(long id, string message);

		// Token: 0x06000266 RID: 614
		void TracePerformance<T0>(long id, string formatString, T0 arg0);

		// Token: 0x06000267 RID: 615
		void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1);

		// Token: 0x06000268 RID: 616
		void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000269 RID: 617
		void TracePerformance(long id, string formatString, params object[] args);

		// Token: 0x0600026A RID: 618
		void Dump(TextWriter writer, bool addHeader, bool verbose);

		// Token: 0x0600026B RID: 619
		ITracer Compose(ITracer other);

		// Token: 0x0600026C RID: 620
		bool IsTraceEnabled(TraceType traceType);
	}
}
