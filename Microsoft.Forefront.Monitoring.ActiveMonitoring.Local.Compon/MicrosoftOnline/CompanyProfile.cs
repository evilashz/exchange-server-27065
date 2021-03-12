using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200009F RID: 159
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[Serializable]
	public class CompanyProfile
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001ECC9 File Offset: 0x0001CEC9
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0001ECD1 File Offset: 0x0001CED1
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001ECDA File Offset: 0x0001CEDA
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001ECE2 File Offset: 0x0001CEE2
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001ECEB File Offset: 0x0001CEEB
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001ECF3 File Offset: 0x0001CEF3
		public string TelephoneNumber
		{
			get
			{
				return this.telephoneNumberField;
			}
			set
			{
				this.telephoneNumberField = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001ECFC File Offset: 0x0001CEFC
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0001ED04 File Offset: 0x0001CF04
		public string Street
		{
			get
			{
				return this.streetField;
			}
			set
			{
				this.streetField = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001ED0D File Offset: 0x0001CF0D
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001ED15 File Offset: 0x0001CF15
		public string City
		{
			get
			{
				return this.cityField;
			}
			set
			{
				this.cityField = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001ED1E File Offset: 0x0001CF1E
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001ED26 File Offset: 0x0001CF26
		public string State
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001ED2F File Offset: 0x0001CF2F
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001ED37 File Offset: 0x0001CF37
		public string PostalCode
		{
			get
			{
				return this.postalCodeField;
			}
			set
			{
				this.postalCodeField = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001ED40 File Offset: 0x0001CF40
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0001ED48 File Offset: 0x0001CF48
		public string CountryLetterCode
		{
			get
			{
				return this.countryLetterCodeField;
			}
			set
			{
				this.countryLetterCodeField = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001ED51 File Offset: 0x0001CF51
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0001ED59 File Offset: 0x0001CF59
		public string CommunicationCulture
		{
			get
			{
				return this.communicationCultureField;
			}
			set
			{
				this.communicationCultureField = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001ED62 File Offset: 0x0001CF62
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0001ED6A File Offset: 0x0001CF6A
		public CompanyProfileState LifecycleState
		{
			get
			{
				return this.lifecycleStateField;
			}
			set
			{
				this.lifecycleStateField = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001ED73 File Offset: 0x0001CF73
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001ED7B File Offset: 0x0001CF7B
		public CompanyProfileStateChangeReason LifecycleStateChangeReason
		{
			get
			{
				return this.lifecycleStateChangeReasonField;
			}
			set
			{
				this.lifecycleStateChangeReasonField = value;
			}
		}

		// Token: 0x040002D6 RID: 726
		private string displayNameField;

		// Token: 0x040002D7 RID: 727
		private string descriptionField;

		// Token: 0x040002D8 RID: 728
		private string telephoneNumberField;

		// Token: 0x040002D9 RID: 729
		private string streetField;

		// Token: 0x040002DA RID: 730
		private string cityField;

		// Token: 0x040002DB RID: 731
		private string stateField;

		// Token: 0x040002DC RID: 732
		private string postalCodeField;

		// Token: 0x040002DD RID: 733
		private string countryLetterCodeField;

		// Token: 0x040002DE RID: 734
		private string communicationCultureField;

		// Token: 0x040002DF RID: 735
		private CompanyProfileState lifecycleStateField;

		// Token: 0x040002E0 RID: 736
		private CompanyProfileStateChangeReason lifecycleStateChangeReasonField;
	}
}
