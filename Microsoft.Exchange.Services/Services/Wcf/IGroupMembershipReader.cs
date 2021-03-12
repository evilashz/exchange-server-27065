using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200008C RID: 140
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IGroupMembershipReader<T>
	{
		// Token: 0x06000378 RID: 888
		IEnumerable<T> GetJoinedGroups();
	}
}
