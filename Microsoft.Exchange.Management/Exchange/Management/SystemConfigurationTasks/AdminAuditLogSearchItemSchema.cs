using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000091 RID: 145
	internal static class AdminAuditLogSearchItemSchema
	{
		// Token: 0x04000252 RID: 594
		public const string MessageClass = "IPM.AuditLogSearch.Admin";

		// Token: 0x04000253 RID: 595
		public const string FolderName = "AdminAuditLogSearch";

		// Token: 0x04000254 RID: 596
		public static readonly StorePropertyDefinition Cmdlets = GuidIdPropertyDefinition.CreateCustom("Cmdlets", typeof(string[]), AuditLogSearchItemSchema.AdminPropertyGuid, 1, PropertyFlags.None);

		// Token: 0x04000255 RID: 597
		public static readonly StorePropertyDefinition Parameters = GuidIdPropertyDefinition.CreateCustom("Parameters", typeof(string[]), AuditLogSearchItemSchema.AdminPropertyGuid, 2, PropertyFlags.None);

		// Token: 0x04000256 RID: 598
		public static readonly StorePropertyDefinition ObjectIds = GuidIdPropertyDefinition.CreateCustom("ObjectIds", typeof(string[]), AuditLogSearchItemSchema.AdminPropertyGuid, 3, PropertyFlags.None);

		// Token: 0x04000257 RID: 599
		public static readonly StorePropertyDefinition RawUserIds = GuidIdPropertyDefinition.CreateCustom("RawUserIds", typeof(string[]), AuditLogSearchItemSchema.AdminPropertyGuid, 4, PropertyFlags.None);

		// Token: 0x04000258 RID: 600
		public static readonly StorePropertyDefinition ResolvedUsers = GuidIdPropertyDefinition.CreateCustom("ResolvedUsers", typeof(string[]), AuditLogSearchItemSchema.AdminPropertyGuid, 5, PropertyFlags.None);

		// Token: 0x04000259 RID: 601
		public static readonly StorePropertyDefinition RedactDatacenterAdmins = GuidIdPropertyDefinition.CreateCustom("RedactDatacenterAdmins", typeof(bool), AuditLogSearchItemSchema.AdminPropertyGuid, 6, PropertyFlags.None);
	}
}
