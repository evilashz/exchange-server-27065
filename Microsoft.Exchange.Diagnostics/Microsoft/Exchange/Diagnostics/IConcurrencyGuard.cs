using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000115 RID: 277
	public interface IConcurrencyGuard
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000803 RID: 2051
		string GuardName { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000804 RID: 2052
		int MaxConcurrency { get; }

		// Token: 0x06000805 RID: 2053
		long GetCurrentValue();

		// Token: 0x06000806 RID: 2054
		long GetCurrentValue(string bucketName);

		// Token: 0x06000807 RID: 2055
		long Increment(object stateObject = null);

		// Token: 0x06000808 RID: 2056
		long Increment(string bucketName, object stateObject = null);

		// Token: 0x06000809 RID: 2057
		long Decrement(object stateObject = null);

		// Token: 0x0600080A RID: 2058
		long Decrement(string bucketName, object stateObject = null);
	}
}
