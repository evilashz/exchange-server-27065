using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D1 RID: 977
	[DataContract(Name = "PartnerInformation", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class PartnerInformation : IExtensibleDataObject
	{
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0008CB38 File Offset: 0x0008AD38
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0008CB40 File Offset: 0x0008AD40
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0008CB49 File Offset: 0x0008AD49
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0008CB51 File Offset: 0x0008AD51
		[DataMember]
		public CompanyType? CompanyType
		{
			get
			{
				return this.CompanyTypeField;
			}
			set
			{
				this.CompanyTypeField = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0008CB5A File Offset: 0x0008AD5A
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0008CB62 File Offset: 0x0008AD62
		[DataMember]
		public Guid[] Contracts
		{
			get
			{
				return this.ContractsField;
			}
			set
			{
				this.ContractsField = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0008CB6B File Offset: 0x0008AD6B
		// (set) Token: 0x060017C3 RID: 6083 RVA: 0x0008CB73 File Offset: 0x0008AD73
		[DataMember]
		public bool? DapEnabled
		{
			get
			{
				return this.DapEnabledField;
			}
			set
			{
				this.DapEnabledField = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0008CB7C File Offset: 0x0008AD7C
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x0008CB84 File Offset: 0x0008AD84
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0008CB8D File Offset: 0x0008AD8D
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x0008CB95 File Offset: 0x0008AD95
		[DataMember]
		public string PartnerCommerceUrl
		{
			get
			{
				return this.PartnerCommerceUrlField;
			}
			set
			{
				this.PartnerCommerceUrlField = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0008CB9E File Offset: 0x0008AD9E
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x0008CBA6 File Offset: 0x0008ADA6
		[DataMember]
		public string PartnerCompanyName
		{
			get
			{
				return this.PartnerCompanyNameField;
			}
			set
			{
				this.PartnerCompanyNameField = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0008CBAF File Offset: 0x0008ADAF
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x0008CBB7 File Offset: 0x0008ADB7
		[DataMember]
		public string PartnerHelpUrl
		{
			get
			{
				return this.PartnerHelpUrlField;
			}
			set
			{
				this.PartnerHelpUrlField = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0008CBC0 File Offset: 0x0008ADC0
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x0008CBC8 File Offset: 0x0008ADC8
		[DataMember]
		public string[] PartnerSupportEmails
		{
			get
			{
				return this.PartnerSupportEmailsField;
			}
			set
			{
				this.PartnerSupportEmailsField = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0008CBD1 File Offset: 0x0008ADD1
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0008CBD9 File Offset: 0x0008ADD9
		[DataMember]
		public string[] PartnerSupportTelephones
		{
			get
			{
				return this.PartnerSupportTelephonesField;
			}
			set
			{
				this.PartnerSupportTelephonesField = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0008CBE2 File Offset: 0x0008ADE2
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x0008CBEA File Offset: 0x0008ADEA
		[DataMember]
		public string PartnerSupportUrl
		{
			get
			{
				return this.PartnerSupportUrlField;
			}
			set
			{
				this.PartnerSupportUrlField = value;
			}
		}

		// Token: 0x040010C3 RID: 4291
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010C4 RID: 4292
		private CompanyType? CompanyTypeField;

		// Token: 0x040010C5 RID: 4293
		private Guid[] ContractsField;

		// Token: 0x040010C6 RID: 4294
		private bool? DapEnabledField;

		// Token: 0x040010C7 RID: 4295
		private Guid? ObjectIdField;

		// Token: 0x040010C8 RID: 4296
		private string PartnerCommerceUrlField;

		// Token: 0x040010C9 RID: 4297
		private string PartnerCompanyNameField;

		// Token: 0x040010CA RID: 4298
		private string PartnerHelpUrlField;

		// Token: 0x040010CB RID: 4299
		private string[] PartnerSupportEmailsField;

		// Token: 0x040010CC RID: 4300
		private string[] PartnerSupportTelephonesField;

		// Token: 0x040010CD RID: 4301
		private string PartnerSupportUrlField;
	}
}
