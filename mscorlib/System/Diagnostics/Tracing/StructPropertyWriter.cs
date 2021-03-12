using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000421 RID: 1057
	internal class StructPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x06003537 RID: 13623 RVA: 0x000CE678 File Offset: 0x000CC878
		public StructPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (StructPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(StructPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000CE6B4 File Offset: 0x000CC8B4
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = (container == null) ? default(ValueType) : this.getter(ref container);
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000CE6F4 File Offset: 0x000CC8F4
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(ref container);
		}

		// Token: 0x04001797 RID: 6039
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x04001798 RID: 6040
		private readonly StructPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000B67 RID: 2919
		// (Invoke) Token: 0x06006B73 RID: 27507
		private delegate ValueType Getter(ref ContainerType container);
	}
}
