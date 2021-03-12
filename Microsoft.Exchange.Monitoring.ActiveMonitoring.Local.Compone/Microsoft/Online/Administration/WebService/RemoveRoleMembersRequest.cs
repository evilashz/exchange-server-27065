using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002DE RID: 734
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RemoveRoleMembersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class RemoveRoleMembersRequest : Request
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0008AD31 File Offset: 0x00088F31
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0008AD39 File Offset: 0x00088F39
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

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0008AD42 File Offset: 0x00088F42
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0008AD4A File Offset: 0x00088F4A
		[DataMember]
		public Guid RoleObjectId
		{
			get
			{
				return this.RoleObjectIdField;
			}
			set
			{
				this.RoleObjectIdField = value;
			}
		}

		// Token: 0x04000F3F RID: 3903
		private RoleMember[] RoleMembersField;

		// Token: 0x04000F40 RID: 3904
		private Guid RoleObjectIdField;
	}
}
