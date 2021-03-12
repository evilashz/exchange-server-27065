using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000449 RID: 1097
	internal sealed class EnumUInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060035B5 RID: 13749 RVA: 0x000CEE01 File Offset: 0x000CD001
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000CEE12 File Offset: 0x000CD012
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ulong>.Cast<EnumType>(value));
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000CEE25 File Offset: 0x000CD025
		public override object GetData(object value)
		{
			return value;
		}
	}
}
