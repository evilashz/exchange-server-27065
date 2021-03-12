using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingHostingData
{
	// Token: 0x02000221 RID: 545
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TextMessagingHostingDataSerializer : XmlSerializer1
	{
		// Token: 0x060013A8 RID: 5032 RVA: 0x0005396C File Offset: 0x00051B6C
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("TextMessagingHostingData", "");
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0005397E File Offset: 0x00051B7E
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterTextMessagingHostingData)writer).Write20_TextMessagingHostingData(objectToSerialize);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0005398C File Offset: 0x00051B8C
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderTextMessagingHostingData)reader).Read20_TextMessagingHostingData();
		}
	}
}
