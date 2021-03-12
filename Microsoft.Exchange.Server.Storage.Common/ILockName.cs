using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000058 RID: 88
	public interface ILockName : IEquatable<ILockName>, IComparable<ILockName>
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000522 RID: 1314
		LockManager.LockLevel LockLevel { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000523 RID: 1315
		// (set) Token: 0x06000524 RID: 1316
		LockManager.NamedLockObject CachedLockObject { get; set; }

		// Token: 0x06000525 RID: 1317
		ILockName GetLockNameToCache();
	}
}
