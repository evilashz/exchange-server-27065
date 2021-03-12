using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E9 RID: 745
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ResetUserPasswordByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ResetUserPasswordByUpnRequest : Request
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0008AF43 File Offset: 0x00089143
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x0008AF4B File Offset: 0x0008914B
		[DataMember]
		public bool? ForceChangePassword
		{
			get
			{
				return this.ForceChangePasswordField;
			}
			set
			{
				this.ForceChangePasswordField = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0008AF54 File Offset: 0x00089154
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0008AF5C File Offset: 0x0008915C
		[DataMember]
		public string NewPassword
		{
			get
			{
				return this.NewPasswordField;
			}
			set
			{
				this.NewPasswordField = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0008AF65 File Offset: 0x00089165
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x0008AF6D File Offset: 0x0008916D
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

		// Token: 0x04000F59 RID: 3929
		private bool? ForceChangePasswordField;

		// Token: 0x04000F5A RID: 3930
		private string NewPasswordField;

		// Token: 0x04000F5B RID: 3931
		private string UserPrincipalNameField;
	}
}
