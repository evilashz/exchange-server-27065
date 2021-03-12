using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IProperty<TItem, TPropertyType> : IWriteableProperty<TItem, TPropertyType>, IReadableProperty<TItem, TPropertyType>
	{
	}
}
