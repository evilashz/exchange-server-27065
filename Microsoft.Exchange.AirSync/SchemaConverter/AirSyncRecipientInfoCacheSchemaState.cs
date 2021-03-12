using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B0 RID: 432
	internal class AirSyncRecipientInfoCacheSchemaState : AirSyncSchemaState, IDataObjectGenerator
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x00062F06 File Offset: 0x00061106
		public RecipientInfoCacheDataObject GetRecipientInfoCacheDataObject()
		{
			return new RecipientInfoCacheDataObject(base.GetSchema(1));
		}
	}
}
