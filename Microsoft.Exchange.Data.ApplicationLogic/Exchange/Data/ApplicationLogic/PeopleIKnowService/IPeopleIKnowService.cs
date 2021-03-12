using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000184 RID: 388
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPeopleIKnowService
	{
		// Token: 0x06000F24 RID: 3876
		string GetSerializedPeopleIKnowGraph(IMailboxSession mailboxSession, IXSOFactory xsoFactory);

		// Token: 0x06000F25 RID: 3877
		IComparer<string> GetRelevancyComparer(string serializedPeopleIKnow);
	}
}
