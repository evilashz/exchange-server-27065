using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties
{
	// Token: 0x0200008B RID: 139
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IWriteableProperty<TItem, TPropertyType>
	{
		// Token: 0x060003CC RID: 972
		void WriteProperty(TItem item, TPropertyType value);
	}
}
