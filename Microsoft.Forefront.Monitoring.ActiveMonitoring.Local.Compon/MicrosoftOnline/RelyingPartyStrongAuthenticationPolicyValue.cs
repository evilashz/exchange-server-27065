using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000AA RID: 170
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class RelyingPartyStrongAuthenticationPolicyValue
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		public RelyingPartyStrongAuthenticationPolicyValue()
		{
			this.enabledField = true;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0001EEF3 File Offset: 0x0001D0F3
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x0001EEFB File Offset: 0x0001D0FB
		[XmlArrayItem("RelyingParty", DataType = "token", IsNullable = false)]
		public string[] RelyingParties
		{
			get
			{
				return this.relyingPartiesField;
			}
			set
			{
				this.relyingPartiesField = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0001EF04 File Offset: 0x0001D104
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0001EF0C File Offset: 0x0001D10C
		[XmlArrayItem("Rule", IsNullable = false)]
		public StrongAuthenticationRuleValue[] Rules
		{
			get
			{
				return this.rulesField;
			}
			set
			{
				this.rulesField = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001EF15 File Offset: 0x0001D115
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001EF1D File Offset: 0x0001D11D
		[XmlAttribute]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x04000305 RID: 773
		private string[] relyingPartiesField;

		// Token: 0x04000306 RID: 774
		private StrongAuthenticationRuleValue[] rulesField;

		// Token: 0x04000307 RID: 775
		private bool enabledField;
	}
}
