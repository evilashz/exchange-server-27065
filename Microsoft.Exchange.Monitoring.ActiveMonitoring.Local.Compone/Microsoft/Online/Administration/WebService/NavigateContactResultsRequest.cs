using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000305 RID: 773
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "NavigateContactResultsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class NavigateContactResultsRequest : Request
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0008B3B9 File Offset: 0x000895B9
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x0008B3C1 File Offset: 0x000895C1
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0008B3CA File Offset: 0x000895CA
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x0008B3D2 File Offset: 0x000895D2
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

		// Token: 0x04000F8F RID: 3983
		private byte[] ListContextField;

		// Token: 0x04000F90 RID: 3984
		private Page PageToNavigateField;
	}
}
