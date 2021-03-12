using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationContentVersion1Point0
{
	// Token: 0x02000EF8 RID: 3832
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarNotificationContentVersion1Point0Serializer : XmlSerializer1
	{
		// Token: 0x06008419 RID: 33817 RVA: 0x0023DDAB File Offset: 0x0023BFAB
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("CalendarNotificationContent", "");
		}

		// Token: 0x0600841A RID: 33818 RVA: 0x0023DDBD File Offset: 0x0023BFBD
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterCalendarNotificationContentVersion1Point0)writer).Write7_CalendarNotificationContent(objectToSerialize);
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x0023DDCB File Offset: 0x0023BFCB
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderCalendarNotificationContentVersion1Point0)reader).Read7_CalendarNotificationContent();
		}
	}
}
