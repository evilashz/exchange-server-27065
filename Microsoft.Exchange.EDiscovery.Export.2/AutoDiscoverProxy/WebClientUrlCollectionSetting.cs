using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200008A RID: 138
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class WebClientUrlCollectionSetting : UserSetting
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0001F75C File Offset: 0x0001D95C
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0001F764 File Offset: 0x0001D964
		[XmlArray(IsNullable = true)]
		public WebClientUrl[] WebClientUrls
		{
			get
			{
				return this.webClientUrlsField;
			}
			set
			{
				this.webClientUrlsField = value;
			}
		}

		// Token: 0x04000332 RID: 818
		private WebClientUrl[] webClientUrlsField;
	}
}
