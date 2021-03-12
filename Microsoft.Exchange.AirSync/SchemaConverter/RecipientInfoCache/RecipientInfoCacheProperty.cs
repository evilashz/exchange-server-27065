using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache
{
	// Token: 0x020001E8 RID: 488
	internal abstract class RecipientInfoCacheProperty : PropertyBase
	{
		// Token: 0x0600137C RID: 4988
		public abstract void Bind(RecipientInfoCacheEntry entry);
	}
}
