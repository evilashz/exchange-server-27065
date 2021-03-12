using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C25 RID: 3109
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DIFindDomainsResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DIFindDomainsResponse : DIResponseBase
	{
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x000B6A76 File Offset: 0x000B4C76
		// (set) Token: 0x06004429 RID: 17449 RVA: 0x000B6A7E File Offset: 0x000B4C7E
		[DataMember]
		public DIDomainInfo[] DIDomainInformationList
		{
			get
			{
				return this.DIDomainInformationListField;
			}
			set
			{
				this.DIDomainInformationListField = value;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x000B6A87 File Offset: 0x000B4C87
		// (set) Token: 0x0600442B RID: 17451 RVA: 0x000B6A8F File Offset: 0x000B4C8F
		[DataMember]
		public DITenantInfo DITenantInformation
		{
			get
			{
				return this.DITenantInformationField;
			}
			set
			{
				this.DITenantInformationField = value;
			}
		}

		// Token: 0x040039D5 RID: 14805
		private DIDomainInfo[] DIDomainInformationListField;

		// Token: 0x040039D6 RID: 14806
		private DITenantInfo DITenantInformationField;
	}
}
