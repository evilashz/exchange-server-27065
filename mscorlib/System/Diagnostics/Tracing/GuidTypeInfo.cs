using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044B RID: 1099
	internal sealed class GuidTypeInfo : TraceLoggingTypeInfo<Guid>
	{
		// Token: 0x060035BD RID: 13757 RVA: 0x000CEE73 File Offset: 0x000CD073
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000CEE84 File Offset: 0x000CD084
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid value)
		{
			collector.AddScalar(value);
		}
	}
}
