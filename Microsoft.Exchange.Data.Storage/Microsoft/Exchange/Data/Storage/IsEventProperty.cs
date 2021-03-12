using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C30 RID: 3120
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsEventProperty : SmartPropertyDefinition
	{
		// Token: 0x06006ECA RID: 28362 RVA: 0x001DCC08 File Offset: 0x001DAE08
		internal IsEventProperty() : base("IsEvent", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiStartTime, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006ECB RID: 28363 RVA: 0x001DCC54 File Offset: 0x001DAE54
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			bool flag = false;
			ExDateTime? valueAsNullable = propertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.MapiStartTime);
			ExDateTime? valueAsNullable2 = propertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.MapiEndTime);
			if (valueAsNullable != null && valueAsNullable2 != null && valueAsNullable2.Value - valueAsNullable.Value >= IsEventProperty.Hours24)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x04004214 RID: 16916
		private static readonly TimeSpan Hours24 = TimeSpan.FromHours(24.0);
	}
}
