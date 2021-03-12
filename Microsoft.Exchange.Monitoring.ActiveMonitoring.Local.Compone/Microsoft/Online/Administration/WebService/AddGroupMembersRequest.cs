using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000316 RID: 790
	[DataContract(Name = "AddGroupMembersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AddGroupMembersRequest : Request
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0008B5D9 File Offset: 0x000897D9
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0008B5E1 File Offset: 0x000897E1
		[DataMember]
		public GroupMember[] GroupMembers
		{
			get
			{
				return this.GroupMembersField;
			}
			set
			{
				this.GroupMembersField = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0008B5EA File Offset: 0x000897EA
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x0008B5F2 File Offset: 0x000897F2
		[DataMember]
		public Guid GroupObjectId
		{
			get
			{
				return this.GroupObjectIdField;
			}
			set
			{
				this.GroupObjectIdField = value;
			}
		}

		// Token: 0x04000FA7 RID: 4007
		private GroupMember[] GroupMembersField;

		// Token: 0x04000FA8 RID: 4008
		private Guid GroupObjectIdField;
	}
}
