using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class NativeStorePropertyDefinition : AtomicStorePropertyDefinition
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00051457 File Offset: 0x0004F657
		internal NativeStorePropertyDefinition(PropertyTypeSpecifier propertyTypeSpecifier, string displayName, Type type, PropType mapiPropertyType, PropertyFlags childFlags, PropertyDefinitionConstraint[] constraints) : base(propertyTypeSpecifier, displayName, type, childFlags, constraints)
		{
			this.mapiPropertyType = mapiPropertyType;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0005146E File Offset: 0x0004F66E
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00051474 File Offset: 0x0004F674
		protected static void OnFailedPropertyTypeCheck(object key, PropType type, NativeStorePropertyDefinition.TypeCheckingFlag typeCheckingFlag, out bool createNewDefinition)
		{
			switch (typeCheckingFlag)
			{
			case NativeStorePropertyDefinition.TypeCheckingFlag.DoNotCreateInvalidType:
			case NativeStorePropertyDefinition.TypeCheckingFlag.AllowCompatibleType:
				createNewDefinition = false;
				return;
			case NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck:
				createNewDefinition = true;
				return;
			}
			throw new InvalidPropertyTypeException(ServerStrings.ExInvalidPropertyType(key.ToString(), type.ToString()));
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000514C0 File Offset: 0x0004F6C0
		protected static PropertyFlags CalculatePropertyTagPropertyFlags(PropertyFlags userFlags, bool isCustom)
		{
			PropertyFlags propertyFlags = userFlags & (PropertyFlags)(-2147418113);
			if (isCustom)
			{
				propertyFlags |= PropertyFlags.Custom;
			}
			return propertyFlags | PropertyFlags.Transmittable;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000514E7 File Offset: 0x0004F6E7
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (propertyBag.CanIgnoreUnchangedProperties && (base.PropertyFlags & PropertyFlags.SetIfNotChanged) != PropertyFlags.SetIfNotChanged && propertyBag.IsLoaded(this) && Util.ValueEquals(value, propertyBag.GetValue(this)))
			{
				return;
			}
			propertyBag.SetValue(this, value);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00051523 File Offset: 0x0004F723
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0005152D File Offset: 0x0004F72D
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00051538 File Offset: 0x0004F738
		internal override SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			return new SortBy[]
			{
				new SortBy(this, sortOrder)
			};
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00051557 File Offset: 0x0004F757
		internal override NativeStorePropertyDefinition GetNativeGroupBy()
		{
			return this;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0005155A File Offset: 0x0004F75A
		internal override GroupSort GetNativeGroupSort(SortOrder sortOrder, Aggregate aggregate)
		{
			return new GroupSort(this, sortOrder, aggregate);
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00051564 File Offset: 0x0004F764
		public PropType MapiPropertyType
		{
			get
			{
				return this.mapiPropertyType;
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0005156C File Offset: 0x0004F76C
		protected override void ForEachMatch(PropertyDependencyType targetDependencyType, Action<NativeStorePropertyDefinition> action)
		{
			if ((targetDependencyType & PropertyDependencyType.NeedForRead) != PropertyDependencyType.None)
			{
				action(this);
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0005157A File Offset: 0x0004F77A
		public static Type ClrTypeFromPropertyTag(uint propertyTag)
		{
			return InternalSchema.ClrTypeFromPropTag((PropTag)propertyTag);
		}

		// Token: 0x0400033B RID: 827
		private readonly PropType mapiPropertyType;

		// Token: 0x020000AA RID: 170
		internal enum TypeCheckingFlag
		{
			// Token: 0x0400033D RID: 829
			ThrowOnInvalidType,
			// Token: 0x0400033E RID: 830
			DoNotCreateInvalidType,
			// Token: 0x0400033F RID: 831
			DisableTypeCheck,
			// Token: 0x04000340 RID: 832
			AllowCompatibleType
		}
	}
}
