using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B7C RID: 2940
	internal static class Soap
	{
		// Token: 0x040036D4 RID: 14036
		public const string NamespaceUri = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x040036D5 RID: 14037
		public static readonly XmlNamespaceDefinition Namespace = new XmlNamespaceDefinition("s", "http://www.w3.org/2003/05/soap-envelope");

		// Token: 0x040036D6 RID: 14038
		public static readonly XmlElementDefinition Envelope = new XmlElementDefinition("Envelope", Soap.Namespace);

		// Token: 0x040036D7 RID: 14039
		public static readonly XmlElementDefinition Header = new XmlElementDefinition("Header", Soap.Namespace);

		// Token: 0x040036D8 RID: 14040
		public static readonly XmlElementDefinition Body = new XmlElementDefinition("Body", Soap.Namespace);

		// Token: 0x040036D9 RID: 14041
		public static readonly XmlElementDefinition Fault = new XmlElementDefinition("Fault", Soap.Namespace);

		// Token: 0x040036DA RID: 14042
		public static readonly XmlElementDefinition Code = new XmlElementDefinition("Code", Soap.Namespace);

		// Token: 0x040036DB RID: 14043
		public static readonly XmlElementDefinition Value = new XmlElementDefinition("Value", Soap.Namespace);

		// Token: 0x040036DC RID: 14044
		public static readonly XmlElementDefinition Subcode = new XmlElementDefinition("Subcode", Soap.Namespace);

		// Token: 0x040036DD RID: 14045
		public static readonly XmlAttributeDefinition MustUnderstand = new XmlAttributeDefinition("mustUnderstand", Soap.Namespace);
	}
}
