using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041F RID: 1055
	internal abstract class PropertyAccessor<ContainerType>
	{
		// Token: 0x06003530 RID: 13616
		public abstract void Write(TraceLoggingDataCollector collector, ref ContainerType value);

		// Token: 0x06003531 RID: 13617
		public abstract object GetData(ContainerType value);

		// Token: 0x06003532 RID: 13618 RVA: 0x000CE574 File Offset: 0x000CC774
		public static PropertyAccessor<ContainerType> Create(PropertyAnalysis property)
		{
			Type returnType = property.getterInfo.ReturnType;
			if (!Statics.IsValueType(typeof(ContainerType)))
			{
				if (returnType == typeof(int))
				{
					return new ClassPropertyWriter<ContainerType, int>(property);
				}
				if (returnType == typeof(long))
				{
					return new ClassPropertyWriter<ContainerType, long>(property);
				}
				if (returnType == typeof(string))
				{
					return new ClassPropertyWriter<ContainerType, string>(property);
				}
			}
			return new NonGenericProperytWriter<ContainerType>(property);
		}
	}
}
