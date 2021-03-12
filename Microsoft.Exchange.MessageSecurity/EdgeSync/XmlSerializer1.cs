using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000016 RID: 22
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00004DBE File Offset: 0x00002FBE
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderEdgeSubscriptionData();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004DC5 File Offset: 0x00002FC5
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterEdgeSubscriptionData();
		}
	}
}
