using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200034E RID: 846
	[DataContract(Name = "ListGroupMembersResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ListGroupMembersResponse : Response
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0008BC1D File Offset: 0x00089E1D
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0008BC25 File Offset: 0x00089E25
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

		// Token: 0x04000FEB RID: 4075
		private ListGroupMemberResults ReturnValueField;
	}
}
