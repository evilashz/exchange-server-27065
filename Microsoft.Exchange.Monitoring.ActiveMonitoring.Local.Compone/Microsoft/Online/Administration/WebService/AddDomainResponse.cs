using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000345 RID: 837
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddDomainResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class AddDomainResponse : Response
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0008BB3C File Offset: 0x00089D3C
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x0008BB44 File Offset: 0x00089D44
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

		// Token: 0x04000FE2 RID: 4066
		private Domain ReturnValueField;
	}
}
