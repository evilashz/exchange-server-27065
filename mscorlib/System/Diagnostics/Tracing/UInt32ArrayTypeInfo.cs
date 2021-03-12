using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043A RID: 1082
	internal sealed class UInt32ArrayTypeInfo : TraceLoggingTypeInfo<uint[]>
	{
		// Token: 0x06003581 RID: 13697 RVA: 0x000CEB9E File Offset: 0x000CCD9E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000CEBAE File Offset: 0x000CCDAE
		public override void WriteData(TraceLoggingDataCollector collector, ref uint[] value)
		{
			collector.AddArray(value);
		}
	}
}
