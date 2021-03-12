using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020006E1 RID: 1761
	[CLSCompliant(false)]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SpecialLogonType
	{
		// Token: 0x04001F7E RID: 8062
		Admin,
		// Token: 0x04001F7F RID: 8063
		SystemService
	}
}
