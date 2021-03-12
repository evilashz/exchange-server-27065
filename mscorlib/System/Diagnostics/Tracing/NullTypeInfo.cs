using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000425 RID: 1061
	internal sealed class NullTypeInfo<DataType> : TraceLoggingTypeInfo<DataType>
	{
		// Token: 0x06003541 RID: 13633 RVA: 0x000CE85E File Offset: 0x000CCA5E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddGroup(name);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000CE868 File Offset: 0x000CCA68
		public override void WriteData(TraceLoggingDataCollector collector, ref DataType value)
		{
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000CE86A File Offset: 0x000CCA6A
		public override object GetData(object value)
		{
			return null;
		}
	}
}
