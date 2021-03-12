using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D7 RID: 727
	[DataContract(Name = "NavigateGroupMemberResultsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class NavigateGroupMemberResultsRequest : Request
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0008AC4F File Offset: 0x00088E4F
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0008AC57 File Offset: 0x00088E57
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0008AC60 File Offset: 0x00088E60
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0008AC68 File Offset: 0x00088E68
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

		// Token: 0x04000F35 RID: 3893
		private byte[] ListContextField;

		// Token: 0x04000F36 RID: 3894
		private Page PageToNavigateField;
	}
}
