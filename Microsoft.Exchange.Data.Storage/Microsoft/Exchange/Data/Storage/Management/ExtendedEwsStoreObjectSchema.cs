using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009C7 RID: 2503
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExtendedEwsStoreObjectSchema : ObjectSchema
	{
		// Token: 0x040032E8 RID: 13032
		private static readonly Guid DefaultPropertySetId = new Guid("5822CB43-247D-4953-AB15-83AB07C54CE8");

		// Token: 0x040032E9 RID: 13033
		public static readonly ExtendedPropertyDefinition AlternativeId = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "AlternativeId", 25);

		// Token: 0x040032EA RID: 13034
		public static readonly ExtendedPropertyDefinition ExchangeVersion = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "ExchangeVersion", 16);

		// Token: 0x040032EB RID: 13035
		public static readonly ExtendedPropertyDefinition ExtendedAttributes = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "ExtendedAttributes", 2);

		// Token: 0x040032EC RID: 13036
		public static readonly ExtendedPropertyDefinition Message = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "Message", 2);

		// Token: 0x040032ED RID: 13037
		public static readonly ExtendedPropertyDefinition PercentComplete = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "PercentComplete", 14);

		// Token: 0x040032EE RID: 13038
		public static readonly ExtendedPropertyDefinition Status = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "Status", 14);

		// Token: 0x040032EF RID: 13039
		public static readonly ExtendedPropertyDefinition DisplayName = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "DisplayName", 2);

		// Token: 0x040032F0 RID: 13040
		public static readonly ExtendedPropertyDefinition IsNotificationEmailFromTaskSent = new ExtendedPropertyDefinition(ExtendedEwsStoreObjectSchema.DefaultPropertySetId, "IsNotificationEmailFromTaskSent", 4);
	}
}
