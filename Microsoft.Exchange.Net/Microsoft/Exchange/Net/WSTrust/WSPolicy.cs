using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B82 RID: 2946
	internal static class WSPolicy
	{
		// Token: 0x04003710 RID: 14096
		public const string NamespaceUri = "http://schemas.xmlsoap.org/ws/2004/09/policy";

		// Token: 0x04003711 RID: 14097
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("wsp", "http://schemas.xmlsoap.org/ws/2004/09/policy");

		// Token: 0x04003712 RID: 14098
		public static readonly XmlElementDefinition AppliesTo = new XmlElementDefinition("AppliesTo", WSPolicy.Namespace);

		// Token: 0x04003713 RID: 14099
		public static readonly XmlElementDefinition PolicyReference = new XmlElementDefinition("PolicyReference", WSPolicy.Namespace);
	}
}
