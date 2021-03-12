using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x0200001B RID: 27
	internal class MdbOneToOneTransformPropertyMapping : MdbOneToOnePropertyMapping
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000422C File Offset: 0x0000242C
		public MdbOneToOneTransformPropertyMapping(PropertyDefinition propertyDefinition, StorePropertyDefinition storePropertyDefinition, MdbOneToOneTransformPropertyMapping.TransformDelegate getterTransform, MdbOneToOneTransformPropertyMapping.TransformDelegate setterTransform) : base(propertyDefinition, storePropertyDefinition, (getterTransform != null) ? new MdbOneToOnePropertyMapping.ItemGetterDelegate(MdbOneToOnePropertyMapping.DefaultItemGetter) : null, (setterTransform != null) ? new MdbOneToOnePropertyMapping.ItemSetterDelegate(MdbOneToOnePropertyMapping.DefaultItemSetter) : null, (getterTransform != null) ? new MdbOneToOnePropertyMapping.DictionaryGetterDelegate(MdbOneToOnePropertyMapping.DefaultDictionaryGetter) : null, (setterTransform != null) ? new MdbOneToOnePropertyMapping.DictionarySetterDelegate(MdbOneToOnePropertyMapping.DefaultDictionarySetter) : null)
		{
			if (getterTransform == null && setterTransform == null)
			{
				throw new ArgumentException("Both getter and setter transforms are null");
			}
			this.GetterTransform = getterTransform;
			this.SetterTransform = setterTransform;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000042AC File Offset: 0x000024AC
		public MdbOneToOneTransformPropertyMapping(PropertyDefinition propertyDefinition, StorePropertyDefinition storePropertyDefinition, MdbOneToOnePropertyMapping.ItemGetterDelegate itemGetter, MdbOneToOnePropertyMapping.ItemSetterDelegate itemSetter) : base(propertyDefinition, storePropertyDefinition, itemGetter, itemSetter, null, null)
		{
			if (itemGetter == null && itemSetter == null)
			{
				throw new ArgumentException("Both getter and setter delegates are null");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000042CD File Offset: 0x000024CD
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000042D5 File Offset: 0x000024D5
		private protected MdbOneToOneTransformPropertyMapping.TransformDelegate GetterTransform { protected get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000042DE File Offset: 0x000024DE
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000042E6 File Offset: 0x000024E6
		private protected MdbOneToOneTransformPropertyMapping.TransformDelegate SetterTransform { protected get; private set; }

		// Token: 0x0600009D RID: 157 RVA: 0x000042F0 File Offset: 0x000024F0
		public override object GetPropertyValue(IItem item, IMdbPropertyMappingContext context)
		{
			object propertyValue = base.GetPropertyValue(item, context);
			if (this.GetterTransform == null)
			{
				return propertyValue;
			}
			return this.GetterTransform(this.PropertyDefinition, base.StorePropertyDefinition, propertyValue);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004328 File Offset: 0x00002528
		public override void SetPropertyValue(IItem item, object value, IMdbPropertyMappingContext context)
		{
			if (this.SetterTransform != null)
			{
				value = this.SetterTransform(this.PropertyDefinition, base.StorePropertyDefinition, value);
			}
			base.SetPropertyValue(item, value, context);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004358 File Offset: 0x00002558
		public override object GetPropertyValue(IDictionary<StorePropertyDefinition, object> dictionary)
		{
			object propertyValue = base.GetPropertyValue(dictionary);
			if (this.GetterTransform == null)
			{
				return propertyValue;
			}
			return this.GetterTransform(this.PropertyDefinition, base.StorePropertyDefinition, propertyValue);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000438F File Offset: 0x0000258F
		public override void SetPropertyValue(IDictionary<StorePropertyDefinition, object> dictionary, object value)
		{
			if (this.SetterTransform != null)
			{
				value = this.SetterTransform(this.PropertyDefinition, base.StorePropertyDefinition, value);
			}
			base.SetPropertyValue(dictionary, value);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000043BB File Offset: 0x000025BB
		internal static object FixTypeGetterTranform(PropertyDefinition genericPropertyDefinition, StorePropertyDefinition storePropertyDefinition, object storePropertyValue)
		{
			return MdbOneToOneTransformPropertyMapping.FixType(genericPropertyDefinition.Type, storePropertyValue);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000043C9 File Offset: 0x000025C9
		internal static object FixTypeSetterTranform(PropertyDefinition genericPropertyDefinition, StorePropertyDefinition storePropertyDefinition, object genericPropertyValue)
		{
			return MdbOneToOneTransformPropertyMapping.FixType(storePropertyDefinition.Type, genericPropertyValue);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043D8 File Offset: 0x000025D8
		private static object FixType(Type expectedType, object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (expectedType == type)
			{
				return value;
			}
			if (expectedType.IsPrimitive && type.IsEnum)
			{
				if (expectedType == Enum.GetUnderlyingType(type))
				{
					return Convert.ChangeType(value, expectedType);
				}
			}
			else if (expectedType.IsEnum && type.IsPrimitive && Enum.GetUnderlyingType(expectedType) == type)
			{
				return Enum.ToObject(expectedType, value);
			}
			return value;
		}

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x060000A5 RID: 165
		public delegate object TransformDelegate(PropertyDefinition genericPropertyDefinition, StorePropertyDefinition storePropertyDefinition, object value);
	}
}
