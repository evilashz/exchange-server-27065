using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000015 RID: 21
	internal abstract class MdbOneToOnePropertyMapping : MdbPropertyMapping
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003FE4 File Offset: 0x000021E4
		protected MdbOneToOnePropertyMapping(PropertyDefinition propertyDefinition, StorePropertyDefinition storePropertyDefinition, MdbOneToOnePropertyMapping.ItemGetterDelegate itemGetter, MdbOneToOnePropertyMapping.ItemSetterDelegate itemSetter, MdbOneToOnePropertyMapping.DictionaryGetterDelegate dictionaryGetter, MdbOneToOnePropertyMapping.DictionarySetterDelegate dictionarySetter) : base(propertyDefinition, new StorePropertyDefinition[]
		{
			storePropertyDefinition
		})
		{
			this.ItemGetter = itemGetter;
			this.ItemSetter = itemSetter;
			this.DictionaryGetter = dictionaryGetter;
			this.DictionarySetter = dictionarySetter;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004023 File Offset: 0x00002223
		public StorePropertyDefinition StorePropertyDefinition
		{
			get
			{
				return base.StorePropertyDefinitions[0];
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000402D File Offset: 0x0000222D
		public override bool IsReadOnly
		{
			get
			{
				return PropertyFlags.ReadOnly == (this.StorePropertyDefinition.PropertyFlags & PropertyFlags.ReadOnly);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000403F File Offset: 0x0000223F
		public override bool IsStreamable
		{
			get
			{
				return PropertyFlags.Streamable == (this.StorePropertyDefinition.PropertyFlags & PropertyFlags.Streamable);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004053 File Offset: 0x00002253
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000405B File Offset: 0x0000225B
		private protected MdbOneToOnePropertyMapping.ItemGetterDelegate ItemGetter { protected get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004064 File Offset: 0x00002264
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000406C File Offset: 0x0000226C
		private protected MdbOneToOnePropertyMapping.ItemSetterDelegate ItemSetter { protected get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004075 File Offset: 0x00002275
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000407D File Offset: 0x0000227D
		private protected MdbOneToOnePropertyMapping.DictionaryGetterDelegate DictionaryGetter { protected get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004086 File Offset: 0x00002286
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000408E File Offset: 0x0000228E
		private protected MdbOneToOnePropertyMapping.DictionarySetterDelegate DictionarySetter { protected get; private set; }

		// Token: 0x0600007E RID: 126 RVA: 0x00004098 File Offset: 0x00002298
		public override object GetPropertyValue(IItem item, IMdbPropertyMappingContext context)
		{
			if (this.ItemGetter == null)
			{
				throw new NotImplementedException(this.PropertyDefinition.Name);
			}
			return this.ItemGetter(item, this.StorePropertyDefinition, context);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000040D3 File Offset: 0x000022D3
		public override void SetPropertyValue(IItem item, object value, IMdbPropertyMappingContext context)
		{
			if (this.ItemSetter == null)
			{
				throw new NotImplementedException(this.PropertyDefinition.Name);
			}
			this.ItemSetter(item, this.StorePropertyDefinition, value, context);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004104 File Offset: 0x00002304
		public override object GetPropertyValue(IDictionary<StorePropertyDefinition, object> dictionary)
		{
			if (this.DictionaryGetter == null)
			{
				throw new NotImplementedException(this.PropertyDefinition.Name);
			}
			return this.DictionaryGetter(dictionary, this.StorePropertyDefinition);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000413E File Offset: 0x0000233E
		public override void SetPropertyValue(IDictionary<StorePropertyDefinition, object> dictionary, object value)
		{
			if (this.DictionarySetter == null)
			{
				throw new NotImplementedException(this.PropertyDefinition.Name);
			}
			this.DictionarySetter(dictionary, this.StorePropertyDefinition, value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000416C File Offset: 0x0000236C
		internal static object DefaultItemGetter(IItem item, StorePropertyDefinition propertyDefinition, IMdbPropertyMappingContext context)
		{
			return item.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004175 File Offset: 0x00002375
		internal static void DefaultItemSetter(IItem item, StorePropertyDefinition propertyDefinition, object value, IMdbPropertyMappingContext context)
		{
			item.SetOrDeleteProperty(propertyDefinition, value);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004180 File Offset: 0x00002380
		internal static object DefaultDictionaryGetter(IDictionary<StorePropertyDefinition, object> dictionary, StorePropertyDefinition propertyDefinition)
		{
			object result = null;
			dictionary.TryGetValue(propertyDefinition, out result);
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000419A File Offset: 0x0000239A
		internal static void DefaultDictionarySetter(IDictionary<StorePropertyDefinition, object> dictionary, StorePropertyDefinition propertyDefinition, object value)
		{
			if (value == null)
			{
				if (dictionary.ContainsKey(propertyDefinition))
				{
					dictionary.Remove(propertyDefinition);
					return;
				}
			}
			else
			{
				dictionary[propertyDefinition] = value;
			}
		}

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000087 RID: 135
		public delegate object ItemGetterDelegate(IItem item, StorePropertyDefinition propertyDefinition, IMdbPropertyMappingContext context);

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x0600008B RID: 139
		public delegate void ItemSetterDelegate(IItem item, StorePropertyDefinition propertyDefinition, object value, IMdbPropertyMappingContext context);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x0600008F RID: 143
		public delegate object DictionaryGetterDelegate(IDictionary<StorePropertyDefinition, object> dictionary, StorePropertyDefinition propertyDefinition);

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x06000093 RID: 147
		public delegate void DictionarySetterDelegate(IDictionary<StorePropertyDefinition, object> dictionary, StorePropertyDefinition propertyDefinition, object value);
	}
}
