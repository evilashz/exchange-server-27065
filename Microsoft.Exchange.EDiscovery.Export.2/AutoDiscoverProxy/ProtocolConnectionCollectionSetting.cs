using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200008C RID: 140
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtocolConnectionCollectionSetting : UserSetting
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001F78E File Offset: 0x0001D98E
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001F796 File Offset: 0x0001D996
		[XmlArray(IsNullable = true)]
		public ProtocolConnection[] ProtocolConnections
		{
			get
			{
				return this.protocolConnectionsField;
			}
			set
			{
				this.protocolConnectionsField = value;
			}
		}

		// Token: 0x04000334 RID: 820
		private ProtocolConnection[] protocolConnectionsField;
	}
}
