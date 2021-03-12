using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A0 RID: 416
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MailboxSearchLocationType
	{
		// Token: 0x04000C1C RID: 3100
		PrimaryOnly,
		// Token: 0x04000C1D RID: 3101
		ArchiveOnly,
		// Token: 0x04000C1E RID: 3102
		All
	}
}
