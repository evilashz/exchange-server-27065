using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C8 RID: 712
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContactBase : IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001E98 RID: 7832
		// (set) Token: 0x06001E99 RID: 7833
		string DisplayName { get; set; }
	}
}
