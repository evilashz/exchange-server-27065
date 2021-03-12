using System;
using System.IO;
using System.Net;
using System.Xml;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C2 RID: 194
	internal class OwaLanguagePostProxyRequestHandler : OwaProxyRequestHandler
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x0002B63C File Offset: 0x0002983C
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			if (base.ProxyToDownLevel)
			{
				using (StringWriter stringWriter = new StringWriter())
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						SerializedClientSecurityContext serializedClientSecurityContext = base.HttpContext.GetSerializedClientSecurityContext();
						serializedClientSecurityContext.Serialize(xmlTextWriter);
						stringWriter.Flush();
						headers["X-OwaLanguageProxySerializedSecurityContext"] = stringWriter.ToString();
					}
				}
			}
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0002B6C4 File Offset: 0x000298C4
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return !string.Equals(headerName, "X-OwaLanguageProxySerializedSecurityContext", StringComparison.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
		}

		// Token: 0x040004A3 RID: 1187
		private const string LanguageProxySerializedSecurityContextHeaderName = "X-OwaLanguageProxySerializedSecurityContext";
	}
}
