using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B7F RID: 2943
	internal static class WSSecurityExtensions
	{
		// Token: 0x040036EC RID: 14060
		public const string NamespaceUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x040036ED RID: 14061
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("o", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

		// Token: 0x040036EE RID: 14062
		public static readonly XmlElementDefinition Security = new XmlElementDefinition("Security", WSSecurityExtensions.Namespace);

		// Token: 0x040036EF RID: 14063
		public static readonly XmlElementDefinition BinarySecurityToken = new XmlElementDefinition("BinarySecurityToken", WSSecurityExtensions.Namespace);

		// Token: 0x040036F0 RID: 14064
		public static readonly XmlElementDefinition SecurityTokenReference = new XmlElementDefinition("SecurityTokenReference", WSSecurityExtensions.Namespace);

		// Token: 0x040036F1 RID: 14065
		public static readonly XmlElementDefinition Reference = new XmlElementDefinition("Reference", WSSecurityExtensions.Namespace);

		// Token: 0x040036F2 RID: 14066
		public static readonly XmlElementDefinition KeyIdentifier = new XmlElementDefinition("KeyIdentifier", WSSecurityExtensions.Namespace);
	}
}
