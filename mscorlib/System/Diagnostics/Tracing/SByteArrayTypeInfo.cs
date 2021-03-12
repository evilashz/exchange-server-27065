using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000436 RID: 1078
	internal sealed class SByteArrayTypeInfo : TraceLoggingTypeInfo<sbyte[]>
	{
		// Token: 0x06003575 RID: 13685 RVA: 0x000CEB16 File Offset: 0x000CCD16
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000CEB26 File Offset: 0x000CCD26
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte[] value)
		{
			collector.AddArray(value);
		}
	}
}
