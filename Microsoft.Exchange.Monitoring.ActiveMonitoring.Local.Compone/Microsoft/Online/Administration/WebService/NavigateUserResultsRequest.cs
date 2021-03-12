using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002EE RID: 750
	[DataContract(Name = "NavigateUserResultsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class NavigateUserResultsRequest : Request
	{
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0008B004 File Offset: 0x00089204
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0008B00C File Offset: 0x0008920C
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0008B015 File Offset: 0x00089215
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x0008B01D File Offset: 0x0008921D
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

		// Token: 0x04000F62 RID: 3938
		private byte[] ListContextField;

		// Token: 0x04000F63 RID: 3939
		private Page PageToNavigateField;
	}
}
