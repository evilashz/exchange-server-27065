using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000333 RID: 819
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListUsersResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListUsersResponse : Response
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0008B8D0 File Offset: 0x00089AD0
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x0008B8D8 File Offset: 0x00089AD8
		[DataMember]
		public ListUserResults ReturnValue
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

		// Token: 0x04000FC6 RID: 4038
		private ListUserResults ReturnValueField;
	}
}
