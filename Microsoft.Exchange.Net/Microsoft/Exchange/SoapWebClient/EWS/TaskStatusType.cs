using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000245 RID: 581
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum TaskStatusType
	{
		// Token: 0x04000F78 RID: 3960
		NotStarted,
		// Token: 0x04000F79 RID: 3961
		InProgress,
		// Token: 0x04000F7A RID: 3962
		Completed,
		// Token: 0x04000F7B RID: 3963
		WaitingOnOthers,
		// Token: 0x04000F7C RID: 3964
		Deferred
	}
}
