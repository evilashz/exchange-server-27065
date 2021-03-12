using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B81 RID: 2945
	internal static class WSAuthorization
	{
		// Token: 0x0400370A RID: 14090
		public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2006/12/authorization";

		// Token: 0x0400370B RID: 14091
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("auth", "http://schemas.xmlsoap.org/ws/2006/12/authorization");

		// Token: 0x0400370C RID: 14092
		public static readonly XmlElementDefinition Value = new XmlElementDefinition("Value", WSAuthorization.Namespace);

		// Token: 0x0400370D RID: 14093
		public static readonly XmlElementDefinition AdditionalContext = new XmlElementDefinition("AdditionalContext", WSAuthorization.Namespace);

		// Token: 0x0400370E RID: 14094
		public static readonly XmlElementDefinition ContextItem = new XmlElementDefinition("ContextItem", WSAuthorization.Namespace);

		// Token: 0x0400370F RID: 14095
		public static readonly XmlElementDefinition ClaimType = new XmlElementDefinition("ClaimType", WSAuthorization.Namespace);
	}
}
