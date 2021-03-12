using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E8 RID: 744
	[DataContract(Name = "ResetUserPasswordRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ResetUserPasswordRequest : Request
	{
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0008AF08 File Offset: 0x00089108
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x0008AF10 File Offset: 0x00089110
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

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0008AF19 File Offset: 0x00089119
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0008AF21 File Offset: 0x00089121
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0008AF2A File Offset: 0x0008912A
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x0008AF32 File Offset: 0x00089132
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

		// Token: 0x04000F56 RID: 3926
		private bool? ForceChangePasswordField;

		// Token: 0x04000F57 RID: 3927
		private string NewPasswordField;

		// Token: 0x04000F58 RID: 3928
		private Guid ObjectIdField;
	}
}
