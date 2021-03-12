using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F8 RID: 1272
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsContactWritableProperty : SmartPropertyDefinition
	{
		// Token: 0x0600374A RID: 14154 RVA: 0x000DDFB4 File Offset: 0x000DC1B4
		internal IsContactWritableProperty() : base("IsContactWritable", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, IsContactWritableProperty.PropertyDependencies)
		{
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x000DDFD6 File Offset: 0x000DC1D6
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return string.IsNullOrEmpty(propertyBag.GetValueOrDefault<string>(InternalSchema.PartnerNetworkId, null));
		}

		// Token: 0x04001D5C RID: 7516
		private static readonly PropertyDependency[] PropertyDependencies = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.PartnerNetworkId, PropertyDependencyType.NeedForRead)
		};
	}
}
