using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationSettingsVersion1Point0
{
	// Token: 0x02000EFD RID: 3837
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarNotificationSettingsVersion1Point0Serializer : XmlSerializer1
	{
		// Token: 0x06008446 RID: 33862 RVA: 0x0023FFDD File Offset: 0x0023E1DD
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("CalendarNotificationSettings", "");
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x0023FFEF File Offset: 0x0023E1EF
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterCalendarNotificationSettingsVersion1Point0)writer).Write16_CalendarNotificationSettings(objectToSerialize);
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x0023FFFD File Offset: 0x0023E1FD
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderCalendarNotificationSettingsVersion1Point0)reader).Read16_CalendarNotificationSettings();
		}
	}
}
