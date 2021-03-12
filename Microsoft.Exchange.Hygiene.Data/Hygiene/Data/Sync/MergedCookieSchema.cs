using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200021F RID: 543
	internal class MergedCookieSchema : BaseCookieSchema
	{
		// Token: 0x04000B43 RID: 2883
		public static readonly HygienePropertyDefinition ContextIdProperty = new HygienePropertyDefinition("ContextId", typeof(ADObjectId));
	}
}
