using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044C RID: 1100
	internal sealed class GuidArrayTypeInfo : TraceLoggingTypeInfo<Guid[]>
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x000CEE9A File Offset: 0x000CD09A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000CEEAB File Offset: 0x000CD0AB
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid[] value)
		{
			collector.AddArray(value);
		}
	}
}
