using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000340 RID: 832
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListPartnerContractsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListPartnerContractsResponse : Response
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0008BABF File Offset: 0x00089CBF
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x0008BAC7 File Offset: 0x00089CC7
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

		// Token: 0x04000FDD RID: 4061
		private ListPartnerContractResults ReturnValueField;
	}
}
