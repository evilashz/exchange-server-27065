using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200000D RID: 13
	internal interface ITaskOutputHelper : ILogTraceHelper
	{
		// Token: 0x06000069 RID: 105
		void WriteErrorSimple(Exception error);

		// Token: 0x0600006A RID: 106
		void WriteWarning(LocalizedString locString);

		// Token: 0x0600006B RID: 107
		void WriteProgressSimple(LocalizedString locString);
	}
}
