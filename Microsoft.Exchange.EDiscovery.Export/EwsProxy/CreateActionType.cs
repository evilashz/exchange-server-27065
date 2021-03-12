using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200030B RID: 779
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CreateActionType
	{
		// Token: 0x04001157 RID: 4439
		CreateNew,
		// Token: 0x04001158 RID: 4440
		Update,
		// Token: 0x04001159 RID: 4441
		UpdateOrCreate
	}
}
