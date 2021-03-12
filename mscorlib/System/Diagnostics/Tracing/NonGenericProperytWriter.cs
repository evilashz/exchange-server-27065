using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000420 RID: 1056
	internal class NonGenericProperytWriter<ContainerType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x06003534 RID: 13620 RVA: 0x000CE5F7 File Offset: 0x000CC7F7
		public NonGenericProperytWriter(PropertyAnalysis property)
		{
			this.getterInfo = property.getterInfo;
			this.typeInfo = property.typeInfo;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000CE618 File Offset: 0x000CC818
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			object value = (container == null) ? null : this.getterInfo.Invoke(container, null);
			this.typeInfo.WriteObjectData(collector, value);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000CE65A File Offset: 0x000CC85A
		public override object GetData(ContainerType container)
		{
			if (container != null)
			{
				return this.getterInfo.Invoke(container, null);
			}
			return null;
		}

		// Token: 0x04001795 RID: 6037
		private readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x04001796 RID: 6038
		private readonly MethodInfo getterInfo;
	}
}
