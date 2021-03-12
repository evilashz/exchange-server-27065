using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200001E RID: 30
	public interface IADRawEntry : IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C4 RID: 452
		ADObjectId Id { get; }
	}
}
