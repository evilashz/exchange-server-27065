using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E7 RID: 743
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ChangeUserPrincipalNameByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class ChangeUserPrincipalNameByUpnRequest : Request
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0008AEBC File Offset: 0x000890BC
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x0008AEC4 File Offset: 0x000890C4
		[DataMember]
		public string ImmutableId
		{
			get
			{
				return this.ImmutableIdField;
			}
			set
			{
				this.ImmutableIdField = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0008AECD File Offset: 0x000890CD
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x0008AED5 File Offset: 0x000890D5
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0008AEDE File Offset: 0x000890DE
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x0008AEE6 File Offset: 0x000890E6
		[DataMember]
		public string NewUserPrincipalName
		{
			get
			{
				return this.NewUserPrincipalNameField;
			}
			set
			{
				this.NewUserPrincipalNameField = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0008AEEF File Offset: 0x000890EF
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x0008AEF7 File Offset: 0x000890F7
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

		// Token: 0x04000F52 RID: 3922
		private string ImmutableIdField;

		// Token: 0x04000F53 RID: 3923
		private string NewPasswordField;

		// Token: 0x04000F54 RID: 3924
		private string NewUserPrincipalNameField;

		// Token: 0x04000F55 RID: 3925
		private string UserPrincipalNameField;
	}
}
