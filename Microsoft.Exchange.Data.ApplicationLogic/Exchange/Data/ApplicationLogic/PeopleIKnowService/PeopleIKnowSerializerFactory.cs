using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000188 RID: 392
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleIKnowSerializerFactory : IPeopleIKnowSerializerFactory
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x0003D343 File Offset: 0x0003B543
		public IPeopleIKnowSerializer CreatePeopleIKnowSerializer()
		{
			return PeopleIKnowJsonSerializer.Singleton;
		}
	}
}
