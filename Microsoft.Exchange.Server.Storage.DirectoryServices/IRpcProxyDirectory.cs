using System;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000025 RID: 37
	public interface IRpcProxyDirectory
	{
		// Token: 0x060000E7 RID: 231
		SecurityDescriptor GetDatabaseSecurityDescriptor(IExecutionContext context, Guid databaseGuid);

		// Token: 0x060000E8 RID: 232
		SecurityDescriptor GetServerSecurityDescriptor(IExecutionContext context);

		// Token: 0x060000E9 RID: 233
		void RefreshDatabaseInfo(IExecutionContext context, Guid databaseGuid);

		// Token: 0x060000EA RID: 234
		void RefreshServerInfo(IExecutionContext context);

		// Token: 0x060000EB RID: 235
		int? GetMaximumRpcThreadCount(IExecutionContext context);

		// Token: 0x060000EC RID: 236
		DatabaseInfo GetDatabaseInfo(IExecutionContext context, Guid databaseGuid);
	}
}
