using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C39 RID: 3129
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class NullLocationProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EE1 RID: 28385 RVA: 0x001DD393 File Offset: 0x001DB593
		internal NullLocationProperty(string propertyName) : base(propertyName + "_NullValue", typeof(object), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[0])
		{
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x001DD3BC File Offset: 0x001DB5BC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return null;
		}
	}
}
