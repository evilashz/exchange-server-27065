using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000007 RID: 7
	public enum FaultInjectionLid
	{
		// Token: 0x04000011 RID: 17
		DataAccess_AsyncExecuteReader,
		// Token: 0x04000012 RID: 18
		DataAccess_AsyncExecuteScalar,
		// Token: 0x04000013 RID: 19
		DataAccess_AsyncExecuteNonQuery,
		// Token: 0x04000014 RID: 20
		DataAccess_AsyncInsert,
		// Token: 0x04000015 RID: 21
		DataAccess_AsyncGetExclusive,
		// Token: 0x04000016 RID: 22
		HttpWebRequestUtility_SendRequest = 100,
		// Token: 0x04000017 RID: 23
		HttpWebRequestUtility_GetHttpResponse
	}
}
