using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020006C6 RID: 1734
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot("SharingSecurity", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public class EncryptedDataContainerType : SoapHeader
	{
		// Token: 0x04001F38 RID: 7992
		[XmlAnyElement]
		public XmlElement Any;
	}
}
