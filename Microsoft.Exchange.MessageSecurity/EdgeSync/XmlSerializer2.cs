using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x0200001B RID: 27
	internal abstract class XmlSerializer2 : XmlSerializer
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00005518 File Offset: 0x00003718
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderEdgeSyncCredential();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000551F File Offset: 0x0000371F
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterEdgeSyncCredential();
		}
	}
}
