using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A4 RID: 2212
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x0006BB2D File Offset: 0x00069D2D
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderProvision();
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x0006BB34 File Offset: 0x00069D34
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterProvision();
		}
	}
}
