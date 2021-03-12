using System;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Tracking;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000289 RID: 649
	internal class TraceWrapper
	{
		// Token: 0x06001274 RID: 4724 RVA: 0x00055820 File Offset: 0x00053A20
		private TraceWrapper()
		{
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00055828 File Offset: 0x00053A28
		public static TraceWrapper SearchLibraryTracer
		{
			get
			{
				return TraceWrapper.searchLibraryTracer;
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0005582F File Offset: 0x00053A2F
		public void Register(TraceWrapper.ITraceWriter traceWriter)
		{
			TraceWrapper.traceWriter = traceWriter;
			Interlocked.Increment(ref this.enabledCount);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00055843 File Offset: 0x00053A43
		public void Unregister()
		{
			Interlocked.Decrement(ref this.enabledCount);
			TraceWrapper.traceWriter = null;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00055858 File Offset: 0x00053A58
		public void TraceDebug<T0>(int id, string formatString, T0 arg0)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Debug", id, formatString, new object[]
				{
					arg0
				});
			}
			ExTraceGlobals.SearchLibraryTracer.TraceDebug<T0>((long)id, formatString, arg0);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000558A4 File Offset: 0x00053AA4
		public void TraceDebug<T0, T1>(int id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Debug", id, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			ExTraceGlobals.SearchLibraryTracer.TraceDebug<T0, T1>((long)id, formatString, arg0, arg1);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000558FC File Offset: 0x00053AFC
		public void TraceDebug<T0, T1, T2>(int id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Debug", id, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			ExTraceGlobals.SearchLibraryTracer.TraceDebug<T0, T1, T2>((long)id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0005595D File Offset: 0x00053B5D
		public void TraceDebug(int id, string formatString, params object[] args)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Debug", id, formatString, args);
			}
			ExTraceGlobals.SearchLibraryTracer.TraceDebug((long)id, formatString, args);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0005598C File Offset: 0x00053B8C
		public void TraceError<T0>(int id, string formatString, T0 arg0)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Error", id, formatString, new object[]
				{
					arg0
				});
			}
			ExTraceGlobals.SearchLibraryTracer.TraceError<T0>((long)id, formatString, arg0);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000559D8 File Offset: 0x00053BD8
		public void TraceError<T0, T1>(int id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Error", id, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			ExTraceGlobals.SearchLibraryTracer.TraceError<T0, T1>((long)id, formatString, arg0, arg1);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00055A30 File Offset: 0x00053C30
		public void TraceError<T0, T1, T2>(int id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Error", id, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			ExTraceGlobals.SearchLibraryTracer.TraceError<T0, T1, T2>((long)id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00055A91 File Offset: 0x00053C91
		public void TraceError(int id, string formatString, params object[] args)
		{
			if (this.enabledCount > 0 && TraceWrapper.traceWriter != null)
			{
				this.RedirectTrace("Error", id, formatString, args);
			}
			ExTraceGlobals.SearchLibraryTracer.TraceError((long)id, formatString, args);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00055AC0 File Offset: 0x00053CC0
		private void RedirectTrace(string traceTypePrefix, int id, string formatString, params object[] args)
		{
			string value = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
			int capacity = traceTypePrefix.Length + formatString.Length + args.Length * 10;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append(traceTypePrefix);
			stringBuilder.Append(", ");
			stringBuilder.Append(value);
			stringBuilder.Append(", ");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, formatString, args);
			TraceWrapper.traceWriter.Write(stringBuilder.ToString());
		}

		// Token: 0x04000C11 RID: 3089
		private const string DebugPrefix = "Debug";

		// Token: 0x04000C12 RID: 3090
		private const string ErrorPrefix = "Error";

		// Token: 0x04000C13 RID: 3091
		private static readonly TraceWrapper searchLibraryTracer = new TraceWrapper();

		// Token: 0x04000C14 RID: 3092
		[ThreadStatic]
		private static TraceWrapper.ITraceWriter traceWriter;

		// Token: 0x04000C15 RID: 3093
		private int enabledCount;

		// Token: 0x0200028A RID: 650
		internal interface ITraceWriter
		{
			// Token: 0x06001282 RID: 4738
			void Write(string message);
		}
	}
}
