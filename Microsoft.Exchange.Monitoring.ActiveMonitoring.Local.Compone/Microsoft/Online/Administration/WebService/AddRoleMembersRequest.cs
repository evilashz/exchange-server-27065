using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002DC RID: 732
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AddRoleMembersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class AddRoleMembersRequest : Request
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0008ACDD File Offset: 0x00088EDD
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0008ACE5 File Offset: 0x00088EE5
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

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0008ACEE File Offset: 0x00088EEE
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x0008ACF6 File Offset: 0x00088EF6
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

		// Token: 0x04000F3B RID: 3899
		private RoleMember[] RoleMembersField;

		// Token: 0x04000F3C RID: 3900
		private Guid RoleObjectIdField;
	}
}
