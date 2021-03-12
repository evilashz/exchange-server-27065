using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.CalendarNotificationContentVersion1Point0
{
	// Token: 0x02000EF7 RID: 3831
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06008416 RID: 33814 RVA: 0x0023DD95 File Offset: 0x0023BF95
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderCalendarNotificationContentVersion1Point0();
		}

		// Token: 0x06008417 RID: 33815 RVA: 0x0023DD9C File Offset: 0x0023BF9C
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterCalendarNotificationContentVersion1Point0();
		}
	}
}
