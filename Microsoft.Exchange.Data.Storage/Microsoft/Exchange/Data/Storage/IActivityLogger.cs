using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F0D RID: 3853
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IActivityLogger
	{
		// Token: 0x060084A8 RID: 33960
		void Log(IEnumerable<Activity> activities);
	}
}
