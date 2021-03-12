using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000452 RID: 1106
	internal sealed class NullableTypeInfo<T> : TraceLoggingTypeInfo<T?> where T : struct
	{
		// Token: 0x060035D3 RID: 13779 RVA: 0x000CF0E3 File Offset: 0x000CD2E3
		public NullableTypeInfo(List<Type> recursionCheck)
		{
			this.valueInfo = TraceLoggingTypeInfo<T>.GetInstance(recursionCheck);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000CF0F8 File Offset: 0x000CD2F8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000CF130 File Offset: 0x000CD330
		public override void WriteData(TraceLoggingDataCollector collector, ref T? value)
		{
			bool flag = value != null;
			collector.AddScalar(flag);
			T t = flag ? value.Value : default(T);
			this.valueInfo.WriteData(collector, ref t);
		}

		// Token: 0x040017A3 RID: 6051
		private readonly TraceLoggingTypeInfo<T> valueInfo;
	}
}
