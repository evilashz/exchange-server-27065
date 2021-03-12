using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000422 RID: 1058
	internal class ClassPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x0600353A RID: 13626 RVA: 0x000CE726 File Offset: 0x000CC926
		public ClassPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (ClassPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(ClassPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000CE760 File Offset: 0x000CC960
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = (container == null) ? default(ValueType) : this.getter(container);
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000CE7A8 File Offset: 0x000CC9A8
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(container);
		}

		// Token: 0x04001799 RID: 6041
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x0400179A RID: 6042
		private readonly ClassPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000B68 RID: 2920
		// (Invoke) Token: 0x06006B77 RID: 27511
		private delegate ValueType Getter(ContainerType container);
	}
}
