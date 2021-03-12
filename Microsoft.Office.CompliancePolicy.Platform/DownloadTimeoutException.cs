using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public sealed class DownloadTimeoutException : Exception
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00005D88 File Offset: 0x00003F88
		public DownloadTimeoutException() : base("The operation timed out.")
		{
		}
	}
}
