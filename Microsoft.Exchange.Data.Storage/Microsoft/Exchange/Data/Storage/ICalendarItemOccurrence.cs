using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003AD RID: 941
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarItemOccurrence : ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06002AEB RID: 10987
		bool IsException { get; }

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06002AEC RID: 10988
		VersionedId MasterId { get; }

		// Token: 0x06002AED RID: 10989
		void MakeModifiedOccurrence();
	}
}
