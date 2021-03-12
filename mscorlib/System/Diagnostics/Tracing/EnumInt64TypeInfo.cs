using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000448 RID: 1096
	internal sealed class EnumInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060035B1 RID: 13745 RVA: 0x000CEDD2 File Offset: 0x000CCFD2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000CEDE3 File Offset: 0x000CCFE3
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<long>.Cast<EnumType>(value));
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000CEDF6 File Offset: 0x000CCFF6
		public override object GetData(object value)
		{
			return value;
		}
	}
}
