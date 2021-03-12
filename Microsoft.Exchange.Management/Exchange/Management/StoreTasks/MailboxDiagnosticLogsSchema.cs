using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200079E RID: 1950
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDiagnosticLogsSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04002A78 RID: 10872
		public static readonly SimplePropertyDefinition MailboxLog = new SimplePropertyDefinition("MailboxLog", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002A79 RID: 10873
		public static readonly SimplePropertyDefinition LogName = new SimplePropertyDefinition("LogName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
