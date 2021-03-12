using System;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000058 RID: 88
	public static class XmlStringSerializer
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public static string ToString<T>(object data)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true,
				CheckCharacters = false
			};
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(T));
				safeXmlSerializer.Serialize(xmlWriter, data);
			}
			return stringBuilder.ToString();
		}
	}
}
