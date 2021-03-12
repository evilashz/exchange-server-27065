using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200035A RID: 858
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SharingDataType
	{
		// Token: 0x04001268 RID: 4712
		Calendar,
		// Token: 0x04001269 RID: 4713
		Contacts
	}
}
