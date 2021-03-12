using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C4 RID: 708
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarItemInstance : ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
	}
}
