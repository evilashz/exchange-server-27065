using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000447 RID: 1095
	internal sealed class EnumUInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x000CEDA4 File Offset: 0x000CCFA4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000CEDB4 File Offset: 0x000CCFB4
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<uint>.Cast<EnumType>(value));
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000CEDC7 File Offset: 0x000CCFC7
		public override object GetData(object value)
		{
			return value;
		}
	}
}
