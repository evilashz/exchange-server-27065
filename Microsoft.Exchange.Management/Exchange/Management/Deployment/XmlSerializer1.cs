using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200025A RID: 602
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06001698 RID: 5784 RVA: 0x000608AE File Offset: 0x0005EAAE
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderSetupComponentInfo();
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000608B5 File Offset: 0x0005EAB5
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterSetupComponentInfo();
		}
	}
}
