using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200007E RID: 126
	public abstract class ExtractionSet<TExtraction> : ExtractionSet, IXmlSerializable
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000E073 File Offset: 0x0000C273
		protected ExtractionSet(IExtractionSerializer<TExtraction> extractionSerializer)
		{
			this.Version = ExtractionVersions.CurrentVersion;
			this.serializer = extractionSerializer;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000E08D File Offset: 0x0000C28D
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000E095 File Offset: 0x0000C295
		public Version Version { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000E09E File Offset: 0x0000C29E
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000E0A6 File Offset: 0x0000C2A6
		public IEnumerable<TExtraction> Extractions { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000E0AF File Offset: 0x0000C2AF
		public override bool IsEmpty
		{
			get
			{
				return this.Extractions == null || !this.Extractions.Any<TExtraction>();
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E0C9 File Offset: 0x0000C2C9
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		public void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				return;
			}
			reader.ReadStartElement();
			reader.ReadStartElement("Version");
			string version = reader.ReadContentAsString();
			reader.ReadEndElement();
			this.Version = new Version(version);
			this.Extractions = this.serializer.ReadXml(reader, this.Version);
			reader.ReadEndElement();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E130 File Offset: 0x0000C330
		public void WriteXml(XmlWriter writer)
		{
			if (this.Extractions != null && this.Extractions.Any<TExtraction>())
			{
				writer.WriteStartElement("Version");
				writer.WriteString(this.Version.ToString());
				writer.WriteEndElement();
				this.serializer.WriteXml(writer, this.Extractions.ToArray<TExtraction>());
			}
		}

		// Token: 0x0400018B RID: 395
		private const string VersionElementName = "Version";

		// Token: 0x0400018C RID: 396
		private IExtractionSerializer<TExtraction> serializer;
	}
}
