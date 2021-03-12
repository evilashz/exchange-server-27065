using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000016 RID: 22
	internal interface IMdbCollection
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006B RID: 107
		IEnumerable<MdbInfo> Databases { get; }

		// Token: 0x0600006C RID: 108
		void UpdateDatabasesIndexStatusInfo(int numberOfCopiesToIndexPerDatabase);

		// Token: 0x0600006D RID: 109
		void UpdateDatabasesCopyStatusInfo();
	}
}
