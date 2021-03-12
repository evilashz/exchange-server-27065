using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000092 RID: 146
	public static class ExTraceInternal
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000BC4C File Offset: 0x00009E4C
		public static void Trace(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string message)
		{
			if (ExTraceConfiguration.Instance.ConsoleTracingEnabled)
			{
				Console.WriteLine("{0}: {1}", id, message);
			}
			if (ExTraceConfiguration.Instance.SystemDiagnosticsTracingEnabled)
			{
				System.Diagnostics.Trace.WriteLine(string.Format("{0}: {1}", id, message));
			}
			if (ETWTrace.IsEnabled)
			{
				ETWTrace.Write(lid, traceType, componentGuid, traceTag, id, message);
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public static void Trace<T0>(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, T0 argument0)
		{
			if (ExTraceConfiguration.Instance.ConsoleTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}: ", id);
				stringBuilder.AppendFormat(format, argument0);
				Console.WriteLine(stringBuilder.ToString());
			}
			if (ExTraceConfiguration.Instance.SystemDiagnosticsTracingEnabled)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("{0}: ", id);
				stringBuilder2.AppendFormat(format, argument0);
				System.Diagnostics.Trace.WriteLine(stringBuilder2.ToString());
			}
			if (ETWTrace.IsEnabled)
			{
				string message;
				try
				{
					message = string.Format(ExTraceInternal.formatProvider, format, new object[]
					{
						argument0
					});
				}
				catch (FormatException ex)
				{
					message = ex.ToString();
				}
				ETWTrace.Write(lid, traceType, componentGuid, traceTag, id, message);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BD90 File Offset: 0x00009F90
		public static void Trace<T0, T1>(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, T0 argument0, T1 argument1)
		{
			if (ExTraceConfiguration.Instance.ConsoleTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}: ", id);
				stringBuilder.AppendFormat(format, argument0, argument1);
				Console.WriteLine(stringBuilder.ToString());
			}
			if (ExTraceConfiguration.Instance.SystemDiagnosticsTracingEnabled)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("{0}: ", id);
				stringBuilder2.AppendFormat(format, argument0, argument1);
				System.Diagnostics.Trace.WriteLine(stringBuilder2.ToString());
			}
			if (ETWTrace.IsEnabled)
			{
				string message;
				try
				{
					message = string.Format(ExTraceInternal.formatProvider, format, new object[]
					{
						argument0,
						argument1
					});
				}
				catch (FormatException ex)
				{
					message = ex.ToString();
				}
				ETWTrace.Write(lid, traceType, componentGuid, traceTag, id, message);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BE88 File Offset: 0x0000A088
		public static void Trace<T0, T1, T2>(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, T0 argument0, T1 argument1, T2 argument2)
		{
			if (ExTraceConfiguration.Instance.ConsoleTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}: ", id);
				stringBuilder.AppendFormat(format, argument0, argument1, argument2);
				Console.WriteLine(stringBuilder.ToString());
			}
			if (ExTraceConfiguration.Instance.SystemDiagnosticsTracingEnabled)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("{0}: ", id);
				stringBuilder2.AppendFormat(format, argument0, argument1, argument2);
				System.Diagnostics.Trace.WriteLine(stringBuilder2.ToString());
			}
			if (ETWTrace.IsEnabled)
			{
				string message;
				try
				{
					message = string.Format(ExTraceInternal.formatProvider, format, new object[]
					{
						argument0,
						argument1,
						argument2
					});
				}
				catch (FormatException ex)
				{
					message = ex.ToString();
				}
				ETWTrace.Write(lid, traceType, componentGuid, traceTag, id, message);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BF98 File Offset: 0x0000A198
		public static void Trace(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, object[] arguments)
		{
			if (arguments == null)
			{
				return;
			}
			if (ExTraceConfiguration.Instance.ConsoleTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}: ", id);
				stringBuilder.AppendFormat(format, arguments);
				Console.WriteLine(stringBuilder.ToString());
			}
			if (ExTraceConfiguration.Instance.SystemDiagnosticsTracingEnabled)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.AppendFormat("{0}: ", id);
				stringBuilder2.AppendFormat(format, arguments);
				System.Diagnostics.Trace.WriteLine(stringBuilder2.ToString());
			}
			if (ETWTrace.IsEnabled)
			{
				string message;
				try
				{
					message = string.Format(ExTraceInternal.formatProvider, format, arguments);
				}
				catch (FormatException ex)
				{
					message = ex.ToString();
				}
				ETWTrace.Write(lid, traceType, componentGuid, traceTag, id, message);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C060 File Offset: 0x0000A260
		public static void TraceInMemory(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string message)
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			bool flag = false;
			try
			{
				if (!memoryTraceBuilder.InsideTraceCall)
				{
					memoryTraceBuilder.BeginEntry(traceType, componentGuid, traceTag, id, message);
					memoryTraceBuilder.EndEntry();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					memoryTraceBuilder.Reset();
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public static void TraceInMemory<T0>(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, T0 argument0)
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			bool flag = false;
			try
			{
				if (!memoryTraceBuilder.InsideTraceCall)
				{
					memoryTraceBuilder.BeginEntry(traceType, componentGuid, traceTag, id, format);
					memoryTraceBuilder.AddArgument<T0>(argument0);
					memoryTraceBuilder.EndEntry();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					memoryTraceBuilder.Reset();
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C108 File Offset: 0x0000A308
		public static void TraceInMemory<T0, T1>(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, T0 argument0, T1 argument1)
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			bool flag = false;
			try
			{
				if (!memoryTraceBuilder.InsideTraceCall)
				{
					memoryTraceBuilder.BeginEntry(traceType, componentGuid, traceTag, id, format);
					memoryTraceBuilder.AddArgument<T0>(argument0);
					memoryTraceBuilder.AddArgument<T1>(argument1);
					memoryTraceBuilder.EndEntry();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					memoryTraceBuilder.Reset();
				}
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C168 File Offset: 0x0000A368
		public static void TraceInMemory<T0, T1, T2>(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, T0 argument0, T1 argument1, T2 argument2)
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			bool flag = false;
			try
			{
				if (!memoryTraceBuilder.InsideTraceCall)
				{
					memoryTraceBuilder.BeginEntry(traceType, componentGuid, traceTag, id, format);
					memoryTraceBuilder.AddArgument<T0>(argument0);
					memoryTraceBuilder.AddArgument<T1>(argument1);
					memoryTraceBuilder.AddArgument<T2>(argument2);
					memoryTraceBuilder.EndEntry();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					memoryTraceBuilder.Reset();
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public static void TraceInMemory(int lid, TraceType traceType, Guid componentGuid, int traceTag, long id, string format, object[] arguments)
		{
			if (arguments == null)
			{
				return;
			}
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			bool flag = false;
			try
			{
				if (!memoryTraceBuilder.InsideTraceCall)
				{
					memoryTraceBuilder.BeginEntry(traceType, componentGuid, traceTag, id, format);
					for (int i = 0; i < arguments.Length; i++)
					{
						ExTraceInternal.AddTraceArgument(memoryTraceBuilder, arguments[i]);
					}
					memoryTraceBuilder.EndEntry();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					memoryTraceBuilder.Reset();
				}
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C23C File Offset: 0x0000A43C
		public static List<KeyValuePair<TraceEntry, List<object>>> GetMemoryTrace()
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.GetMemoryTraceBuilder();
			if (memoryTraceBuilder != null)
			{
				return memoryTraceBuilder.GetTraceEntries();
			}
			return null;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C25C File Offset: 0x0000A45C
		public static void DumpMemoryTrace(TextWriter writer)
		{
			if (writer == null)
			{
				return;
			}
			bool addHeader = true;
			lock (ExTraceInternal.traceBuilderList)
			{
				writer.WriteLine("ThreadId;ComponentGuid;Instance ID;TraceTag;TraceType;TimeStamp;Message;");
				foreach (WeakReference weakReference in ExTraceInternal.traceBuilderList)
				{
					MemoryTraceBuilder memoryTraceBuilder = weakReference.Target as MemoryTraceBuilder;
					if (memoryTraceBuilder != null)
					{
						memoryTraceBuilder.Dump(writer, addHeader, true);
						addHeader = false;
					}
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000C300 File Offset: 0x0000A500
		internal static bool AreAnyTraceProvidersEnabled
		{
			get
			{
				return ETWTrace.IsEnabled || ExTraceConfiguration.Instance.InMemoryTracingEnabled || ExTraceConfiguration.Instance.ConsoleTracingEnabled || ExTraceConfiguration.Instance.SystemDiagnosticsTracingEnabled || ExTraceConfiguration.Instance.FaultInjectionConfiguration.Count > 0;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C34C File Offset: 0x0000A54C
		internal static void AddTraceArgument(MemoryTraceBuilder builder, object argument)
		{
			if (argument == null)
			{
				builder.AddArgument(string.Empty);
				return;
			}
			if (argument is int)
			{
				builder.AddArgument((int)argument);
				return;
			}
			if (argument is long)
			{
				builder.AddArgument((long)argument);
				return;
			}
			if (argument is Guid)
			{
				builder.AddArgument((Guid)argument);
				return;
			}
			ITraceable traceable = argument as ITraceable;
			if (traceable != null)
			{
				builder.AddArgument<ITraceable>(traceable);
				return;
			}
			builder.AddArgument<object>(argument);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		internal static MemoryTraceBuilder GetMemoryTraceBuilder()
		{
			MemoryTraceBuilder memoryTraceBuilder = ExTraceInternal.memoryTraceBuilder;
			if (memoryTraceBuilder != null)
			{
				return memoryTraceBuilder;
			}
			return ExTraceInternal.CreateMemoryTraceBuilder();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		private static MemoryTraceBuilder CreateMemoryTraceBuilder()
		{
			MemoryTraceBuilder memoryTraceBuilder = new MemoryTraceBuilder(1000, 64000);
			lock (ExTraceInternal.traceBuilderList)
			{
				ExTraceInternal.traceBuilderList.RemoveAll((WeakReference reference) => reference.Target == null);
				ExTraceInternal.traceBuilderList.Add(new WeakReference(memoryTraceBuilder));
			}
			ExTraceInternal.memoryTraceBuilder = memoryTraceBuilder;
			return memoryTraceBuilder;
		}

		// Token: 0x04000305 RID: 773
		private const int TraceBufferSize = 64000;

		// Token: 0x04000306 RID: 774
		private const int MaximumTraceEntries = 1000;

		// Token: 0x04000307 RID: 775
		private static ExFormatProvider formatProvider = new ExFormatProvider();

		// Token: 0x04000308 RID: 776
		[ThreadStatic]
		private static MemoryTraceBuilder memoryTraceBuilder;

		// Token: 0x04000309 RID: 777
		private static List<WeakReference> traceBuilderList = new List<WeakReference>(128);
	}
}
