using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000432 RID: 1074
	internal sealed class SingleTypeInfo : TraceLoggingTypeInfo<float>
	{
		// Token: 0x06003569 RID: 13673 RVA: 0x000CEA1C File Offset: 0x000CCC1C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000CEA2D File Offset: 0x000CCC2D
		public override void WriteData(TraceLoggingDataCollector collector, ref float value)
		{
			collector.AddScalar(value);
		}
	}
}
