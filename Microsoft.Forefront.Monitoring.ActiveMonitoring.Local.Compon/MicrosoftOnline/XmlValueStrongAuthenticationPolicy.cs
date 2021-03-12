using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000AC RID: 172
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueStrongAuthenticationPolicy
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001EF3F File Offset: 0x0001D13F
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0001EF47 File Offset: 0x0001D147
		public StrongAuthenticationPolicyValue StrongAuthenticationPolicy
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

		// Token: 0x04000309 RID: 777
		private StrongAuthenticationPolicyValue strongAuthenticationPolicyField;
	}
}
