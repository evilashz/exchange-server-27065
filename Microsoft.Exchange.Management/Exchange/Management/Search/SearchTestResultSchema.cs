using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x0200015F RID: 351
	internal class SearchTestResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000633 RID: 1587
		public static SimpleProviderPropertyDefinition Mailbox = new SimpleProviderPropertyDefinition("Mailbox", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000634 RID: 1588
		public static SimpleProviderPropertyDefinition MailboxGuid = new SimpleProviderPropertyDefinition("MailboxGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000635 RID: 1589
		public static SimpleProviderPropertyDefinition UserLegacyExchangeDN = new SimpleProviderPropertyDefinition("UserLegacyExchangeDN", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000636 RID: 1590
		public static SimpleProviderPropertyDefinition Database = new SimpleProviderPropertyDefinition("Database", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000637 RID: 1591
		public static SimpleProviderPropertyDefinition DatabaseGuid = new SimpleProviderPropertyDefinition("DatabaseGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000638 RID: 1592
		public static SimpleProviderPropertyDefinition ServerGuid = new SimpleProviderPropertyDefinition("ServerGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000639 RID: 1593
		public static SimpleProviderPropertyDefinition ResultFound = new SimpleProviderPropertyDefinition("ResultFound", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400063A RID: 1594
		public static SimpleProviderPropertyDefinition SearchTimeInSeconds = new SimpleProviderPropertyDefinition("SearchTimeInSeconds", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400063B RID: 1595
		public static SimpleProviderPropertyDefinition DetailEvents = new SimpleProviderPropertyDefinition("DetailEvents", ExchangeObjectVersion.Exchange2007, typeof(List<MonitoringEvent>), PropertyDefinitionFlags.None, new List<MonitoringEvent>(), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400063C RID: 1596
		public static SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400063D RID: 1597
		public static SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
