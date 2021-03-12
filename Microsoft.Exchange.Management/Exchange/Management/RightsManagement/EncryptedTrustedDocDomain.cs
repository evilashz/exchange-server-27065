using System;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000733 RID: 1843
	[Serializable]
	public class EncryptedTrustedDocDomain
	{
		// Token: 0x06004168 RID: 16744 RVA: 0x0010C690 File Offset: 0x0010A890
		public EncryptedTrustedDocDomain()
		{
			this.m_strTrustedDocDomainInfo = null;
			this.m_strKeyData = null;
		}

		// Token: 0x04002940 RID: 10560
		public string m_strTrustedDocDomainInfo;

		// Token: 0x04002941 RID: 10561
		public string m_strKeyData;
	}
}
