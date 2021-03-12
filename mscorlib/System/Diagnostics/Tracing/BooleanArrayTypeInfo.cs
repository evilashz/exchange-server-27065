using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000434 RID: 1076
	internal sealed class BooleanArrayTypeInfo : TraceLoggingTypeInfo<bool[]>
	{
		// Token: 0x0600356F RID: 13679 RVA: 0x000CEA65 File Offset: 0x000CCC65
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000CEA79 File Offset: 0x000CCC79
		public override void WriteData(TraceLoggingDataCollector collector, ref bool[] value)
		{
			collector.AddArray(value);
		}
	}
}
