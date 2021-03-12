using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public sealed class UnsupportedUriFormatException : Exception
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00005DBB File Offset: 0x00003FBB
		public UnsupportedUriFormatException(string uri) : base(string.Format("The HTTP client doesn't support the format of the URI ({0}).", uri))
		{
		}
	}
}
