using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200020D RID: 525
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum CalendarItemTypeType
	{
		// Token: 0x04000DDD RID: 3549
		Single,
		// Token: 0x04000DDE RID: 3550
		Occurrence,
		// Token: 0x04000DDF RID: 3551
		Exception,
		// Token: 0x04000DE0 RID: 3552
		RecurringMaster
	}
}
