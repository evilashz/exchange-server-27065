using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000315 RID: 789
	[DataContract(Name = "NavigateGroupResultsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class NavigateGroupResultsRequest : Request
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0008B5AF File Offset: 0x000897AF
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x0008B5B7 File Offset: 0x000897B7
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0008B5C0 File Offset: 0x000897C0
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x0008B5C8 File Offset: 0x000897C8
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

		// Token: 0x04000FA5 RID: 4005
		private byte[] ListContextField;

		// Token: 0x04000FA6 RID: 4006
		private Page PageToNavigateField;
	}
}
