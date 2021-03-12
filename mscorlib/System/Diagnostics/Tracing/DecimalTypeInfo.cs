using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000450 RID: 1104
	internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo<decimal>
	{
		// Token: 0x060035CC RID: 13772 RVA: 0x000CEFC0 File Offset: 0x000CD1C0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000CEFD1 File Offset: 0x000CD1D1
		public override void WriteData(TraceLoggingDataCollector collector, ref decimal value)
		{
			collector.AddScalar((double)value);
		}
	}
}
