using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000B2 RID: 178
	public interface IMailboxLockName : ILockName, IEquatable<ILockName>, IComparable<ILockName>
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006EE RID: 1774
		Guid DatabaseGuid { get; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006EF RID: 1775
		int MailboxPartitionNumber { get; }
	}
}
