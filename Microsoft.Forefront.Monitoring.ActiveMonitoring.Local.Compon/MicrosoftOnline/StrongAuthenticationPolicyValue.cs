using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000AB RID: 171
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class StrongAuthenticationPolicyValue
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001EF26 File Offset: 0x0001D126
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0001EF2E File Offset: 0x0001D12E
		[XmlArrayItem("RelyingPartyStrongAuthenticationPolicy", IsNullable = false)]
		public RelyingPartyStrongAuthenticationPolicyValue[] RelyingPartyStrongAuthenticationPolicies
		{
			get
			{
				return this.relyingPartyStrongAuthenticationPoliciesField;
			}
			set
			{
				this.relyingPartyStrongAuthenticationPoliciesField = value;
			}
		}

		// Token: 0x04000308 RID: 776
		private RelyingPartyStrongAuthenticationPolicyValue[] relyingPartyStrongAuthenticationPoliciesField;
	}
}
