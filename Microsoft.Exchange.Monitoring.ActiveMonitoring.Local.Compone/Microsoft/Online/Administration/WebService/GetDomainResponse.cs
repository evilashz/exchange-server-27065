using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000347 RID: 839
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetDomainResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetDomainResponse : Response
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0008BB6E File Offset: 0x00089D6E
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x0008BB76 File Offset: 0x00089D76
		[DataMember]
		public Domain ReturnValue
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

		// Token: 0x04000FE4 RID: 4068
		private Domain ReturnValueField;
	}
}
