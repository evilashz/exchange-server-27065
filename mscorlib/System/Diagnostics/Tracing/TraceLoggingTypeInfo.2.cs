using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045A RID: 1114
	internal abstract class TraceLoggingTypeInfo<DataType> : TraceLoggingTypeInfo
	{
		// Token: 0x06003641 RID: 13889 RVA: 0x000D0785 File Offset: 0x000CE985
		protected TraceLoggingTypeInfo() : base(typeof(DataType))
		{
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000D0797 File Offset: 0x000CE997
		protected TraceLoggingTypeInfo(string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags) : base(typeof(DataType), name, level, opcode, keywords, tags)
		{
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000D07B0 File Offset: 0x000CE9B0
		public static TraceLoggingTypeInfo<DataType> Instance
		{
			get
			{
				return TraceLoggingTypeInfo<DataType>.instance ?? TraceLoggingTypeInfo<DataType>.InitInstance();
			}
		}

		// Token: 0x06003644 RID: 13892
		public abstract void WriteData(TraceLoggingDataCollector collector, ref DataType value);

		// Token: 0x06003645 RID: 13893 RVA: 0x000D07C0 File Offset: 0x000CE9C0
		public override void WriteObjectData(TraceLoggingDataCollector collector, object value)
		{
			DataType dataType = (value == null) ? default(DataType) : ((DataType)((object)value));
			this.WriteData(collector, ref dataType);
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000D07EC File Offset: 0x000CE9EC
		internal static TraceLoggingTypeInfo<DataType> GetInstance(List<Type> recursionCheck)
		{
			if (TraceLoggingTypeInfo<DataType>.instance == null)
			{
				int count = recursionCheck.Count;
				TraceLoggingTypeInfo<DataType> value = Statics.CreateDefaultTypeInfo<DataType>(recursionCheck);
				Interlocked.CompareExchange<TraceLoggingTypeInfo<DataType>>(ref TraceLoggingTypeInfo<DataType>.instance, value, null);
				recursionCheck.RemoveRange(count, recursionCheck.Count - count);
			}
			return TraceLoggingTypeInfo<DataType>.instance;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000D082F File Offset: 0x000CEA2F
		private static TraceLoggingTypeInfo<DataType> InitInstance()
		{
			return TraceLoggingTypeInfo<DataType>.GetInstance(new List<Type>());
		}

		// Token: 0x040017F0 RID: 6128
		private static TraceLoggingTypeInfo<DataType> instance;
	}
}
