using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000317 RID: 791
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RemoveGroupMembersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class RemoveGroupMembersRequest : Request
	{
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x0008B603 File Offset: 0x00089803
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x0008B60B File Offset: 0x0008980B
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x0008B614 File Offset: 0x00089814
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x0008B61C File Offset: 0x0008981C
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

		// Token: 0x04000FA9 RID: 4009
		private GroupMember[] GroupMembersField;

		// Token: 0x04000FAA RID: 4010
		private Guid GroupObjectIdField;
	}
}
