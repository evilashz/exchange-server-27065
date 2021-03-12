using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200008E RID: 142
	internal interface IHashBucket
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000507 RID: 1287
		string StoreName { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000508 RID: 1288
		string ConnectionString { get; }

		// Token: 0x06000509 RID: 1289
		object GetPhysicalInstanceId(string hashKey);

		// Token: 0x0600050A RID: 1290
		object GetPhysicalInstanceIdByHashValue(int hashValue);

		// Token: 0x0600050B RID: 1291
		int GetLogicalHash(string hashKey);
	}
}
