using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200010D RID: 269
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ClientExtensionResponseType : ResponseMessageType
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00021391 File Offset: 0x0001F591
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00021399 File Offset: 0x0001F599
		[XmlArrayItem("ClientExtension", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ClientExtensionType[] ClientExtensions
		{
			get
			{
				return this.clientExtensionsField;
			}
			set
			{
				this.clientExtensionsField = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000213A2 File Offset: 0x0001F5A2
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x000213AA File Offset: 0x0001F5AA
		public string RawMasterTableXml
		{
			get
			{
				return this.rawMasterTableXmlField;
			}
			set
			{
				this.rawMasterTableXmlField = value;
			}
		}

		// Token: 0x04000870 RID: 2160
		private ClientExtensionType[] clientExtensionsField;

		// Token: 0x04000871 RID: 2161
		private string rawMasterTableXmlField;
	}
}
