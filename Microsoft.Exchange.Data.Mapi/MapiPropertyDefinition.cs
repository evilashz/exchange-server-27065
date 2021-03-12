using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	internal sealed class MapiPropertyDefinition : ProviderPropertyDefinition
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003F40 File Offset: 0x00002140
		public override bool IsMultivalued
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.MultiValued & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003F50 File Offset: 0x00002150
		public override bool IsReadOnly
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.ReadOnly & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003F60 File Offset: 0x00002160
		public override bool IsCalculated
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.Calculated & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003F70 File Offset: 0x00002170
		public override bool IsFilterOnly
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.FilterOnly & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003F80 File Offset: 0x00002180
		public override bool IsMandatory
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.Mandatory & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003F91 File Offset: 0x00002191
		public override bool PersistDefaultValue
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.PersistDefaultValue & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003FA2 File Offset: 0x000021A2
		public override bool IsWriteOnce
		{
			get
			{
				return MapiPropertyDefinitionFlags.None != (MapiPropertyDefinitionFlags.WriteOnce & this.propertyDefinitionFlags);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003FB3 File Offset: 0x000021B3
		public override bool IsBinary
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003FB6 File Offset: 0x000021B6
		public PropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003FBE File Offset: 0x000021BE
		public MapiPropertyDefinitionFlags PropertyDefinitionFlags
		{
			get
			{
				return this.propertyDefinitionFlags;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003FC6 File Offset: 0x000021C6
		public object InitialValue
		{
			get
			{
				return this.initialValue;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003FCE File Offset: 0x000021CE
		public MapiPropValueExtractorDelegate Extractor
		{
			get
			{
				return this.propertyValueExtractor;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003FD6 File Offset: 0x000021D6
		public MapiPropValuePackerDelegate Packer
		{
			get
			{
				return this.propertyValuePacker;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FE0 File Offset: 0x000021E0
		public MapiPropertyDefinition(string name, Type type, PropTag propertyTag, MapiPropertyDefinitionFlags propertyDefinitionFlags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : this(name, type, propertyTag, propertyDefinitionFlags, defaultValue, defaultValue, readConstraints, writeConstraints)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004000 File Offset: 0x00002200
		public MapiPropertyDefinition(string name, Type type, PropTag propertyTag, MapiPropertyDefinitionFlags propertyDefinitionFlags, object defaultValue, object initialValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : this(name, type, propertyTag, propertyDefinitionFlags, defaultValue, initialValue, null, null, readConstraints, writeConstraints)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004024 File Offset: 0x00002224
		public MapiPropertyDefinition(string name, Type type, PropTag propertyTag, MapiPropertyDefinitionFlags propertyDefinitionFlags, object defaultValue, MapiPropValueExtractorDelegate propertyValueExtractor, MapiPropValuePackerDelegate propertyValuePacker, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : this(name, type, propertyTag, propertyDefinitionFlags, defaultValue, defaultValue, propertyValueExtractor, propertyValuePacker, readConstraints, writeConstraints)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004048 File Offset: 0x00002248
		public MapiPropertyDefinition(string name, Type type, PropTag propertyTag, MapiPropertyDefinitionFlags propertyDefinitionFlags, object defaultValue, object initialValue, MapiPropValueExtractorDelegate propertyValueExtractor, MapiPropValuePackerDelegate propertyValuePacker, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : this(name, type, propertyTag, propertyDefinitionFlags, defaultValue, initialValue, propertyValueExtractor, propertyValuePacker, readConstraints, writeConstraints, null, null, null, null)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004070 File Offset: 0x00002270
		public MapiPropertyDefinition(string name, Type type, object defaultValue, MapiPropertyDefinitionFlags propertyDefinitionFlags, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, CustomFilterBuilderDelegate customFilterBuilderDelegate, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : this(name, type, PropTag.Null, propertyDefinitionFlags, defaultValue, null, null, null, readConstraints, writeConstraints, supportingProperties, customFilterBuilderDelegate, getterDelegate, setterDelegate)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004098 File Offset: 0x00002298
		private MapiPropertyDefinition(string name, Type type, PropTag propertyTag, MapiPropertyDefinitionFlags propertyDefinitionFlags, object defaultValue, object initialValue, MapiPropValueExtractorDelegate propertyValueExtractor, MapiPropValuePackerDelegate propertyValuePacker, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, CustomFilterBuilderDelegate customFilterBuilderDelegate, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : base(name ?? propertyTag.ToString(), ExchangeObjectVersion.Exchange2003, type ?? MapiPropValueConvertor.TypeFromPropType(propertyTag.ValueType(), true), defaultValue, readConstraints ?? PropertyDefinitionConstraint.None, writeConstraints ?? PropertyDefinitionConstraint.None, supportingProperties ?? ProviderPropertyDefinition.None, customFilterBuilderDelegate, getterDelegate, setterDelegate)
		{
			this.propertyTag = propertyTag;
			if (((PropTag)12288U & propertyTag) != PropTag.Null)
			{
				propertyDefinitionFlags |= MapiPropertyDefinitionFlags.MultiValued;
			}
			this.propertyDefinitionFlags = propertyDefinitionFlags;
			this.initialValue = initialValue;
			this.propertyValueExtractor = (propertyValueExtractor ?? new MapiPropValueExtractorDelegate(MapiPropValueConvertor.Extract));
			this.propertyValuePacker = (propertyValuePacker ?? new MapiPropValuePackerDelegate(MapiPropValueConvertor.Pack));
		}

		// Token: 0x04000047 RID: 71
		private readonly PropTag propertyTag;

		// Token: 0x04000048 RID: 72
		private readonly MapiPropertyDefinitionFlags propertyDefinitionFlags;

		// Token: 0x04000049 RID: 73
		private readonly object initialValue;

		// Token: 0x0400004A RID: 74
		private readonly MapiPropValueExtractorDelegate propertyValueExtractor;

		// Token: 0x0400004B RID: 75
		private readonly MapiPropValuePackerDelegate propertyValuePacker;
	}
}
