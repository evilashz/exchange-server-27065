using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000437 RID: 1079
	internal sealed class Int16ArrayTypeInfo : TraceLoggingTypeInfo<short[]>
	{
		// Token: 0x06003578 RID: 13688 RVA: 0x000CEB38 File Offset: 0x000CCD38
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000CEB48 File Offset: 0x000CCD48
		public override void WriteData(TraceLoggingDataCollector collector, ref short[] value)
		{
			collector.AddArray(value);
		}
	}
}
