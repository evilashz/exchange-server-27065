using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class GuidIdPropertyDefinition : NamedPropertyDefinition
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x00051B84 File Offset: 0x0004FD84
		private GuidIdPropertyDefinition(string displayName, Type propertyType, PropType mapiPropertyType, GuidIdPropertyDefinition.GuidIdKey key, PropertyFlags flags, bool isCustom, PropertyDefinitionConstraint[] constraints) : base(PropertyTypeSpecifier.GuidId, displayName, propertyType, mapiPropertyType, NativeStorePropertyDefinition.CalculatePropertyTagPropertyFlags(flags, isCustom), constraints)
		{
			this.InternalKey = key;
			this.hashCode = (this.Guid.GetHashCode() ^ this.Id ^ (int)base.MapiPropertyType);
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00051BD6 File Offset: 0x0004FDD6
		public Guid Guid
		{
			get
			{
				return this.InternalKey.PropertyGuid;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00051BE3 File Offset: 0x0004FDE3
		public int Id
		{
			get
			{
				return this.InternalKey.PropertyId;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00051BF0 File Offset: 0x0004FDF0
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00051BF8 File Offset: 0x0004FDF8
		private GuidIdPropertyDefinition.GuidIdKey InternalKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00051C01 File Offset: 0x0004FE01
		public static GuidIdPropertyDefinition CreateCustom(string displayName, Type propertyType, Guid propertyGuid, int dispId, PropertyFlags flags)
		{
			return GuidIdPropertyDefinition.CreateCustom(displayName, propertyType, propertyGuid, dispId, flags, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00051C13 File Offset: 0x0004FE13
		public static GuidIdPropertyDefinition CreateCustom(string displayName, ushort mapiPropertyType, Guid propertyGuid, int dispId, PropertyFlags flags)
		{
			return GuidIdPropertyDefinition.CreateCustom(displayName, InternalSchema.ClrTypeFromPropTagType((PropType)mapiPropertyType), propertyGuid, dispId, flags, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00051C2C File Offset: 0x0004FE2C
		public static GuidIdPropertyDefinition CreateCustom(string displayName, Type propertyType, Guid propertyGuid, int dispId, PropertyFlags flags, params PropertyDefinitionConstraint[] constraints)
		{
			PropType mapiPropType = InternalSchema.PropTagTypeFromClrType(propertyType);
			return GuidIdPropertyDefinition.InternalCreate(displayName, propertyType, mapiPropType, propertyGuid, dispId, flags | PropertyFlags.Custom, NativeStorePropertyDefinition.TypeCheckingFlag.ThrowOnInvalidType, true, constraints);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00051C58 File Offset: 0x0004FE58
		internal static bool TryFindEquivalentDefinition(GuidIdPropertyDefinition.GuidIdKey key, bool isCustom, PropType type, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, out GuidIdPropertyDefinition definition, out bool createNewDefinition)
		{
			createNewDefinition = true;
			if (!isCustom)
			{
				definition = null;
				return false;
			}
			switch (NativeStorePropertyDefinitionDictionary.TryFindInstance(key, type, typeCheckingFlag == NativeStorePropertyDefinition.TypeCheckingFlag.AllowCompatibleType, out definition))
			{
			case PropertyMatchResult.Found:
				createNewDefinition = false;
				return true;
			case PropertyMatchResult.TypeMismatch:
				NativeStorePropertyDefinition.OnFailedPropertyTypeCheck(key, type, typeCheckingFlag, out createNewDefinition);
				break;
			}
			return false;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		internal static GuidIdPropertyDefinition InternalCreateCustom(string displayName, PropType mapiPropType, Guid propertyGuid, int dispId, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, params PropertyDefinitionConstraint[] constraints)
		{
			Type propertyType = InternalSchema.ClrTypeFromPropTagType(mapiPropType);
			return GuidIdPropertyDefinition.InternalCreate(displayName, propertyType, mapiPropType, propertyGuid, dispId, flags | PropertyFlags.Custom, typeCheckingFlag, true, constraints);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00051CD4 File Offset: 0x0004FED4
		internal static GuidIdPropertyDefinition InternalCreate(string displayName, Type propertyType, PropType mapiPropType, Guid propertyGuid, int dispId, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, bool isCustom, params PropertyDefinitionConstraint[] constraints)
		{
			if (mapiPropType == PropType.AnsiString)
			{
				mapiPropType = PropType.String;
				propertyType = typeof(string);
			}
			else if (mapiPropType == PropType.AnsiStringArray)
			{
				mapiPropType = PropType.StringArray;
				propertyType = typeof(string[]);
			}
			NamedProp namedProp = new NamedProp(propertyGuid, dispId);
			NamedProp namedProp2 = WellKnownNamedProperties.Find(namedProp);
			if (namedProp2 != null)
			{
				namedProp = namedProp2;
			}
			else
			{
				namedProp = NamedPropertyDefinition.NamedPropertyKey.GetSingleton(namedProp);
			}
			GuidIdPropertyDefinition.GuidIdKey guidIdKey = new GuidIdPropertyDefinition.GuidIdKey(namedProp);
			bool flag;
			if (propertyGuid == WellKnownPropertySet.InternetHeaders)
			{
				NativeStorePropertyDefinition.OnFailedPropertyTypeCheck(guidIdKey, mapiPropType, typeCheckingFlag, out flag);
				return null;
			}
			GuidIdPropertyDefinition result;
			if (GuidIdPropertyDefinition.TryFindEquivalentDefinition(guidIdKey, isCustom, mapiPropType, typeCheckingFlag, out result, out flag))
			{
				return result;
			}
			if (!flag)
			{
				return null;
			}
			return new GuidIdPropertyDefinition(displayName, propertyType, mapiPropType, guidIdKey, flags, isCustom, constraints);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00051D7C File Offset: 0x0004FF7C
		protected override string GetPropertyDefinitionString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{{0}}}:0x{1:x4}", new object[]
			{
				this.Guid,
				this.Id
			});
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00051DBC File Offset: 0x0004FFBC
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			GuidIdPropertyDefinition guidIdPropertyDefinition = obj as GuidIdPropertyDefinition;
			return guidIdPropertyDefinition != null && this.GetHashCode() == guidIdPropertyDefinition.GetHashCode() && this.Guid == guidIdPropertyDefinition.Guid && this.Id == guidIdPropertyDefinition.Id && base.MapiPropertyType == guidIdPropertyDefinition.MapiPropertyType;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00051E1D File Offset: 0x0005001D
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00051E25 File Offset: 0x00050025
		public override NamedPropertyDefinition.NamedPropertyKey GetKey()
		{
			return this.InternalKey;
		}

		// Token: 0x04000344 RID: 836
		private GuidIdPropertyDefinition.GuidIdKey key;

		// Token: 0x04000345 RID: 837
		private int hashCode;

		// Token: 0x020000AE RID: 174
		[Serializable]
		public sealed class GuidIdKey : NamedPropertyDefinition.NamedPropertyKey, IEquatable<GuidIdPropertyDefinition.GuidIdKey>
		{
			// Token: 0x06000BD6 RID: 3030 RVA: 0x00051E2D File Offset: 0x0005002D
			internal GuidIdKey(NamedProp namedProp) : base(namedProp)
			{
			}

			// Token: 0x06000BD7 RID: 3031 RVA: 0x00051E36 File Offset: 0x00050036
			public GuidIdKey(Guid propGuid, int propId) : base(propGuid, propId)
			{
			}

			// Token: 0x06000BD8 RID: 3032 RVA: 0x00051E40 File Offset: 0x00050040
			public override bool Equals(object obj)
			{
				GuidIdPropertyDefinition.GuidIdKey other = obj as GuidIdPropertyDefinition.GuidIdKey;
				return this.Equals(other);
			}

			// Token: 0x06000BD9 RID: 3033 RVA: 0x00051E5B File Offset: 0x0005005B
			public bool Equals(GuidIdPropertyDefinition.GuidIdKey other)
			{
				return other != null && this.PropertyId == other.PropertyId && this.PropertyGuid == other.PropertyGuid;
			}

			// Token: 0x06000BDA RID: 3034 RVA: 0x00051E84 File Offset: 0x00050084
			public override int GetHashCode()
			{
				return this.PropertyGuid.GetHashCode() ^ this.PropertyId;
			}

			// Token: 0x06000BDB RID: 3035 RVA: 0x00051EAC File Offset: 0x000500AC
			public override string ToString()
			{
				return string.Format("{{{0}}}:0x{1:x4}", this.PropertyGuid, this.PropertyId);
			}

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00051ECE File Offset: 0x000500CE
			public Guid PropertyGuid
			{
				get
				{
					return base.NamedProp.Guid;
				}
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00051EDB File Offset: 0x000500DB
			public int PropertyId
			{
				get
				{
					return base.NamedProp.Id;
				}
			}
		}
	}
}
