using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public sealed class DownloadCanceledException : Exception
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00005D63 File Offset: 0x00003F63
		public DownloadCanceledException() : base("The operation is canceled.")
		{
		}
	}
}
