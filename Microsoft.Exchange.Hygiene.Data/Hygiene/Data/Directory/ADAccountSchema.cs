using System;
using Microsoft.Exchange.Hygiene.Data.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000B9 RID: 185
	internal class ADAccountSchema : CommonSyncProperties
	{
		// Token: 0x040003C0 RID: 960
		public static readonly HygienePropertyDefinition DisplayNameProperty = new HygienePropertyDefinition("DisplayName", typeof(string));
	}
}
