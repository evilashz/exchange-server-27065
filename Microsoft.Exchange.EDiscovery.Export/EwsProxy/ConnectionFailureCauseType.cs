using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000244 RID: 580
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConnectionFailureCauseType
	{
		// Token: 0x04000EFC RID: 3836
		None,
		// Token: 0x04000EFD RID: 3837
		UserBusy,
		// Token: 0x04000EFE RID: 3838
		NoAnswer,
		// Token: 0x04000EFF RID: 3839
		Unavailable,
		// Token: 0x04000F00 RID: 3840
		Other
	}
}
