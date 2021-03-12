using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingHostingData
{
	// Token: 0x02000220 RID: 544
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060013A5 RID: 5029 RVA: 0x00053956 File Offset: 0x00051B56
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderTextMessagingHostingData();
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0005395D File Offset: 0x00051B5D
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterTextMessagingHostingData();
		}
	}
}
