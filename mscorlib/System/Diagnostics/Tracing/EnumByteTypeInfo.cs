using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000442 RID: 1090
	internal sealed class EnumByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x06003599 RID: 13721 RVA: 0x000CECBE File Offset: 0x000CCEBE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000CECCE File Offset: 0x000CCECE
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<byte>.Cast<EnumType>(value));
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000CECE1 File Offset: 0x000CCEE1
		public override object GetData(object value)
		{
			return value;
		}
	}
}
