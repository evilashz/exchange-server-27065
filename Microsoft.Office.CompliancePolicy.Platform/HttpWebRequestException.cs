using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000046 RID: 70
	internal class HttpWebRequestException : Exception
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00005D95 File Offset: 0x00003F95
		public HttpWebRequestException(Exception exception) : base("HttpWebRequest exception", exception)
		{
		}
	}
}
