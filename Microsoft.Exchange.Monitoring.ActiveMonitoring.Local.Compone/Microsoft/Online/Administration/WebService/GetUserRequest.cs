using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002EA RID: 746
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetUserRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetUserRequest : Request
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0008AF7E File Offset: 0x0008917E
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0008AF86 File Offset: 0x00089186
		[DataMember]
		public Guid ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0008AF8F File Offset: 0x0008918F
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x0008AF97 File Offset: 0x00089197
		[DataMember]
		public bool? ReturnDeletedUsers
		{
			get
			{
				return this.ReturnDeletedUsersField;
			}
			set
			{
				this.ReturnDeletedUsersField = value;
			}
		}

		// Token: 0x04000F5C RID: 3932
		private Guid ObjectIdField;

		// Token: 0x04000F5D RID: 3933
		private bool? ReturnDeletedUsersField;
	}
}
