using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000808 RID: 2056
	[XmlRoot(ElementName = "Response", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
	public class AutoDiscoverDataResponse
	{
		// Token: 0x04002B3F RID: 11071
		public AutoDiscoverUser User;

		// Token: 0x04002B40 RID: 11072
		public AutoDiscoverAccount Account;
	}
}
