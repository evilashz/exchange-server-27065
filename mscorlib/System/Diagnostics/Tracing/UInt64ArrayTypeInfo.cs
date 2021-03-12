using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043C RID: 1084
	internal sealed class UInt64ArrayTypeInfo : TraceLoggingTypeInfo<ulong[]>
	{
		// Token: 0x06003587 RID: 13703 RVA: 0x000CEBE3 File Offset: 0x000CCDE3
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000CEBF4 File Offset: 0x000CCDF4
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong[] value)
		{
			collector.AddArray(value);
		}
	}
}
