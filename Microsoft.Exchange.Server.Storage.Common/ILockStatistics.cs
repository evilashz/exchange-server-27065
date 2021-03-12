using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000057 RID: 87
	public interface ILockStatistics
	{
		// Token: 0x0600051F RID: 1311
		byte GetClientType();

		// Token: 0x06000520 RID: 1312
		byte GetOperation();

		// Token: 0x06000521 RID: 1313
		void OnAfterLockAcquisition(LockManager.LockType lockType, bool locked, bool contested, ILockStatistics recentOwner, TimeSpan timeSpentWaiting);
	}
}
