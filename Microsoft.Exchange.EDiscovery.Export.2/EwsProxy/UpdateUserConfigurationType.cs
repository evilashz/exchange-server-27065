using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000354 RID: 852
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateUserConfigurationType : BaseRequestType
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x000297B2 File Offset: 0x000279B2
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x000297BA File Offset: 0x000279BA
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

		// Token: 0x04001258 RID: 4696
		private UserConfigurationType userConfigurationField;
	}
}
