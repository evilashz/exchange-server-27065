using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000016 RID: 22
	internal interface IPropertyBag : IReadOnlyPropertyBag
	{
		// Token: 0x0600006B RID: 107
		void SetProperty(PropertyDefinition property, object value);
	}
}
