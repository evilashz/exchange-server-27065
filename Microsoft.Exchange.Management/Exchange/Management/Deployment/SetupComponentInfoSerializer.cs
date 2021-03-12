using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200025B RID: 603
	internal sealed class SetupComponentInfoSerializer : XmlSerializer1
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x000608C4 File Offset: 0x0005EAC4
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("SetupComponentInfo", "");
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000608D6 File Offset: 0x0005EAD6
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterSetupComponentInfo)writer).Write13_SetupComponentInfo(objectToSerialize);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000608E4 File Offset: 0x0005EAE4
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderSetupComponentInfo)reader).Read13_SetupComponentInfo();
		}
	}
}
