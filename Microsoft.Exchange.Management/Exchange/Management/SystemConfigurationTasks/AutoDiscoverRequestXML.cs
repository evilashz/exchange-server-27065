using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000804 RID: 2052
	[XmlRoot(ElementName = "Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006")]
	public class AutoDiscoverRequestXML
	{
		// Token: 0x06004771 RID: 18289 RVA: 0x00125BE4 File Offset: 0x00123DE4
		public static AutoDiscoverRequestXML NewRequest(string emailAddress)
		{
			AutoDiscoverRequest autoDiscoverRequest = new AutoDiscoverRequest();
			autoDiscoverRequest.EMailAddress = emailAddress;
			autoDiscoverRequest.AcceptableResponseSchema = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a";
			return new AutoDiscoverRequestXML
			{
				Request = autoDiscoverRequest
			};
		}

		// Token: 0x04002B39 RID: 11065
		[XmlElement(IsNullable = false)]
		public AutoDiscoverRequest Request;
	}
}
