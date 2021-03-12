using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InMemorySyncLogImplementation : ISyncLogImplementation
	{
		// Token: 0x06000382 RID: 898 RVA: 0x00014DB0 File Offset: 0x00012FB0
		public void Configure(bool enabled, string path, long ageQuota, long directorySizeQuota, long perFileSizeQuota)
		{
			throw new InvalidOperationException("The in memory sync log cannot be configured.");
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00014DBC File Offset: 0x00012FBC
		public bool IsEnabled()
		{
			return false;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00014DBF File Offset: 0x00012FBF
		public void Append(LogRowFormatter row, int timestampField)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00014DC1 File Offset: 0x00012FC1
		public void Close()
		{
		}
	}
}
