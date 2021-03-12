using System;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000731 RID: 1841
	[Serializable]
	public class KeyInformation
	{
		// Token: 0x04002936 RID: 10550
		public string strID;

		// Token: 0x04002937 RID: 10551
		public string strIDType;

		// Token: 0x04002938 RID: 10552
		public int nCSPType;

		// Token: 0x04002939 RID: 10553
		public string strCSPName;

		// Token: 0x0400293A RID: 10554
		public string strKeyContainerName;

		// Token: 0x0400293B RID: 10555
		public int nKeyNumber;

		// Token: 0x0400293C RID: 10556
		public string strEncryptedPrivateKey;
	}
}
