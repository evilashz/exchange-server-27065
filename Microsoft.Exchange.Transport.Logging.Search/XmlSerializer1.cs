using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000021 RID: 33
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000709D File Offset: 0x0000529D
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderLogQuery();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000070A4 File Offset: 0x000052A4
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterLogQuery();
		}
	}
}
