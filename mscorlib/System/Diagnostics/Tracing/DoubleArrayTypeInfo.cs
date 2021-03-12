using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000440 RID: 1088
	internal sealed class DoubleArrayTypeInfo : TraceLoggingTypeInfo<double[]>
	{
		// Token: 0x06003593 RID: 13715 RVA: 0x000CEC78 File Offset: 0x000CCE78
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000CEC89 File Offset: 0x000CCE89
		public override void WriteData(TraceLoggingDataCollector collector, ref double[] value)
		{
			collector.AddArray(value);
		}
	}
}
