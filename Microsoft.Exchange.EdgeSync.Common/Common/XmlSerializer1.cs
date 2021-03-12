using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000033 RID: 51
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00006E90 File Offset: 0x00005090
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderStatus();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006E97 File Offset: 0x00005097
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterStatus();
		}
	}
}
