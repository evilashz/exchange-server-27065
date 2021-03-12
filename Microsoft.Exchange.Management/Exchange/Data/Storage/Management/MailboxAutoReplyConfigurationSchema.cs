using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.OOF;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020007BC RID: 1980
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAutoReplyConfigurationSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x04002AE1 RID: 10977
		public static readonly SimplePropertyDefinition AutoReplyState = new SimplePropertyDefinition("AutoReplyState", ExchangeObjectVersion.Exchange2007, typeof(OofState), PropertyDefinitionFlags.None, OofState.Disabled, OofState.Disabled, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002AE2 RID: 10978
		public static readonly SimplePropertyDefinition EndTime = new SimplePropertyDefinition("EndTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime), PropertyDefinitionFlags.None, DateTime.MinValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002AE3 RID: 10979
		public static readonly SimplePropertyDefinition ExternalAudience = new SimplePropertyDefinition("ExternalAudience", ExchangeObjectVersion.Exchange2007, typeof(ExternalAudience), PropertyDefinitionFlags.None, Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.None, Microsoft.Exchange.InfoWorker.Common.OOF.ExternalAudience.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04002AE4 RID: 10980
		public static readonly SimplePropertyDefinition ExternalMessage = new SimplePropertyDefinition("ExternalMessage", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 128000)
		});

		// Token: 0x04002AE5 RID: 10981
		public static readonly SimpleProviderPropertyDefinition Identity = XsoMailboxConfigurationObjectSchema.MailboxOwnerId;

		// Token: 0x04002AE6 RID: 10982
		public static readonly SimplePropertyDefinition InternalMessage = new SimplePropertyDefinition("InternalMessage", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 128000)
		});

		// Token: 0x04002AE7 RID: 10983
		public static readonly SimplePropertyDefinition StartTime = new SimplePropertyDefinition("StartTime", ExchangeObjectVersion.Exchange2003, typeof(DateTime), PropertyDefinitionFlags.None, DateTime.MinValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
