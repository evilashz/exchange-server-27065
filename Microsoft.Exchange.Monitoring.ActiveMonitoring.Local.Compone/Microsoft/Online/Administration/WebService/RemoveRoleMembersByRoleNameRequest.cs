using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002DF RID: 735
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RemoveRoleMembersByRoleNameRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class RemoveRoleMembersByRoleNameRequest : Request
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0008AD5B File Offset: 0x00088F5B
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x0008AD63 File Offset: 0x00088F63
		[DataMember]
		public RoleMember[] RoleMembers
		{
			get
			{
				return this.RoleMembersField;
			}
			set
			{
				this.RoleMembersField = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0008AD6C File Offset: 0x00088F6C
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x0008AD74 File Offset: 0x00088F74
		[DataMember]
		public string RoleName
		{
			get
			{
				return this.RoleNameField;
			}
			set
			{
				this.RoleNameField = value;
			}
		}

		// Token: 0x04000F41 RID: 3905
		private RoleMember[] RoleMembersField;

		// Token: 0x04000F42 RID: 3906
		private string RoleNameField;
	}
}
