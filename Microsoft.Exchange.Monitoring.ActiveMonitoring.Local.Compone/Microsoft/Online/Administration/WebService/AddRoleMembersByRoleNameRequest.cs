using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002DD RID: 733
	[DataContract(Name = "AddRoleMembersByRoleNameRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddRoleMembersByRoleNameRequest : Request
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0008AD07 File Offset: 0x00088F07
		// (set) Token: 0x06001422 RID: 5154 RVA: 0x0008AD0F File Offset: 0x00088F0F
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0008AD18 File Offset: 0x00088F18
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x0008AD20 File Offset: 0x00088F20
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

		// Token: 0x04000F3D RID: 3901
		private RoleMember[] RoleMembersField;

		// Token: 0x04000F3E RID: 3902
		private string RoleNameField;
	}
}
