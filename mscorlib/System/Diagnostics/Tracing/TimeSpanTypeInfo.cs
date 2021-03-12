using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044F RID: 1103
	internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo<TimeSpan>
	{
		// Token: 0x060035C9 RID: 13769 RVA: 0x000CEF99 File Offset: 0x000CD199
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000CEFAA File Offset: 0x000CD1AA
		public override void WriteData(TraceLoggingDataCollector collector, ref TimeSpan value)
		{
			collector.AddScalar(value.Ticks);
		}
	}
}
