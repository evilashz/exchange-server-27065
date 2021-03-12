using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E6 RID: 742
	[DataContract(Name = "ChangeUserPrincipalNameRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ChangeUserPrincipalNameRequest : Request
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0008AE70 File Offset: 0x00089070
		// (set) Token: 0x0600144D RID: 5197 RVA: 0x0008AE78 File Offset: 0x00089078
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0008AE81 File Offset: 0x00089081
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x0008AE89 File Offset: 0x00089089
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0008AE92 File Offset: 0x00089092
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x0008AE9A File Offset: 0x0008909A
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0008AEA3 File Offset: 0x000890A3
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x0008AEAB File Offset: 0x000890AB
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

		// Token: 0x04000F4E RID: 3918
		private string ImmutableIdField;

		// Token: 0x04000F4F RID: 3919
		private string NewPasswordField;

		// Token: 0x04000F50 RID: 3920
		private string NewUserPrincipalNameField;

		// Token: 0x04000F51 RID: 3921
		private Guid ObjectIdField;
	}
}
