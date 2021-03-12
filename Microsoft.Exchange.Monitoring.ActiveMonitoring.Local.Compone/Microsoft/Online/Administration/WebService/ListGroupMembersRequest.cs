using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000318 RID: 792
	[DataContract(Name = "ListGroupMembersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListGroupMembersRequest : Request
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0008B62D File Offset: 0x0008982D
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0008B635 File Offset: 0x00089835
		[DataMember]
		public GroupMemberSearchDefinition GroupMemberSearchDefinition
		{
			get
			{
				return this.GroupMemberSearchDefinitionField;
			}
			set
			{
				this.GroupMemberSearchDefinitionField = value;
			}
		}

		// Token: 0x04000FAB RID: 4011
		private GroupMemberSearchDefinition GroupMemberSearchDefinitionField;
	}
}
