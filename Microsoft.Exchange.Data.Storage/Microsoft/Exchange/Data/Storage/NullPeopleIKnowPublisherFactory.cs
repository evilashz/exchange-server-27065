using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004FD RID: 1277
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NullPeopleIKnowPublisherFactory : IPeopleIKnowPublisherFactory
	{
		// Token: 0x06003778 RID: 14200 RVA: 0x000DF36E File Offset: 0x000DD56E
		private NullPeopleIKnowPublisherFactory()
		{
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000DF376 File Offset: 0x000DD576
		public IPeopleIKnowPublisher Create()
		{
			return NullPeopleIKnowPublisher.Instance;
		}

		// Token: 0x04001D65 RID: 7525
		public static readonly NullPeopleIKnowPublisherFactory Instance = new NullPeopleIKnowPublisherFactory();
	}
}
