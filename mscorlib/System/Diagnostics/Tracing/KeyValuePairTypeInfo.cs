using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000451 RID: 1105
	internal sealed class KeyValuePairTypeInfo<K, V> : TraceLoggingTypeInfo<KeyValuePair<K, V>>
	{
		// Token: 0x060035CF RID: 13775 RVA: 0x000CEFED File Offset: 0x000CD1ED
		public KeyValuePairTypeInfo(List<Type> recursionCheck)
		{
			this.keyInfo = TraceLoggingTypeInfo<K>.GetInstance(recursionCheck);
			this.valueInfo = TraceLoggingTypeInfo<V>.GetInstance(recursionCheck);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000CF010 File Offset: 0x000CD210
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector collector2 = collector.AddGroup(name);
			this.keyInfo.WriteMetadata(collector2, "Key", EventFieldFormat.Default);
			this.valueInfo.WriteMetadata(collector2, "Value", format);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000CF04C File Offset: 0x000CD24C
		public override void WriteData(TraceLoggingDataCollector collector, ref KeyValuePair<K, V> value)
		{
			K key = value.Key;
			V value2 = value.Value;
			this.keyInfo.WriteData(collector, ref key);
			this.valueInfo.WriteData(collector, ref value2);
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000CF084 File Offset: 0x000CD284
		public override object GetData(object value)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>)value;
			dictionary.Add("Key", this.keyInfo.GetData(keyValuePair.Key));
			dictionary.Add("Value", this.valueInfo.GetData(keyValuePair.Value));
			return dictionary;
		}

		// Token: 0x040017A1 RID: 6049
		private readonly TraceLoggingTypeInfo<K> keyInfo;

		// Token: 0x040017A2 RID: 6050
		private readonly TraceLoggingTypeInfo<V> valueInfo;
	}
}
