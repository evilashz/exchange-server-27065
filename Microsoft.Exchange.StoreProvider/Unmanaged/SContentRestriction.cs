using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002D5 RID: 725
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct SContentRestriction
	{
		// Token: 0x040011F9 RID: 4601
		internal int ulFuzzyLevel;

		// Token: 0x040011FA RID: 4602
		internal int ulPropTag;

		// Token: 0x040011FB RID: 4603
		internal unsafe SPropValue* lpProp;
	}
}
