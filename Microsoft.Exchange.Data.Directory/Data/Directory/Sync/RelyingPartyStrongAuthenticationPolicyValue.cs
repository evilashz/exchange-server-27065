using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000915 RID: 2325
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class RelyingPartyStrongAuthenticationPolicyValue
	{
		// Token: 0x06006F3A RID: 28474 RVA: 0x00176A11 File Offset: 0x00174C11
		public RelyingPartyStrongAuthenticationPolicyValue()
		{
			this.enabledField = true;
		}

		// Token: 0x17002784 RID: 10116
		// (get) Token: 0x06006F3B RID: 28475 RVA: 0x00176A20 File Offset: 0x00174C20
		// (set) Token: 0x06006F3C RID: 28476 RVA: 0x00176A28 File Offset: 0x00174C28
		[XmlArrayItem("RelyingParty", DataType = "token", IsNullable = false)]
		[XmlArray(Order = 0)]
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

		// Token: 0x17002785 RID: 10117
		// (get) Token: 0x06006F3D RID: 28477 RVA: 0x00176A31 File Offset: 0x00174C31
		// (set) Token: 0x06006F3E RID: 28478 RVA: 0x00176A39 File Offset: 0x00174C39
		[XmlArrayItem("Rule", IsNullable = false)]
		[XmlArray(Order = 1)]
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

		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x06006F3F RID: 28479 RVA: 0x00176A42 File Offset: 0x00174C42
		// (set) Token: 0x06006F40 RID: 28480 RVA: 0x00176A4A File Offset: 0x00174C4A
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

		// Token: 0x04004830 RID: 18480
		private string[] relyingPartiesField;

		// Token: 0x04004831 RID: 18481
		private StrongAuthenticationRuleValue[] rulesField;

		// Token: 0x04004832 RID: 18482
		private bool enabledField;
	}
}
