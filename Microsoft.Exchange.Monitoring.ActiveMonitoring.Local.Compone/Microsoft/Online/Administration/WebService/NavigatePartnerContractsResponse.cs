using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000341 RID: 833
	[DataContract(Name = "NavigatePartnerContractsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class NavigatePartnerContractsResponse : Response
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0008BAD8 File Offset: 0x00089CD8
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0008BAE0 File Offset: 0x00089CE0
		[DataMember]
		public ListPartnerContractResults ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x04000FDE RID: 4062
		private ListPartnerContractResults ReturnValueField;
	}
}
