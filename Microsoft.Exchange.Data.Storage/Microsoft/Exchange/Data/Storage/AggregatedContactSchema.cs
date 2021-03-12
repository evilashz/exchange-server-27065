using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C0E RID: 3086
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedContactSchema : Schema
	{
		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06006E19 RID: 28185 RVA: 0x001D8EB0 File Offset: 0x001D70B0
		public new static AggregatedContactSchema Instance
		{
			get
			{
				return AggregatedContactSchema.instance;
			}
		}

		// Token: 0x04003EF7 RID: 16119
		public static readonly PropertyDefinition CompanyName = new ReadonlySmartProperty(InternalSchema.InternalPersonCompanyName);

		// Token: 0x04003EF8 RID: 16120
		public static readonly PropertyDefinition DisplayName = new ReadonlySmartProperty(InternalSchema.InternalPersonDisplayName);

		// Token: 0x04003EF9 RID: 16121
		public static readonly PropertyDefinition GivenName = new ReadonlySmartProperty(InternalSchema.InternalPersonGivenName);

		// Token: 0x04003EFA RID: 16122
		public static readonly PropertyDefinition Surname = new ReadonlySmartProperty(InternalSchema.InternalPersonSurname);

		// Token: 0x04003EFB RID: 16123
		public static readonly PropertyDefinition FileAs = new FileAsStringProperty(InternalSchema.InternalPersonFileAs);

		// Token: 0x04003EFC RID: 16124
		public static readonly PropertyDefinition HomeCity = new ReadonlySmartProperty(InternalSchema.InternalPersonHomeCity);

		// Token: 0x04003EFD RID: 16125
		public static readonly PropertyDefinition CreationTime = new ReadonlySmartProperty(InternalSchema.InternalPersonCreationTime);

		// Token: 0x04003EFE RID: 16126
		public static readonly PropertyDefinition WorkCity = new ReadonlySmartProperty(InternalSchema.InternalPersonWorkCity);

		// Token: 0x04003EFF RID: 16127
		public static readonly PropertyDefinition DisplayNameFirstLast = new ReadonlySmartProperty(InternalSchema.InternalPersonDisplayNameFirstLast);

		// Token: 0x04003F00 RID: 16128
		public static readonly PropertyDefinition DisplayNameLastFirst = new ReadonlySmartProperty(InternalSchema.InternalPersonDisplayNameLastFirst);

		// Token: 0x04003F01 RID: 16129
		public static readonly PropertyDefinition RelevanceScore = new ReadonlySmartProperty(InternalSchema.InternalPersonRelevanceScore);

		// Token: 0x04003F02 RID: 16130
		public static readonly PropertyDefinition MessageClass = new ReadonlySmartProperty(InternalSchema.InternalConversationMessageClasses);

		// Token: 0x04003F03 RID: 16131
		private static readonly AggregatedContactSchema instance = new AggregatedContactSchema();
	}
}
