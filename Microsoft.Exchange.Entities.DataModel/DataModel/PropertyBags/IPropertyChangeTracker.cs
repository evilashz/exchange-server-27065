using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x02000005 RID: 5
	public interface IPropertyChangeTracker<in TPropertyDefinition>
	{
		// Token: 0x0600000C RID: 12
		bool IsPropertySet(TPropertyDefinition property);
	}
}
