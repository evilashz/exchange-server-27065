using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E0 RID: 736
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListRoleMembersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListRoleMembersRequest : Request
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0008AD85 File Offset: 0x00088F85
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x0008AD8D File Offset: 0x00088F8D
		[DataMember]
		public RoleMemberSearchDefinition RoleMemberSearchDefinition
		{
			get
			{
				return this.RoleMemberSearchDefinitionField;
			}
			set
			{
				this.RoleMemberSearchDefinitionField = value;
			}
		}

		// Token: 0x04000F43 RID: 3907
		private RoleMemberSearchDefinition RoleMemberSearchDefinitionField;
	}
}
