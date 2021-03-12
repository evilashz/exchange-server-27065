using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000300 RID: 768
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListPartnerContractsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListPartnerContractsRequest : Request
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0008B32B File Offset: 0x0008952B
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0008B333 File Offset: 0x00089533
		[DataMember]
		public PartnerContractSearchDefinition PartnerContractSearchDefinition
		{
			get
			{
				return this.PartnerContractSearchDefinitionField;
			}
			set
			{
				this.PartnerContractSearchDefinitionField = value;
			}
		}

		// Token: 0x04000F89 RID: 3977
		private PartnerContractSearchDefinition PartnerContractSearchDefinitionField;
	}
}
