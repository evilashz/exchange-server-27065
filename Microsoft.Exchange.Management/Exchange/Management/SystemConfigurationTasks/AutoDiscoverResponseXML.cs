using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000805 RID: 2053
	[XmlRoot(ElementName = "Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
	public class AutoDiscoverResponseXML
	{
		// Token: 0x04002B3A RID: 11066
		[XmlElement(IsNullable = false, ElementName = "Response", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
		public AutoDiscoverErrorResponse ErrorResponse;

		// Token: 0x04002B3B RID: 11067
		[XmlElement(IsNullable = false, ElementName = "Response", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
		public AutoDiscoverDataResponse DataResponse;
	}
}
