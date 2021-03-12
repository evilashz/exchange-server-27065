using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E5 RID: 741
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetUserRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class SetUserRequest : Request
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0008AE57 File Offset: 0x00089057
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x0008AE5F File Offset: 0x0008905F
		[DataMember]
		public User User
		{
			get
			{
				return this.UserField;
			}
			set
			{
				this.UserField = value;
			}
		}

		// Token: 0x04000F4D RID: 3917
		private User UserField;
	}
}
