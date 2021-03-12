using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C29 RID: 3113
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DIDomainInfo", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DIDomainInfo : DITimeStamp
	{
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x000B6B2F File Offset: 0x000B4D2F
		// (set) Token: 0x0600443F RID: 17471 RVA: 0x000B6B37 File Offset: 0x000B4D37
		[DataMember(IsRequired = true)]
		public string DomainKey
		{
			get
			{
				return this.DomainKeyField;
			}
			set
			{
				this.DomainKeyField = value;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x000B6B40 File Offset: 0x000B4D40
		// (set) Token: 0x06004441 RID: 17473 RVA: 0x000B6B48 File Offset: 0x000B4D48
		[DataMember(IsRequired = true)]
		public string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x000B6B51 File Offset: 0x000B4D51
		// (set) Token: 0x06004443 RID: 17475 RVA: 0x000B6B59 File Offset: 0x000B4D59
		[DataMember]
		public GLSProperty[] DomainProperties
		{
			get
			{
				return this.DomainPropertiesField;
			}
			set
			{
				this.DomainPropertiesField = value;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x000B6B62 File Offset: 0x000B4D62
		// (set) Token: 0x06004445 RID: 17477 RVA: 0x000B6B6A File Offset: 0x000B4D6A
		[DataMember]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x000B6B73 File Offset: 0x000B4D73
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x000B6B7B File Offset: 0x000B4D7B
		[DataMember]
		public string ErrorMessage
		{
			get
			{
				return this.ErrorMessageField;
			}
			set
			{
				this.ErrorMessageField = value;
			}
		}

		// Token: 0x040039DE RID: 14814
		private string DomainKeyField;

		// Token: 0x040039DF RID: 14815
		private string DomainNameField;

		// Token: 0x040039E0 RID: 14816
		private GLSProperty[] DomainPropertiesField;

		// Token: 0x040039E1 RID: 14817
		private Guid TenantIdField;

		// Token: 0x040039E2 RID: 14818
		private string ErrorMessageField;
	}
}
