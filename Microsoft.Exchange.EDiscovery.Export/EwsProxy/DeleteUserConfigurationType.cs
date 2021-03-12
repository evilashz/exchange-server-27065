using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000357 RID: 855
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DeleteUserConfigurationType : BaseRequestType
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x000297F5 File Offset: 0x000279F5
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x000297FD File Offset: 0x000279FD
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

		// Token: 0x04001261 RID: 4705
		private UserConfigurationNameType userConfigurationNameField;
	}
}
