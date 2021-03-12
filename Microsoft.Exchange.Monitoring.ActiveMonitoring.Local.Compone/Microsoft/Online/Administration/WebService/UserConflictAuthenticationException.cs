using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200036F RID: 879
	[DataContract(Name = "UserConflictAuthenticationException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class UserConflictAuthenticationException : DataOperationException
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0008BE8A File Offset: 0x0008A08A
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x0008BE92 File Offset: 0x0008A092
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x04001000 RID: 4096
		private string UserPrincipalNameField;
	}
}
