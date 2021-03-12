using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000441 RID: 1089
	internal sealed class SingleArrayTypeInfo : TraceLoggingTypeInfo<float[]>
	{
		// Token: 0x06003596 RID: 13718 RVA: 0x000CEC9B File Offset: 0x000CCE9B
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000CECAC File Offset: 0x000CCEAC
		public override void WriteData(TraceLoggingDataCollector collector, ref float[] value)
		{
			collector.AddArray(value);
		}
	}
}
