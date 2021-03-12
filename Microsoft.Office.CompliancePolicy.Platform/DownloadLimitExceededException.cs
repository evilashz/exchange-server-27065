using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public sealed class DownloadLimitExceededException : Exception
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00005D70 File Offset: 0x00003F70
		public DownloadLimitExceededException(long size) : base(string.Format("The total download size limit ({0}) has been exceeded.", size))
		{
		}
	}
}
