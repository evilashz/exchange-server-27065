using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200090C RID: 2316
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class StrongAuthenticationRequirementValue
	{
		// Token: 0x17002774 RID: 10100
		// (get) Token: 0x06006F11 RID: 28433 RVA: 0x001768B2 File Offset: 0x00174AB2
		// (set) Token: 0x06006F12 RID: 28434 RVA: 0x001768BA File Offset: 0x00174ABA
		[XmlAttribute(DataType = "token")]
		public string RelyingParty
		{
			get
			{
				return this.relyingPartyField;
			}
			set
			{
				this.relyingPartyField = value;
			}
		}

		// Token: 0x17002775 RID: 10101
		// (get) Token: 0x06006F13 RID: 28435 RVA: 0x001768C3 File Offset: 0x00174AC3
		// (set) Token: 0x06006F14 RID: 28436 RVA: 0x001768CB File Offset: 0x00174ACB
		[XmlAttribute]
		public int State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x17002776 RID: 10102
		// (get) Token: 0x06006F15 RID: 28437 RVA: 0x001768D4 File Offset: 0x00174AD4
		// (set) Token: 0x06006F16 RID: 28438 RVA: 0x001768DC File Offset: 0x00174ADC
		[XmlIgnore]
		public bool StateSpecified
		{
			get
			{
				return this.stateFieldSpecified;
			}
			set
			{
				this.stateFieldSpecified = value;
			}
		}

		// Token: 0x17002777 RID: 10103
		// (get) Token: 0x06006F17 RID: 28439 RVA: 0x001768E5 File Offset: 0x00174AE5
		// (set) Token: 0x06006F18 RID: 28440 RVA: 0x001768ED File Offset: 0x00174AED
		[XmlAttribute]
		public DateTime RememberDevicesNotIssuedBefore
		{
			get
			{
				return this.rememberDevicesNotIssuedBeforeField;
			}
			set
			{
				this.rememberDevicesNotIssuedBeforeField = value;
			}
		}

		// Token: 0x17002778 RID: 10104
		// (get) Token: 0x06006F19 RID: 28441 RVA: 0x001768F6 File Offset: 0x00174AF6
		// (set) Token: 0x06006F1A RID: 28442 RVA: 0x001768FE File Offset: 0x00174AFE
		[XmlIgnore]
		public bool RememberDevicesNotIssuedBeforeSpecified
		{
			get
			{
				return this.rememberDevicesNotIssuedBeforeFieldSpecified;
			}
			set
			{
				this.rememberDevicesNotIssuedBeforeFieldSpecified = value;
			}
		}

		// Token: 0x04004820 RID: 18464
		private string relyingPartyField;

		// Token: 0x04004821 RID: 18465
		private int stateField;

		// Token: 0x04004822 RID: 18466
		private bool stateFieldSpecified;

		// Token: 0x04004823 RID: 18467
		private DateTime rememberDevicesNotIssuedBeforeField;

		// Token: 0x04004824 RID: 18468
		private bool rememberDevicesNotIssuedBeforeFieldSpecified;
	}
}
