using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041D RID: 1053
	internal sealed class InvokeTypeInfo<ContainerType> : TraceLoggingTypeInfo<ContainerType>
	{
		// Token: 0x06003525 RID: 13605 RVA: 0x000CE278 File Offset: 0x000CC478
		public InvokeTypeInfo(TypeAnalysis typeAnalysis) : base(typeAnalysis.name, typeAnalysis.level, typeAnalysis.opcode, typeAnalysis.keywords, typeAnalysis.tags)
		{
			if (typeAnalysis.properties.Length != 0)
			{
				this.properties = typeAnalysis.properties;
				this.accessors = new PropertyAccessor<ContainerType>[this.properties.Length];
				for (int i = 0; i < this.accessors.Length; i++)
				{
					this.accessors[i] = PropertyAccessor<ContainerType>.Create(this.properties[i]);
				}
			}
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000CE2FC File Offset: 0x000CC4FC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					EventFieldFormat format2 = EventFieldFormat.Default;
					EventFieldAttribute fieldAttribute = propertyAnalysis.fieldAttribute;
					if (fieldAttribute != null)
					{
						traceLoggingMetadataCollector.Tags = fieldAttribute.Tags;
						format2 = fieldAttribute.Format;
					}
					propertyAnalysis.typeInfo.WriteMetadata(traceLoggingMetadataCollector, propertyAnalysis.name, format2);
				}
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000CE36C File Offset: 0x000CC56C
		public override void WriteData(TraceLoggingDataCollector collector, ref ContainerType value)
		{
			if (this.accessors != null)
			{
				foreach (PropertyAccessor<ContainerType> propertyAccessor in this.accessors)
				{
					propertyAccessor.Write(collector, ref value);
				}
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000CE3A4 File Offset: 0x000CC5A4
		public override object GetData(object value)
		{
			if (this.properties != null)
			{
				List<string> list = new List<string>();
				List<object> list2 = new List<object>();
				for (int i = 0; i < this.properties.Length; i++)
				{
					object data = this.accessors[i].GetData((ContainerType)((object)value));
					list.Add(this.properties[i].name);
					list2.Add(this.properties[i].typeInfo.GetData(data));
				}
				return new EventPayload(list, list2);
			}
			return null;
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000CE424 File Offset: 0x000CC624
		public override void WriteObjectData(TraceLoggingDataCollector collector, object valueObj)
		{
			if (this.accessors != null)
			{
				ContainerType containerType = (valueObj == null) ? default(ContainerType) : ((ContainerType)((object)valueObj));
				this.WriteData(collector, ref containerType);
			}
		}

		// Token: 0x0400178E RID: 6030
		private readonly PropertyAnalysis[] properties;

		// Token: 0x0400178F RID: 6031
		private readonly PropertyAccessor<ContainerType>[] accessors;
	}
}
