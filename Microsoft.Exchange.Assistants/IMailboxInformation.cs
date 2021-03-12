using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000063 RID: 99
	internal interface IMailboxInformation
	{
		// Token: 0x060002D4 RID: 724
		object GetMailboxProperty(PropertyTagPropertyDefinition property);

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D5 RID: 725
		Guid MailboxGuid { get; }
	}
}
