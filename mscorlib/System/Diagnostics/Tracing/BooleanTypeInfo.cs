using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000426 RID: 1062
	internal sealed class BooleanTypeInfo : TraceLoggingTypeInfo<bool>
	{
		// Token: 0x06003545 RID: 13637 RVA: 0x000CE875 File Offset: 0x000CCA75
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000CE889 File Offset: 0x000CCA89
		public override void WriteData(TraceLoggingDataCollector collector, ref bool value)
		{
			collector.AddScalar(value);
		}
	}
}
