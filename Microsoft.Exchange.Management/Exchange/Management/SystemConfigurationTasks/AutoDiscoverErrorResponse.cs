using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000807 RID: 2055
	[XmlRoot(ElementName = "Response", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
	public class AutoDiscoverErrorResponse
	{
		// Token: 0x04002B3E RID: 11070
		[XmlElement(ElementName = "Error", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
		public AutoDiscoverError Error;
	}
}
