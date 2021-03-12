using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000022 RID: 34
	internal sealed class LogQuerySerializer : XmlSerializer1
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000070B3 File Offset: 0x000052B3
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("LogQuery", "");
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000070C5 File Offset: 0x000052C5
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterLogQuery)writer).Write28_LogQuery(objectToSerialize);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000070D3 File Offset: 0x000052D3
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderLogQuery)reader).Read28_LogQuery();
		}
	}
}
