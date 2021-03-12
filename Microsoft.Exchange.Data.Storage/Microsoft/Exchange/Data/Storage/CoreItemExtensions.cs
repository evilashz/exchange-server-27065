using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000649 RID: 1609
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CoreItemExtensions
	{
		// Token: 0x06004295 RID: 17045 RVA: 0x0011C234 File Offset: 0x0011A434
		public static string ClassName(this ICoreItem coreItem)
		{
			return coreItem.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
		}
	}
}
