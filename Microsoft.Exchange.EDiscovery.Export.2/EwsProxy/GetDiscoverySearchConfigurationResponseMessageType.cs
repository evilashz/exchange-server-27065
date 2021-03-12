using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000199 RID: 409
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetDiscoverySearchConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000242B4 File Offset: 0x000224B4
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x000242BC File Offset: 0x000224BC
		[XmlArrayItem("DiscoverySearchConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public DiscoverySearchConfigurationType[] DiscoverySearchConfigurations
		{
			get
			{
				return this.discoverySearchConfigurationsField;
			}
			set
			{
				this.discoverySearchConfigurationsField = value;
			}
		}

		// Token: 0x04000BFC RID: 3068
		private DiscoverySearchConfigurationType[] discoverySearchConfigurationsField;
	}
}
