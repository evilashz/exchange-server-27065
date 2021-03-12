using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200037F RID: 895
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IAttendeeCollectionImpl : IAttendeeCollection, IRecipientBaseCollection<Attendee>, IList<Attendee>, ICollection<Attendee>, IEnumerable<Attendee>, IEnumerable
	{
		// Token: 0x06002790 RID: 10128
		void Cleanup();

		// Token: 0x06002791 RID: 10129
		void LoadIsDistributionList();
	}
}
