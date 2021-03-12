using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000212 RID: 530
	internal sealed class CacheDiagnosticMethods
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x000447EC File Offset: 0x000429EC
		internal static XmlNode ClearExchangeRunspaceConfigurationCache(XmlNode param)
		{
			ExchangeRunspaceConfigurationCache.Singleton.ClearCache();
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlNode xmlNode = safeXmlDocument.CreateElement("Success", "http://schemas.microsoft.com/exchange/services/2006/types");
			xmlNode.InnerText = true.ToString();
			return xmlNode;
		}

		// Token: 0x04000AC3 RID: 2755
		private const string SuccessXmlElement = "Success";
	}
}
