using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000344 RID: 836
	[DataContract(Name = "NavigateContactResultsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class NavigateContactResultsResponse : Response
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0008BB23 File Offset: 0x00089D23
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x0008BB2B File Offset: 0x00089D2B
		[DataMember]
		public ListContactResults ReturnValue
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

		// Token: 0x04000FE1 RID: 4065
		private ListContactResults ReturnValueField;
	}
}
