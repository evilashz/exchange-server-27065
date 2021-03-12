using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D3 RID: 723
	[DebuggerStepThrough]
	[DataContract(Name = "NavigateListServicePrincipalsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class NavigateListServicePrincipalsRequest : Request
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0008ABDA File Offset: 0x00088DDA
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x0008ABE2 File Offset: 0x00088DE2
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0008ABEB File Offset: 0x00088DEB
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0008ABF3 File Offset: 0x00088DF3
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

		// Token: 0x04000F30 RID: 3888
		private byte[] ListContextField;

		// Token: 0x04000F31 RID: 3889
		private Page PageToNavigateField;
	}
}
