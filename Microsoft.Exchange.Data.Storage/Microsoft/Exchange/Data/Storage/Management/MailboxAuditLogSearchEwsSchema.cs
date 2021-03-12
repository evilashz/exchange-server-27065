using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C1 RID: 2497
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAuditLogSearchEwsSchema : AuditLogSearchBaseEwsSchema
	{
		// Token: 0x040032D6 RID: 13014
		public static readonly EwsStoreObjectPropertyDefinition MailboxIds = new EwsStoreObjectPropertyDefinition("MailboxIds", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.ReturnOnBind, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.MailboxPropertySetId, 1, 3));

		// Token: 0x040032D7 RID: 13015
		public static readonly EwsStoreObjectPropertyDefinition LogonTypeStrings = new EwsStoreObjectPropertyDefinition("LogonTypeStrings", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.MailboxPropertySetId, 2, 26));

		// Token: 0x040032D8 RID: 13016
		public static readonly EwsStoreObjectPropertyDefinition ShowDetails = new EwsStoreObjectPropertyDefinition("ShowDetails", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.MailboxPropertySetId, 3, 4));

		// Token: 0x040032D9 RID: 13017
		public static readonly EwsStoreObjectPropertyDefinition Operations = new EwsStoreObjectPropertyDefinition("Operations", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.MailboxPropertySetId, 4, 26));
	}
}
