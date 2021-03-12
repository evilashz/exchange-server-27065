using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CCB RID: 3275
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SimpleVirtualPropertyDefinition : AtomicStorePropertyDefinition
	{
		// Token: 0x0600719A RID: 29082 RVA: 0x001F7A9B File Offset: 0x001F5C9B
		internal SimpleVirtualPropertyDefinition(string displayName, Type propertyValueType, PropertyFlags flags, params PropertyDefinitionConstraint[] constraints) : this(PropertyTypeSpecifier.SimpleVirtual, displayName, propertyValueType, flags, constraints)
		{
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x001F7AA9 File Offset: 0x001F5CA9
		protected SimpleVirtualPropertyDefinition(PropertyTypeSpecifier specifiedWith, string displayName, Type propertyValueType, PropertyFlags flags, params PropertyDefinitionConstraint[] constraints) : base(specifiedWith, displayName, propertyValueType, flags, constraints)
		{
			EnumValidator.AssertValid<PropertyFlags>(flags);
			this.hashCode = new LazilyInitialized<int>(new Func<int>(this.ComputeHashCode));
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x001F7AD7 File Offset: 0x001F5CD7
		protected override string GetPropertyDefinitionString()
		{
			return "SV:" + base.Name;
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x0600719D RID: 29085 RVA: 0x001F7AE9 File Offset: 0x001F5CE9
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.None;
			}
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x001F7AEC File Offset: 0x001F5CEC
		internal override SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			throw new UnsupportedPropertyForSortGroupException(ServerStrings.ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition, this);
		}

		// Token: 0x0600719F RID: 29087 RVA: 0x001F7AF9 File Offset: 0x001F5CF9
		internal override NativeStorePropertyDefinition GetNativeGroupBy()
		{
			throw new UnsupportedPropertyForSortGroupException(ServerStrings.ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition, this);
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x001F7B06 File Offset: 0x001F5D06
		internal override GroupSort GetNativeGroupSort(SortOrder sortOrder, Aggregate aggregate)
		{
			throw new UnsupportedPropertyForSortGroupException(ServerStrings.ExFilterAndSortNotSupportedInSimpleVirtualPropertyDefinition, this);
		}

		// Token: 0x060071A1 RID: 29089 RVA: 0x001F7B13 File Offset: 0x001F5D13
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValue(this, value);
		}

		// Token: 0x060071A2 RID: 29090 RVA: 0x001F7B1E File Offset: 0x001F5D1E
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return propertyBag.GetValue(this);
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x001F7B28 File Offset: 0x001F5D28
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(this);
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x001F7B32 File Offset: 0x001F5D32
		protected virtual int ComputeHashCode()
		{
			return base.Name.GetHashCode() ^ base.Type.GetHashCode();
		}

		// Token: 0x060071A5 RID: 29093 RVA: 0x001F7B4B File Offset: 0x001F5D4B
		public sealed override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x001F7B58 File Offset: 0x001F5D58
		public sealed override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			SimpleVirtualPropertyDefinition simpleVirtualPropertyDefinition = obj as SimpleVirtualPropertyDefinition;
			return simpleVirtualPropertyDefinition != null && this.GetHashCode() == simpleVirtualPropertyDefinition.GetHashCode() && base.GetType() == simpleVirtualPropertyDefinition.GetType() && base.Name == simpleVirtualPropertyDefinition.Name && base.Type.Equals(simpleVirtualPropertyDefinition.Type);
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x001F7BC1 File Offset: 0x001F5DC1
		protected sealed override void ForEachMatch(PropertyDependencyType targetDependencyType, Action<NativeStorePropertyDefinition> action)
		{
		}

		// Token: 0x04004EFA RID: 20218
		private readonly LazilyInitialized<int> hashCode;
	}
}
