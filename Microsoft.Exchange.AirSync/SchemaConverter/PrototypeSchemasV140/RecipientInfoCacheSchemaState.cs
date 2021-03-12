using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV140
{
	// Token: 0x020001D6 RID: 470
	internal class RecipientInfoCacheSchemaState : AirSyncRecipientInfoCacheSchemaState
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x0006B8D9 File Offset: 0x00069AD9
		public RecipientInfoCacheSchemaState()
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0006B8F0 File Offset: 0x00069AF0
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
