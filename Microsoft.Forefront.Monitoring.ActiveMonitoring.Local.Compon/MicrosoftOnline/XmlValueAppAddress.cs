using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C3 RID: 195
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueAppAddress
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001F266 File Offset: 0x0001D466
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0001F26E File Offset: 0x0001D46E
		public AppAddressValue AppAddress
		{
			get
			{
				return this.appAddressField;
			}
			set
			{
				this.appAddressField = value;
			}
		}

		// Token: 0x04000342 RID: 834
		private AppAddressValue appAddressField;
	}
}
