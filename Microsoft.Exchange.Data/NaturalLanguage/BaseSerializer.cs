using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000074 RID: 116
	public abstract class BaseSerializer<T> : IExtractionSerializer<T>
	{
		// Token: 0x0600039E RID: 926
		protected abstract XmlSerializer GetSerializer();

		// Token: 0x0600039F RID: 927 RVA: 0x0000DE77 File Offset: 0x0000C077
		public virtual T[] ReadOldXml(XmlReader reader, Version version)
		{
			reader.Skip();
			return null;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000DE80 File Offset: 0x0000C080
		public T[] ReadXml(XmlReader reader, Version version)
		{
			if (version == ExtractionVersions.CurrentVersion)
			{
				XmlSerializer serializer = this.GetSerializer();
				return serializer.Deserialize(reader) as T[];
			}
			return this.ReadOldXml(reader, version);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public void WriteXml(XmlWriter writer, T[] t)
		{
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add(string.Empty, string.Empty);
			XmlSerializer serializer = this.GetSerializer();
			serializer.Serialize(writer, t, xmlSerializerNamespaces);
		}
	}
}
