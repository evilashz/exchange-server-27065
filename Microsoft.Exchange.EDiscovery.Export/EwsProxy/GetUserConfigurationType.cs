using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000355 RID: 853
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUserConfigurationType : BaseRequestType
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000297CB File Offset: 0x000279CB
		// (set) Token: 0x06001B82 RID: 7042 RVA: 0x000297D3 File Offset: 0x000279D3
		public UserConfigurationNameType UserConfigurationName
		{
			get
			{
				return this.userConfigurationNameField;
			}
			set
			{
				this.userConfigurationNameField = value;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000297DC File Offset: 0x000279DC
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x000297E4 File Offset: 0x000279E4
		public UserConfigurationPropertyType UserConfigurationProperties
		{
			get
			{
				return this.userConfigurationPropertiesField;
			}
			set
			{
				this.userConfigurationPropertiesField = value;
			}
		}

		// Token: 0x04001259 RID: 4697
		private UserConfigurationNameType userConfigurationNameField;

		// Token: 0x0400125A RID: 4698
		private UserConfigurationPropertyType userConfigurationPropertiesField;
	}
}
