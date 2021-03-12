using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000914 RID: 2324
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class StrongAuthenticationPolicyValue
	{
		// Token: 0x17002783 RID: 10115
		// (get) Token: 0x06006F37 RID: 28471 RVA: 0x001769F8 File Offset: 0x00174BF8
		// (set) Token: 0x06006F38 RID: 28472 RVA: 0x00176A00 File Offset: 0x00174C00
		[XmlArrayItem("RelyingPartyStrongAuthenticationPolicy", IsNullable = false)]
		[XmlArray(Order = 0)]
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

		// Token: 0x0400482F RID: 18479
		private RelyingPartyStrongAuthenticationPolicyValue[] relyingPartyStrongAuthenticationPoliciesField;
	}
}
