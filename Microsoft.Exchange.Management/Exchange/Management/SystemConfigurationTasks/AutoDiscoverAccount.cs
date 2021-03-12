using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200080B RID: 2059
	[XmlRoot(ElementName = "Account", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
	public class AutoDiscoverAccount
	{
		// Token: 0x04002B4B RID: 11083
		public string AccountType;

		// Token: 0x04002B4C RID: 11084
		public string Action;

		// Token: 0x04002B4D RID: 11085
		public string RedirectAddr;

		// Token: 0x04002B4E RID: 11086
		[XmlElement]
		public AutoDiscoverProtocol[] Protocol;

		// Token: 0x04002B4F RID: 11087
		public string SSL;

		// Token: 0x04002B50 RID: 11088
		public string AuthPackage;
	}
}
