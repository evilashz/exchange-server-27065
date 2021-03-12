using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000185 RID: 389
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPeopleIKnowServiceFactory
	{
		// Token: 0x06000F26 RID: 3878
		IPeopleIKnowService CreatePeopleIKnowService(ITracer tracer);
	}
}
