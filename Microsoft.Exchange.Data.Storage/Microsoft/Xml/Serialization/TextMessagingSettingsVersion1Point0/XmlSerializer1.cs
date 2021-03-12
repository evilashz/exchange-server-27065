using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingSettingsVersion1Point0
{
	// Token: 0x02000F01 RID: 3841
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06008463 RID: 33891 RVA: 0x002417B2 File Offset: 0x0023F9B2
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderTextMessagingSettingsVersion1Point0();
		}

		// Token: 0x06008464 RID: 33892 RVA: 0x002417B9 File Offset: 0x0023F9B9
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterTextMessagingSettingsVersion1Point0();
		}
	}
}
