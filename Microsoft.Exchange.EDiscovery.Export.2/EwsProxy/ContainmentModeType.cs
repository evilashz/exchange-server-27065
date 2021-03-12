using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200021B RID: 539
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ContainmentModeType
	{
		// Token: 0x04000E92 RID: 3730
		FullString,
		// Token: 0x04000E93 RID: 3731
		Prefixed,
		// Token: 0x04000E94 RID: 3732
		Substring,
		// Token: 0x04000E95 RID: 3733
		PrefixOnWords,
		// Token: 0x04000E96 RID: 3734
		ExactPhrase
	}
}
