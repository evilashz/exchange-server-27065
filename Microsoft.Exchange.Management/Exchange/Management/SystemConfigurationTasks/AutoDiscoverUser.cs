using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200080A RID: 2058
	[XmlRoot(ElementName = "User", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
	public class AutoDiscoverUser
	{
		// Token: 0x04002B46 RID: 11078
		public string DisplayName;

		// Token: 0x04002B47 RID: 11079
		public string LegacyDN;

		// Token: 0x04002B48 RID: 11080
		public string AutoDiscoverSMTPAddress;

		// Token: 0x04002B49 RID: 11081
		public string DefaultABView;

		// Token: 0x04002B4A RID: 11082
		public string DeploymentId;
	}
}
