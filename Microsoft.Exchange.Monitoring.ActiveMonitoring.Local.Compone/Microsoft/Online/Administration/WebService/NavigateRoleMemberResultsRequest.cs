using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E1 RID: 737
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "NavigateRoleMemberResultsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class NavigateRoleMemberResultsRequest : Request
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0008AD9E File Offset: 0x00088F9E
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x0008ADA6 File Offset: 0x00088FA6
		[DataMember]
		public byte[] ListContext
		{
			get
			{
				return this.ListContextField;
			}
			set
			{
				this.ListContextField = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0008ADAF File Offset: 0x00088FAF
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x0008ADB7 File Offset: 0x00088FB7
		[DataMember]
		public Page PageToNavigate
		{
			get
			{
				return this.PageToNavigateField;
			}
			set
			{
				this.PageToNavigateField = value;
			}
		}

		// Token: 0x04000F44 RID: 3908
		private byte[] ListContextField;

		// Token: 0x04000F45 RID: 3909
		private Page PageToNavigateField;
	}
}
