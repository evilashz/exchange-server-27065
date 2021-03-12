using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C0 RID: 2496
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditLogSearchBaseEwsSchema : ObjectSchema
	{
		// Token: 0x040032C6 RID: 12998
		public static readonly Guid BasePropertySetId = new Guid("9cff9e83-a0b3-4110-bcd8-617e9ea1e0fe");

		// Token: 0x040032C7 RID: 12999
		public static readonly Guid MailboxPropertySetId = new Guid("7295f845-69ad-401b-a1ae-13dcadbe4c60");

		// Token: 0x040032C8 RID: 13000
		public static readonly Guid AdminPropertySetId = new Guid("00914066-1863-4c10-82f1-d52616e61eab");

		// Token: 0x040032C9 RID: 13001
		public static readonly SimplePropertyDefinition ObjectState = EwsStoreObjectSchema.ObjectState;

		// Token: 0x040032CA RID: 13002
		public static readonly EwsStoreObjectPropertyDefinition Identity = new EwsStoreObjectPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(AuditLogSearchId), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 1, 5));

		// Token: 0x040032CB RID: 13003
		public static readonly EwsStoreObjectPropertyDefinition Name = new EwsStoreObjectPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 2, 25));

		// Token: 0x040032CC RID: 13004
		public static readonly EwsStoreObjectPropertyDefinition CreationTime = new EwsStoreObjectPropertyDefinition("CreationTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.DateTimeCreated);

		// Token: 0x040032CD RID: 13005
		public static readonly EwsStoreObjectPropertyDefinition StartDateUtc = new EwsStoreObjectPropertyDefinition("StartDateUtc", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 3, 23));

		// Token: 0x040032CE RID: 13006
		public static readonly EwsStoreObjectPropertyDefinition EndDateUtc = new EwsStoreObjectPropertyDefinition("EndDateUtc", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 4, 23));

		// Token: 0x040032CF RID: 13007
		public static readonly EwsStoreObjectPropertyDefinition StatusMailRecipients = new EwsStoreObjectPropertyDefinition("StatusMailRecipients", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 5, 26));

		// Token: 0x040032D0 RID: 13008
		public static readonly EwsStoreObjectPropertyDefinition CreatedByEx = new EwsStoreObjectPropertyDefinition("CreatedByEx", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 6, 2));

		// Token: 0x040032D1 RID: 13009
		public static readonly EwsStoreObjectPropertyDefinition CreatedBy = new EwsStoreObjectPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, 7, 25));

		// Token: 0x040032D2 RID: 13010
		public static readonly EwsStoreObjectPropertyDefinition ExternalAccess = new EwsStoreObjectPropertyDefinition("ExternalAccess", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.MailboxPropertySetId, 4, 25));

		// Token: 0x040032D3 RID: 13011
		public static readonly EwsStoreObjectPropertyDefinition Type = new EwsStoreObjectPropertyDefinition("Type", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.BasePropertySetId, "Type", 25));

		// Token: 0x040032D4 RID: 13012
		public static readonly EwsStoreObjectPropertyDefinition ItemClass = new EwsStoreObjectPropertyDefinition("ItemClass", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.ItemClass);

		// Token: 0x040032D5 RID: 13013
		public static readonly EwsStoreObjectPropertyDefinition EwsItemId = new EwsStoreObjectPropertyDefinition("EwsItemId", ExchangeObjectVersion.Exchange2010, typeof(EwsStoreObjectId), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.Id);
	}
}
