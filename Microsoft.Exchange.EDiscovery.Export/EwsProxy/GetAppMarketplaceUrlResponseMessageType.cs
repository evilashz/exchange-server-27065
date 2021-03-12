using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000109 RID: 265
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetAppMarketplaceUrlResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000212E9 File Offset: 0x0001F4E9
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000212F1 File Offset: 0x0001F4F1
		public string AppMarketplaceUrl
		{
			get
			{
				return this.appMarketplaceUrlField;
			}
			set
			{
				this.appMarketplaceUrlField = value;
			}
		}

		// Token: 0x04000868 RID: 2152
		private string appMarketplaceUrlField;
	}
}
