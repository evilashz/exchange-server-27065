using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003CC RID: 972
	[DebuggerStepThrough]
	[DataContract(Name = "UserExtended", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UserExtended : User
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0008CA00 File Offset: 0x0008AC00
		// (set) Token: 0x06001798 RID: 6040 RVA: 0x0008CA08 File Offset: 0x0008AC08
		[DataMember]
		public string Password
		{
			get
			{
				return this.PasswordField;
			}
			set
			{
				this.PasswordField = value;
			}
		}

		// Token: 0x040010B3 RID: 4275
		private string PasswordField;
	}
}
