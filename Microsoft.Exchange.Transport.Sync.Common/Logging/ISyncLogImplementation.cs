using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncLogImplementation
	{
		// Token: 0x0600032E RID: 814
		void Configure(bool enabled, string path, long ageQuota, long directorySizeQuota, long perFileSizeQuota);

		// Token: 0x0600032F RID: 815
		bool IsEnabled();

		// Token: 0x06000330 RID: 816
		void Append(LogRowFormatter row, int timestampField);

		// Token: 0x06000331 RID: 817
		void Close();
	}
}
