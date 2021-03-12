using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000806 RID: 2054
	[XmlRoot(ElementName = "Request", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/requestschema/2006")]
	public class AutoDiscoverRequest
	{
		// Token: 0x04002B3C RID: 11068
		public string EMailAddress;

		// Token: 0x04002B3D RID: 11069
		public string AcceptableResponseSchema;
	}
}
