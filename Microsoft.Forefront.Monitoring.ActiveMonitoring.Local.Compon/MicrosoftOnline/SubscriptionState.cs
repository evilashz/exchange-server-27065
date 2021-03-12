using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A1 RID: 417
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[Serializable]
	public enum SubscriptionState
	{
		// Token: 0x040006B3 RID: 1715
		Active,
		// Token: 0x040006B4 RID: 1716
		Warning,
		// Token: 0x040006B5 RID: 1717
		Suspend,
		// Token: 0x040006B6 RID: 1718
		Delete
	}
}
