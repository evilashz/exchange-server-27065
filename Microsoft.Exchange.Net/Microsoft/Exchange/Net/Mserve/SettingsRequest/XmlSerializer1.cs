using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008D0 RID: 2256
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x0600307A RID: 12410 RVA: 0x00072D19 File Offset: 0x00070F19
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderSettings();
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x00072D20 File Offset: 0x00070F20
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterSettings();
		}
	}
}
