using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x0200009C RID: 156
	[XmlRoot(ElementName = "ConversationIndex", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class ConversationIndex : stringWithEncodingType
	{
	}
}
