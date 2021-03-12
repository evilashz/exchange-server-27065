using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000026 RID: 38
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000052C1 File Offset: 0x000034C1
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderUserOofSettingsSerializer();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000052C8 File Offset: 0x000034C8
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterUserOofSettingsSerializer();
		}
	}
}
