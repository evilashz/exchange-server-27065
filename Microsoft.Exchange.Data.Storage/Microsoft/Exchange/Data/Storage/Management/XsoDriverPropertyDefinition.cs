using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A8E RID: 2702
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class XsoDriverPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x060062F6 RID: 25334 RVA: 0x001A1D92 File Offset: 0x0019FF92
		// (set) Token: 0x060062F7 RID: 25335 RVA: 0x001A1D9A File Offset: 0x0019FF9A
		public object InitialValue { get; private set; }

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x001A1DA3 File Offset: 0x0019FFA3
		// (set) Token: 0x060062F9 RID: 25337 RVA: 0x001A1DAB File Offset: 0x0019FFAB
		public StorePropertyDefinition StorePropertyDefinition { get; private set; }

		// Token: 0x060062FA RID: 25338 RVA: 0x001A1DB4 File Offset: 0x0019FFB4
		public XsoDriverPropertyDefinition(StorePropertyDefinition storePropertyDefinition, string name, ExchangeObjectVersion versionAdded, PropertyDefinitionFlags flags, object defaultValue, object initialValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : base(name, versionAdded, XsoDriverPropertyDefinition.WrapValueTypeByNullable(storePropertyDefinition.Type), flags, defaultValue, readConstraints, XsoDriverPropertyDefinition.MergeWithXsoConstraints(writeConstraints, storePropertyDefinition))
		{
			this.InitialValue = initialValue;
			this.StorePropertyDefinition = storePropertyDefinition;
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x001A1DF4 File Offset: 0x0019FFF4
		private static Type WrapValueTypeByNullable(Type propType)
		{
			if (null == propType)
			{
				throw new ArgumentNullException("propType");
			}
			if (!propType.IsValueType)
			{
				return propType;
			}
			if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return propType;
			}
			return typeof(Nullable<>).MakeGenericType(new Type[]
			{
				propType
			});
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x001A1E5C File Offset: 0x001A005C
		private static PropertyDefinitionConstraint[] MergeWithXsoConstraints(PropertyDefinitionConstraint[] constraints, StorePropertyDefinition storePropertyDefinition)
		{
			if (storePropertyDefinition.Constraints.Count == 0)
			{
				return constraints;
			}
			if (constraints == null || constraints.Length == 0)
			{
				return new PropertyDefinitionConstraint[]
				{
					new XsoDriverPropertyConstraint(storePropertyDefinition)
				};
			}
			return new List<PropertyDefinitionConstraint>(constraints)
			{
				new XsoDriverPropertyConstraint(storePropertyDefinition)
			}.ToArray();
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x001A1EAC File Offset: 0x001A00AC
		public override bool Equals(ProviderPropertyDefinition other)
		{
			if (object.ReferenceEquals(other, this))
			{
				return true;
			}
			XsoDriverPropertyDefinition xsoDriverPropertyDefinition = other as XsoDriverPropertyDefinition;
			return xsoDriverPropertyDefinition != null && object.Equals(this.InitialValue, xsoDriverPropertyDefinition.InitialValue) && this.StorePropertyDefinition.CompareTo(xsoDriverPropertyDefinition.StorePropertyDefinition) == 0 && base.Equals(other);
		}
	}
}
