using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Xml.Serialization.TextMessagingSettingsVersion1Point0
{
	// Token: 0x02000F02 RID: 3842
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class TextMessagingSettingsVersion1Point0Serializer : XmlSerializer1
	{
		// Token: 0x06008466 RID: 33894 RVA: 0x002417C8 File Offset: 0x0023F9C8
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("TextMessagingSettings", "");
		}

		// Token: 0x06008467 RID: 33895 RVA: 0x002417DA File Offset: 0x0023F9DA
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterTextMessagingSettingsVersion1Point0)writer).Write9_TextMessagingSettings(objectToSerialize);
		}

		// Token: 0x06008468 RID: 33896 RVA: 0x002417E8 File Offset: 0x0023F9E8
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderTextMessagingSettingsVersion1Point0)reader).Read9_TextMessagingSettings();
		}
	}
}
