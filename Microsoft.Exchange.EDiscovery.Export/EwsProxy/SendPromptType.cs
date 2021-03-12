using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000121 RID: 289
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SendPromptType
	{
		// Token: 0x04000910 RID: 2320
		None,
		// Token: 0x04000911 RID: 2321
		Send,
		// Token: 0x04000912 RID: 2322
		VotingOption
	}
}
