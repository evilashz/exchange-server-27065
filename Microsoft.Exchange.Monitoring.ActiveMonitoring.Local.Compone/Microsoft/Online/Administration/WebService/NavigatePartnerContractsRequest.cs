using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000301 RID: 769
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavigatePartnerContractsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class NavigatePartnerContractsRequest : Request
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0008B344 File Offset: 0x00089544
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x0008B34C File Offset: 0x0008954C
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0008B355 File Offset: 0x00089555
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x0008B35D File Offset: 0x0008955D
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

		// Token: 0x04000F8A RID: 3978
		private byte[] ListContextField;

		// Token: 0x04000F8B RID: 3979
		private Page PageToNavigateField;
	}
}
