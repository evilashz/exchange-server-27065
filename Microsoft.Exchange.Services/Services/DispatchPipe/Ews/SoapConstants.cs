using System;

namespace Microsoft.Exchange.Services.DispatchPipe.Ews
{
	// Token: 0x02000DD5 RID: 3541
	internal static class SoapConstants
	{
		// Token: 0x04003200 RID: 12800
		public const string Soap11NamespaceUri = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x04003201 RID: 12801
		public const string Soap12NamespaceUri = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x04003202 RID: 12802
		public const string EnvelopeElementName = "Envelope";

		// Token: 0x04003203 RID: 12803
		public const string HeaderElementName = "Header";

		// Token: 0x04003204 RID: 12804
		public const string BodyElementName = "Body";

		// Token: 0x04003205 RID: 12805
		public const string TextXmlUtf8ContentType = "text/xml; charset=utf-8";
	}
}
