using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000020 RID: 32
	public interface IObjectComparer : ISupportTextComparer
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B8 RID: 440
		ITextComparer TextComparer { get; }

		// Token: 0x060001B9 RID: 441
		SortMode GetSortMode(Type type);
	}
}
