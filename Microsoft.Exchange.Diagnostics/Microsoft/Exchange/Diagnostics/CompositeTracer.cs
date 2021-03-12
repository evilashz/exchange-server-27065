using System;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000075 RID: 117
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CompositeTracer : ITracer
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000874E File Offset: 0x0000694E
		public CompositeTracer(ITracer first, ITracer second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			this.first = first;
			this.second = second;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008780 File Offset: 0x00006980
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.first.TraceDebug<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
			this.second.TraceDebug<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000087A6 File Offset: 0x000069A6
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.first.TraceDebug<T0, T1>(id, formatString, arg0, arg1);
			this.second.TraceDebug<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000087C8 File Offset: 0x000069C8
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.first.TraceDebug<T0>(id, formatString, arg0);
			this.second.TraceDebug<T0>(id, formatString, arg0);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000087E6 File Offset: 0x000069E6
		public void TraceDebug(long id, string message)
		{
			this.first.TraceDebug(id, message);
			this.second.TraceDebug(id, message);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008802 File Offset: 0x00006A02
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.first.TraceDebug(id, formatString, args);
			this.second.TraceDebug(id, formatString, args);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008820 File Offset: 0x00006A20
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.first.TraceWarning<T0>(id, formatString, arg0);
			this.second.TraceWarning<T0>(id, formatString, arg0);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000883E File Offset: 0x00006A3E
		public void TraceWarning(long id, string message)
		{
			this.first.TraceWarning(id, message);
			this.second.TraceWarning(id, message);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000885A File Offset: 0x00006A5A
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.first.TraceWarning(id, formatString, args);
			this.second.TraceWarning(id, formatString, args);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008878 File Offset: 0x00006A78
		public void TraceError(long id, string message)
		{
			this.first.TraceError(id, message);
			this.second.TraceError(id, message);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008894 File Offset: 0x00006A94
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.first.TraceError<T0>(id, formatString, arg0);
			this.second.TraceError<T0>(id, formatString, arg0);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000088B2 File Offset: 0x00006AB2
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.first.TraceError<T0, T1>(id, formatString, arg0, arg1);
			this.second.TraceError<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000088D4 File Offset: 0x00006AD4
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.first.TraceError<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
			this.second.TraceError<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000088FA File Offset: 0x00006AFA
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.first.TraceError(id, formatString, args);
			this.second.TraceError(id, formatString, args);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008918 File Offset: 0x00006B18
		public void TracePerformance(long id, string message)
		{
			this.first.TracePerformance(id, message);
			this.second.TracePerformance(id, message);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008934 File Offset: 0x00006B34
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.first.TracePerformance<T0>(id, formatString, arg0);
			this.second.TracePerformance<T0>(id, formatString, arg0);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008952 File Offset: 0x00006B52
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.first.TracePerformance<T0, T1>(id, formatString, arg0, arg1);
			this.second.TracePerformance<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008974 File Offset: 0x00006B74
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.first.TracePerformance<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
			this.second.TracePerformance<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000899A File Offset: 0x00006B9A
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.first.TracePerformance(id, formatString, args);
			this.second.TracePerformance(id, formatString, args);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000089B8 File Offset: 0x00006BB8
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			this.first.Dump(writer, addHeader, verbose);
			this.second.Dump(writer, addHeader, verbose);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000089D6 File Offset: 0x00006BD6
		public ITracer Compose(ITracer other)
		{
			if (other == null || NullTracer.Instance.Equals(other))
			{
				return this;
			}
			return new CompositeTracer(this, other);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000089F1 File Offset: 0x00006BF1
		public bool IsTraceEnabled(TraceType traceType)
		{
			return this.first.IsTraceEnabled(traceType) || this.second.IsTraceEnabled(traceType);
		}

		// Token: 0x04000252 RID: 594
		private ITracer first;

		// Token: 0x04000253 RID: 595
		private ITracer second;
	}
}
