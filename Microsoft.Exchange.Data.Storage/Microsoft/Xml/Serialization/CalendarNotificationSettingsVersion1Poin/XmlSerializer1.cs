using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationSettingsVersion1Point0
{
	// Token: 0x02000EFC RID: 3836
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06008443 RID: 33859 RVA: 0x0023FFC7 File Offset: 0x0023E1C7
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderCalendarNotificationSettingsVersion1Point0();
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x0023FFCE File Offset: 0x0023E1CE
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterCalendarNotificationSettingsVersion1Point0();
		}
	}
}
