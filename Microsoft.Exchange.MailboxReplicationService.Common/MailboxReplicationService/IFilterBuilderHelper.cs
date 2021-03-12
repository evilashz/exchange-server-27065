using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000EA RID: 234
	internal interface IFilterBuilderHelper
	{
		// Token: 0x060008E3 RID: 2275
		PropTag MapNamedProperty(NamedPropData npd, PropType propType);

		// Token: 0x060008E4 RID: 2276
		string[] MapRecipient(string recipientId);

		// Token: 0x060008E5 RID: 2277
		Guid[] MapPolicyTag(string policyTagStr);
	}
}
