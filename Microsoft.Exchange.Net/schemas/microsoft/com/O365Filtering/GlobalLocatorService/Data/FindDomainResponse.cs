using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C30 RID: 3120
	[DataContract(Name = "FindDomainResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class FindDomainResponse : ResponseBase
	{
		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x000B6CCC File Offset: 0x000B4ECC
		// (set) Token: 0x06004470 RID: 17520 RVA: 0x000B6CD4 File Offset: 0x000B4ED4
		[DataMember]
		public DomainInfo DomainInfo
		{
			get
			{
				return this.DomainInfoField;
			}
			set
			{
				this.DomainInfoField = value;
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06004471 RID: 17521 RVA: 0x000B6CDD File Offset: 0x000B4EDD
		// (set) Token: 0x06004472 RID: 17522 RVA: 0x000B6CE5 File Offset: 0x000B4EE5
		[DataMember]
		public TenantInfo TenantInfo
		{
			get
			{
				return this.TenantInfoField;
			}
			set
			{
				this.TenantInfoField = value;
			}
		}

		// Token: 0x040039FA RID: 14842
		private DomainInfo DomainInfoField;

		// Token: 0x040039FB RID: 14843
		private TenantInfo TenantInfoField;
	}
}
