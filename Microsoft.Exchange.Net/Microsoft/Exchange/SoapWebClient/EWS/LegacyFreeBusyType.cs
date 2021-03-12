using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200020C RID: 524
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum LegacyFreeBusyType
	{
		// Token: 0x04000DD6 RID: 3542
		Free,
		// Token: 0x04000DD7 RID: 3543
		Tentative,
		// Token: 0x04000DD8 RID: 3544
		Busy,
		// Token: 0x04000DD9 RID: 3545
		OOF,
		// Token: 0x04000DDA RID: 3546
		WorkingElsewhere,
		// Token: 0x04000DDB RID: 3547
		NoData
	}
}
