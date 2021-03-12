using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200034D RID: 845
	[DataContract(Name = "NavigateGroupResultsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class NavigateGroupResultsResponse : Response
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0008BC04 File Offset: 0x00089E04
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x0008BC0C File Offset: 0x00089E0C
		[DataMember]
		public ListGroupResults ReturnValue
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

		// Token: 0x04000FEA RID: 4074
		private ListGroupResults ReturnValueField;
	}
}
