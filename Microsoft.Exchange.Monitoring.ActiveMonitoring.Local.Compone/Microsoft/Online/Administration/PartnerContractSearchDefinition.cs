using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003BC RID: 956
	[DataContract(Name = "PartnerContractSearchDefinition", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class PartnerContractSearchDefinition : SearchDefinition
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0008C5B3 File Offset: 0x0008A7B3
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x0008C5BB File Offset: 0x0008A7BB
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

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0008C5C4 File Offset: 0x0008A7C4
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x0008C5CC File Offset: 0x0008A7CC
		[DataMember]
		public Guid? ManagedTenantId
		{
			get
			{
				return this.ManagedTenantIdField;
			}
			set
			{
				this.ManagedTenantIdField = value;
			}
		}

		// Token: 0x04001055 RID: 4181
		private string DomainNameField;

		// Token: 0x04001056 RID: 4182
		private Guid? ManagedTenantIdField;
	}
}
