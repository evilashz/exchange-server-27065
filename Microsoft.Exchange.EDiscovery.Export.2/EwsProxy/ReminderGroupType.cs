using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D6 RID: 470
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ReminderGroupType
	{
		// Token: 0x04000DA6 RID: 3494
		Calendar,
		// Token: 0x04000DA7 RID: 3495
		Task
	}
}
