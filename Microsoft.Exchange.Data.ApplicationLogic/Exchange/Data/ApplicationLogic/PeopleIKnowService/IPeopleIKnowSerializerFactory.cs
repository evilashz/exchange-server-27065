using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000183 RID: 387
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IPeopleIKnowSerializerFactory
	{
		// Token: 0x06000F23 RID: 3875
		IPeopleIKnowSerializer CreatePeopleIKnowSerializer();
	}
}
