using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000329 RID: 809
	[DataContract(Name = "ListRoleMembersResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListRoleMembersResponse : Response
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0008B7D6 File Offset: 0x000899D6
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x0008B7DE File Offset: 0x000899DE
		[DataMember]
		public ListRoleMemberResults ReturnValue
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

		// Token: 0x04000FBC RID: 4028
		private ListRoleMemberResults ReturnValueField;
	}
}
