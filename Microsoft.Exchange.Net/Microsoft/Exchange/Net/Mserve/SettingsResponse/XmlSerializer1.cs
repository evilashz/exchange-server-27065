using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008FE RID: 2302
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060031E0 RID: 12768 RVA: 0x0007B09C File Offset: 0x0007929C
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderSettings();
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x0007B0A3 File Offset: 0x000792A3
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterSettings();
		}
	}
}
