using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B7D RID: 2941
	internal static class WSAddressing
	{
		// Token: 0x040036DE RID: 14046
		public const string NamespaceUri = "http://www.w3.org/2005/08/addressing";

		// Token: 0x040036DF RID: 14047
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("a", "http://www.w3.org/2005/08/addressing");

		// Token: 0x040036E0 RID: 14048
		public static readonly XmlElementDefinition To = new XmlElementDefinition("To", WSAddressing.Namespace);

		// Token: 0x040036E1 RID: 14049
		public static readonly XmlElementDefinition Action = new XmlElementDefinition("Action", WSAddressing.Namespace);

		// Token: 0x040036E2 RID: 14050
		public static readonly XmlElementDefinition MessageId = new XmlElementDefinition("MessageID", WSAddressing.Namespace);

		// Token: 0x040036E3 RID: 14051
		public static readonly XmlElementDefinition ReplyTo = new XmlElementDefinition("ReplyTo", WSAddressing.Namespace);

		// Token: 0x040036E4 RID: 14052
		public static readonly XmlElementDefinition Address = new XmlElementDefinition("Address", WSAddressing.Namespace);

		// Token: 0x040036E5 RID: 14053
		public static readonly XmlElementDefinition EndpointReference = new XmlElementDefinition("EndpointReference", WSAddressing.Namespace);
	}
}
