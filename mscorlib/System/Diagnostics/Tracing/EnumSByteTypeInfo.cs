using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000443 RID: 1091
	internal sealed class EnumSByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x0600359D RID: 13725 RVA: 0x000CECEC File Offset: 0x000CCEEC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000CECFC File Offset: 0x000CCEFC
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<sbyte>.Cast<EnumType>(value));
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000CED0F File Offset: 0x000CCF0F
		public override object GetData(object value)
		{
			return value;
		}
	}
}
