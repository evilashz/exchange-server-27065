using System;

namespace Microsoft.Filtering
{
	// Token: 0x02000003 RID: 3
	public static class ExceptionDataConstants
	{
		// Token: 0x02000004 RID: 4
		public class Key
		{
			// Token: 0x04000001 RID: 1
			public const string RetryCount = "RetryCount";

			// Token: 0x04000002 RID: 2
			public const string MinSecondsBetweenRetries = "MinSecondsBetweenRetries";
		}

		// Token: 0x02000005 RID: 5
		public class RetryCount
		{
			// Token: 0x04000003 RID: 3
			public const int Infinite = -1;

			// Token: 0x04000004 RID: 4
			public const int None = 0;
		}
	}
}
