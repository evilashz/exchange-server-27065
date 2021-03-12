using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x0200089F RID: 2207
	[XmlType(Namespace = "DeltaSyncV2:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class AccountType
	{
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x0006A8C1 File Offset: 0x00068AC1
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x0006A8C9 File Offset: 0x00068AC9
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x0006A8D2 File Offset: 0x00068AD2
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x0006A8DA File Offset: 0x00068ADA
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x0006A8E3 File Offset: 0x00068AE3
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x0006A8EB File Offset: 0x00068AEB
		public Fault Fault
		{
			get
			{
				return this.faultField;
			}
			set
			{
				this.faultField = value;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x0006A8F4 File Offset: 0x00068AF4
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x0006A8FC File Offset: 0x00068AFC
		public string PartnerID
		{
			get
			{
				return this.partnerIDField;
			}
			set
			{
				this.partnerIDField = value;
			}
		}

		// Token: 0x04002904 RID: 10500
		private string nameField;

		// Token: 0x04002905 RID: 10501
		private int statusField;

		// Token: 0x04002906 RID: 10502
		private Fault faultField;

		// Token: 0x04002907 RID: 10503
		private string partnerIDField;
	}
}
