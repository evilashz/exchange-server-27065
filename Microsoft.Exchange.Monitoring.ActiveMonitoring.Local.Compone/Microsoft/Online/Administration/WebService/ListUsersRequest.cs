using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002ED RID: 749
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListUsersRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListUsersRequest : Request
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0008AFEB File Offset: 0x000891EB
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0008AFF3 File Offset: 0x000891F3
		[DataMember]
		public UserSearchDefinition UserSearchDefinition
		{
			get
			{
				return this.UserSearchDefinitionField;
			}
			set
			{
				this.UserSearchDefinitionField = value;
			}
		}

		// Token: 0x04000F61 RID: 3937
		private UserSearchDefinition UserSearchDefinitionField;
	}
}
