using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B83 RID: 2947
	internal static class XmlEncryption
	{
		// Token: 0x04003714 RID: 14100
		public const string NamespaceUri = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x04003715 RID: 14101
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("de", "http://www.w3.org/2001/04/xmlenc#");

		// Token: 0x04003716 RID: 14102
		public static readonly XmlElementDefinition EncryptedKey = new XmlElementDefinition("EncryptedKey", XmlEncryption.Namespace);
	}
}
