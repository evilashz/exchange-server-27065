using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000575 RID: 1397
	internal class MapiTransactionOutcomeSchema : ObjectSchema
	{
		// Token: 0x040022E0 RID: 8928
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E1 RID: 8929
		public static readonly SimpleProviderPropertyDefinition Database = new SimpleProviderPropertyDefinition("Database", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E2 RID: 8930
		public static readonly SimpleProviderPropertyDefinition Mailbox = new SimpleProviderPropertyDefinition("Mailbox", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E3 RID: 8931
		public static readonly SimpleProviderPropertyDefinition MailboxGuid = new SimpleProviderPropertyDefinition("MailboxGuid", ExchangeObjectVersion.Exchange2003, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E4 RID: 8932
		public static readonly SimpleProviderPropertyDefinition IsArchive = new SimpleProviderPropertyDefinition("IsArchive", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E5 RID: 8933
		public static readonly SimpleProviderPropertyDefinition IsDatabaseCopyActive = new SimpleProviderPropertyDefinition("IsDatabaseCopyActive", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E6 RID: 8934
		public static readonly SimpleProviderPropertyDefinition Latency = new SimpleProviderPropertyDefinition("Latency", ExchangeObjectVersion.Exchange2003, typeof(TimeSpan?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E7 RID: 8935
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040022E8 RID: 8936
		public static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2003, typeof(MapiTransactionResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
