using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008ED RID: 2285
	internal interface IEntity<T>
	{
		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060050F5 RID: 20725
		ADObjectId Identity { get; }

		// Token: 0x060050F6 RID: 20726
		bool Equals(T obj);

		// Token: 0x060050F7 RID: 20727
		T Clone(ADObjectId identity);

		// Token: 0x060050F8 RID: 20728
		void UpdateFrom(T obj);
	}
}
