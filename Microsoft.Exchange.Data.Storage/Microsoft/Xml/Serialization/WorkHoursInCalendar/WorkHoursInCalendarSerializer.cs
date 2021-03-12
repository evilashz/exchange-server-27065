using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.WorkHoursInCalendar
{
	// Token: 0x02000F07 RID: 3847
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class WorkHoursInCalendarSerializer : XmlSerializer1
	{
		// Token: 0x0600848B RID: 33931 RVA: 0x00242E84 File Offset: 0x00241084
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Root", "WorkingHours.xsd");
		}

		// Token: 0x0600848C RID: 33932 RVA: 0x00242E96 File Offset: 0x00241096
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterWorkHoursInCalendar)writer).Write9_Root(objectToSerialize);
		}

		// Token: 0x0600848D RID: 33933 RVA: 0x00242EA4 File Offset: 0x002410A4
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderWorkHoursInCalendar)reader).Read9_Root();
		}
	}
}
