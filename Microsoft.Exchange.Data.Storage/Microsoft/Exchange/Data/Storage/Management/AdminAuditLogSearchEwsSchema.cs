using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C2 RID: 2498
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AdminAuditLogSearchEwsSchema : AuditLogSearchBaseEwsSchema
	{
		// Token: 0x040032DA RID: 13018
		public static readonly EwsStoreObjectPropertyDefinition Cmdlets = new EwsStoreObjectPropertyDefinition("Cmdlets", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.ReturnOnBind, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.AdminPropertySetId, 1, 26));

		// Token: 0x040032DB RID: 13019
		public static readonly EwsStoreObjectPropertyDefinition Parameters = new EwsStoreObjectPropertyDefinition("Parameters", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.AdminPropertySetId, 2, 26));

		// Token: 0x040032DC RID: 13020
		public static readonly EwsStoreObjectPropertyDefinition ObjectIds = new EwsStoreObjectPropertyDefinition("ObjectIds", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.ReturnOnBind, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.AdminPropertySetId, 3, 26));

		// Token: 0x040032DD RID: 13021
		public static readonly EwsStoreObjectPropertyDefinition UserIds = new EwsStoreObjectPropertyDefinition("UserIds", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.ReturnOnBind, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.AdminPropertySetId, 4, 26));

		// Token: 0x040032DE RID: 13022
		public static readonly EwsStoreObjectPropertyDefinition ResolvedUsers = new EwsStoreObjectPropertyDefinition("ResolvedUsers", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.AdminPropertySetId, 5, 26));

		// Token: 0x040032DF RID: 13023
		public static readonly EwsStoreObjectPropertyDefinition RedactDatacenterAdmins = new EwsStoreObjectPropertyDefinition("RedactDatacenterAdmins", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, false, new ExtendedPropertyDefinition(AuditLogSearchBaseEwsSchema.AdminPropertySetId, 6, 4));
	}
}
