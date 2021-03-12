using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000381 RID: 897
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum GroupMemberIdentifierType
	{
		// Token: 0x040012D1 RID: 4817
		ExternalDirectoryObjectId,
		// Token: 0x040012D2 RID: 4818
		LegacyExchangeDN,
		// Token: 0x040012D3 RID: 4819
		SmtpAddress
	}
}
