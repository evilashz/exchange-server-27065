using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B84 RID: 2948
	internal static class XmlDigitalSignature
	{
		// Token: 0x04003717 RID: 14103
		public const string NamespaceUri = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04003718 RID: 14104
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("ds", "http://www.w3.org/2000/09/xmldsig#");

		// Token: 0x04003719 RID: 14105
		public static readonly XmlElementDefinition KeyInfo = new XmlElementDefinition("KeyInfo", XmlDigitalSignature.Namespace);
	}
}
