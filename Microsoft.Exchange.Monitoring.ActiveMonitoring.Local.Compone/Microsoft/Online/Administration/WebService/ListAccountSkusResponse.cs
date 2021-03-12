using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200033F RID: 831
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListAccountSkusResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListAccountSkusResponse : Response
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x0008BAA6 File Offset: 0x00089CA6
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x0008BAAE File Offset: 0x00089CAE
		[DataMember]
		public AccountSkuDetails[] ReturnValue
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

		// Token: 0x04000FDC RID: 4060
		private AccountSkuDetails[] ReturnValueField;
	}
}
