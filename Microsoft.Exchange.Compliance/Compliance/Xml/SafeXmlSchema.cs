using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.Exchange.Compliance.Xml
{
	// Token: 0x02000008 RID: 8
	internal class SafeXmlSchema : XmlSchema
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003F08 File Offset: 0x00002108
		public new static XmlSchema Read(Stream stream, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(stream)
			{
				EntityHandling = EntityHandling.ExpandCharEntities,
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			}, validationEventHandler);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003F38 File Offset: 0x00002138
		public new static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			XmlSchema result;
			using (XmlTextReader xmlTextReader = new XmlTextReader(reader))
			{
				xmlTextReader.EntityHandling = EntityHandling.ExpandCharEntities;
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				xmlTextReader.XmlResolver = null;
				result = XmlSchema.Read(xmlTextReader, validationEventHandler);
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003F88 File Offset: 0x00002188
		public new static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			return XmlSchema.Read(reader, validationEventHandler);
		}
	}
}
