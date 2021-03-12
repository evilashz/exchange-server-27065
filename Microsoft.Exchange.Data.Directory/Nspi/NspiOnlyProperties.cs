using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x020001D1 RID: 465
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class NspiOnlyProperties
	{
		// Token: 0x04000AD1 RID: 2769
		public static readonly NspiPropertyDefinition DisplayType = new NspiPropertyDefinition(PropTag.DisplayType, typeof(LegacyRecipientDisplayType?), "nspiDisplayType", ADPropertyDefinitionFlags.ReadOnly, null, true);

		// Token: 0x04000AD2 RID: 2770
		public static readonly NspiPropertyDefinition DisplayTypeEx = new NspiPropertyDefinition(PropTag.DisplayTypeEx, typeof(RecipientDisplayType?), "nspiDisplayTypeEx", ADPropertyDefinitionFlags.ReadOnly, null, true);
	}
}
