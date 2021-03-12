using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x020000A2 RID: 162
	[DataContract]
	internal struct PropertyBag : IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000075E8 File Offset: 0x000057E8
		public static PropertyBag CreateInstance()
		{
			return new PropertyBag
			{
				propertyValues = new Dictionary<string, object>()
			};
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000760A File Offset: 0x0000580A
		public bool IsPropertySet(PropertyDefinition property)
		{
			return this.propertyValues.ContainsKey(property.Name);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00007620 File Offset: 0x00005820
		public TValue GetValueOrDefault<TValue>(TypedPropertyDefinition<TValue> typedProperty)
		{
			object obj;
			if (!this.propertyValues.TryGetValue(typedProperty.Name, out obj))
			{
				return typedProperty.DefaultValue;
			}
			return (TValue)((object)obj);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000764F File Offset: 0x0000584F
		public void SetValue<TValue>(TypedPropertyDefinition<TValue> typedProperty, TValue value)
		{
			this.propertyValues[typedProperty.Name] = value;
		}

		// Token: 0x040001F9 RID: 505
		[DataMember]
		private Dictionary<string, object> propertyValues;
	}
}
