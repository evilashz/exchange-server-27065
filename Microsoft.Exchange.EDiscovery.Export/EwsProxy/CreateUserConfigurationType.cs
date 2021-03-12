using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000358 RID: 856
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateUserConfigurationType : BaseRequestType
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0002980E File Offset: 0x00027A0E
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x00029816 File Offset: 0x00027A16
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

		// Token: 0x04001262 RID: 4706
		private UserConfigurationType userConfigurationField;
	}
}
