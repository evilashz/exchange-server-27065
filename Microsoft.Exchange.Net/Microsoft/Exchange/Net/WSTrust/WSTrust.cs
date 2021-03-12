using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B80 RID: 2944
	internal static class WSTrust
	{
		// Token: 0x040036F3 RID: 14067
		public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2005/02/trust";

		// Token: 0x040036F4 RID: 14068
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("t", "http://schemas.xmlsoap.org/ws/2005/02/trust");

		// Token: 0x040036F5 RID: 14069
		public static readonly XmlElementDefinition RequestedSecurityToken = new XmlElementDefinition("RequestedSecurityToken", WSTrust.Namespace);

		// Token: 0x040036F6 RID: 14070
		public static readonly XmlElementDefinition RequestedProofToken = new XmlElementDefinition("RequestedProofToken", WSTrust.Namespace);

		// Token: 0x040036F7 RID: 14071
		public static readonly XmlElementDefinition BinarySecret = new XmlElementDefinition("BinarySecret", WSTrust.Namespace);

		// Token: 0x040036F8 RID: 14072
		public static readonly XmlElementDefinition TokenType = new XmlElementDefinition("TokenType", WSTrust.Namespace);

		// Token: 0x040036F9 RID: 14073
		public static readonly XmlElementDefinition KeyType = new XmlElementDefinition("KeyType", WSTrust.Namespace);

		// Token: 0x040036FA RID: 14074
		public static readonly XmlElementDefinition RequestSecurityTokenResponse = new XmlElementDefinition("RequestSecurityTokenResponse", WSTrust.Namespace);

		// Token: 0x040036FB RID: 14075
		public static readonly XmlElementDefinition RequestSecurityTokenResponseCollection = new XmlElementDefinition("RequestSecurityTokenResponseCollection", WSTrust.Namespace);

		// Token: 0x040036FC RID: 14076
		public static readonly XmlElementDefinition RequestType = new XmlElementDefinition("RequestType", WSTrust.Namespace);

		// Token: 0x040036FD RID: 14077
		public static readonly XmlElementDefinition KeySize = new XmlElementDefinition("KeySize", WSTrust.Namespace);

		// Token: 0x040036FE RID: 14078
		public static readonly XmlElementDefinition CanonicalizationAlgorithm = new XmlElementDefinition("CanonicalizationAlgorithm", WSTrust.Namespace);

		// Token: 0x040036FF RID: 14079
		public static readonly XmlElementDefinition EncryptionAlgorithm = new XmlElementDefinition("EncryptionAlgorithm", WSTrust.Namespace);

		// Token: 0x04003700 RID: 14080
		public static readonly XmlElementDefinition EncryptWith = new XmlElementDefinition("EncryptWith", WSTrust.Namespace);

		// Token: 0x04003701 RID: 14081
		public static readonly XmlElementDefinition SignWith = new XmlElementDefinition("SignWith", WSTrust.Namespace);

		// Token: 0x04003702 RID: 14082
		public static readonly XmlElementDefinition OnBehalfOf = new XmlElementDefinition("OnBehalfOf", WSTrust.Namespace);

		// Token: 0x04003703 RID: 14083
		public static readonly XmlElementDefinition Claims = new XmlElementDefinition("Claims", WSTrust.Namespace);

		// Token: 0x04003704 RID: 14084
		public static readonly XmlElementDefinition ComputedKeyAlgorithm = new XmlElementDefinition("ComputedKeyAlgorithm", WSTrust.Namespace);

		// Token: 0x04003705 RID: 14085
		public static readonly XmlElementDefinition RequestSecurityToken = new XmlElementDefinition("RequestSecurityToken", WSTrust.Namespace);

		// Token: 0x04003706 RID: 14086
		public static readonly XmlElementDefinition Entropy = new XmlElementDefinition("Entropy", WSTrust.Namespace);

		// Token: 0x04003707 RID: 14087
		public static readonly XmlElementDefinition RequestedAttachedReference = new XmlElementDefinition("RequestedAttachedReference", WSTrust.Namespace);

		// Token: 0x04003708 RID: 14088
		public static readonly XmlElementDefinition RequestedUnattachedReference = new XmlElementDefinition("RequestedUnattachedReference", WSTrust.Namespace);

		// Token: 0x04003709 RID: 14089
		public static readonly XmlElementDefinition Lifetime = new XmlElementDefinition("Lifetime", WSTrust.Namespace);
	}
}
