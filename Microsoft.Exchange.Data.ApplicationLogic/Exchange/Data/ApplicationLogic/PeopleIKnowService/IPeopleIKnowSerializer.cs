using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000182 RID: 386
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPeopleIKnowSerializer
	{
		// Token: 0x06000F21 RID: 3873
		string Serialize(PeopleIKnowGraph peopleIKnowGraph);

		// Token: 0x06000F22 RID: 3874
		PeopleIKnowGraph Deserialize(string serializedPeopleIKnow);
	}
}
