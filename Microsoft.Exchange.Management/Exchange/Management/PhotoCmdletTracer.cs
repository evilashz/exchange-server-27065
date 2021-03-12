using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02000D76 RID: 3446
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoCmdletTracer : ITracer
	{
		// Token: 0x06008436 RID: 33846 RVA: 0x0021CD73 File Offset: 0x0021AF73
		public PhotoCmdletTracer(bool tracingEnabled)
		{
			if (!tracingEnabled)
			{
				this.output = NullTracer.Instance;
				return;
			}
			this.output = new InMemoryTracer(ExTraceGlobals.UserPhotosTracer.Category, ExTraceGlobals.UserPhotosTracer.TraceTag);
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x0021CDA9 File Offset: 0x0021AFA9
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.output.TraceDebug(id, string.Format(formatString, arg0));
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x0021CDC3 File Offset: 0x0021AFC3
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.output.TraceDebug(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x0021CDE4 File Offset: 0x0021AFE4
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.output.TraceDebug(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x0600843A RID: 33850 RVA: 0x0021CE0C File Offset: 0x0021B00C
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.output.TraceDebug(id, string.Format(formatString, args));
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x0021CE21 File Offset: 0x0021B021
		public void TraceDebug(long id, string message)
		{
			this.output.TraceDebug(id, message);
		}

		// Token: 0x0600843C RID: 33852 RVA: 0x0021CE30 File Offset: 0x0021B030
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.output.TraceWarning(id, string.Format(formatString, arg0));
		}

		// Token: 0x0600843D RID: 33853 RVA: 0x0021CE4A File Offset: 0x0021B04A
		public void TraceWarning(long id, string message)
		{
			this.output.TraceWarning(id, message);
		}

		// Token: 0x0600843E RID: 33854 RVA: 0x0021CE59 File Offset: 0x0021B059
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.output.TraceWarning(id, string.Format(formatString, args));
		}

		// Token: 0x0600843F RID: 33855 RVA: 0x0021CE6E File Offset: 0x0021B06E
		public void TraceError(long id, string message)
		{
			this.output.TraceError(id, message);
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x0021CE7D File Offset: 0x0021B07D
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.output.TraceError(id, string.Format(formatString, args));
		}

		// Token: 0x06008441 RID: 33857 RVA: 0x0021CE92 File Offset: 0x0021B092
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.output.TraceError(id, string.Format(formatString, arg0));
		}

		// Token: 0x06008442 RID: 33858 RVA: 0x0021CEAC File Offset: 0x0021B0AC
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.output.TraceError(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06008443 RID: 33859 RVA: 0x0021CECD File Offset: 0x0021B0CD
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.output.TraceError(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x0021CEF5 File Offset: 0x0021B0F5
		public void TracePerformance(long id, string message)
		{
			this.output.TracePerformance(id, message);
		}

		// Token: 0x06008445 RID: 33861 RVA: 0x0021CF04 File Offset: 0x0021B104
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.output.TracePerformance(id, string.Format(formatString, args));
		}

		// Token: 0x06008446 RID: 33862 RVA: 0x0021CF19 File Offset: 0x0021B119
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.output.TracePerformance(id, string.Format(formatString, arg0));
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x0021CF33 File Offset: 0x0021B133
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.output.TracePerformance(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x0021CF54 File Offset: 0x0021B154
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.output.TracePerformance(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06008449 RID: 33865 RVA: 0x0021CF7C File Offset: 0x0021B17C
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600844A RID: 33866 RVA: 0x0021CF83 File Offset: 0x0021B183
		public ITracer Compose(ITracer other)
		{
			return new CompositeTracer(this, other);
		}

		// Token: 0x0600844B RID: 33867 RVA: 0x0021CF8C File Offset: 0x0021B18C
		public bool IsTraceEnabled(TraceType traceType)
		{
			return this.output.IsTraceEnabled(traceType);
		}

		// Token: 0x0600844C RID: 33868 RVA: 0x0021CF9A File Offset: 0x0021B19A
		public void Dump(ITraceEntryWriter writer)
		{
			ArgumentValidator.ThrowIfNull("writer", writer);
			if (NullTracer.Instance.Equals(this.output))
			{
				return;
			}
			((InMemoryTracer)this.output).Dump(writer);
		}

		// Token: 0x04004012 RID: 16402
		private readonly ITracer output;
	}
}
