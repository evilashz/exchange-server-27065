using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F7 RID: 1527
	internal abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x060048AE RID: 18606 RVA: 0x0010D413 File Offset: 0x0010B613
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderAutoAttendantSettings();
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0010D41A File Offset: 0x0010B61A
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterAutoAttendantSettings();
		}
	}
}
