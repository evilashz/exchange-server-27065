using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200042F RID: 1071
	internal sealed class IntPtrTypeInfo : TraceLoggingTypeInfo<IntPtr>
	{
		// Token: 0x06003560 RID: 13664 RVA: 0x000CE9AD File Offset: 0x000CCBAD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000CE9C1 File Offset: 0x000CCBC1
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr value)
		{
			collector.AddScalar(value);
		}
	}
}
