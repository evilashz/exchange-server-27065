using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200000C RID: 12
	internal interface ILogTraceHelper
	{
		// Token: 0x06000066 RID: 102
		void AppendLogMessage(LocalizedString locMessage);

		// Token: 0x06000067 RID: 103
		void AppendLogMessage(string englishMessage, params object[] args);

		// Token: 0x06000068 RID: 104
		void WriteVerbose(LocalizedString locString);
	}
}
