using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000444 RID: 1092
	internal sealed class EnumInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060035A1 RID: 13729 RVA: 0x000CED1A File Offset: 0x000CCF1A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000CED2A File Offset: 0x000CCF2A
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<short>.Cast<EnumType>(value));
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000CED3D File Offset: 0x000CCF3D
		public override object GetData(object value)
		{
			return value;
		}
	}
}
