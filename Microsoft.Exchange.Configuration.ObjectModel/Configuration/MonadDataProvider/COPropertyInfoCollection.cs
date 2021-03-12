using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001CE RID: 462
	internal class COPropertyInfoCollection : KeyedCollection<string, COPropertyInfo>
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x00031C2F File Offset: 0x0002FE2F
		protected override string GetKeyForItem(COPropertyInfo item)
		{
			return item.Name;
		}
	}
}
