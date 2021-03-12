using System;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000732 RID: 1842
	[Serializable]
	public class TrustedDocDomain
	{
		// Token: 0x06004167 RID: 16743 RVA: 0x0010C67D File Offset: 0x0010A87D
		public TrustedDocDomain()
		{
			this.m_ttdki = new KeyInformation();
		}

		// Token: 0x0400293D RID: 10557
		public KeyInformation m_ttdki;

		// Token: 0x0400293E RID: 10558
		public string[] m_strLicensorCertChain;

		// Token: 0x0400293F RID: 10559
		public string[] m_astrRightsTemplates;
	}
}
