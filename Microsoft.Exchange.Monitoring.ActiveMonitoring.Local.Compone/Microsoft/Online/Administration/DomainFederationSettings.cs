using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D8 RID: 984
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainFederationSettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class DomainFederationSettings : IExtensibleDataObject
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0008CCB5 File Offset: 0x0008AEB5
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0008CCBD File Offset: 0x0008AEBD
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0008CCC6 File Offset: 0x0008AEC6
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0008CCCE File Offset: 0x0008AECE
		[DataMember]
		public string ActiveLogOnUri
		{
			get
			{
				return this.ActiveLogOnUriField;
			}
			set
			{
				this.ActiveLogOnUriField = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0008CCD7 File Offset: 0x0008AED7
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0008CCDF File Offset: 0x0008AEDF
		[DataMember]
		public string FederationBrandName
		{
			get
			{
				return this.FederationBrandNameField;
			}
			set
			{
				this.FederationBrandNameField = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0008CCE8 File Offset: 0x0008AEE8
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x0008CCF0 File Offset: 0x0008AEF0
		[DataMember]
		public string IssuerUri
		{
			get
			{
				return this.IssuerUriField;
			}
			set
			{
				this.IssuerUriField = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0008CCF9 File Offset: 0x0008AEF9
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x0008CD01 File Offset: 0x0008AF01
		[DataMember]
		public string LogOffUri
		{
			get
			{
				return this.LogOffUriField;
			}
			set
			{
				this.LogOffUriField = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0008CD0A File Offset: 0x0008AF0A
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0008CD12 File Offset: 0x0008AF12
		[DataMember]
		public string MetadataExchangeUri
		{
			get
			{
				return this.MetadataExchangeUriField;
			}
			set
			{
				this.MetadataExchangeUriField = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0008CD1B File Offset: 0x0008AF1B
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0008CD23 File Offset: 0x0008AF23
		[DataMember]
		public string NextSigningCertificate
		{
			get
			{
				return this.NextSigningCertificateField;
			}
			set
			{
				this.NextSigningCertificateField = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0008CD2C File Offset: 0x0008AF2C
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x0008CD34 File Offset: 0x0008AF34
		[DataMember]
		public string PassiveLogOnUri
		{
			get
			{
				return this.PassiveLogOnUriField;
			}
			set
			{
				this.PassiveLogOnUriField = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0008CD3D File Offset: 0x0008AF3D
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0008CD45 File Offset: 0x0008AF45
		[DataMember]
		public AuthenticationProtocol? PreferredAuthenticationProtocol
		{
			get
			{
				return this.PreferredAuthenticationProtocolField;
			}
			set
			{
				this.PreferredAuthenticationProtocolField = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0008CD4E File Offset: 0x0008AF4E
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x0008CD56 File Offset: 0x0008AF56
		[DataMember]
		public string SigningCertificate
		{
			get
			{
				return this.SigningCertificateField;
			}
			set
			{
				this.SigningCertificateField = value;
			}
		}

		// Token: 0x040010EC RID: 4332
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010ED RID: 4333
		private string ActiveLogOnUriField;

		// Token: 0x040010EE RID: 4334
		private string FederationBrandNameField;

		// Token: 0x040010EF RID: 4335
		private string IssuerUriField;

		// Token: 0x040010F0 RID: 4336
		private string LogOffUriField;

		// Token: 0x040010F1 RID: 4337
		private string MetadataExchangeUriField;

		// Token: 0x040010F2 RID: 4338
		private string NextSigningCertificateField;

		// Token: 0x040010F3 RID: 4339
		private string PassiveLogOnUriField;

		// Token: 0x040010F4 RID: 4340
		private AuthenticationProtocol? PreferredAuthenticationProtocolField;

		// Token: 0x040010F5 RID: 4341
		private string SigningCertificateField;
	}
}
