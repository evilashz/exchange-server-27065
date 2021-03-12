using System;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.Compliance.Xml
{
	// Token: 0x02000006 RID: 6
	internal class SafeXmlDocument : XmlDocument
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00003A0D File Offset: 0x00001C0D
		public SafeXmlDocument()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003A15 File Offset: 0x00001C15
		public SafeXmlDocument(XmlImplementation imp)
		{
			throw new NotSupportedException("Not supported");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003A27 File Offset: 0x00001C27
		public SafeXmlDocument(XmlNameTable nt) : base(nt)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003A30 File Offset: 0x00001C30
		public override void Load(Stream inStream)
		{
			this.Load(new XmlTextReader(inStream)
			{
				EntityHandling = EntityHandling.ExpandCharEntities,
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003A60 File Offset: 0x00001C60
		public override void Load(string filename)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(filename))
			{
				xmlTextReader.EntityHandling = EntityHandling.ExpandCharEntities;
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				xmlTextReader.XmlResolver = null;
				this.Load(xmlTextReader);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003AAC File Offset: 0x00001CAC
		public override void Load(TextReader txtReader)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(txtReader))
			{
				xmlTextReader.EntityHandling = EntityHandling.ExpandCharEntities;
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				xmlTextReader.XmlResolver = null;
				this.Load(xmlTextReader);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public override void Load(XmlReader reader)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			try
			{
				base.Load(reader);
			}
			catch (XmlException ex)
			{
				if (ex.Message.StartsWith("DTD is prohibited in this XML document.", StringComparison.OrdinalIgnoreCase))
				{
					throw new XmlDtdException();
				}
				throw;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003B58 File Offset: 0x00001D58
		public override void LoadXml(string xml)
		{
			using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(xml)))
			{
				xmlTextReader.EntityHandling = EntityHandling.ExpandCharEntities;
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				xmlTextReader.XmlResolver = null;
				base.Load(xmlTextReader);
			}
		}
	}
}
