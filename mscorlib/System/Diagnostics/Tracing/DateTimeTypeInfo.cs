using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044D RID: 1101
	internal sealed class DateTimeTypeInfo : TraceLoggingTypeInfo<DateTime>
	{
		// Token: 0x060035C3 RID: 13763 RVA: 0x000CEEBD File Offset: 0x000CD0BD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000CEED0 File Offset: 0x000CD0D0
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTime value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
		}
	}
}
