using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class GuidNamePropertyDefinition : NamedPropertyDefinition
	{
		// Token: 0x06000BDE RID: 3038 RVA: 0x00051EE8 File Offset: 0x000500E8
		private GuidNamePropertyDefinition(string displayName, Type propertyType, PropType mapiPropertyType, GuidNamePropertyDefinition.GuidNameKey key, PropertyFlags flags, bool isCustom, PropertyDefinitionConstraint[] constraints) : base(PropertyTypeSpecifier.GuidString, displayName, propertyType, mapiPropertyType, GuidNamePropertyDefinition.CalculatePropertyTagPropertyFlags(key.PropertyName, key.PropertyGuid, flags, isCustom), constraints)
		{
			this.InternalKey = key;
			this.hashCode = (this.Guid.GetHashCode() ^ this.PropertyName.GetHashCode() ^ (int)base.MapiPropertyType);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00051F4D File Offset: 0x0005014D
		public static bool IsValidName(Guid propertyNamespace, string propertyName)
		{
			return !string.IsNullOrEmpty(propertyName) && (!(propertyNamespace == WellKnownNamedPropertyGuid.InternetHeaders) || NamedProp.IsValidInternetHeadersName(propertyName)) && propertyName.Length <= StorageLimits.Instance.NamedPropertyNameMaximumLength;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00051F85 File Offset: 0x00050185
		public Guid Guid
		{
			get
			{
				return this.InternalKey.PropertyGuid;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00051F92 File Offset: 0x00050192
		public string PropertyName
		{
			get
			{
				return this.InternalKey.PropertyName;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00051F9F File Offset: 0x0005019F
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00051FA7 File Offset: 0x000501A7
		private GuidNamePropertyDefinition.GuidNameKey InternalKey
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

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00051FB0 File Offset: 0x000501B0
		public static GuidNamePropertyDefinition CreateCustom(string displayName, Type propertyType, Guid propertyGuid, string propertyName, PropertyFlags flags)
		{
			return GuidNamePropertyDefinition.CreateCustom(displayName, propertyType, propertyGuid, propertyName, flags, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00051FC4 File Offset: 0x000501C4
		public static GuidNamePropertyDefinition CreateCustom(string displayName, Type propertyType, Guid propertyGuid, string propertyName, PropertyFlags flags, params PropertyDefinitionConstraint[] constraints)
		{
			PropType mapiPropType = InternalSchema.PropTagTypeFromClrType(propertyType);
			return GuidNamePropertyDefinition.InternalCreate(displayName, propertyType, mapiPropType, propertyGuid, propertyName, flags | PropertyFlags.Custom, NativeStorePropertyDefinition.TypeCheckingFlag.ThrowOnInvalidType, true, constraints);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00051FF0 File Offset: 0x000501F0
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			GuidNamePropertyDefinition guidNamePropertyDefinition = obj as GuidNamePropertyDefinition;
			return guidNamePropertyDefinition != null && this.GetHashCode() == guidNamePropertyDefinition.GetHashCode() && this.Guid == guidNamePropertyDefinition.Guid && this.PropertyName == guidNamePropertyDefinition.PropertyName && base.MapiPropertyType == guidNamePropertyDefinition.MapiPropertyType;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00052056 File Offset: 0x00050256
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0005205E File Offset: 0x0005025E
		public override NamedPropertyDefinition.NamedPropertyKey GetKey()
		{
			return this.InternalKey;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00052068 File Offset: 0x00050268
		internal static bool TryFindEquivalentDefinition(GuidNamePropertyDefinition.GuidNameKey key, bool isCustom, PropType type, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, out GuidNamePropertyDefinition definition, out bool createNewDefinition)
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

		// Token: 0x06000BEA RID: 3050 RVA: 0x000520B8 File Offset: 0x000502B8
		private static PropertyFlags CalculatePropertyTagPropertyFlags(string propertyName, Guid guid, PropertyFlags userFlags, bool isCustom)
		{
			PropertyFlags propertyFlags = NativeStorePropertyDefinition.CalculatePropertyTagPropertyFlags(userFlags, isCustom);
			if (guid == WellKnownPropertySet.InternetHeaders && MimeConstants.IsInReservedHeaderNamespace(propertyName))
			{
				propertyFlags &= ~PropertyFlags.Transmittable;
			}
			return propertyFlags;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000520EC File Offset: 0x000502EC
		internal static GuidNamePropertyDefinition InternalCreateCustom(string displayName, PropType mapiPropType, Guid propertyGuid, string propertyName, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, params PropertyDefinitionConstraint[] constraints)
		{
			Type propertyType = InternalSchema.ClrTypeFromPropTagType(mapiPropType);
			return GuidNamePropertyDefinition.InternalCreate(displayName, propertyType, mapiPropType, propertyGuid, propertyName, flags | PropertyFlags.Custom, typeCheckingFlag, true, constraints);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00052118 File Offset: 0x00050318
		internal static GuidNamePropertyDefinition InternalCreate(string displayName, Type propertyType, PropType mapiPropType, Guid propertyGuid, string propertyName, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, bool isCustom, params PropertyDefinitionConstraint[] constraints)
		{
			if (!GuidNamePropertyDefinition.IsValidName(propertyGuid, propertyName))
			{
				throw new ArgumentException("Invalid property name for property", "propertyName");
			}
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
			NamedProp namedProp = new NamedProp(propertyGuid, propertyName);
			NamedProp namedProp2 = WellKnownNamedProperties.Find(namedProp);
			if (namedProp2 != null)
			{
				namedProp = namedProp2;
			}
			else
			{
				namedProp = NamedPropertyDefinition.NamedPropertyKey.GetSingleton(namedProp);
			}
			GuidNamePropertyDefinition.GuidNameKey guidNameKey = new GuidNamePropertyDefinition.GuidNameKey(namedProp);
			GuidNamePropertyDefinition result;
			bool flag;
			if (GuidNamePropertyDefinition.TryFindEquivalentDefinition(guidNameKey, isCustom, mapiPropType, typeCheckingFlag, out result, out flag))
			{
				return result;
			}
			if (!flag)
			{
				return null;
			}
			return new GuidNamePropertyDefinition(displayName, propertyType, mapiPropType, guidNameKey, flags, isCustom, constraints);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000521BE File Offset: 0x000503BE
		protected override string GetPropertyDefinitionString()
		{
			return string.Format("{{{0}}}:'{1}'", this.Guid, this.PropertyName);
		}

		// Token: 0x04000346 RID: 838
		private const int MaxNameLength = 125;

		// Token: 0x04000347 RID: 839
		private GuidNamePropertyDefinition.GuidNameKey key;

		// Token: 0x04000348 RID: 840
		private int hashCode;

		// Token: 0x020000B0 RID: 176
		[Serializable]
		public sealed class GuidNameKey : NamedPropertyDefinition.NamedPropertyKey, IEquatable<GuidNamePropertyDefinition.GuidNameKey>
		{
			// Token: 0x06000BEE RID: 3054 RVA: 0x000521DB File Offset: 0x000503DB
			internal GuidNameKey(NamedProp namedProp) : base(namedProp)
			{
			}

			// Token: 0x06000BEF RID: 3055 RVA: 0x000521E4 File Offset: 0x000503E4
			public GuidNameKey(Guid propGuid, string propName) : base(propGuid, propName)
			{
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x000521F0 File Offset: 0x000503F0
			public override bool Equals(object obj)
			{
				GuidNamePropertyDefinition.GuidNameKey other = obj as GuidNamePropertyDefinition.GuidNameKey;
				return this.Equals(other);
			}

			// Token: 0x06000BF1 RID: 3057 RVA: 0x0005220B File Offset: 0x0005040B
			public bool Equals(GuidNamePropertyDefinition.GuidNameKey other)
			{
				return other != null && this.PropertyGuid == other.PropertyGuid && this.PropertyName == other.PropertyName;
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x00052238 File Offset: 0x00050438
			public override int GetHashCode()
			{
				return this.PropertyGuid.GetHashCode() ^ this.PropertyName.GetHashCode();
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x00052265 File Offset: 0x00050465
			public override string ToString()
			{
				return string.Format("{{{0}}}:'{1}'", this.PropertyGuid, this.PropertyName);
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00052282 File Offset: 0x00050482
			public Guid PropertyGuid
			{
				get
				{
					return base.NamedProp.Guid;
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0005228F File Offset: 0x0005048F
			public string PropertyName
			{
				get
				{
					return base.NamedProp.Name;
				}
			}
		}
	}
}
