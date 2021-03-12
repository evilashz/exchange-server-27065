using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007B9 RID: 1977
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class OwaFilteredViewProperties
	{
		// Token: 0x0400281C RID: 10268
		private const string PropertyNamePrefix = "http://schemas.microsoft.com/exchange/";

		// Token: 0x0400281D RID: 10269
		private static readonly Guid publicStringsGuid = new Guid("00020329-0000-0000-C000-000000000046");

		// Token: 0x0400281E RID: 10270
		public static readonly PropertyDefinition FilteredViewLabel = GuidNamePropertyDefinition.CreateCustom("FilteredViewLabel", typeof(string[]), OwaFilteredViewProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fldfltr", PropertyFlags.None);

		// Token: 0x0400281F RID: 10271
		public static readonly PropertyDefinition FilteredViewAccessTime = GuidNamePropertyDefinition.CreateCustom("FilteredViewAccessTime", typeof(ExDateTime), OwaFilteredViewProperties.publicStringsGuid, "http://schemas.microsoft.com/exchange/fltract", PropertyFlags.None);
	}
}
