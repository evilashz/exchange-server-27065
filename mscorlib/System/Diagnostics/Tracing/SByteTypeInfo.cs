using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000428 RID: 1064
	internal sealed class SByteTypeInfo : TraceLoggingTypeInfo<sbyte>
	{
		// Token: 0x0600354B RID: 13643 RVA: 0x000CE8BD File Offset: 0x000CCABD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000CE8CD File Offset: 0x000CCACD
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte value)
		{
			collector.AddScalar(value);
		}
	}
}
