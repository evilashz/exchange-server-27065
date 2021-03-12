using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200008F RID: 143
	internal static class AuditLogSearchItemSchema
	{
		// Token: 0x04000241 RID: 577
		public static readonly Guid BasePropertyGuid = new Guid("9cff9e83-a0b3-4110-bcd8-617e9ea1e0fe");

		// Token: 0x04000242 RID: 578
		public static readonly Guid MailboxPropertyGuid = new Guid("7295f845-69ad-401b-a1ae-13dcadbe4c60");

		// Token: 0x04000243 RID: 579
		public static readonly Guid AdminPropertyGuid = new Guid("00914066-1863-4c10-82f1-d52616e61eab");

		// Token: 0x04000244 RID: 580
		public static readonly StorePropertyDefinition Identity = GuidIdPropertyDefinition.CreateCustom("Identity", typeof(Guid), AuditLogSearchItemSchema.BasePropertyGuid, 1, PropertyFlags.None);

		// Token: 0x04000245 RID: 581
		public static readonly StorePropertyDefinition Name = GuidIdPropertyDefinition.CreateCustom("Name", typeof(string), AuditLogSearchItemSchema.BasePropertyGuid, 2, PropertyFlags.None);

		// Token: 0x04000246 RID: 582
		public static readonly StorePropertyDefinition StartDate = GuidIdPropertyDefinition.CreateCustom("StartDate", typeof(ExDateTime), AuditLogSearchItemSchema.BasePropertyGuid, 3, PropertyFlags.None);

		// Token: 0x04000247 RID: 583
		public static readonly StorePropertyDefinition EndDate = GuidIdPropertyDefinition.CreateCustom("EndDate", typeof(ExDateTime), AuditLogSearchItemSchema.BasePropertyGuid, 4, PropertyFlags.None);

		// Token: 0x04000248 RID: 584
		public static readonly StorePropertyDefinition StatusMailRecipients = GuidIdPropertyDefinition.CreateCustom("StatusMailRecipients", typeof(string[]), AuditLogSearchItemSchema.BasePropertyGuid, 5, PropertyFlags.None);

		// Token: 0x04000249 RID: 585
		public static readonly StorePropertyDefinition CreatedByEx = GuidIdPropertyDefinition.CreateCustom("CreatedByEx", typeof(byte[]), AuditLogSearchItemSchema.BasePropertyGuid, 6, PropertyFlags.None);

		// Token: 0x0400024A RID: 586
		public static readonly StorePropertyDefinition CreatedBy = GuidIdPropertyDefinition.CreateCustom("CreatedBy", typeof(string), AuditLogSearchItemSchema.BasePropertyGuid, 7, PropertyFlags.None);

		// Token: 0x0400024B RID: 587
		public static readonly StorePropertyDefinition ExternalAccess = GuidIdPropertyDefinition.CreateCustom("ExternalAccess", typeof(string), AuditLogSearchItemSchema.MailboxPropertyGuid, 4, PropertyFlags.None);
	}
}
