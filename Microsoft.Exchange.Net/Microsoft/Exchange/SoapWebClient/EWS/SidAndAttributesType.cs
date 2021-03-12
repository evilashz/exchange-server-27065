using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020006D3 RID: 1747
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[CLSCompliant(false)]
	[Serializable]
	public class SidAndAttributesType
	{
		// Token: 0x04001F5D RID: 8029
		public string SecurityIdentifier;

		// Token: 0x04001F5E RID: 8030
		[XmlAttribute]
		public uint Attributes;
	}
}
