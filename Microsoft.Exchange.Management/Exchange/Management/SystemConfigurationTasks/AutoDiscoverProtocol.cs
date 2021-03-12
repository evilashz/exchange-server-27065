using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000803 RID: 2051
	[XmlRoot(ElementName = "Protocol", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
	public class AutoDiscoverProtocol
	{
		// Token: 0x04002B26 RID: 11046
		public string Type;

		// Token: 0x04002B27 RID: 11047
		public string Server;

		// Token: 0x04002B28 RID: 11048
		public string ServerDN;

		// Token: 0x04002B29 RID: 11049
		public string ServerVersion;

		// Token: 0x04002B2A RID: 11050
		public string MdbDN;

		// Token: 0x04002B2B RID: 11051
		public string ASUrl;

		// Token: 0x04002B2C RID: 11052
		public string EwsUrl;

		// Token: 0x04002B2D RID: 11053
		public string OOFUrl;

		// Token: 0x04002B2E RID: 11054
		public string OABUrl;

		// Token: 0x04002B2F RID: 11055
		public string UMUrl;

		// Token: 0x04002B30 RID: 11056
		public int Port;

		// Token: 0x04002B31 RID: 11057
		public int DirectoryPort;

		// Token: 0x04002B32 RID: 11058
		public int ReferralPort;

		// Token: 0x04002B33 RID: 11059
		public string FBPublish;

		// Token: 0x04002B34 RID: 11060
		public string SSL;

		// Token: 0x04002B35 RID: 11061
		public string TTL;

		// Token: 0x04002B36 RID: 11062
		public string AuthPackage;

		// Token: 0x04002B37 RID: 11063
		public string CertPincipalName;

		// Token: 0x04002B38 RID: 11064
		[XmlAnyElement]
		public object[] OtherXml;
	}
}
