using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000087 RID: 135
	[XmlInclude(typeof(DocumentSharingLocationCollectionSetting))]
	[XmlInclude(typeof(WebClientUrlCollectionSetting))]
	[XmlInclude(typeof(StringSetting))]
	[XmlInclude(typeof(ProtocolConnectionCollectionSetting))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(AlternateMailboxCollectionSetting))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DesignerCategory("code")]
	[Serializable]
	public class UserSetting
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001F711 File Offset: 0x0001D911
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0001F719 File Offset: 0x0001D919
		[XmlElement(IsNullable = true)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x0400032F RID: 815
		private string nameField;
	}
}
