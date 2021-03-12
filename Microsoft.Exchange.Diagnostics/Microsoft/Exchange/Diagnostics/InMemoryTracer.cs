using System;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class InMemoryTracer : ITracer
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0000DD9D File Offset: 0x0000BF9D
		public InMemoryTracer(Guid component, int tag) : this(component, tag, 1024, 16384)
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
		public InMemoryTracer(Guid component, int tag, int maxEntries, int maxBufferSize)
		{
			this.component = component;
			this.tag = tag;
			this.builder = new MemoryTraceBuilder(maxEntries, maxBufferSize);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000DDD5 File Offset: 0x0000BFD5
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.AppendFormat<T0, T1, T2>(id, TraceType.DebugTrace, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000DDE5 File Offset: 0x0000BFE5
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.AppendFormat<T0, T1>(id, TraceType.DebugTrace, formatString, arg0, arg1);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000DDF3 File Offset: 0x0000BFF3
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.AppendFormat<T0>(id, TraceType.DebugTrace, formatString, arg0);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000DDFF File Offset: 0x0000BFFF
		public void TraceDebug(long id, string message)
		{
			this.Append(id, TraceType.DebugTrace, message);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DE0A File Offset: 0x0000C00A
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.AppendFormat(id, TraceType.DebugTrace, formatString, args);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DE16 File Offset: 0x0000C016
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.AppendFormat<T0>(id, TraceType.WarningTrace, formatString, arg0);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DE22 File Offset: 0x0000C022
		public void TraceWarning(long id, string message)
		{
			this.Append(id, TraceType.WarningTrace, message);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DE2D File Offset: 0x0000C02D
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.AppendFormat(id, TraceType.WarningTrace, formatString, args);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DE39 File Offset: 0x0000C039
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.AppendFormat<T0, T1, T2>(id, TraceType.ErrorTrace, formatString, arg0, arg1, arg2);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DE49 File Offset: 0x0000C049
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.AppendFormat<T0, T1>(id, TraceType.ErrorTrace, formatString, arg0, arg1);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DE57 File Offset: 0x0000C057
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.AppendFormat<T0>(id, TraceType.ErrorTrace, formatString, arg0);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DE63 File Offset: 0x0000C063
		public void TraceError(long id, string message)
		{
			this.Append(id, TraceType.ErrorTrace, message);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DE6E File Offset: 0x0000C06E
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.AppendFormat(id, TraceType.ErrorTrace, formatString, args);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DE7A File Offset: 0x0000C07A
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.AppendFormat<T0, T1, T2>(id, TraceType.PerformanceTrace, formatString, arg0, arg1, arg2);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000DE8A File Offset: 0x0000C08A
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.AppendFormat<T0, T1>(id, TraceType.PerformanceTrace, formatString, arg0, arg1);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000DE98 File Offset: 0x0000C098
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.AppendFormat<T0>(id, TraceType.PerformanceTrace, formatString, arg0);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public void TracePerformance(long id, string message)
		{
			this.Append(id, TraceType.PerformanceTrace, message);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DEAF File Offset: 0x0000C0AF
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.AppendFormat(id, TraceType.PerformanceTrace, formatString, args);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			this.builder.Dump(writer, addHeader, verbose);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public void Dump(ITraceEntryWriter writer)
		{
			ArgumentValidator.ThrowIfNull("writer", writer);
			foreach (TraceEntry entry in this.builder.GetTraces())
			{
				writer.Write(entry);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000DF2C File Offset: 0x0000C12C
		public ITracer Compose(ITracer other)
		{
			if (other == null || NullTracer.Instance.Equals(other))
			{
				return this;
			}
			return new CompositeTracer(this, other);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DF47 File Offset: 0x0000C147
		public bool IsTraceEnabled(TraceType traceType)
		{
			return true;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DF4A File Offset: 0x0000C14A
		private void Append(long id, TraceType type, string message)
		{
			this.builder.BeginEntry(type, this.component, this.tag, id, message);
			this.builder.EndEntry();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DF74 File Offset: 0x0000C174
		private void AppendFormat(long id, TraceType type, string formatString, params object[] args)
		{
			if (args == null)
			{
				return;
			}
			this.builder.BeginEntry(type, this.component, this.tag, id, formatString);
			for (int i = 0; i < args.Length; i++)
			{
				this.builder.AddArgument<object>(args[i]);
			}
			this.builder.EndEntry();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DFC9 File Offset: 0x0000C1C9
		private void AppendFormat<T0>(long id, TraceType type, string formatString, T0 arg0)
		{
			this.builder.BeginEntry(type, this.component, this.tag, id, formatString);
			this.builder.AddArgument<T0>(arg0);
			this.builder.EndEntry();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E000 File Offset: 0x0000C200
		private void AppendFormat<T0, T1>(long id, TraceType type, string formatString, T0 arg0, T1 arg1)
		{
			this.builder.BeginEntry(type, this.component, this.tag, id, formatString);
			this.builder.AddArgument<T0>(arg0);
			this.builder.AddArgument<T1>(arg1);
			this.builder.EndEntry();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E04C File Offset: 0x0000C24C
		private void AppendFormat<T0, T1, T2>(long id, TraceType type, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.builder.BeginEntry(type, this.component, this.tag, id, formatString);
			this.builder.AddArgument<T0>(arg0);
			this.builder.AddArgument<T1>(arg1);
			this.builder.AddArgument<T2>(arg2);
			this.builder.EndEntry();
		}

		// Token: 0x0400031E RID: 798
		private const int DefaultMaxEntries = 1024;

		// Token: 0x0400031F RID: 799
		private const int DefaultMaxBufferSize = 16384;

		// Token: 0x04000320 RID: 800
		private readonly Guid component;

		// Token: 0x04000321 RID: 801
		private readonly int tag;

		// Token: 0x04000322 RID: 802
		private readonly MemoryTraceBuilder builder;
	}
}
