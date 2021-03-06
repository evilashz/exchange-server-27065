using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000153 RID: 339
	internal class FailedDocumentSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040005F6 RID: 1526
		public static SimpleProviderPropertyDefinition DocID = new SimpleProviderPropertyDefinition("DocID", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005F7 RID: 1527
		public static SimpleProviderPropertyDefinition Database = new SimpleProviderPropertyDefinition("Database", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005F8 RID: 1528
		public static SimpleProviderPropertyDefinition MailboxGuid = new SimpleProviderPropertyDefinition("MailboxGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005F9 RID: 1529
		public static SimpleProviderPropertyDefinition Mailbox = new SimpleProviderPropertyDefinition("Mailbox", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005FA RID: 1530
		public static SimpleProviderPropertyDefinition SmtpAddress = new SimpleProviderPropertyDefinition("SmtpAddress", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005FB RID: 1531
		public static SimpleProviderPropertyDefinition EntryID = new SimpleProviderPropertyDefinition("EntryID", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005FC RID: 1532
		public static SimpleProviderPropertyDefinition Subject = new SimpleProviderPropertyDefinition("Subject", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005FD RID: 1533
		public static SimpleProviderPropertyDefinition ErrorCode = new SimpleProviderPropertyDefinition("ErrorCode", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005FE RID: 1534
		public static SimpleProviderPropertyDefinition Description = new SimpleProviderPropertyDefinition("Description", ExchangeObjectVersion.Exchange2007, typeof(LocalizedString), PropertyDefinitionFlags.None, LocalizedString.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040005FF RID: 1535
		public static SimpleProviderPropertyDefinition IsPartialIndexed = new SimpleProviderPropertyDefinition("IsPartialIndexed", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000600 RID: 1536
		public static SimpleProviderPropertyDefinition FailedTime = new SimpleProviderPropertyDefinition("FailedTime", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000601 RID: 1537
		public static SimpleProviderPropertyDefinition AdditionalInfo = new SimpleProviderPropertyDefinition("AdditionalInfo", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000602 RID: 1538
		public static SimpleProviderPropertyDefinition FailureMode = new SimpleProviderPropertyDefinition("FailureMode", ExchangeObjectVersion.Exchange2007, typeof(FailureMode), PropertyDefinitionFlags.None, Microsoft.Exchange.Search.Core.Abstraction.FailureMode.Permanent, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000603 RID: 1539
		public static SimpleProviderPropertyDefinition AttemptCount = new SimpleProviderPropertyDefinition("AttemptCount", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
