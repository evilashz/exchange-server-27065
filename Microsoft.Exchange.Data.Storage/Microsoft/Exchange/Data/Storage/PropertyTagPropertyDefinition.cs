using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA8 RID: 3240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class PropertyTagPropertyDefinition : NativeStorePropertyDefinition
	{
		// Token: 0x060070E7 RID: 28903 RVA: 0x001F50F0 File Offset: 0x001F32F0
		private PropertyTagPropertyDefinition(string displayName, PropTag propertyTag, PropertyTagPropertyDefinition.PropTagKey key, PropertyFlags flags, bool isCustom, PropertyDefinitionConstraint[] constraints) : base(PropertyTypeSpecifier.PropertyTag, displayName, InternalSchema.ClrTypeFromPropTag(propertyTag), propertyTag.ValueType(), PropertyTagPropertyDefinition.CalculatePropertyTagPropertyFlags(propertyTag, flags), constraints)
		{
			if (propertyTag.IsNamedProperty() || !propertyTag.IsValid())
			{
				throw new ArgumentException("Invalid property tag", "propertyTag");
			}
			this.InternalKey = key;
			this.propertyTag = propertyTag;
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x060070E8 RID: 28904 RVA: 0x001F5149 File Offset: 0x001F3349
		public uint PropertyTag
		{
			get
			{
				return (uint)this.propertyTag;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x060070E9 RID: 28905 RVA: 0x001F5151 File Offset: 0x001F3351
		public bool IsApplicationSpecific
		{
			get
			{
				return this.propertyTag.IsApplicationSpecific();
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x060070EA RID: 28906 RVA: 0x001F515E File Offset: 0x001F335E
		public bool IsTransmittable
		{
			get
			{
				return this.propertyTag.IsTransmittable();
			}
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x060070EB RID: 28907 RVA: 0x001F516B File Offset: 0x001F336B
		// (set) Token: 0x060070EC RID: 28908 RVA: 0x001F5173 File Offset: 0x001F3373
		private PropertyTagPropertyDefinition.PropTagKey InternalKey
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

		// Token: 0x060070ED RID: 28909 RVA: 0x001F517C File Offset: 0x001F337C
		public static PropertyTagPropertyDefinition CreateCustom(string displayName, uint propertyTag)
		{
			return PropertyTagPropertyDefinition.CreateCustom(displayName, propertyTag, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x001F518A File Offset: 0x001F338A
		public static PropertyTagPropertyDefinition CreateCustom(string displayName, uint propertyTag, params PropertyDefinitionConstraint[] constraints)
		{
			return PropertyTagPropertyDefinition.CreateCustom(displayName, propertyTag, PropertyFlags.None, constraints);
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x001F5195 File Offset: 0x001F3395
		public static PropertyTagPropertyDefinition CreateCustom(string displayName, uint propertyTag, PropertyFlags flags, params PropertyDefinitionConstraint[] constraints)
		{
			return PropertyTagPropertyDefinition.InternalCreateCustom(displayName, (PropTag)propertyTag, flags, NativeStorePropertyDefinition.TypeCheckingFlag.ThrowOnInvalidType, constraints);
		}

		// Token: 0x060070F0 RID: 28912 RVA: 0x001F51A4 File Offset: 0x001F33A4
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			PropertyTagPropertyDefinition propertyTagPropertyDefinition = obj as PropertyTagPropertyDefinition;
			return propertyTagPropertyDefinition != null && this.propertyTag == propertyTagPropertyDefinition.propertyTag;
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x001F51D6 File Offset: 0x001F33D6
		public override int GetHashCode()
		{
			return (int)this.propertyTag;
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x001F51DE File Offset: 0x001F33DE
		public PropertyTagPropertyDefinition.PropTagKey GetKey()
		{
			return this.InternalKey;
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x001F51E8 File Offset: 0x001F33E8
		internal static bool TryFindEquivalentDefinition(PropertyTagPropertyDefinition.PropTagKey key, bool isCustom, PropType type, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, out PropertyTagPropertyDefinition definition, out bool createNewDefinition)
		{
			createNewDefinition = true;
			if (!isCustom)
			{
				definition = null;
				return false;
			}
			switch (NativeStorePropertyDefinitionDictionary.TryFindInstance(key, type, out definition))
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

		// Token: 0x060070F4 RID: 28916 RVA: 0x001F5238 File Offset: 0x001F3438
		internal static PropertyTagPropertyDefinition InternalCreate(string displayName, PropTag propertyTag)
		{
			return PropertyTagPropertyDefinition.InternalCreate(displayName, propertyTag, PropertyFlags.None);
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x001F5242 File Offset: 0x001F3442
		internal static PropertyTagPropertyDefinition InternalCreate(string displayName, PropTag propertyTag, params PropertyDefinitionConstraint[] constraints)
		{
			return PropertyTagPropertyDefinition.InternalCreate(displayName, propertyTag, PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, false, constraints);
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x001F524F File Offset: 0x001F344F
		internal static PropertyTagPropertyDefinition InternalCreate(string displayName, PropTag propertyTag, PropertyFlags propertyFlags)
		{
			return PropertyTagPropertyDefinition.InternalCreate(displayName, propertyTag, propertyFlags, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, false, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x001F5260 File Offset: 0x001F3460
		internal static PropertyTagPropertyDefinition InternalCreateCustom(string displayName, PropTag propertyTag, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag)
		{
			return PropertyTagPropertyDefinition.InternalCreateCustom(displayName, propertyTag, flags, typeCheckingFlag, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x001F5270 File Offset: 0x001F3470
		internal static PropertyTagPropertyDefinition InternalCreateCustom(string displayName, PropTag propertyTag, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, PropertyDefinitionConstraint[] constraints)
		{
			return PropertyTagPropertyDefinition.InternalCreate(displayName, propertyTag, flags | PropertyFlags.Custom, typeCheckingFlag, true, constraints);
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x001F5284 File Offset: 0x001F3484
		private static PropertyTagPropertyDefinition InternalCreate(string displayName, PropTag propertyTag, PropertyFlags flags, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, bool isCustom, PropertyDefinitionConstraint[] constraints)
		{
			if (!propertyTag.IsValid())
			{
				throw new ArgumentException("Invalid property tag", "propertyTag");
			}
			PropType propType = propertyTag.ValueType();
			if (propType == PropType.AnsiString || propType == PropType.AnsiStringArray)
			{
				propertyTag = ((propertyTag & (PropTag)4294967265U) | (PropTag)31U);
				propType = propertyTag.ValueType();
			}
			PropertyTagPropertyDefinition.PropTagKey propTagKey = new PropertyTagPropertyDefinition.PropTagKey(propertyTag);
			PropertyTagPropertyDefinition result;
			bool flag;
			if (PropertyTagPropertyDefinition.TryFindEquivalentDefinition(propTagKey, isCustom, propType, typeCheckingFlag, out result, out flag))
			{
				return result;
			}
			if (flag)
			{
				try
				{
					return new PropertyTagPropertyDefinition(displayName, propertyTag, propTagKey, flags, isCustom, constraints);
				}
				catch (InvalidPropertyTypeException)
				{
					if (typeCheckingFlag == NativeStorePropertyDefinition.TypeCheckingFlag.ThrowOnInvalidType)
					{
						throw;
					}
				}
			}
			return null;
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x001F5318 File Offset: 0x001F3518
		private static PropertyFlags CalculatePropertyTagPropertyFlags(PropTag propertyTag, PropertyFlags userFlags)
		{
			PropertyFlags propertyFlags = userFlags & (PropertyFlags)(-2147418113);
			if (propertyTag.IsTransmittable())
			{
				propertyFlags |= PropertyFlags.Transmittable;
			}
			return propertyFlags;
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x001F533E File Offset: 0x001F353E
		[Obsolete("Use propertyTag.IsPropertyTransmittable().")]
		internal static bool IsPropertyTransmittable(PropTag propertyTag)
		{
			return propertyTag.IsTransmittable();
		}

		// Token: 0x060070FC RID: 28924 RVA: 0x001F5348 File Offset: 0x001F3548
		protected override string GetPropertyDefinitionString()
		{
			return "0x" + this.PropertyTag.ToString("x8", CultureInfo.InvariantCulture);
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x001F5377 File Offset: 0x001F3577
		[Obsolete("Use propertyTag.IsApplicationSpecific().")]
		internal static bool IsApplicationSpecificPropertyTag(PropTag propertyTag)
		{
			return propertyTag.IsApplicationSpecific();
		}

		// Token: 0x04004E8D RID: 20109
		private readonly PropTag propertyTag;

		// Token: 0x04004E8E RID: 20110
		private PropertyTagPropertyDefinition.PropTagKey key;

		// Token: 0x02000CA9 RID: 3241
		[Serializable]
		public struct PropTagKey : IEquatable<PropertyTagPropertyDefinition.PropTagKey>
		{
			// Token: 0x060070FE RID: 28926 RVA: 0x001F537F File Offset: 0x001F357F
			internal PropTagKey(PropTag propertyTag)
			{
				if (propertyTag.IsApplicationSpecific())
				{
					this.propertyTag = propertyTag;
					return;
				}
				this.propertyTag = PropTagHelper.PropTagFromIdAndType(propertyTag.Id(), PropType.Unspecified);
			}

			// Token: 0x060070FF RID: 28927 RVA: 0x001F53A3 File Offset: 0x001F35A3
			public bool Equals(PropertyTagPropertyDefinition.PropTagKey other)
			{
				return this.propertyTag == other.propertyTag;
			}

			// Token: 0x06007100 RID: 28928 RVA: 0x001F53B4 File Offset: 0x001F35B4
			public override int GetHashCode()
			{
				return (int)this.propertyTag;
			}

			// Token: 0x06007101 RID: 28929 RVA: 0x001F53BC File Offset: 0x001F35BC
			public override string ToString()
			{
				return string.Format("PropertyTagPropertyDefinition:{0:x8}", (uint)this.propertyTag);
			}

			// Token: 0x04004E8F RID: 20111
			private readonly PropTag propertyTag;
		}
	}
}
