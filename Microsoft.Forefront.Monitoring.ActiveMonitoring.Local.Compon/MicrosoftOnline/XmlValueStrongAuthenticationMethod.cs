using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A7 RID: 167
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueStrongAuthenticationMethod
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001EE77 File Offset: 0x0001D077
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0001EE7F File Offset: 0x0001D07F
		public StrongAuthenticationMethodValue StrongAuthenticationPolicy
		{
			get
			{
				return this.strongAuthenticationPolicyField;
			}
			set
			{
				this.strongAuthenticationPolicyField = value;
			}
		}

		// Token: 0x04000300 RID: 768
		private StrongAuthenticationMethodValue strongAuthenticationPolicyField;
	}
}
