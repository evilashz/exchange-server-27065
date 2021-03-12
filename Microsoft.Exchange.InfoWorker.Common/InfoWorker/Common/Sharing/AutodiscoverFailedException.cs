using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	public sealed class AutodiscoverFailedException : SharingSynchronizationException
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00050DE8 File Offset: 0x0004EFE8
		public AutodiscoverFailedException() : base(Strings.AutodiscoverFailedException)
		{
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00050DF5 File Offset: 0x0004EFF5
		public AutodiscoverFailedException(Exception innerException, Exception additionalException) : base(Strings.AutodiscoverFailedException, innerException)
		{
			base.AdditionalException = additionalException;
		}
	}
}
