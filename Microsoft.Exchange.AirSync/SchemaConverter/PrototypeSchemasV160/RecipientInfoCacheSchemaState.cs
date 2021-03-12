using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV160
{
	// Token: 0x020001E5 RID: 485
	internal class RecipientInfoCacheSchemaState : AirSyncRecipientInfoCacheSchemaState
	{
		// Token: 0x06001372 RID: 4978 RVA: 0x0007014D File Offset: 0x0006E34D
		public RecipientInfoCacheSchemaState()
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00070164 File Offset: 0x0006E364
		private void CreatePropertyConversionTable()
		{
			string xmlNodeNamespace = "Contacts:";
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Email1Address", false),
				new RecipientInfoCacheStringProperty(RecipientInfoCacheEntryElements.EmailAddress)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "FileAs", false),
				new RecipientInfoCacheStringProperty(RecipientInfoCacheEntryElements.DisplayName)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncStringProperty(xmlNodeNamespace, "Alias", false),
				new RecipientInfoCacheStringProperty(RecipientInfoCacheEntryElements.Alias)
			});
			base.AddProperty(new IProperty[]
			{
				new AirSyncIntegerProperty(xmlNodeNamespace, "WeightedRank", false),
				new RecipientInfoCacheIntProperty(RecipientInfoCacheEntryElements.WeightedRank)
			});
		}
	}
}
