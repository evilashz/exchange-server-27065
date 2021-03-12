using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B7E RID: 2942
	internal static class WSSecurityUtility
	{
		// Token: 0x040036E6 RID: 14054
		public const string NamespaceUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x040036E7 RID: 14055
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("u", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

		// Token: 0x040036E8 RID: 14056
		public static readonly XmlAttributeDefinition Id = new XmlAttributeDefinition("Id", WSSecurityUtility.Namespace);

		// Token: 0x040036E9 RID: 14057
		public static readonly XmlElementDefinition Timestamp = new XmlElementDefinition("Timestamp", WSSecurityUtility.Namespace);

		// Token: 0x040036EA RID: 14058
		public static readonly XmlElementDefinition Created = new XmlElementDefinition("Created", WSSecurityUtility.Namespace);

		// Token: 0x040036EB RID: 14059
		public static readonly XmlElementDefinition Expires = new XmlElementDefinition("Expires", WSSecurityUtility.Namespace);
	}
}
