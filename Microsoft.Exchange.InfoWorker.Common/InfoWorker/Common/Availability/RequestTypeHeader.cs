using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000CA RID: 202
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RequestTypeHeader : SoapHeader
	{
		// Token: 0x040002F9 RID: 761
		[XmlElement]
		public ProxyRequestType RequestType;
	}
}
