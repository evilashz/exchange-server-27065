using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000164 RID: 356
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum TaskStatusType
	{
		// Token: 0x04000B26 RID: 2854
		NotStarted,
		// Token: 0x04000B27 RID: 2855
		InProgress,
		// Token: 0x04000B28 RID: 2856
		Completed,
		// Token: 0x04000B29 RID: 2857
		WaitingOnOthers,
		// Token: 0x04000B2A RID: 2858
		Deferred
	}
}
