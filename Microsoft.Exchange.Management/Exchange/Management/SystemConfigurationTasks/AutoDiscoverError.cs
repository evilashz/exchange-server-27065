using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000809 RID: 2057
	[XmlRoot(ElementName = "Error", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
	public class AutoDiscoverError
	{
		// Token: 0x04002B41 RID: 11073
		[XmlAttribute]
		public string Time;

		// Token: 0x04002B42 RID: 11074
		[XmlAttribute]
		public string Id;

		// Token: 0x04002B43 RID: 11075
		public string ErrorCode;

		// Token: 0x04002B44 RID: 11076
		public string Message;

		// Token: 0x04002B45 RID: 11077
		public string DebugData;
	}
}
