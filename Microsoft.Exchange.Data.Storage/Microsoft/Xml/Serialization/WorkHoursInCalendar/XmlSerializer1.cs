using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.WorkHoursInCalendar
{
	// Token: 0x02000F06 RID: 3846
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class XmlSerializer1 : SafeXmlSerializer
	{
		// Token: 0x06008488 RID: 33928 RVA: 0x00242E6E File Offset: 0x0024106E
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderWorkHoursInCalendar();
		}

		// Token: 0x06008489 RID: 33929 RVA: 0x00242E75 File Offset: 0x00241075
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterWorkHoursInCalendar();
		}
	}
}
