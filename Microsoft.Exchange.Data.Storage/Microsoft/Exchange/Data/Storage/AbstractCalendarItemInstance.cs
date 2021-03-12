using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C5 RID: 709
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractCalendarItemInstance : AbstractCalendarItemBase, ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
	}
}
