using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000090 RID: 144
	internal static class MailboxAuditLogSearchItemSchema
	{
		// Token: 0x0400024C RID: 588
		public const string MessageClass = "IPM.AuditLogSearch.Mailbox";

		// Token: 0x0400024D RID: 589
		public const string FolderName = "MailboxAuditLogSearch";

		// Token: 0x0400024E RID: 590
		public static readonly StorePropertyDefinition MailboxIds = GuidIdPropertyDefinition.CreateCustom("MailboxIds", typeof(byte[][]), AuditLogSearchItemSchema.MailboxPropertyGuid, 1, PropertyFlags.None);

		// Token: 0x0400024F RID: 591
		public static readonly StorePropertyDefinition LogonTypeStrings = GuidIdPropertyDefinition.CreateCustom("LogonTypeStrings", typeof(string[]), AuditLogSearchItemSchema.MailboxPropertyGuid, 2, PropertyFlags.None);

		// Token: 0x04000250 RID: 592
		public static readonly StorePropertyDefinition ShowDetails = GuidIdPropertyDefinition.CreateCustom("ShowDetails", typeof(bool), AuditLogSearchItemSchema.MailboxPropertyGuid, 3, PropertyFlags.None);

		// Token: 0x04000251 RID: 593
		public static readonly StorePropertyDefinition Operations = GuidIdPropertyDefinition.CreateCustom("Operations", typeof(string[]), AuditLogSearchItemSchema.MailboxPropertyGuid, 4, PropertyFlags.None);
	}
}
