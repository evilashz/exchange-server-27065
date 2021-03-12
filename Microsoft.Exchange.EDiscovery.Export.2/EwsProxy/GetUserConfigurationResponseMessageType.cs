using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001DD RID: 477
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUserConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00025713 File Offset: 0x00023913
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0002571B File Offset: 0x0002391B
		public UserConfigurationType UserConfiguration
		{
			get
			{
				return this.userConfigurationField;
			}
			set
			{
				this.userConfigurationField = value;
			}
		}

		// Token: 0x04000DAB RID: 3499
		private UserConfigurationType userConfigurationField;
	}
}
