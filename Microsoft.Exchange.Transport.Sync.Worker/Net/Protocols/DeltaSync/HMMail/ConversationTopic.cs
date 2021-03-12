using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x0200009A RID: 154
	[XmlRoot(ElementName = "ConversationTopic", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class ConversationTopic : stringWithEncodingType
	{
	}
}
