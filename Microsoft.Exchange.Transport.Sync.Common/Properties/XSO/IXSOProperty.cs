using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties.XSO
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IXSOProperty<T> : IProperty<Item, T>, IWriteableProperty<Item, T>, IReadableProperty<Item, T>
	{
	}
}
