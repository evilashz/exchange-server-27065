using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C22 RID: 3106
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DIFindDomainsRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	public class DIFindDomainsRequest : DIRequestBase
	{
		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x000B69F8 File Offset: 0x000B4BF8
		// (set) Token: 0x0600441A RID: 17434 RVA: 0x000B6A00 File Offset: 0x000B4C00
		[DataMember]
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

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x000B6A09 File Offset: 0x000B4C09
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x000B6A11 File Offset: 0x000B4C11
		[DataMember]
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

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x000B6A1A File Offset: 0x000B4C1A
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x000B6A22 File Offset: 0x000B4C22
		[DataMember]
		public Guid? TenantId
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

		// Token: 0x040039CF RID: 14799
		private string DomainKeyField;

		// Token: 0x040039D0 RID: 14800
		private string DomainNameField;

		// Token: 0x040039D1 RID: 14801
		private Guid? TenantIdField;
	}
}
