using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties
{
	// Token: 0x0200008C RID: 140
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReadableProperty<TItem, TPropertyType>
	{
		// Token: 0x060003CD RID: 973
		TPropertyType ReadProperty(TItem item);
	}
}
