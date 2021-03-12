using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000020 RID: 32
	public interface IADObject : IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D8 RID: 472
		Guid Guid { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D9 RID: 473
		string Name { get; }
	}
}
