using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionRequest
{
	// Token: 0x0200089B RID: 2203
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06002F28 RID: 12072 RVA: 0x0006A732 File Offset: 0x00068932
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderProvision();
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0006A739 File Offset: 0x00068939
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterProvision();
		}
	}
}
