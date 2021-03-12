using System;
using System.ServiceModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CDF RID: 3295
	[MessageContract(IsWrapped = false)]
	[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class GetStreamingEventsSoapResponse : BaseSoapResponse, IXmlSerializable
	{
		// Token: 0x060057C1 RID: 22465 RVA: 0x00113A2B File Offset: 0x00111C2B
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x00113A2E File Offset: 0x00111C2E
		public void ReadXml(XmlReader reader)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x00113A38 File Offset: 0x00111C38
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("soap11", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
			GetStreamingEventsSoapResponse.serverVersionSerializer.Serialize(writer, this.ServerVersionInfo);
			writer.WriteEndElement();
			writer.WriteStartElement("soap11", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
			GetStreamingEventsSoapResponse.bodySerializer.Serialize(writer, this.Body);
			writer.WriteEndElement();
		}

		// Token: 0x0400306F RID: 12399
		[MessageBodyMember(Name = "GetStreamingEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		[XmlElement("GetStreamingEventsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public GetStreamingEventsResponse Body;

		// Token: 0x04003070 RID: 12400
		private static SafeXmlSerializer serverVersionSerializer = new SafeXmlSerializer(typeof(ServerVersionInfo));

		// Token: 0x04003071 RID: 12401
		private static SafeXmlSerializer bodySerializer = new SafeXmlSerializer(typeof(GetStreamingEventsResponse));
	}
}
