using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002EB RID: 747
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "GetUserByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetUserByUpnRequest : Request
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0008AFA8 File Offset: 0x000891A8
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0008AFB0 File Offset: 0x000891B0
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0008AFB9 File Offset: 0x000891B9
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0008AFC1 File Offset: 0x000891C1
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

		// Token: 0x04000F5E RID: 3934
		private bool? ReturnDeletedUsersField;

		// Token: 0x04000F5F RID: 3935
		private string UserPrincipalNameField;
	}
}
