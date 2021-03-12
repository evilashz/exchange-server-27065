using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class BadRedirectedUriException : Exception
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005D4F File Offset: 0x00003F4F
		public BadRedirectedUriException(string uri, Exception innerException) : base(string.Format("Bad redirect URI: {0}.", uri), innerException)
		{
		}
	}
}
