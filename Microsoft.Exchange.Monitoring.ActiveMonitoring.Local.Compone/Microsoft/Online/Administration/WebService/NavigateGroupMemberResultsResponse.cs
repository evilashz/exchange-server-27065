using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000323 RID: 803
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavigateGroupMemberResultsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class NavigateGroupMemberResultsResponse : Response
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0008B740 File Offset: 0x00089940
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x0008B748 File Offset: 0x00089948
		[DataMember]
		public ListGroupMemberResults ReturnValue
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

		// Token: 0x04000FB6 RID: 4022
		private ListGroupMemberResults ReturnValueField;
	}
}
