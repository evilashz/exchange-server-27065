using System;

namespace Microsoft.Filtering
{
	// Token: 0x02000007 RID: 7
	public static class FilteringRequestExtensions
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002160 File Offset: 0x00000360
		public static void AddRecoveryOptions(this FilteringRequest request, RecoveryOptions options)
		{
			request.AddProperty("EnableRecovery", true);
			if (options.HasFlag(RecoveryOptions.Crash))
			{
				request.AddProperty("RecoverFromCrash", true);
			}
			if (options.HasFlag(RecoveryOptions.Timeout))
			{
				request.AddProperty("RecoverFromTimeout", true);
			}
		}
	}
}
