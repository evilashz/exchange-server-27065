using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F7 RID: 1271
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPlace : IContact, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
	}
}
