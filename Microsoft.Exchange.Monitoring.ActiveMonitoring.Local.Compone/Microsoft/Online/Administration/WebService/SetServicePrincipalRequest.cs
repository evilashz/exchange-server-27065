using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002CE RID: 718
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "SetServicePrincipalRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class SetServicePrincipalRequest : Request
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0008AB3B File Offset: 0x00088D3B
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x0008AB43 File Offset: 0x00088D43
		[DataMember]
		public ServicePrincipal ServicePrincipal
		{
			get
			{
				return this.ServicePrincipalField;
			}
			set
			{
				this.ServicePrincipalField = value;
			}
		}

		// Token: 0x04000F29 RID: 3881
		private ServicePrincipal ServicePrincipalField;
	}
}
