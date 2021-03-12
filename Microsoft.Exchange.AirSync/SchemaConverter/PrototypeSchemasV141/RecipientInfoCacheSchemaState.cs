using System;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.RecipientInfoCache;

namespace Microsoft.Exchange.AirSync.SchemaConverter.PrototypeSchemasV141
{
	// Token: 0x020001DE RID: 478
	internal class RecipientInfoCacheSchemaState : AirSyncRecipientInfoCacheSchemaState
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x0006DADD File Offset: 0x0006BCDD
		public RecipientInfoCacheSchemaState()
		{
			base.InitConversionTable(2);
			this.CreatePropertyConversionTable();
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0006DAF4 File Offset: 0x0006BCF4
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
